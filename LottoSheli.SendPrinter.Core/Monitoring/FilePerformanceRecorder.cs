using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public interface IFilePerformanceRecorder : IPerformanceRecorder 
    { 
        public string FilePath { get; set; }
    }
    public class FilePerformanceRecorder : IFilePerformanceRecorder
    {
        private string _filePath;

        public FilePerformanceRecorder()
        {
        }

        public FilePerformanceRecorder(string filepath) 
        { 
            _filePath = filepath;
        }

        public string FilePath 
        { 
            get => _filePath; 
            set => _filePath = value; 
        }

        public void Record(PerformanceRecord record)
        {
            File.AppendAllLines(_filePath, new List<string> { SerializeRecord(record) });
        }

        public Task RecordAsync(PerformanceRecord record)
        {
            return File.AppendAllLinesAsync(_filePath, new List<string> { SerializeRecord(record) });
        }

        private string SerializeRecord(PerformanceRecord record) => $"{DateTime.Now.ToString("G")};{record}";
    }
}
