namespace ClienteTatoo
{
    partial class FormClientes
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.msPricipal = new System.Windows.Forms.MenuStrip();
            this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.termoDeResponsabilidadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCadastrar = new System.Windows.Forms.Button();
            this.lsvClientes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.msPricipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // msPricipal
            // 
            this.msPricipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurarToolStripMenuItem});
            this.msPricipal.Location = new System.Drawing.Point(0, 0);
            this.msPricipal.Name = "msPricipal";
            this.msPricipal.Size = new System.Drawing.Size(807, 24);
            this.msPricipal.TabIndex = 0;
            this.msPricipal.Text = "menuStrip1";
            // 
            // configurarToolStripMenuItem
            // 
            this.configurarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.termoDeResponsabilidadeToolStripMenuItem});
            this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
            this.configurarToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.configurarToolStripMenuItem.Text = "Configurar";
            // 
            // termoDeResponsabilidadeToolStripMenuItem
            // 
            this.termoDeResponsabilidadeToolStripMenuItem.Name = "termoDeResponsabilidadeToolStripMenuItem";
            this.termoDeResponsabilidadeToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.termoDeResponsabilidadeToolStripMenuItem.Text = "Termo de Responsabilidade";
            this.termoDeResponsabilidadeToolStripMenuItem.Click += new System.EventHandler(this.termoDeResponsabilidadeToolStripMenuItem_Click);
            // 
            // txtCadastrar
            // 
            this.txtCadastrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCadastrar.Location = new System.Drawing.Point(693, 423);
            this.txtCadastrar.Name = "txtCadastrar";
            this.txtCadastrar.Size = new System.Drawing.Size(102, 23);
            this.txtCadastrar.TabIndex = 1;
            this.txtCadastrar.Text = "Cadastrar Cliente";
            this.txtCadastrar.UseVisualStyleBackColor = true;
            this.txtCadastrar.Click += new System.EventHandler(this.txtCadastrar_Click);
            // 
            // lsvClientes
            // 
            this.lsvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvClientes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lsvClientes.FullRowSelect = true;
            this.lsvClientes.Location = new System.Drawing.Point(12, 27);
            this.lsvClientes.Name = "lsvClientes";
            this.lsvClientes.Size = new System.Drawing.Size(783, 390);
            this.lsvClientes.TabIndex = 2;
            this.lsvClientes.UseCompatibleStateImageBehavior = false;
            this.lsvClientes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Código";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nome";
            this.columnHeader2.Width = 40;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Data de Nascimento";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "CPF";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Telefone";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Celular";
            this.columnHeader6.Width = 120;
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 458);
            this.Controls.Add(this.lsvClientes);
            this.Controls.Add(this.txtCadastrar);
            this.Controls.Add(this.msPricipal);
            this.MainMenuStrip = this.msPricipal;
            this.Name = "FormClientes";
            this.Text = "Clientes";
            this.msPricipal.ResumeLayout(false);
            this.msPricipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msPricipal;
        private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem termoDeResponsabilidadeToolStripMenuItem;
        private System.Windows.Forms.Button txtCadastrar;
        private System.Windows.Forms.ListView lsvClientes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}

