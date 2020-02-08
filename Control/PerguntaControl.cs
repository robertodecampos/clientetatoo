using ClienteTatoo.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClienteTatoo.Control
{
    public class PerguntaControl : Panel
    {
        public Pergunta Pergunta { get; private set; }
        public IList<AlternativaControl> Alternativas { get; private set; }

        public event AlternativaControl.AlternativaHandler CheckedChanged {
            add
            {
                for (int i = 0; i < Alternativas.Count; i++)
                    Alternativas[i].CheckedChanged += value;
            }
            remove
            {
                for (int i = 0; i < Alternativas.Count; i++)
                    Alternativas[i].CheckedChanged -= value;
            }
        }

        protected Label lblDescricao;

        public PerguntaControl(Pergunta pergunta, IList<Alternativa> alternativas, PesquisaControl.TipoFonte tipoFonte, bool somenteLeitura) : base()
        {
            if (tipoFonte == PesquisaControl.TipoFonte.Grande)
                Font = new Font(Font.Name, 12, Font.Style, GraphicsUnit.Point);

            Pergunta = pergunta;

            Alternativas = new List<AlternativaControl>(alternativas.Count);

            if (pergunta.Dissertativa)
                Alternativas.Add(new AlternativaControl(null, tipoFonte, somenteLeitura));
            else
            {
                foreach (Alternativa alternativa in alternativas)
                    Alternativas.Add(new AlternativaControl(alternativa, !pergunta.AlternativaUnica, tipoFonte, somenteLeitura));
            }

            lblDescricao = new Label();
            Controls.Add(lblDescricao);
            lblDescricao.AutoSize = false;
            lblDescricao.Font = new Font(lblDescricao.Font.Name, lblDescricao.Font.Size, FontStyle.Bold, lblDescricao.Font.Unit);
            lblDescricao.Left = 0;
            lblDescricao.Top = 0;
            lblDescricao.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            lblDescricao.Text = Pergunta.Descricao;

            PosicionarComponentes();
        }

        public bool IsValid(out string mensagem)
        {
            mensagem = "";

            if (Pergunta.Obrigatoria)
            {
                bool respondida = false;

                if (Pergunta.Dissertativa)
                    respondida = (Alternativas[0].Text != string.Empty);
                else
                {
                    foreach (AlternativaControl alternativa in Alternativas)
                    {
                        if (alternativa.Checked)
                        {
                            respondida = true;
                            break;
                        }
                    }
                }

                if (!respondida)
                {
                    mensagem = $"A pergunta `{Pergunta.Descricao}` é obrigatória e não foi respondida!";
                    return false;
                }
            }

            return true;
        }

        public bool IsValid()
        {
            string mensagem;
            return IsValid(out mensagem);
        }

        public IList<Resposta> GetRespostas()
        {
            var respostas = new List<Resposta>();

            foreach (AlternativaControl alternativa in Alternativas)
            {
                if (alternativa.Checked || (alternativa.Text.Trim() != string.Empty))
                {
                    Resposta resposta = alternativa.GetResposta();
                    resposta.IdPergunta = Pergunta.Id;

                    respostas.Add(resposta);
                }
            }

            return respostas;
        }

        public void SetResposta(Resposta resposta)
        {
            if (Pergunta.Dissertativa)
            {
                Alternativas[0].Text = resposta.RespostaDissertativa;
                return;
            }

            foreach (AlternativaControl alternativa in Alternativas)
            {
                if (alternativa.Alternativa.Id == resposta.IdAlternativa)
                {
                    alternativa.Checked = true;
                    break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            PosicionarComponentes();
        }

        private void Alternativa_CheckedChanged(int idAlternativa, bool marcada)
        {
            if ((!Pergunta.AlternativaUnica) || (!marcada))
                return;

            foreach (AlternativaControl alternativa in Alternativas)
            {
                if ((alternativa.Alternativa.Id != idAlternativa) && (alternativa.Checked))
                    alternativa.Checked = false;
            }
        }

        private void PosicionarComponentes()
        {
            lblDescricao.Width = ClientSize.Width;

            using (Graphics graphics = lblDescricao.CreateGraphics())
            {
                lblDescricao.Height = (int)Math.Ceiling(graphics.MeasureString(lblDescricao.Text, lblDescricao.Font, lblDescricao.Width).Height);
            }

            int topComponent = lblDescricao.Height + 8;

            for (int i = 0; i < Alternativas.Count; i++)
            {
                AlternativaControl alternativa = Alternativas[i];
                alternativa.Top = topComponent;
                alternativa.Left = 0;
                alternativa.Width = ClientSize.Width;
                alternativa.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                alternativa.CheckedChanged += Alternativa_CheckedChanged;

                Controls.Add(alternativa);

                topComponent += alternativa.Height + 4;
            }

            Height = topComponent + 4;
        }
    }
}
