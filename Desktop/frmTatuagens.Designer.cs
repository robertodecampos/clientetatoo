﻿namespace ClienteTatoo
{
    partial class FormTatuagens
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            "",
            "99/99/9999",
            "99/99/9999"}, -1);
            this.lsvTatuagens = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnSessoes = new System.Windows.Forms.Button();
            this.btnVisualizarRespostas = new System.Windows.Forms.Button();
            this.btnVisualizarTermoResponsabilidade = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvTatuagens
            // 
            this.lsvTatuagens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvTatuagens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7});
            this.lsvTatuagens.FullRowSelect = true;
            this.lsvTatuagens.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lsvTatuagens.Location = new System.Drawing.Point(12, 12);
            this.lsvTatuagens.MultiSelect = false;
            this.lsvTatuagens.Name = "lsvTatuagens";
            this.lsvTatuagens.Size = new System.Drawing.Size(703, 341);
            this.lsvTatuagens.TabIndex = 0;
            this.lsvTatuagens.UseCompatibleStateImageBehavior = false;
            this.lsvTatuagens.View = System.Windows.Forms.View.Details;
            this.lsvTatuagens.SelectedIndexChanged += new System.EventHandler(this.lsvTatuagens_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Código";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Local";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Desenho";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Sessoes";
            this.columnHeader4.Width = 52;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Primeira";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Última";
            this.columnHeader7.Width = 70;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionar.Location = new System.Drawing.Point(640, 359);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 1;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlterar.Location = new System.Drawing.Point(12, 359);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(75, 23);
            this.btnAlterar.TabIndex = 2;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Visible = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnSessoes
            // 
            this.btnSessoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSessoes.Location = new System.Drawing.Point(93, 359);
            this.btnSessoes.Name = "btnSessoes";
            this.btnSessoes.Size = new System.Drawing.Size(75, 23);
            this.btnSessoes.TabIndex = 3;
            this.btnSessoes.Text = "Sessões";
            this.btnSessoes.UseVisualStyleBackColor = true;
            this.btnSessoes.Visible = false;
            this.btnSessoes.Click += new System.EventHandler(this.btnSessoes_Click);
            // 
            // btnVisualizarRespostas
            // 
            this.btnVisualizarRespostas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVisualizarRespostas.Location = new System.Drawing.Point(174, 359);
            this.btnVisualizarRespostas.Name = "btnVisualizarRespostas";
            this.btnVisualizarRespostas.Size = new System.Drawing.Size(115, 23);
            this.btnVisualizarRespostas.TabIndex = 4;
            this.btnVisualizarRespostas.Text = "Visualizar Respostas";
            this.btnVisualizarRespostas.UseVisualStyleBackColor = true;
            this.btnVisualizarRespostas.Visible = false;
            this.btnVisualizarRespostas.Click += new System.EventHandler(this.btnVisualizarRespostas_Click);
            // 
            // btnVisualizarTermoResponsabilidade
            // 
            this.btnVisualizarTermoResponsabilidade.Location = new System.Drawing.Point(295, 359);
            this.btnVisualizarTermoResponsabilidade.Name = "btnVisualizarTermoResponsabilidade";
            this.btnVisualizarTermoResponsabilidade.Size = new System.Drawing.Size(203, 23);
            this.btnVisualizarTermoResponsabilidade.TabIndex = 5;
            this.btnVisualizarTermoResponsabilidade.Text = "Visualizar Termo de Responsabilidade";
            this.btnVisualizarTermoResponsabilidade.UseVisualStyleBackColor = true;
            this.btnVisualizarTermoResponsabilidade.Visible = false;
            this.btnVisualizarTermoResponsabilidade.Click += new System.EventHandler(this.btnVisualizarTermoResponsabilidade_Click);
            // 
            // FormTatuagens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 394);
            this.Controls.Add(this.btnVisualizarTermoResponsabilidade);
            this.Controls.Add(this.btnVisualizarRespostas);
            this.Controls.Add(this.btnSessoes);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.lsvTatuagens);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "FormTatuagens";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tatuagens";
            this.Resize += new System.EventHandler(this.FormTatuagens_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvTatuagens;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnSessoes;
        private System.Windows.Forms.Button btnVisualizarRespostas;
        private System.Windows.Forms.Button btnVisualizarTermoResponsabilidade;
    }
}