﻿namespace ClienteTatoo
{
    partial class FormPergunta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.gbxTipoResposta = new System.Windows.Forms.GroupBox();
            this.rbDissertativa = new System.Windows.Forms.RadioButton();
            this.rbMultiplaSelecao = new System.Windows.Forms.RadioButton();
            this.rbSelecaoUnica = new System.Windows.Forms.RadioButton();
            this.cbxRespostaObrigatoria = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.gbxTipoResposta.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.Location = new System.Drawing.Point(12, 25);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(311, 20);
            this.txtDescricao.TabIndex = 1;
            // 
            // gbxTipoResposta
            // 
            this.gbxTipoResposta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxTipoResposta.Controls.Add(this.rbSelecaoUnica);
            this.gbxTipoResposta.Controls.Add(this.rbMultiplaSelecao);
            this.gbxTipoResposta.Controls.Add(this.rbDissertativa);
            this.gbxTipoResposta.Location = new System.Drawing.Point(12, 51);
            this.gbxTipoResposta.Name = "gbxTipoResposta";
            this.gbxTipoResposta.Size = new System.Drawing.Size(311, 90);
            this.gbxTipoResposta.TabIndex = 2;
            this.gbxTipoResposta.TabStop = false;
            this.gbxTipoResposta.Text = "Tipo de Reposta";
            // 
            // rbDissertativa
            // 
            this.rbDissertativa.AutoSize = true;
            this.rbDissertativa.Location = new System.Drawing.Point(6, 19);
            this.rbDissertativa.Name = "rbDissertativa";
            this.rbDissertativa.Size = new System.Drawing.Size(80, 17);
            this.rbDissertativa.TabIndex = 0;
            this.rbDissertativa.TabStop = true;
            this.rbDissertativa.Text = "Dissertativa";
            this.rbDissertativa.UseVisualStyleBackColor = true;
            // 
            // rbMultiplaSelecao
            // 
            this.rbMultiplaSelecao.AutoSize = true;
            this.rbMultiplaSelecao.Location = new System.Drawing.Point(6, 42);
            this.rbMultiplaSelecao.Name = "rbMultiplaSelecao";
            this.rbMultiplaSelecao.Size = new System.Drawing.Size(103, 17);
            this.rbMultiplaSelecao.TabIndex = 1;
            this.rbMultiplaSelecao.TabStop = true;
            this.rbMultiplaSelecao.Text = "Múltipla Seleção";
            this.rbMultiplaSelecao.UseVisualStyleBackColor = true;
            // 
            // rbSelecaoUnica
            // 
            this.rbSelecaoUnica.AutoSize = true;
            this.rbSelecaoUnica.Location = new System.Drawing.Point(6, 65);
            this.rbSelecaoUnica.Name = "rbSelecaoUnica";
            this.rbSelecaoUnica.Size = new System.Drawing.Size(93, 17);
            this.rbSelecaoUnica.TabIndex = 2;
            this.rbSelecaoUnica.TabStop = true;
            this.rbSelecaoUnica.Text = "Seleção ùnica";
            this.rbSelecaoUnica.UseVisualStyleBackColor = true;
            // 
            // cbxRespostaObrigatoria
            // 
            this.cbxRespostaObrigatoria.AutoSize = true;
            this.cbxRespostaObrigatoria.Location = new System.Drawing.Point(12, 147);
            this.cbxRespostaObrigatoria.Name = "cbxRespostaObrigatoria";
            this.cbxRespostaObrigatoria.Size = new System.Drawing.Size(125, 17);
            this.cbxRespostaObrigatoria.TabIndex = 3;
            this.cbxRespostaObrigatoria.Text = "Resposta Obrigatória";
            this.cbxRespostaObrigatoria.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSalvar.Location = new System.Drawing.Point(248, 193);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FormPergunta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 228);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cbxRespostaObrigatoria);
            this.Controls.Add(this.gbxTipoResposta);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPergunta";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pergunta";
            this.gbxTipoResposta.ResumeLayout(false);
            this.gbxTipoResposta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.GroupBox gbxTipoResposta;
        private System.Windows.Forms.RadioButton rbSelecaoUnica;
        private System.Windows.Forms.RadioButton rbMultiplaSelecao;
        private System.Windows.Forms.RadioButton rbDissertativa;
        private System.Windows.Forms.CheckBox cbxRespostaObrigatoria;
        private System.Windows.Forms.Button btnSalvar;
    }
}