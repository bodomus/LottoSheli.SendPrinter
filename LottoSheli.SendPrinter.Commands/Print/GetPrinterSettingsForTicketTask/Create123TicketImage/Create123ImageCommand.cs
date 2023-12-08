using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates 123 regular ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<Created123TicketImageResult>))]
    public class Create123TicketImageCommand : ICreateLottoTicketImageCommand<Created123TicketImageResult>
    {
        private static readonly IList<int> PredefinedAmounts = new List<int> { 1, 2, 3, 5, 10, 25 };

        private readonly ILogger<Create123TicketImageCommand> _logger;
        private readonly ITicketRenderer _ticketRenderer;

        public Create123TicketImageCommand(ITicketRenderer ticketRenderer, ILogger<Create123TicketImageCommand> logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }

        /// <summary>
        /// draws custom amount (stored as a json property in layout file)
        /// </summary>
        /// <param name="graphics">graphic unit</param>
        /// <param name="ticketLayout">ticket layout instance</param>
        /// <param name="amount"></param>
        /// <param name="draw">flag if drawing is needed. If not just returns</param>
        private void DrawCustomAmount(Graphics graphics, TicketLayout ticketLayout, int amount, bool draw)
        {
            if (!draw)
                return;

            var customAmountLocation = ticketLayout.Groups["custom_amount"][0];

            using (Font f = new Font("Arial", 0.7f * customAmountLocation.Height))
            {
                RectangleF rect = new RectangleF(customAmountLocation.X, customAmountLocation.Y, customAmountLocation.Width, customAmountLocation.Height);

                var format = new StringFormat { Alignment = StringAlignment.Center };
                graphics.DrawString(amount.ToString(), f, Brushes.Black, rect, format);
            }
        }

        public bool CanExecute()
        {
            return true;
        }

        public Created123TicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {
            var amount = unchecked((int)data.ExisintgTicketTask.Settings["inv_amount"]);

            // Build element indices
            var indices = new Dictionary<string, List<int>>();

            if (amount <= 0 || amount > 500)
                throw new Exception("Unexpected investment amount");

            int amountIndex = PredefinedAmounts.IndexOf(amount);
            if (amountIndex != -1)
            {
                indices.Add("investment_amount", new List<int> { amountIndex });
            }
            else
            {
                throw new Exception("Custom amount is not supported");
            }

            foreach (var ticketTable in data.ExisintgTicketTask.Tables)
            {
                int tableIndex = ticketTable.Index + 1;

                for (int rowIndex = 0; rowIndex < 3; rowIndex++)
                {
                    string groupName = String.Format("num{0}{1}", tableIndex, rowIndex + 1);
                    indices.Add(groupName, new List<int> { Convert.ToInt32(ticketTable.Numbers[rowIndex]) });
                }
            }

            if (data.ExisintgTicketTask.MultipleDraw > 1)
            {
                if (data.ExisintgTicketTask.MultipleDraw > 4)
                    throw new Exception("Unexpected draw count");

                indices.Add("multiple_draw", new List<int> { data.ExisintgTicketTask.MultipleDraw - 2 });
            }

            return new Created123TicketImageResult { Image = _ticketRenderer.RenderTicket("123", indices,
                                         (g, l) => DrawCustomAmount(g, l, amount, amountIndex == -1))
            };
        }

    }
}
