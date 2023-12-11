using NLog;
using NLog.Config;
using NLog.Targets.Wrappers;
using NLog.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.Bootstraper
{
    public static class PublicExtensions
{       /// <summary>
        /// Gets command executor.
        /// </summary>
        /// <returns></returns>
        public static Control CreateLoggerUIControl(this Microsoft.Extensions.Logging.ILoggerFactory factory)
        {
            RichTextBox rtbLogs = new RichTextBox();
            rtbLogs.Dock = DockStyle.Fill;
            rtbLogs.Name = "rtbLogs";
            rtbLogs.ReadOnly = true;
            rtbLogs.BorderStyle = BorderStyle.FixedSingle;
            rtbLogs.BackColor = Color.White;
            //rtbLogs.RightToLeft = RightToLeft.No;

            RichTextBoxTarget target = new RichTextBoxTarget();
            target.Layout = "${longdate} [${threadname:whenEmpty=${threadid}}] ${uppercase:${level}} ${message:withException=true} ${all-event-properties}";
            target.TargetRichTextBox = rtbLogs;
            target.UseDefaultRowColoringRules = true;
            target.Name = "rtf";
            //NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, NLog.LogLevel.Trace);

            LoggingRule richTextBoxRule = new LoggingRule("*", target);
            
            richTextBoxRule.EnableLoggingForLevel(LogLevel.Fatal);
            richTextBoxRule.EnableLoggingForLevel(LogLevel.Error);
            richTextBoxRule.EnableLoggingForLevel(LogLevel.Warn);
            richTextBoxRule.EnableLoggingForLevel(LogLevel.Info);
            //richTextBoxRule.EnableLoggingForLevel(LogLevel.Debug);
            //richTextBoxRule.EnableLoggingForLevel(LogLevel.Trace);
            

            LogManager.Configuration.AddTarget(target);
            LogManager.Configuration.LoggingRules.Add(richTextBoxRule);

            LogManager.ReconfigExistingLoggers();

            return rtbLogs;
        }
    }
}
