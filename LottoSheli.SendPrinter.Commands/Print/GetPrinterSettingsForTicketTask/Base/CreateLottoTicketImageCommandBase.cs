using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Print.Base
{

    /// <summary>
    /// Creates lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    public abstract class CreateLottoTicketImageCommandBase<TResult> : ICreateLottoTicketImageCommand<TResult>
        where TResult : CreatedImageResultBase, new()
    {
        private readonly ILogger _logger;
        private readonly ITicketRenderer _ticketRenderer;

        protected CreateLottoTicketImageCommandBase(ITicketRenderer ticketRenderer, ILogger logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }

        public bool CanExecute()
        {
            return true;
        }

        public TResult Execute(CreateLottoTicketImageCommandData data)
        {

            IDictionary<string, List<int>> indices = new Dictionary<string, List<int>>();

            foreach (var table in data.ExisintgTicketTask.Tables)
            {
                if (table.Strong.Count != 1)
                {
                    throw new Exception("Expected 1 strong number");
                }

                string numGroup = "num" + (table.Index + 1).ToString();
                string strongGroup = "strong" + (table.Index + 1).ToString();

                indices.Add(numGroup, table.Numbers.Select(num => (int)(int.Parse(num.ToString()) - 1)).ToList());
                indices.Add(strongGroup, new List<int> { int.Parse(table.Strong[0].ToString()) - 1 });
            }

            if (data.ExisintgTicketTask.Extra == true)
            {
                // miplal hapais layouts contain two lines to draw extra
                var extraElements = new List<int> { 0, 1 };
                indices.Add("extra", extraElements);
            }

            if (data.ExisintgTicketTask.MultipleDraw > 1)
            {
                int index = 0;
                switch (data.ExisintgTicketTask.MultipleDraw)
                {
                    case 2:
                        index = 0;
                        break;

                    case 4:
                        index = 1;
                        break;

                    case 6:
                        index = 2;
                        break;

                    case 8:
                        index = 3;
                        break;

                    case 10:
                        index = 4;
                        break;

                    default:
                        throw new Exception("Unexpected");
                }

                indices.Add("multiple_draw", new List<int> { index });
            }

            return new TResult
            {
                Image = _ticketRenderer.RenderTicket(
                data.ExisintgTicketTask.Type == Entity.Enums.TaskType.LottoRegular ||
                data.ExisintgTicketTask.Type == Entity.Enums.TaskType.LottoSocial ? "LottoRegular" : "LottoDouble", indices)
            };
    }
    }
}
