using System;

namespace LottoSheli.SendPrinter.App
{
    /// <summary>
    /// Mark that calss should be authorized
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RequireAuthenticationAttribute : Attribute
    {
    }
}
