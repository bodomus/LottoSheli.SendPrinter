using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Entity.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    [JsonObject(
        MemberSerialization = MemberSerialization.OptIn,
        ItemNullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class SlipDataEntity : BaseEntity
    {
        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public GameType GameType { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public GameSubtype GameSubtype { get; set; }

        [JsonProperty]
        public string SlipId { get; set; }

        [JsonProperty]
        public string Topbar { get; set; }

        [JsonProperty]
        public string NationalId { get; set; }

        [JsonProperty]
        public string UserId => NationalIdUtils.HexToDec(NationalId);

        [JsonProperty]
        public string Extra { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public ExtraMarkOption ExtraMark { get; set; }

        [JsonProperty]
        public bool IsDouble { get; set; }

        [JsonProperty]
        public bool ShowNationalId { get; set; }

        [JsonProperty]
        public int Price { get; set; }

        [JsonProperty]
        public int TopCropline { get; set; }

        [JsonProperty]
        public int BottomCropline { get; set; }

        [JsonProperty]
        public IEnumerable<TicketTable> Tables { get; set; }

        [JsonProperty]
        public List<SlipBlockType> FailedBlocks { get; set; }

        [JsonProperty]
        public string ErrorMessage { get; set; }

        [JsonProperty]
        public override int Id { get; set; }

        [JsonProperty]
        public override DateTime CreatedDate { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public override bool Equals(object obj) => obj is SlipDataEntity data && Equals(data);

        public bool Equals(SlipDataEntity other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (null == other) return false;
            return SlipId == other.SlipId;
        }

        //TODO: make hash generation compliant with the one used for data fetching
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(SlipId);
            if (ShowNationalId)
                hash.Add(NationalId);
            return hash.ToHashCode();
        }
    }
}
