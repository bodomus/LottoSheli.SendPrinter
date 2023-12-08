using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Print.Base
{
    public abstract class CreateLottoMethodicalTicketImageBase<TResult> : ICreateLottoTicketImageCommand<TResult>
        where TResult : CreatedImageResultBase, new()
    {
        private readonly ITicketRenderer _ticketRenderer;
        private readonly ILogger _logger;

        private static readonly Dictionary<string, int> FormTypeIndex = new Dictionary<string, int>
            {
                {"8", 0},
                {"5=6", 1},
                {"9", 2},
                {"10", 3},
                {"11", 4},
                {"12", 5}
            };

        private static readonly Dictionary<int, int> MultipleDrawIndex = new Dictionary<int, int>
            {
                {2, 0},
                {4, 1},
                {6, 2},
                {8, 3},
                {10, 4},
            };

        protected CreateLottoMethodicalTicketImageBase(ITicketRenderer ticketRenderer, ILogger logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }

        /// <summary>
        /// creates new ticket bitmap
        /// uses task data and ticket renderer to accomplish this
        /// </summary>
        /// <returns>ticket bitmap</returns>
        protected virtual Bitmap RenderTicket(CreateLottoTicketImageCommandData data, string layoutName)
        {
            var indices = new Dictionary<string, List<int>>();
            
            var methodicalTypeString = data.ExisintgTicketTask.Settings["methodical"].ToString().Replace("\"", "");

            if (FormTypeIndex.TryGetValue(methodicalTypeString, out int val))
                indices.Add("form_type", new List<int> { val });
            else
                throw new InvalidOperationException($"Unknown methodical type: {methodicalTypeString}");

            if (data.ExisintgTicketTask.MultipleDraw > 1)
            {
                indices.Add("multiple_draw", new List<int> { MultipleDrawIndex[data.ExisintgTicketTask.MultipleDraw] });
            }

            if (data.ExisintgTicketTask.Tables.Count() != 1)
            {
                throw new Exception("Unexpected number of tables");
            }

            if (data.ExisintgTicketTask.Tables.First().Strong.Count != 1)
            {
                throw new Exception("Unexpected strong number count");
            }

            indices.Add("num", data.ExisintgTicketTask.Tables.First().Numbers.Select(n => (int)(int.Parse(n.ToString()) - 1)).ToList());
            indices.Add("strong", new List<int> { int.Parse(data.ExisintgTicketTask.Tables.First().Strong[0].ToString()) - 1 });

            if (data.ExisintgTicketTask.Extra == true)
            {
                // miplal hapais layouts contain two lines to draw extra
                var extraElements = new List<int> { 0, 1 };
                indices.Add("extra", extraElements);
            }

            return _ticketRenderer.RenderTicket(layoutName, indices);
        }

        public abstract TResult Execute(CreateLottoTicketImageCommandData data);

        public virtual bool CanExecute()
        {
            return true;
        }
    }
}
