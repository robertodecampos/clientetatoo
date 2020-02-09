using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClienteTatoo.Model;

namespace ClienteTatoo.Control
{
    public partial class PesquisaControl : Panel
    {
        public enum TipoFonte { Normal, Grande };

        private IList<PerguntaControl> Perguntas { get; set; }

        public PesquisaControl(IList<KeyValuePair<Pergunta, IList<Alternativa>>> perguntas, TipoFonte tipoFonte, bool somenteLeitura) : base()
        {
            if (tipoFonte == TipoFonte.Grande)
                Font = new Font(Font.Name, 12, Font.Style, GraphicsUnit.Point);

            HorizontalScroll.Enabled = false;
            HorizontalScroll.Visible = false;
            HorizontalScroll.Maximum = 0;
            AutoScroll = true;

            Perguntas = new List<PerguntaControl>(perguntas.Count);

            foreach (KeyValuePair<Pergunta, IList<Alternativa>> pergunta in perguntas)
            {
                var perguntaControl = new PerguntaControl(pergunta.Key, pergunta.Value, tipoFonte, somenteLeitura);
                perguntaControl.Width = ClientSize.Width;
                perguntaControl.Left = 0;
                perguntaControl.Visible = false;
                perguntaControl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                perguntaControl.BorderStyle = BorderStyle.FixedSingle;
                perguntaControl.CheckedChanged += Alternativa_CheckedChanged;

                Perguntas.Add(perguntaControl);
                Controls.Add(perguntaControl);
            }

            ExibirPerguntas(null);
        }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            foreach (PerguntaControl pergunta in Perguntas)
            {
                if (pergunta.Visible && !pergunta.IsValid(out mensagem))
                    return false;
            }

            return true;
        }

        public bool IsValid()
        {
            string mensagem;
            return IsValid(out mensagem);
        }

        public IList<KeyValuePair<Pergunta, IList<Resposta>>> GetRespostas()
        {
            var perguntaRespostas = new List<KeyValuePair<Pergunta, IList<Resposta>>>();

            foreach (PerguntaControl pergunta in Perguntas)
            {
                IList<Resposta> respostas = pergunta.GetRespostas();
                perguntaRespostas.Add(new KeyValuePair<Pergunta, IList<Resposta>>(pergunta.Pergunta, respostas));
            }

            return perguntaRespostas;
        }

        public void SetRespostas(IList<Resposta> respostas)
        {
            foreach (Resposta resposta in respostas)
            {
                foreach (PerguntaControl pergunta in Perguntas)
                {
                    if (pergunta.Pergunta.Id == resposta.IdPergunta)
                    {
                        pergunta.SetResposta(resposta);
                        break;
                    }
                }
            }

            PosicionarPerguntas();
        }

        protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

        private void ExibirPerguntas(int? idAlternativa)
        {
            for (int i = 0; i < Perguntas.Count; i++)
            {
                Pergunta pergunta = Perguntas[i].Pergunta;

                if (pergunta.IdAlternativa == idAlternativa)
                    Perguntas[i].Visible = true;
            }

            PosicionarPerguntas();
        }

        private void OcultarPerguntas(int? idAlternativa)
        {
            for (int i = 0; i < Perguntas.Count; i++)
            {
                Pergunta pergunta = Perguntas[i].Pergunta;

                if (pergunta.IdAlternativa == idAlternativa)
                    Perguntas[i].Visible = false;
            }

            PosicionarPerguntas();
        }

        private void PosicionarPerguntas()
        {
            int posY = 8;
            int posScroll = VerticalScroll.Value;

            VerticalScroll.Value = VerticalScroll.Minimum;

            for (int i = 0; i < Perguntas.Count; i++)
            {

                if (Perguntas[i].Visible)
                {
                    Perguntas[i].Top = posY;

                    posY += Perguntas[i].Height + 8;
                }
            }

            VerticalScroll.Value = posScroll;
        }

        private void Alternativa_CheckedChanged(int idAlternativa, bool marcada)
        {
            for (int i = 0; i < Perguntas.Count; i++)
            {
                if (Perguntas[i].Pergunta.IdAlternativa == idAlternativa)
                    Perguntas[i].Visible = marcada;
            }

            PosicionarPerguntas();
        }
    }
}
