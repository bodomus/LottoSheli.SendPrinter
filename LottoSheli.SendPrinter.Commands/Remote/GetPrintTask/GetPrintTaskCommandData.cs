
namespace LottoSheli.SendPrinter.Commands.Remote.GetPrintTask
{
    using Base;
    using Entity.Enums;

    public class GetPrintTaskCommandData : ICommandData
    {
        public TicketFilter Filter { get; set; }
        public int Limit { get; set; } = 1;
        public bool CheckPrintDuplicates { get; set; }
    }
}
