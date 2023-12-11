using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.App
{
    static class Extensions
    {
        /// <summary>
        /// Setts double buffering for specified control
        /// </summary>
        /// <param name="c"></param>
        public static void SetDoubleBuffered(this Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            if (null != aProp)
                aProp.SetValue(c, true, null);
        }


        /// <summary>
        /// REquires for <see cref="SaveFileDialog"/> or <see cref="OpenFileDialog"/> becuase it doend wnat to load on async Main
        /// </summary>
        /// <param name="thread"></param>
        public static void RunSTA(this Thread thread)
        {
            thread.SetApartmentState(ApartmentState.STA); // Configure for STA
            thread.Start(); // Start running STA thread for action
            thread.Join(); // Sync back to running thread
        }

        /// <summary>
        /// Sets double buffered for DataGridView. Requires for performance https://stackoverflow.com/questions/4255148/how-to-improve-painting-performance-of-datagridview
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="setting"></param>
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            var dgvType = dgv.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        /// <summary>
        /// Provides formated string of ticket table numbers
        /// </summary>
        public static string GetTableNumbers(object tag, TicketTable row)
        {
            if (tag is TicketTask ticketTask &&
                  (ticketTask.Type == TaskType.RegularChance
                  || ticketTask.Type == TaskType.MultipleChance
                  || ticketTask.Type == TaskType.MethodicalChance))
            {
                return string.Join(", ", row.Numbers.Reverse());
            }

            return string.Join(", ", row.Numbers);
        }

        public static GType GetResizedGeometry<GType>(this UserControl ctrl, params int[] inputSizes) 
        {
            var outputSizes = inputSizes.Select(s => ctrl.LogicalToDeviceUnits(s) as object).ToArray();
            var constr = typeof(GType).GetConstructor(inputSizes.Select(s => s.GetType()).ToArray());
            if (null == constr)
                throw new InvalidOperationException(string.Format("Could not find constructor of {0} with {1} arguments", typeof(GType).Name, inputSizes.Length));
            var obj = constr.Invoke(outputSizes);
            if (obj is GType result)
                return result;
            throw new InvalidOperationException(string.Format("Unexpected resulting type of {0}", obj.GetType().Name));
        }

        public static GameType GetGameType(this TicketTask ticket) => ticket?.Type switch
        {
            TaskType.LottoRegular => GameType.Lotto,
            TaskType.LottoDouble => GameType.Lotto,
            TaskType.LottoSocial => GameType.Lotto,
            TaskType.TripleSeven => GameType.G777,
            TaskType.RegularChance => GameType.Chance,
            TaskType.MultipleChance => GameType.Chance,
            TaskType.MethodicalChance => GameType.Chance,
            TaskType.Regular123 => GameType.G123,
            TaskType.Combined123 => GameType.G123,
            _ => GameType.Unknown
        };
    }
}
