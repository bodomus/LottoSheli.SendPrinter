using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Remote;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    [Command(Basic = typeof(IUpdateSummaryReportCommand))]
    public class UpdateSummaryReportCommand : IUpdateSummaryReportCommand
    {
        private readonly IRemoteClient _remoteClient;
        
        public UpdateSummaryReportCommand(IRemoteClient remoteClient) 
        { 
            _remoteClient = remoteClient;
        }
        public bool CanExecute() => true;

        public Task<bool> Execute(UpdateSummaryReportCommandData data) => _remoteClient.UpdateSummaryReport(data.Data);
    }
}
