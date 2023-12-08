using Newtonsoft.Json;
using System;

namespace LottoSheli.SendPrinter.Entity
{
    public class ScannerResponseData
    {
        /// <summary>
        /// slip id
        /// </summary>
        [JsonProperty("slip_id", Order = 5)]
        public virtual string SlipId { get; set; }

        /// <summary>
        /// task status
        /// </summary>
        [JsonProperty("status", Order = 1)]
        public int Status { get; set; }

        /// <summary>
        /// slip crop line (not to show users sensititve data)
        /// </summary>
        [JsonProperty("crop_line", Order = 2)]
        public int CropLine { get; set; }

        /// <summary> 
        /// bottom slip crop line (not to show users sensititve data) 
        /// </summary> 
        [JsonProperty("crop_line_bottom", Order = 6)]
        public int? BottomCropLine { get; set; }

        /// <summary>
        /// ticket id
        /// </summary>
        [JsonProperty("tid", Order = 4)]
        public int Tid { get; set; }

        /// <summary>
        /// barcode mapper instance
        /// </summary>
        [JsonProperty("barcode", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public TicketBarcodeData Barcode { get; set; }

        /// <summary>
        /// key that prevents scan dispatcher from sending metadata to Drupal
        /// </summary>
        [JsonProperty("skipUpdate", Order = 7)]
        public bool SkipUpdate { get; set; } = false;
    }
}
