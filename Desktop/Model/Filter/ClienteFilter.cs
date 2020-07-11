using ClienteTatoo.Utils;

namespace ClienteTatoo.Model.Filter
{
    public enum FieldFilterCliente {ffcNome, ffcCpf, ffcEmail, ffcTelefone, ffcCelular}

    public class ClienteFilter : Filter<FieldFilterCliente>
    {
        public ClienteFilter(FieldFilterCliente field, object value) : base(field, value) { }
    }
}
