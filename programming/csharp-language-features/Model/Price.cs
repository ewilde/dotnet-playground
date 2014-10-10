using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.Model
{
    public struct Price : IEquatable<Price>
    {
        public Price(int marketId, double bid, double mid, double ask) : this()
        {
            MarketId = marketId;
            Bid = bid;
            Mid = mid;
            Ask = ask;
        }

        public int MarketId { get; private set; }

        public double Bid { get; private set; }

        public double Mid { get; private set; }

        public double Ask { get; private set; }

        public bool Equals(Price other)
        {
            return MarketId == other.MarketId && Bid.Equals(other.Bid) && Mid.Equals(other.Mid) && Ask.Equals(other.Ask);
        }

        public override string ToString()
        {
            return string.Format("Bid {0}, Mid {1}, Ask {2}", this.Bid, this.Mid, this.Ask);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Price && Equals((Price) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = MarketId;
                hashCode = (hashCode*397) ^ Bid.GetHashCode();
                hashCode = (hashCode*397) ^ Mid.GetHashCode();
                hashCode = (hashCode*397) ^ Ask.GetHashCode();
                return hashCode;
            }
        }

        private sealed class PriceEqualityComparer : IEqualityComparer<Price>
        {
            public bool Equals(Price x, Price y)
            {
                return x.MarketId == y.MarketId && x.Bid.Equals(y.Bid) && x.Mid.Equals(y.Mid) && x.Ask.Equals(y.Ask);
            }

            public int GetHashCode(Price obj)
            {
                unchecked
                {
                    var hashCode = obj.MarketId;
                    hashCode = (hashCode*397) ^ obj.Bid.GetHashCode();
                    hashCode = (hashCode*397) ^ obj.Mid.GetHashCode();
                    hashCode = (hashCode*397) ^ obj.Ask.GetHashCode();
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<Price> PriceComparerInstance = new PriceEqualityComparer();

        public static IEqualityComparer<Price> PriceComparer
        {
            get { return PriceComparerInstance; }
        }
    }
}
