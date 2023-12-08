using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core
{

    /// <summary>
    /// Hash Generator
    /// </summary>
    public interface IHashGenerator : IDisposable
    {
        /// <summary>
        /// Compute hash for specified data
        /// </summary>
        /// <param name="data">specified data</param>
        /// <returns>Computed hash</returns>
        string ComputeHash(string data);
    }
}
