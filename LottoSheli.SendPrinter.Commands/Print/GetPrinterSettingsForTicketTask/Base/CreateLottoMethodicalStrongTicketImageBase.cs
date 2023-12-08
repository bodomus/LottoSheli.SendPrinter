using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Print.Base
{
    public abstract class CreateLottoMethodicalStrongTicketImageBase<TResult> : CreateLottoMethodicalTicketImageBase<TResult>
        where TResult : CreatedImageResultBase, new()
    {
        private readonly ITicketRenderer _ticketRenderer;
        private readonly ILogger _logger;


        private static readonly Dictionary<string, int> FormStrongTypeIndex = new Dictionary<string, int>
            {
                {"4", 0},
                {"5", 1},
                {"6", 2},
                {"7", 3}
            };

        private static readonly Dictionary<int, int> MultipleDrawIndex = new Dictionary<int, int>
            {
                {2, 0},
                {4, 1},
                {6, 2},
                {8, 3},
                {10, 4},
            };

        protected CreateLottoMethodicalStrongTicketImageBase(ITicketRenderer ticketRenderer, ILogger logger)
            : base(ticketRenderer, logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }


        /// <summary>
        /// creates new ticket bitmap
        /// uses task data and ticket renderer to accomplish this
        /// </summary>
        /// <returns>ticket bitmap</returns>
        protected override Bitmap RenderTicket(CreateLottoTicketImageCommandData data, string layoutName)
        {
            Dictionary<string, List<int>> indices = new Dictionary<string, List<int>>();

            if (data.ExisintgTicketTask.Tables.Count() != 1)
            {
                throw new Exception("Unexpected number of tables");
            }

            // Numbers
            indices.Add("num", data.ExisintgTicketTask.Tables.First().Numbers.Select(n => (int)int.Parse(n.ToString()) - 1).ToList());

            // Strong
            indices.Add("strong", data.ExisintgTicketTask.Tables.First().Strong.Select(n => (int)int.Parse(n.ToString()) - 1).ToList());

            // Form type
            var typeindex = data.ExisintgTicketTask.Settings["methodical"].ToString();
            indices.Add("form_type", new List<int> { FormStrongTypeIndex[typeindex] });

            // Multiple draw
            if (data.ExisintgTicketTask.MultipleDraw > 1)
            {
                indices.Add("multiple_draw", new List<int> { MultipleDrawIndex[data.ExisintgTicketTask.MultipleDraw] });
            }

            // Extra
            if (data.ExisintgTicketTask.Extra == true)
            {
                // miplal hapais layouts contain two lines to draw extra
                var extraElements = new List<int> { 0, 1 };
                indices.Add("extra", extraElements);
            }

            return _ticketRenderer.RenderTicket(layoutName, indices);
        }
    }
}
