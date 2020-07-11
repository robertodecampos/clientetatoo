using System.Collections.Generic;
using ClienteTatoo.Model;

namespace ClienteTatoo.TO
{
    public class ImportacaoClienteTO
    {
        public Cliente Cliente { get; set; }
        public Tatuagem Tatuagem { get; set; }
        public Sessao Sessao { get; set; }
        public List<Resposta> Respostas { get; set; }
        public Cidade Cidade { get; set; }
        public bool Ignorar { get; set; } = false;
        public string Mensagem { get; set; }

        public ImportacaoClienteTO()
        {
            Cliente = new Cliente();
            Tatuagem = new Tatuagem();
            Sessao = new Sessao();
            Respostas = new List<Resposta>();
        }
    }
}
