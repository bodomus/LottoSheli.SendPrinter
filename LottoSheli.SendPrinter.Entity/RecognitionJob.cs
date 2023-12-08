using LiteDB;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    public class RecognitionJob : BaseEntity
    {
        public override int Id { get; set; }
        public override DateTime CreatedDate { get; set; }
        public TicketTask Ticket { get; set; }
        public SlipDataEntity RecognizedData { get; set; }
        public TicketMatchingError MatchStatus { get; set; } = TicketMatchingError.Unmatched;
        public int SendStatus { get; set; }
        public string ImagePath { get; set; }
        [BsonIgnore]
        public Bitmap Scan => null != GetScan ? GetScan() : null;
        public RecognitionJobStatus JobStatus { get; set; } = RecognitionJobStatus.Initial;
        [BsonIgnore]
        public Func<Bitmap> GetScan { get; set; }
        public int ScanType { get; set; } = 0;
        public string DownloadUrl { get; set; }

        public RecognitionJob() 
        {
            Guid = System.Guid.NewGuid().ToString();
            Id = Guid.GetHashCode();
            CreatedDate = DateTime.Now;
        }
    }
}
