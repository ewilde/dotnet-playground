using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_language_features.Generics
{
    class GenericConstraints<T> where T : IComparable<T>
    {
    }
}
