using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Utils
{
    public static class NationalIdUtils
    {
        private const string DELIMETER = "";
        private static readonly List<int> DOUBLED = new List<int> { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
        private static readonly List<string> NATID_WORDS = new List<string> { "6E", "78", "2B", "3D", "47", "57", "3F", "61", "30", "25" };
        private static readonly Regex NATID_REGEX = new Regex(@"(6E|78|2B|3D|47|57|3F|61|30|25)");

        public static IEnumerable<string> NationalIdWords => NATID_WORDS;
        public static string HexToDec(string hex, string delimeter = DELIMETER) 
        {
            if (string.IsNullOrEmpty(hex))
                return string.Empty;

            var matches = NATID_REGEX.Matches(hex);
            if (null == matches || 4 != matches.Count)
                return hex;
            var natIdParts = matches.Select(x => NATID_WORDS.IndexOf(x.Value)).Where(x => x > -1);
            return string.Join(delimeter, natIdParts.ToArray());
        }

        public static string DecToHex(string dec, string delimeter = DELIMETER)
        {
            if (string.IsNullOrEmpty(dec))
                return string.Empty;

            string[] natIdParts = new string[dec.Length];
            for (int i = 0; i < dec.Length; i++) 
            { 
                if (int.TryParse(dec[i].ToString(), out int hexIndex))
                    natIdParts[i] = NATID_WORDS[hexIndex] ?? string.Empty;
            }
            return string.Join(delimeter, natIdParts);
        }

        public static bool IsValidHex(string hex) 
        {
            var matches = NATID_REGEX.Matches(hex);
            return null != matches && 4 == matches.Count;
        }

        public static bool IsValidWord(string word)
        {
            return NATID_WORDS.Contains(word);
        }

        public static bool IsValidUserId(string word)
        {
            return Regex.IsMatch(word, @"^\d\d\d\d$");
        }

        public static string FormatNationalId(string src, char delim = ' ')
        {
            var sb = new StringBuilder();
            for (int i = 0; i < src.Length; i++)
            {
                sb.Append(src[i]);
                if ((i & 1) > 0)
                    sb.Append(delim);
            }
            return sb.ToString().Trim();
        }

        public static string FixNationalId(string src) 
        {
            src = Regex.Replace(src, @"\W", "");

            if (IsValidHex(src))
                return src;

            if (IsValidUserId(src))
                return DecToHex(src);

            if (Regex.IsMatch(src, @"^[0-9]{3,7}"))
                src = DecToHex(src).PadRight(8);

            string[] parts = new string[4];
            for(int i = 0; i < parts.Length; i++) 
            { 
                parts[i] = FixWord(src.Substring(i * 2, 2));
            }
            return string.Join(DELIMETER, parts);
        }

        public static string FixWord(string mayBeWrong)
        {
            return mayBeWrong switch
            {
                "7B" or "76" => "78",
                "28" or "26" or "2E" => "2B",
                "BE" or "8E" or "5E" or "EE" => "6E",
                "81" or "B1" or "51" or "01" or "91" or "E1" => "61",
                "36" or "39" => "30",
                "2F" => "3F",
                // s
                _ => mayBeWrong,
            };
        }

        public static Task SaveNationalId(string natId, string path = "national_ids.txt") 
        {
            return Task.Run(() =>
            {
                natId = FixNationalId(natId);
                string record = $"{natId}:{HexToDec(natId)}:{ComputePartialHash(natId)}";
                using (StreamWriter sw = File.AppendText(path))
                    sw.WriteLine(record);
            });
        }

        public static Task SaveNationalIdSegments(Bitmap img, string natId, string folder)
        {
            return Task.Run(() =>
            {
                if (IsValidUserId(natId))
                    natId = DecToHex(natId);

                var matches = NATID_REGEX.Matches(natId);
                if (null != matches && 4 == matches.Count) 
                {
                    try
                    {
                        Rectangle imgRect = new Rectangle(0, 0, img.Width, img.Height);
                        int xpos = 0;
                        int width = img.Width / matches.Count;
                        foreach (var match in matches)
                        {
                            var segPath = Path.Combine(folder, match.ToString());
                            
                            if (!Directory.Exists(segPath))
                                Directory.CreateDirectory(segPath);
                            var filePath = Path.Combine(segPath, $"{match}_{DateTime.Now.Ticks}.png");
                            var rect = new Rectangle(xpos, 0, width, img.Height);

                            using var segImage = img.Clone(Rectangle.Intersect(rect, imgRect), img.PixelFormat);
                            segImage.Save(filePath);
                            xpos += width;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                
            });
        }

        public static int ComputePartialHash(string natId) 
        {
            if (string.IsNullOrEmpty(natId))
                return 0;

            var matches = NATID_REGEX.Matches(natId);
            if (null == matches || 4 != matches.Count)
                return 0;
            return matches.Select((x, i) => 
                {
                    int dec = NATID_WORDS.IndexOf(x.Value);
                    return i % 2 == 0 ? DOUBLED[dec] : dec;
                })
                .Sum();
            
        }

        public static bool IsValidIsraeliID(string israeliID)
        {
            if (israeliID.Length != 9)
                return false;

            long sum = 0;

            for (int i = 0; i < israeliID.Length; i++)
            {
                var digit = israeliID[israeliID.Length - 1 - i] - '0';
                sum += (i % 2 != 0) ? GetDouble(digit) : digit;
            }

            return sum % 10 == 0;

            int GetDouble(long i)
            {
                switch (i)
                {
                    case 0: return 0;
                    case 1: return 2;
                    case 2: return 4;
                    case 3: return 6;
                    case 4: return 8;
                    case 5: return 1;
                    case 6: return 3;
                    case 7: return 5;
                    case 8: return 7;
                    case 9: return 9;
                    default: return 0;
                }
            }
        }
    }
}
