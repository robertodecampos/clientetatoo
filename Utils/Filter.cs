using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.Utils
{
    public class Filter<FieldFilter>
    {
        public FieldFilter Field { get; set; }
        public object Value { get; set; }

        public Filter(FieldFilter field, object value)
        {
            Field = field;
            Value = value;
        }
    }
}
