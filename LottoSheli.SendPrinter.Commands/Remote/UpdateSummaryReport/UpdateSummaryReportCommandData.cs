using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.DTO.Remote;
using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    public class UpdateSummaryReportCommandData : ICommandData
    {
        private TicketTask _ticket;
        private TicketData _data;

        public TicketTask Ticket
        {
            get => _ticket;
            set
            {
                _ticket = value;
                _data = new TicketData
                {
                    IId = value.Id,
                    TId = value.TicketId,
                    UId = int.TryParse(value.UserId, out int uid) ? uid : -1,
                    GameType = value.Type.ToString(),
                    GameSubType = value.SubType.ToString(),
                    Extra = value.Extra ?? false,
                    Date = DateTime.Now
                };
            }
        }

        public TicketData Data => _data;
    }
}
