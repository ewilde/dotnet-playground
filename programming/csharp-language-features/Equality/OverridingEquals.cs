using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.Equality
{
    class Employee : IEquatable<Employee>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Equals(Employee other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Employee) obj);
        }

        public override int GetHashCode()
        {
             return ((FirstName != null ? FirstName.GetHashCode() * 37 : 0) + 
                       (LastName != null ? LastName.GetHashCode() : 0));
        }
    }
}
