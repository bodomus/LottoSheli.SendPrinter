using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal class RemoteEndpointAttribute : Attribute
    {
        [Required]
        public Role ServerRole { get; init; }

        [Required]
        public string Path { get; init; }

        [Required]
        public string Verb { get; init; }

        public RemoteEndpointAttribute(Role serverRole, string path, string verb) 
        { 
            ServerRole = serverRole;
            Path = path;
            Verb = verb;
        }
    }
}
