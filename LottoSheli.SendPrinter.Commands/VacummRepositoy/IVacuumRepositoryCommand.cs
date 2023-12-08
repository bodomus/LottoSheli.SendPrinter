using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.OCR;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
using AForge.Imaging.Filters;
using LottoSheli.SendPrinter.Settings;

namespace LottoSheli.SendPrinter.Commands
{

    /// <summary>
    /// Vacuums current repository
    /// </summary>
    public interface IVacuumRepositoryCommand : IExecutableCommand
    {

    }
}
