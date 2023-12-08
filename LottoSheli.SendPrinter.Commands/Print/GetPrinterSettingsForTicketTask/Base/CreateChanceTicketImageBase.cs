using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Print.Base
{
    public abstract class CreateChanceTicketImageBase<TResult> : ICreateLottoTicketImageCommand<TResult>
        where TResult : CreatedImageResultBase, new()
    {
        private readonly ITicketRenderer _ticketRenderer;
        private readonly ILogger _logger;

        protected CreateChanceTicketImageBase(ITicketRenderer ticketRenderer, ILogger logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }

        protected abstract string LayoutName {get;}

        protected abstract bool SpecifyChanceType { get; }

        /// <summary>
        /// fills dictionary with indices of elements to be rendered
        /// and renders ticket tacking elements positions by indices from layout
        /// </summary>
        /// <param name="layoutName">layout name</param>
        /// <param name="specifyChanceType">chance subtype</param>
        /// <returns>ticket bitmap</returns>
        private Bitmap RenderTicket(CreateLottoTicketImageCommandData data)
        {

            // Build element indices
            var indices = new Dictionary<string, List<int>>();

            foreach (var table in data.ExisintgTicketTask.Tables)
            {
                if (table.Index < 0 || table.Index > 3)
                    throw new Exception("Unexpected table index");

                string gname = "num" + (table.Index + 1);
                List<int> tableIndices = new List<int>();

                foreach (var item in table.Numbers)
                {
                    int index = Convert.ToString(item) switch
                    {
                        "7" => 0,
                        "8" => 1,
                        "9" => 2,
                        "10" => 3,
                        "J" => 4,
                        "Q" => 5,
                        "K" => 6,
                        "A" => 7,
                        
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                    tableIndices.Add(index);

                }
                indices.Add(gname, tableIndices);
            }

            if (SpecifyChanceType)
            {

                int typeIndex = Convert.ToString(data.ExisintgTicketTask.Settings["chance_type"]) switch
                {
                    "1" => 0,
                    "2" => 1,
                    "3" => 2,
                    "4" => 3,
                    "5" => 4,

                    _ => throw new NotSupportedException("Unexpected chance type")
                };

                indices.Add("chance_type", new List<int> { typeIndex });
            }

            int amountIndex = Convert.ToString(data.ExisintgTicketTask.Settings["inv_amount"]) switch
            {
                "5" => 0,
                "10" => 1,
                "25" => 2,
                "50" => 3,
                "70" => 4,
                "100" => 5,
                "250" => 6,
                "500" => 7,
                _ => throw new NotSupportedException("Unexpected investment amount"),
            };
            indices.Add("investment_amount", new List<int> { amountIndex });

            if (data.ExisintgTicketTask.MultipleDraw >= 2 && data.ExisintgTicketTask.MultipleDraw <= 6)
            {
                indices.Add("multiple_draw", new List<int> { data.ExisintgTicketTask.MultipleDraw - 2 });
            }


            var reprintIds = default(IEnumerable<dynamic>);
            try
            {
                reprintIds = (IEnumerable<dynamic>)data.ExisintgTicketTask.Settings["bulk_ids"];

                if(reprintIds.Any())
                    indices.Add("double", new List<int> { 0 });
            }
            catch (Exception ex) when (ex is RuntimeBinderException || ex is KeyNotFoundException)
            {
                _logger.LogInformation("bulk_ids param is not found");
            }

            return _ticketRenderer.RenderTicket(LayoutName, indices);
        }

        public virtual TResult Execute(CreateLottoTicketImageCommandData data)
        {
            return new TResult()
            {
                Image = RenderTicket(data)
            };
        }

        public virtual bool CanExecute()
        {
            return true;
        }
    }
}
