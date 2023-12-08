using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;
using System;


namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class SessionRepositoryLiteDB : BaseRepositoryLiteDB<SessionInfo>, ISessionRepository
    {
        private ILogger<UserRepositoryLiteDB> _logger;

        public SessionRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<UserRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            Context.GetCollection<UserData>(EntityStorageName).EnsureIndex(obj => obj.Id, true);
        }

        public override SessionInfo CreateNew()
        {
            return new SessionInfo() { CreatedDate = DateTime.Now };
        }

        public SessionInfo GetSession(Role type)
        {
            Context.BeginTrans();
            SessionInfo session = Collection.FindOne(e => e.ServerType == type);
            Context.Commit();
            return session;
        }

        public void ClearSession(Role type)
        {
            var existingSession = GetSession(type);
            if (existingSession is SessionInfo session) 
            {
                Context.BeginTrans();
                Remove(session.Id);
                Context.Commit();
            }
        }

        public void UpsertSession(SessionInfo session) 
        { 
            ClearSession(session.ServerType);
            Context.BeginTrans();
            Collection.Upsert(session);
            Context.Commit();
        }

        public bool HasSession(Role type) => GetSession(type) is SessionInfo session && !session.IsEmpty;

        public override DBType EntityStorageType { get; } = DBType.Credentials;

        protected override string EntityStorageName => "session";
    }
}
