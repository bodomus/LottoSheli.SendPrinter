using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Utils
{
    public class TicketMatchingException : Exception
    {
        public TicketMatchingError MatchingError { get; set; }

        public override string Message => MatchingError switch 
        { 
            TicketMatchingError.None => "Exact match",
            TicketMatchingError.Type => "Game type mismatch",
            TicketMatchingError.Subtype => "Game subtype mismatch",
            TicketMatchingError.Price => "Ticket price mismatch",
            TicketMatchingError.Tables => "Ticket tables mismatch",
            TicketMatchingError.NationalId => "National ID mismatch",
            _ => "No matching ticket found"
        };

        public TicketMatchingException() : base() 
        {
            MatchingError = TicketMatchingError.Unmatched;
        }

        public TicketMatchingException(TicketMatchingError error) : base()
        {
            MatchingError = error;
        }
    }
}
