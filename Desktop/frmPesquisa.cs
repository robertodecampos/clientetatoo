using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClienteTatoo.Control;
using ClienteTatoo.Model;
using ClienteTatoo.Utils;
using ClienteTatoo.Exceptions;

namespace ClienteTatoo
{
    public partial class FormPesquisa : Form
    {
        public IList<KeyValuePair<Pergunta, IList<Resposta>>> Respostas { get; private set; }
        private IList<KeyValuePair<Pergunta, IList<Alternativa>>> Perguntas { get; set; }
        private TipoPergunta TipoPergunta { get; set; }
        private PesquisaControl pesquisaControl { get; set; }
        private int IdReferencia { get; set; }

        public FormPesquisa(TipoPergunta tipoPergunta, PesquisaControl.TipoFonte tipoFonte, bool somenteLeitura)
        {
            InitializeComponent();

            Perguntas = new List<KeyValuePair<Pergunta, IList<Alternativa>>>();

            TipoPergunta = tipoPergunta;

            switch (tipoPergunta)
            {
                case TipoPergunta.Cliente:
                    Text += " - Cliente";
                    break;
                case TipoPergunta.Tatuagem:
                    Text += " - Tatuagem";
                    break;
            }

            btnSalvar.Visible = !somenteLeitura;
            if (tipoFonte == PesquisaControl.TipoFonte.Grande)
                Font = new Font(Font.Name, 12, Font.Style, GraphicsUnit.Point);

            CarregarPerguntas(null);

            if (Perguntas.Count == 0)
                throw new PerguntasNotFoundException("Não existe nenhuma pergunta cadastrada, todas as perguntas estão desativadas ou as perguntas não possuem alternativas!");
            
            pesquisaControl = new PesquisaControl(Perguntas, tipoFonte, somenteLeitura);
            pesquisaControl.Width = ClientSize.Width - 16;
            pesquisaControl.Left = 8;
            pesquisaControl.Height = somenteLeitura ? ClientSize.Height : btnSalvar.Top - 16;
            pesquisaControl.Top = 8;
            pesquisaControl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);

            Controls.Add(pesquisaControl);
        }

        public FormPesquisa(TipoPergunta tipoPergunta, PesquisaControl.TipoFonte tipoFonte, bool somenteLeitura, int id) : this(tipoPergunta, tipoFonte, somenteLeitura) => IdReferencia = id;

        private void CarregarPerguntas(int? idAlternativa)
        {
            using (var conn = new Connection())
            {
                List<Pergunta> perguntas;

                if (idAlternativa == null)
                    perguntas = Pergunta.GetPrincipaisByTipoPergunta(TipoPergunta, true, conn, null);
                else
                    perguntas = Pergunta.GetByIdAlternativa((int)idAlternativa, true, conn, null);

                foreach (Pergunta pergunta in perguntas)
                {
                    List<Alternativa> alternativas = Alternativa.GetByIdPergunta(pergunta.Id, true, conn, null);

                    Perguntas.Add(new KeyValuePair<Pergunta, IList<Alternativa>>(pergunta, alternativas));

                    foreach (Alternativa alternativa in alternativas)
                        CarregarPerguntas(alternativa.Id);
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string mensagem = "";

            if (!pesquisaControl.IsValid(out mensagem))
            {
                MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            Respostas = pesquisaControl.GetRespostas();
        }

        private void FormPesquisa_Load(object sender, EventArgs e)
        {
            try
            {
                IList<Resposta> respostas;

                using (var conn = new Connection())
                {
                    respostas = Resposta.GetByTipoPerguntaAndIdReferencia(TipoPergunta, IdReferencia, true, conn, null);
                }

                pesquisaControl.SetRespostas(respostas);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
