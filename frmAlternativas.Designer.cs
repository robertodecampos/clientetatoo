namespace ClienteTatoo
{
    partial class FormAlternativas
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
            this.lsvAlternativas = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAtivarDesativar = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnSubPerguntas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvAlternativas
            // 
            this.lsvAlternativas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsvAlternativas.FullRowSelect = true;
            this.lsvAlternativas.Location = new System.Drawing.Point(12, 12);
            this.lsvAlternativas.Name = "lsvAlternativas";
            this.lsvAlternativas.Size = new System.Drawing.Size(559, 231);
            this.lsvAlternativas.TabIndex = 0;
            this.lsvAlternativas.UseCompatibleStateImageBehavior = false;
            this.lsvAlternativas.View = System.Windows.Forms.View.Details;
            this.lsvAlternativas.SelectedIndexChanged += new System.EventHandler(this.lsvAlternativas_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Código";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Descrição";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Especificar";
            this.columnHeader3.Width = 64;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ativada";
            this.columnHeader4.Width = 48;
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemover.Location = new System.Drawing.Point(12, 249);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(75, 23);
            this.btnRemover.TabIndex = 1;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnAtivarDesativar
            // 
            this.btnAtivarDesativar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAtivarDesativar.Location = new System.Drawing.Point(93, 249);
            this.btnAtivarDesativar.Name = "btnAtivarDesativar";
            this.btnAtivarDesativar.Size = new System.Drawing.Size(75, 23);
            this.btnAtivarDesativar.TabIndex = 2;
            this.btnAtivarDesativar.Text = "(Des)Ativar";
            this.btnAtivarDesativar.UseVisualStyleBackColor = true;
            this.btnAtivarDesativar.Click += new System.EventHandler(this.btnAtivarDesativar_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlterar.Location = new System.Drawing.Point(174, 249);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(75, 23);
            this.btnAlterar.TabIndex = 3;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionar.Location = new System.Drawing.Point(496, 249);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 4;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnSubPerguntas
            // 
            this.btnSubPerguntas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubPerguntas.Location = new System.Drawing.Point(255, 249);
            this.btnSubPerguntas.Name = "btnSubPerguntas";
            this.btnSubPerguntas.Size = new System.Drawing.Size(90, 23);
            this.btnSubPerguntas.TabIndex = 5;
            this.btnSubPerguntas.Text = "Sub-Perguntas";
            this.btnSubPerguntas.UseVisualStyleBackColor = true;
            this.btnSubPerguntas.Click += new System.EventHandler(this.btnSubPerguntas_Click);
            // 
            // FormAlternativas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 284);
            this.Controls.Add(this.btnSubPerguntas);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnAtivarDesativar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.lsvAlternativas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlternativas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alternativas";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnAtivarDesativar;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.ListView lsvAlternativas;
        private System.Windows.Forms.Button btnSubPerguntas;
    }
}