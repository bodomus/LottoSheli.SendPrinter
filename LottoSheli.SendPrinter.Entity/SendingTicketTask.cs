using System;
using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides entity for sending Ticket task
    /// </summary>
    public class SendingTicketTask : TicketTask
    {
        [JsonIgnore]
        public DateTime SendingDate { get; set; }

        public static SendingTicketTask FromTicketTask(TicketTask source)
        {
            return new SendingTicketTask
            {
                Id = source.Id,
                Type = source.Type,
                SubType = source.SubType,
                DrawId = source.DrawId,
                TicketId = source.TicketId,
                MultipleDraw = source.MultipleDraw,
                Price = source.Price,
                UserId = source.UserId,
                Extra = source.Extra,
                Auto = source.Auto,
                UserIdMandatoryFlag = source.UserIdMandatoryFlag,
                QueueName = source.QueueName,
                PrintedStationId = source.PrintedStationId,
                Tables = source.Tables,
                Settings = source.Settings,
                Sequence = source.Sequence,
                PrintedCount = source.PrintedCount,
                CreatedDate = source.CreatedDate
            };
        }

    }
}
