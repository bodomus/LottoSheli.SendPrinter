using System;
using System.Runtime.Serialization;

namespace LottoSheli.SendPrinter.Core
{
    public static class PublicExtensions
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Gets an <see cref="EnumMemberAttribute"/> value fro specified enum
        /// </summary>
        /// <param name="enumVal">The enum value</param>
        /// <returns>an <see cref="EnumMemberAttribute"/> value or null if the attribute doesn't exists.</returns>
        /// <example><![CDATA[string val = myEnumVariable.GetEnumMeberValue();]]></example>
        public static string GetEnumMemberValue(this Enum enumVal)
        {
           return enumVal.GetAttributeOfType<EnumMemberAttribute>()?.Value;
        }

        public static string GetEnumMemberSummaryValue(this Enum enumVal)
        {
            string result = enumVal.GetAttributeOfType<TaskTypeSummaryNameAttribute>()?.Value;

            if(string.IsNullOrEmpty(result))
                result = enumVal.GetAttributeOfType<EnumMemberAttribute>()?.Value;

            return result;
        }

        public static string GetTicketTypeValue(this Enum enumVal)
        {
            string result = enumVal.GetAttributeOfType<TicketTypeAttribute>()?.Value;

            if (string.IsNullOrEmpty(result))
                result = enumVal.GetAttributeOfType<EnumMemberAttribute>()?.Value;

            return result;
        }
    }
}
