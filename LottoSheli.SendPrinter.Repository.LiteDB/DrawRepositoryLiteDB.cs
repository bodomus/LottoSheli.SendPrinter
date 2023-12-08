using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    /// <summary>
    /// LiteDB implementation of <see cref="IDrawRepository"/> interface.
    /// </summary>
    public class DrawRepositoryLiteDB : BaseRepositoryLiteDB<Draw>, IDrawRepository
    {
        private ILogger<DrawRepositoryLiteDB> _logger;

        public DrawRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<DrawRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            Collection.EnsureIndex(obj => obj.Type, true);
        }

        protected override string EntityStorageName => "draw";

        public override Draw CreateNew()
        {
            return new Draw();
        }

        public Draw GetByDrawGameType(DrawGameType gameType)
        {
            return Collection.FindOne(draw => draw.Type == gameType);
        }

        public void UpsertDraw(Draw draw) 
        {
            if (null == draw)
                return;

            if (Collection.FindById(draw.Id) != null) 
            {
                Collection.Update(draw);
                return;
            }

            var previousDraw = GetByDrawGameType(draw.Type);
            if (null != previousDraw)
                Collection.Delete(previousDraw.Id);
            Collection.Insert(draw);
        }
    }
}
