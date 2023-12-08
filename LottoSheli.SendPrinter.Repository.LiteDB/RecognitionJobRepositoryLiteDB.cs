using LiteDB;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class RecognitionJobRepositoryLiteDB : BaseRepositoryLiteDB<RecognitionJob>, IRecognitionJobRepository
    {
        private const string SCANDIR = "scans";
        private const int MAX_FILE_RETRIES = 3;
        private const int FILE_READ_TIMEOUT = 100;
        private readonly object _lock = new object();
        private string DIRNAME => Path.Combine(SettingsManager.LottoHome, SCANDIR);
        
        private ILogger<RecognitionJobRepositoryLiteDB> _logger;

        public override DBType EntityStorageType => DBType.Jobs;
        protected override string EntityStorageName => "recognition_job";
        protected override ILiteCollection<RecognitionJob> Collection => 
            _dbFactory.GetDBContext(EntityStorageType)
                .GetCollection<RecognitionJob>(EntityStorageName);

        public event EventHandler<RecognitionJob> Inserted;
        public event EventHandler<RecognitionJob> Updated;
        public event EventHandler<RecognitionJob> Removed;

        public RecognitionJobRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<RecognitionJobRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            InitializeFolder();
        }


        public override RecognitionJob CreateNew()
        {
            throw new NotImplementedException("Use factory to create instances instead");
        }

        public RecognitionJob CreateNew(Bitmap scan, int scanType) 
        {
            var job = new RecognitionJob { ScanType = scanType};
            job.ImagePath = GetFilePath(job);
            job.GetScan = () => GetImage(job.ImagePath);
            InsertImage(job.ImagePath, scan);
            Insert(job);
            return job;
        }

        public Task<RecognitionJob> CreateNewAsync(Bitmap scan, int scanType) => 
            Task.Run(() => CreateNew(scan, scanType));

        public override IEnumerable<RecognitionJob> GetAll(int skip = 0, int take = 0) 
        { 
            var jobs = base.GetAll(skip, take).ToList();
            foreach(var job in jobs)
                if (ImageExists(job.ImagePath))
                    job.GetScan = () => GetImage(job.ImagePath);
            return jobs;
        }

        public Task<IEnumerable<RecognitionJob>> GetAllAsync(int skip = 0, int take = 0) => Task.Run(() => GetAll(skip, take));

        public override RecognitionJob GetById(int id)
        {
            var job = base.GetById(id);

            if (null != job && ImageExists(job.ImagePath))
                job.GetScan = () => GetImage(job.ImagePath);

            return job;
        }

        public Task<RecognitionJob> GetByIdAsync(int id) => Task.Run(() => GetById(id));

        public override RecognitionJob GetByGuid(string guid)
        {
            var job = base.GetByGuid(guid);

            if (null != job && ImageExists(job.ImagePath))
                job.GetScan = () => GetImage(job.ImagePath);

            return job;
        }

        public override RecognitionJob Insert(RecognitionJob job)
        {
            var insertedJob = base.Insert(job);
            if (null != insertedJob) 
            {
                if (ImageExists(insertedJob.ImagePath))
                    insertedJob.GetScan = () => GetImage(job.ImagePath);
                Inserted?.Invoke(this, insertedJob);
            }
            return insertedJob;
        }

        public Task<RecognitionJob> InsertAsync(RecognitionJob job) => Task.Run(() => Insert(job));

        public override RecognitionJob Update(RecognitionJob job)
        {
            // rotating the image if paths in new and old version mismatch
            var storedJob = base.GetByGuid(job.Guid);
            if (null != storedJob && storedJob.ImagePath != job.ImagePath) 
            {
                ReplaceImage(job.ImagePath, storedJob.ImagePath);
                job.ImagePath = storedJob.ImagePath;
            }
                
            var updatedJob = base.Update(job);

            if (null != updatedJob) 
            {
                if (ImageExists(updatedJob.ImagePath))
                    updatedJob.GetScan = () => GetImage(job.ImagePath);
                Updated?.Invoke(this, updatedJob);
            }
            return updatedJob;
        }

        public Task<RecognitionJob> UpdateAsync(RecognitionJob job) => Task.Run(() => Update(job));

        public override bool Remove(int id)
        {
            var job = GetById(id);
            bool result = base.Remove(id);
            if (!result) 
            {
                var msg = $"Failed to remove recognition job id {id}";
                _logger.LogError(msg);
                //throw new InvalidOperationException(msg);
            }
                
            if (null != job)
                RemoveImage(job.ImagePath);

            Removed?.Invoke(this, job);
            return result;
        }

        public Task<bool> RemoveAsync(int id) => Task.Run(() => Remove(id));

        public override int Clear()
        {
            var storedJobs = Collection.FindAll();
            int count = 0;
            foreach (var job in storedJobs)
                count += Remove(job.Id) ? 1 : 0;
            
            return count;
        }

        private string GetFilePath(RecognitionJob job)
        {
            if (null == job)
                return string.Empty;
            return Path.Combine(DIRNAME, $"{job.Guid[0..8]}.png");
        }

        private void InitializeFolder() 
        { 
            if (!Directory.Exists(DIRNAME))
                Directory.CreateDirectory(DIRNAME);
        }

        private bool InsertImage(string imgPath, Bitmap img) 
        {
            try
            {
                img.Save(imgPath, ImageFormat.Png);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to store scanned image: {ex.Message}");
                return false;
            }
        }

        private void ReplaceImage(string srcPath, string dstPath) 
        {
            try
            {
                using var image = GetImage(srcPath);
                image?.Save(dstPath);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Failed to replace scanned image: {ex.Message}");
            }
        }

        private Bitmap GetImage(string imgPath) 
        {
            try
            {
                return GetScanFromFile(imgPath);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get scanned image: {ex.Message}");
                return null;
            }
        }

        private bool RemoveImage(string imgPath) 
        {
            try
            {
                if (ImageExists(imgPath))
                    File.Delete(imgPath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to remove scanned image: {ex.Message}");
                return false;
            }
        }

        private Bitmap GetScanFromFile(string scanPath, int retry = 0)
        {
            try
            {
                return (Bitmap)Image.FromFile(scanPath);
            }
            catch (Exception)
            {
                if (retry < MAX_FILE_RETRIES)
                {
                    Thread.Sleep(FILE_READ_TIMEOUT);
                    return GetScanFromFile(scanPath, retry + 1);
                }
                throw;
            }
        }

        private bool ImageExists(string imgPath) => File.Exists(imgPath);
    }
}
