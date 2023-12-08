using Microsoft.Extensions.Logging;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core
{
    public class SHA1HashGenerator : IHashGenerator
    {
        SHA1CryptoServiceProvider unmanagedProvider = new SHA1CryptoServiceProvider();
        private bool disposedValue;

        private object _lockedObject = new object();
        ILogger<SHA1HashGenerator> _logger;

        public SHA1HashGenerator(ILogger<SHA1HashGenerator> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Compute hash for specified data
        /// </summary>
        /// <param name="data">specified data</param>
        /// <returns>Computed hash</returns>
        public string ComputeHash(string data)
        {
            byte[] hash;
            lock (_lockedObject)
            {
                hash = unmanagedProvider.ComputeHash(Encoding.ASCII.GetBytes(data));
            }

            return Convert.ToBase64String(hash);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ((IDisposable)unmanagedProvider).Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SHA1HashGenerator()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
