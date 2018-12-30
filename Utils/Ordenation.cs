using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.Utils
{
    public enum TypeOrder {toAsc, toDesc}

    public class Ordenation<FieldOrder>
    {
        public FieldOrder Field { get; set; }
        public TypeOrder Type { get; set; }

        public Ordenation(FieldOrder field, TypeOrder type)
        {
            Field = field;
            Type = type;
        }
    }
}
