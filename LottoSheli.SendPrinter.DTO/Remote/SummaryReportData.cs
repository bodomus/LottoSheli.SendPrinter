using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class SummaryReportData
    {

        public SummaryReportData()
        {
            PrinterSummaryTypeField = new List<PrinterSummaryTypeData>() {
                new PrinterSummaryTypeData { TargetId = "default", TargetType = "printer_summary_type" }
            };
        }

        [JsonIgnore]
        public string PrinterLogin
        {
            get
            {

                return PrinterLoginField?.FirstOrDefault()?.Value;

            }
            set {
                if (PrinterLoginField == null)
                    PrinterLoginField = new List<D9FieldValue<string>> { new D9FieldValue<string> { Value = value } };
                else
                    PrinterLoginField[0].Value = value;

            }
        }

        [JsonIgnore]
        public TargetValue PrinterStationId
        {
            get
            {

                if (PrinterStationIdField == null)
                    PrinterStationIdField = new List<TargetValue> { new TargetValue() };

                return PrinterStationIdField.First();

            }
        }

        [JsonIgnore]
        public DateTime? Date
        {
            get
            {

                return DateTimeField?.FirstOrDefault()?.Value;

            }
            set
            {
                if (DateTimeField == null)
                    DateTimeField = new List<D9FieldValue<DateTime?>> { new D9FieldValue<DateTime?> { Value = value } };
                else
                    DateTimeField[0].Value = value;

            }
        }

        [JsonIgnore]
        public decimal? TotalAmount
        {
            get
            {

                return TotalAmountField?.FirstOrDefault()?.Value;

            }
            set
            {
                if (TotalAmountField == null)
                    TotalAmountField = new List<D9FieldValue<decimal?>> { new D9FieldValue<decimal?> { Value = value } };
                else
                    TotalAmountField[0].Value = value;

            }
        }

        [JsonIgnore]
        public TicketRangeData TicketRange
        {
            get
            {
                if (TicketRangeField == null)
                    TicketRangeField = new List<TicketRangeData>() { new TicketRangeData() };

                return TicketRangeField?.FirstOrDefault();

            }
            set
            {
                if (TicketRangeField == null)
                    TicketRangeField = new List<TicketRangeData>() { value };
                else
                    TicketRangeField[0] = value;

            }
        }

        [JsonIgnore]
        public TicketPrintType? TicketPrintType
        {
            get
            {

                return TicketPrintTypeField?.FirstOrDefault()?.Value;

            }
            set
            {
                if (TicketPrintTypeField == null)
                    TicketPrintTypeField = new List<D9FieldValue<TicketPrintType?>> { new D9FieldValue<TicketPrintType?> { Value = value } };
                else
                    TicketPrintTypeField[0].Value = value;

            }
        }

        [JsonIgnore]
        public string PrinterStationName
        {
            get
            {

                return PrinterStationNameField?.FirstOrDefault()?.Value;

            }
            set
            {
                if (PrinterStationNameField == null)
                    PrinterStationNameField = new List<D9FieldValue<string>> { new D9FieldValue<string> { Value = value } };
                else
                    PrinterStationNameField[0].Value = value;

            }
        }

        [JsonProperty(PropertyName = "ticket")]
        public IEnumerable<TicketData> Tickets { get; set; }


        //Stub for Drupal 8 server
        [JsonProperty(PropertyName = "printer_summary_type", Required = Required.Always)]
        internal IEnumerable<PrinterSummaryTypeData> PrinterSummaryTypeField { get; private set; }

        [JsonProperty(PropertyName = "printer_login", Required = Required.Always)]
        internal IList<D9FieldValue<string>> PrinterLoginField { get; private set; }

        [JsonProperty(PropertyName = "printer_station_id", Required = Required.Always)]
        internal IList<TargetValue> PrinterStationIdField { get; private set; }

        [JsonProperty(PropertyName = "datetime", Required = Required.Always)]
        internal IList<D9FieldValue<DateTime?>> DateTimeField { get; private set; }

        [JsonIgnore]
        //[JsonProperty(PropertyName = "ticket_range", Required = Required.Always)]
        internal IList<TicketRangeData> TicketRangeField { get; private set; }

        [JsonProperty(PropertyName = "ticket_print_type", Required = Required.Always)]
        internal IList<D9FieldValue<TicketPrintType?>> TicketPrintTypeField { get; private set; }

        [JsonProperty(PropertyName = "total_amount", Required = Required.Always)]
        internal IList<D9FieldValue<decimal?>> TotalAmountField { get; private set; }

        [JsonProperty(PropertyName = "printer_station_name", Required = Required.AllowNull)]
        internal IList<D9FieldValue<string>> PrinterStationNameField { get; private set; }

    }
}
