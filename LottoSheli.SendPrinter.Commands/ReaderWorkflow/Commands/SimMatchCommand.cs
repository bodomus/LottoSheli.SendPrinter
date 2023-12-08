using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    public class SimMatchCommand : IMatchCommand
    {
        private static int _idGen = 0;
        private static int _ticketIdGen = 1250000;
        private static int _drawId = 12345678;
        private static string _stationId = "785TEST_STATION";
        public event EventHandler<RecognitionJobEventArgs> Started;
        public event EventHandler<RecognitionJobEventArgs> Completed;

        public IRecognitionJobCommand CreateNew(string guid)
        {
            return new SimMatchCommand();
        }

        public TrenaryResult Execute(RecognitionJob job)
        {
            job.Ticket = CreateFakeTicket(job);
            return null != job.Ticket 
                ? TrenaryResult.Done 
                : TrenaryResult.Failed;
        }

        private TicketTask CreateFakeTicket(RecognitionJob job) 
        {
            if (job is { RecognizedData: var slipData } && null != slipData) 
            {
                return new TicketTask
                {
                    Id = _idGen,
                    Sequence = _idGen++,
                    Type = GetTicketTaskType(slipData),
                    SubType = GetTicketTaskSubType(slipData),
                    TicketId = _ticketIdGen++,
                    Tables = slipData.Tables,
                    Extra = !string.IsNullOrEmpty(slipData.Extra),
                    DrawId = _drawId,
                    MultipleDraw = 1,
                    Auto = false,
                    Price = slipData.Price,
                    UserId = slipData.UserId,
                    UserIdMandatoryFlag = slipData.ShowNationalId ? 1 : 0,
                    PrintedStationId = _stationId,
                    CreatedDate = DateTime.Now
                };
            }
            return null;
        }

        private TaskType GetTicketTaskType(SlipDataEntity recognizedData) 
        {
            return TicketTaskMatcher.Types.FirstOrDefault(t => t.Item2 == recognizedData.GameType).Item1;
        }

        private TaskSubType GetTicketTaskSubType(SlipDataEntity recognizedData)
        {
            return TicketTaskMatcher.SubTypes.FirstOrDefault(t => t.Item2 == recognizedData.GameSubtype).Item1;
        }
    }
}
