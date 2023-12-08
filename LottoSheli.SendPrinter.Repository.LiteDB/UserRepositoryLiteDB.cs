using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class UserRepositoryLiteDB : BaseRepositoryLiteDB<UserData>, IUserRepository
    {
        private UnicodeEncoding _encoding = new UnicodeEncoding();
        private ILogger<UserRepositoryLiteDB> _logger;
        private byte[] _salt => _encoding.GetBytes("Jibberish salt");

        public UserRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<UserRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            Context.GetCollection<UserData>(EntityStorageName).EnsureIndex(obj => obj.Id, true);
        }

        public override UserData CreateNew()
        {
            return new UserData() { CreatedDate = DateTime.Now };
        }

        public UserData GetByUserRole(Role role)
        {
            Context.BeginTrans();
            var user = Collection.FindOne(e => !string.IsNullOrEmpty(e.Login) && e.Role == role);
            Context.Commit();
            return user;
        }

        public void UpdateCredentials(string login, string pass = "", Role role = Role.User)
        {
            UserData user = GetByUserRole(role) ?? CreateNew();
            pass = role switch 
            { 
                Role.User or Role.OcrUser => SHA256Helper.ComputeHash(pass),
                _ => pass
            };
            
            user.Login = login;
            user.Password = pass;
            user.Role = role;

            Context.BeginTrans();
            Collection.Upsert(user);
            Context.Commit();
        }

        public (string Login, string Password) GetCredentials(Role role = Role.User)
        {
            var user = GetByUserRole(role);
            if (user is { Login: var login, Password: var password }) 
            {
                if (IsServerRole(role) && IsPasswordEncrypted(password))
                {
                    // need to decrypt password if it's encrypted and store open
                    password = SafeDecodePassword(password);
                    UpdateCredentials(login, password, role);
                }
                return new (login, password);
            }
            else
                return new(string.Empty, string.Empty);
        }

        private string SafeDecodePassword(string pass) 
        {
            try 
            { 
                return _encoding.GetString(ProtectedData.Unprotect(Convert.FromBase64String(pass), _salt, DataProtectionScope.CurrentUser));
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Failed to decrypt password from storage: {ex.Message}");
                return string.Empty;
            }
        }

        private string SafeEncodePassword(string pass)
        {
            try
            {
                return Convert.ToBase64String(ProtectedData.Protect(_encoding.GetBytes(pass), _salt, DataProtectionScope.CurrentUser));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to encrypt password: {ex.Message}");
                return string.Empty;
            }
        }

        private bool IsPasswordEncrypted(string pass) 
        {
            try 
            { 
                var decoded = _encoding.GetString(ProtectedData.Unprotect(Convert.FromBase64String(pass), _salt, DataProtectionScope.CurrentUser));
                return decoded.Length > 0;
            }
            catch 
            { return false;  }
        }

        private bool IsServerRole(Role role) => Role.D7 == role || Role.D9 == role;

        public override DBType EntityStorageType { get; } = DBType.Credentials;

        protected override string EntityStorageName => "users";
    }
}
