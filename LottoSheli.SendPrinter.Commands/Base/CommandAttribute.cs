using System;
using System.ComponentModel.DataAnnotations;

namespace LottoSheli.SendPrinter.Commands.Base
{
    /// <summary>
    /// Provide attribute that marks object as command
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Basic command type
        /// </summary>
        [Required]
        public Type Basic { get; init; }
    }
}
