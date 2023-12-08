using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    public enum PlayCardSuit 
    { 
        Empty = -1,
        Clubs = 0,
        Diamonds = 1,
        Hearts = 2,
        Spades = 3
    };
    public enum PlayCardRank 
    { 
        None = 0,
        n7 = 1,
        n8 = 2,
        n9 = 4,
        n10 = 8,
        J = 16,
        Q = 32,
        K = 64,
        A = 128
    };

    public class PlayCard
    {
        public static readonly char[] SUITS = new char[] { '\u2663', '\u2666', '\u2665', '\u2660'};
        public static readonly PlayCard Empty = new PlayCard();


        public PlayCardSuit Suit { get; set; } = PlayCardSuit.Empty;
        public PlayCardRank Rank { get; set; } = PlayCardRank.None;
        public int Id { get; set; }

        public PlayCard()
        {
            Id = GetHashCode();
        }

        public PlayCard(PlayCardSuit suit, PlayCardRank rank)
        {
            Suit = suit;
            Rank = rank;
            Id = GetHashCode();
        }

        public override string ToString()
        {
            return Rank.ToString() + SUITS[(int)Suit].ToString();
        }
        public string ToString(string format) => 
            "R" == format 
                ? Rank.ToString().Replace("n", "") 
                : "S" == format 
                    ? SUITS[(int)Suit].ToString() 
                    : ToString();

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (null == obj) return false;
            if (obj is PlayCard pc) return pc.Suit == Suit && pc.Rank == Rank;
            return false;
        }

        public override int GetHashCode()
        {
            return (int)Suit ^ (int)Rank;
        }

        public static PlayCard Parse(string input)
        {
            var rankString = input.Substring(0, input.Length - 1);
            var suitString = input.Substring(input.Length - 1);
            return new PlayCard()
            {
                Suit = ParseSuit(suitString),
                Rank = ParseRank(rankString)
            };
        }

        public static PlayCardSuit ParseSuit(string input)
        {
            return string.IsNullOrEmpty(input)
                ? PlayCardSuit.Empty
                : (PlayCardSuit)(SUITS.ToList().IndexOf(input[0]));
        }

        public static PlayCardRank ParseRank(string input) => input switch
        { 
            "7" => PlayCardRank.n7,
            "8" => PlayCardRank.n8,
            "9" => PlayCardRank.n9,
            "10" => PlayCardRank.n10,
            "J" => PlayCardRank.J,
            "Q" => PlayCardRank.Q,
            "K" => PlayCardRank.K,
            "A" => PlayCardRank.A,
            _ => PlayCardRank.None
        };
    }
}
