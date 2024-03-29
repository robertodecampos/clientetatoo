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
            this.gbxTipoAlternativa = new System.Windows.Forms.GroupBox();
            this.rbSelecaoUnica = new System.Windows.Forms.RadioButton();
            this.rbMultiplaSelecao = new System.Windows.Forms.RadioButton();
            this.rbDissertativa = new System.Windows.Forms.RadioButton();
            this.cbxAlternativaObrigatoria = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigoImportacao = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numColunasAlternativas = new System.Windows.Forms.NumericUpDown();
            this.gbxTipoAlternativa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColunasAlternativas)).BeginInit();
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
            // gbxTipoAlternativa
            // 
            this.gbxTipoAlternativa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxTipoAlternativa.Controls.Add(this.rbSelecaoUnica);
            this.gbxTipoAlternativa.Controls.Add(this.rbMultiplaSelecao);
            this.gbxTipoAlternativa.Controls.Add(this.rbDissertativa);
            this.gbxTipoAlternativa.Location = new System.Drawing.Point(12, 102);
            this.gbxTipoAlternativa.Name = "gbxTipoAlternativa";
            this.gbxTipoAlternativa.Size = new System.Drawing.Size(311, 90);
            this.gbxTipoAlternativa.TabIndex = 3;
            this.gbxTipoAlternativa.TabStop = false;
            this.gbxTipoAlternativa.Text = "Tipo de Reposta";
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
            this.rbSelecaoUnica.CheckedChanged += new System.EventHandler(this.rbSelecaoUnica_CheckedChanged);
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
            this.rbMultiplaSelecao.CheckedChanged += new System.EventHandler(this.rbMultiplaSelecao_CheckedChanged);
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
            this.rbDissertativa.CheckedChanged += new System.EventHandler(this.rbDissertativa_CheckedChanged);
            // 
            // cbxAlternativaObrigatoria
            // 
            this.cbxAlternativaObrigatoria.AutoSize = true;
            this.cbxAlternativaObrigatoria.Location = new System.Drawing.Point(200, 222);
            this.cbxAlternativaObrigatoria.Name = "cbxAlternativaObrigatoria";
            this.cbxAlternativaObrigatoria.Size = new System.Drawing.Size(123, 17);
            this.cbxAlternativaObrigatoria.TabIndex = 4;
            this.cbxAlternativaObrigatoria.Text = "Pergunta Obrigatória";
            this.cbxAlternativaObrigatoria.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSalvar.Location = new System.Drawing.Point(248, 264);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Código de Importação";
            // 
            // txtCodigoImportacao
            // 
            this.txtCodigoImportacao.Location = new System.Drawing.Point(12, 73);
            this.txtCodigoImportacao.Name = "txtCodigoImportacao";
            this.txtCodigoImportacao.Size = new System.Drawing.Size(311, 20);
            this.txtCodigoImportacao.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Colunas para Alternativas";
            // 
            // numColunasAlternativas
            // 
            this.numColunasAlternativas.Location = new System.Drawing.Point(12, 221);
            this.numColunasAlternativas.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numColunasAlternativas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColunasAlternativas.Name = "numColunasAlternativas";
            this.numColunasAlternativas.Size = new System.Drawing.Size(120, 20);
            this.numColunasAlternativas.TabIndex = 7;
            this.numColunasAlternativas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FormPergunta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 299);
            this.Controls.Add(this.numColunasAlternativas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCodigoImportacao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cbxAlternativaObrigatoria);
            this.Controls.Add(this.gbxTipoAlternativa);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPergunta";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pergunta";
            this.gbxTipoAlternativa.ResumeLayout(false);
            this.gbxTipoAlternativa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColunasAlternativas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.GroupBox gbxTipoAlternativa;
        private System.Windows.Forms.RadioButton rbSelecaoUnica;
        private System.Windows.Forms.RadioButton rbMultiplaSelecao;
        private System.Windows.Forms.RadioButton rbDissertativa;
        private System.Windows.Forms.CheckBox cbxAlternativaObrigatoria;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigoImportacao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numColunasAlternativas;
    }
}