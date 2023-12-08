using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.DTO.Remote;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using System;
using System.Linq;
using System.Reflection;

namespace LottoSheli.SendPrinter.Commands.Print
{

    public class PrintCommandBase
    {
        protected readonly ITicketTaskRepository _ticketTaskRepository;
        
        protected PrintCommandBase(ITicketTaskRepository ticketTaskRepository)
        {
            _ticketTaskRepository = ticketTaskRepository;
        }

        protected SummaryReportData GetSummaryReportData(DateTime date, string printerLogin, int stationId, string stationName, int sequenceStart = 0, int sequenceEnd = Int32.MaxValue)
        {
            var result = new SummaryReportData();
            result.Date = DateTime.Now;

            var items = _ticketTaskRepository
                .GetBetweenSequences(sequenceStart, sequenceEnd);

            var tickets = items.Select(obj =>
            {
                int userId;

                return new TicketData()
                {
                    IId = obj.Sequence,
                    UId = int.TryParse(obj.UserId, out userId) ? (int?)userId : null,
                    TId = obj.TicketId,
                    GameType = obj.Type.GetEnumMemberValue(),
                    GameSubType = obj.SubType.GetEnumMemberValue(),
                    Extra = obj.Extra ?? false,
                    UidMandatoryFlag = IsMandatoryFlag(obj)
                };

            }).ToList();

            tickets.Reverse();

            result.Tickets = tickets;
            result.PrinterLogin = printerLogin;
            result.TotalAmount = (decimal)items.Sum(obj => obj.Price);
            var taskStationId = 0;
            result.PrinterStationId.TargetId = int.TryParse(items.FirstOrDefault(obj => obj.PrintedStationId != null)?.PrintedStationId ?? stationId.ToString(), out taskStationId) ? taskStationId : stationId;
            result.PrinterStationId.TargetName = "station";
            result.PrinterStationName = stationName;
            result.TicketRange = new TicketRangeData() { From = sequenceStart, To = sequenceEnd };

            return result;
        }

        private bool IsMandatoryFlag(TicketTask ticketTask)
        {
            var isLottoGame = ticketTask.Type == Entity.Enums.TaskType.LottoRegular ||
                         ticketTask.Type == Entity.Enums.TaskType.LottoDouble ||
                         ticketTask.Type == Entity.Enums.TaskType.LottoSocial ||
                         ticketTask.Type == Entity.Enums.TaskType.TripleSeven;

            var isChanceGame = ticketTask.Type == Entity.Enums.TaskType.MethodicalChance ||
                         ticketTask.Type == Entity.Enums.TaskType.MultipleChance ||
                         ticketTask.Type == Entity.Enums.TaskType.RegularChance;

            var is123Game = ticketTask.Type == Entity.Enums.TaskType.Regular123 ||
                 ticketTask.Type == Entity.Enums.TaskType.Combined123;

            return isLottoGame || (isChanceGame && ticketTask.UserIdMandatoryFlag > 0) || (is123Game && ticketTask.UserIdMandatoryFlag > 0);
        }
    }
}