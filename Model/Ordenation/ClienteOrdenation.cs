using ClienteTatoo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteTatoo.Model.Ordenation
{
    public enum FieldOrdenationCliente {Codigo, Nome, DataNascimento}

    public class ClienteOrdenation : Ordenation<FieldOrdenationCliente>
    {
        public ClienteOrdenation(FieldOrdenationCliente field, TypeOrder type) : base(field, type) { }
    }
}
