using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    public class ScannedSlip : BaseEntity
    {
        public override int Id { get; set; }
        public string SlipId { get; set; }
        public string TopBarcode { get; set; }
        public int TicketId { get; set; }
        public override DateTime CreatedDate { get; set; }
        public ScannedSlip() 
        { 
            CreatedDate = DateTime.Now;
        }
    }
}
