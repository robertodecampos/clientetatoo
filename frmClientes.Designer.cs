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
            this.gerenciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuáriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.termoDeResponsabilidadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perguntasEAlternativasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tatuagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.lsvClientes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOrdenacao = new System.Windows.Forms.ComboBox();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnTatuagens = new System.Windows.Forms.Button();
            this.btnVisualizarPesquisa = new System.Windows.Forms.Button();
            this.msPricipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // msPricipal
            // 
            this.msPricipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerenciarToolStripMenuItem,
            this.configurarToolStripMenuItem,
            this.informaçõesToolStripMenuItem});
            this.msPricipal.Location = new System.Drawing.Point(0, 0);
            this.msPricipal.Name = "msPricipal";
            this.msPricipal.Size = new System.Drawing.Size(807, 24);
            this.msPricipal.TabIndex = 0;
            this.msPricipal.Text = "menuStrip1";
            // 
            // gerenciarToolStripMenuItem
            // 
            this.gerenciarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuáriosToolStripMenuItem,
            this.importarClientesToolStripMenuItem});
            this.gerenciarToolStripMenuItem.Name = "gerenciarToolStripMenuItem";
            this.gerenciarToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.gerenciarToolStripMenuItem.Text = "Gerenciar";
            // 
            // usuáriosToolStripMenuItem
            // 
            this.usuáriosToolStripMenuItem.Name = "usuáriosToolStripMenuItem";
            this.usuáriosToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.usuáriosToolStripMenuItem.Text = "Usuários";
            this.usuáriosToolStripMenuItem.Click += new System.EventHandler(this.usuáriosToolStripMenuItem_Click);
            // 
            // importarClientesToolStripMenuItem
            // 
            this.importarClientesToolStripMenuItem.Name = "importarClientesToolStripMenuItem";
            this.importarClientesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.importarClientesToolStripMenuItem.Text = "Importar Clientes";
            this.importarClientesToolStripMenuItem.Click += new System.EventHandler(this.importarClientesToolStripMenuItem_Click);
            // 
            // configurarToolStripMenuItem
            // 
            this.configurarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.termoDeResponsabilidadeToolStripMenuItem,
            this.perguntasEAlternativasToolStripMenuItem});
            this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
            this.configurarToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.configurarToolStripMenuItem.Text = "Configurar";
            // 
            // termoDeResponsabilidadeToolStripMenuItem
            // 
            this.termoDeResponsabilidadeToolStripMenuItem.Name = "termoDeResponsabilidadeToolStripMenuItem";
            this.termoDeResponsabilidadeToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.termoDeResponsabilidadeToolStripMenuItem.Text = "Termo de Responsabilidade";
            this.termoDeResponsabilidadeToolStripMenuItem.Click += new System.EventHandler(this.termoDeResponsabilidadeToolStripMenuItem_Click);
            // 
            // perguntasEAlternativasToolStripMenuItem
            // 
            this.perguntasEAlternativasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.tatuagensToolStripMenuItem});
            this.perguntasEAlternativasToolStripMenuItem.Name = "perguntasEAlternativasToolStripMenuItem";
            this.perguntasEAlternativasToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.perguntasEAlternativasToolStripMenuItem.Text = "Perguntas e Alternativas";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // tatuagensToolStripMenuItem
            // 
            this.tatuagensToolStripMenuItem.Name = "tatuagensToolStripMenuItem";
            this.tatuagensToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.tatuagensToolStripMenuItem.Text = "Tatuagens";
            this.tatuagensToolStripMenuItem.Click += new System.EventHandler(this.tatuagensToolStripMenuItem_Click);
            // 
            // informaçõesToolStripMenuItem
            // 
            this.informaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versãoToolStripMenuItem});
            this.informaçõesToolStripMenuItem.Name = "informaçõesToolStripMenuItem";
            this.informaçõesToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.informaçõesToolStripMenuItem.Text = "Informações";
            // 
            // versãoToolStripMenuItem
            // 
            this.versãoToolStripMenuItem.Name = "versãoToolStripMenuItem";
            this.versãoToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.versãoToolStripMenuItem.Text = "Versão";
            this.versãoToolStripMenuItem.Click += new System.EventHandler(this.versãoToolStripMenuItem_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadastrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrar.Location = new System.Drawing.Point(648, 411);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(147, 35);
            this.btnCadastrar.TabIndex = 1;
            this.btnCadastrar.Text = "Cadastrar Cliente";
            this.btnCadastrar.UseVisualStyleBackColor = true;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
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
            this.lsvClientes.HideSelection = false;
            this.lsvClientes.Location = new System.Drawing.Point(12, 56);
            this.lsvClientes.Name = "lsvClientes";
            this.lsvClientes.Size = new System.Drawing.Size(783, 348);
            this.lsvClientes.TabIndex = 2;
            this.lsvClientes.UseCompatibleStateImageBehavior = false;
            this.lsvClientes.View = System.Windows.Forms.View.Details;
            this.lsvClientes.SelectedIndexChanged += new System.EventHandler(this.lsvClientes_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Código";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nome";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Data de Nascimento";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "CPF";
            this.columnHeader4.Width = 87;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Telefone";
            this.columnHeader5.Width = 84;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Celular";
            this.columnHeader6.Width = 93;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiltrar.Location = new System.Drawing.Point(720, 27);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 3;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(527, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ordenação";
            // 
            // cmbOrdenacao
            // 
            this.cmbOrdenacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOrdenacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrdenacao.FormattingEnabled = true;
            this.cmbOrdenacao.Items.AddRange(new object[] {
            "Código",
            "Nome",
            "Data de Nascimento"});
            this.cmbOrdenacao.Location = new System.Drawing.Point(593, 29);
            this.cmbOrdenacao.Name = "cmbOrdenacao";
            this.cmbOrdenacao.Size = new System.Drawing.Size(121, 21);
            this.cmbOrdenacao.TabIndex = 5;
            this.cmbOrdenacao.SelectedIndexChanged += new System.EventHandler(this.cmbOrdenacao_SelectedIndexChanged);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.Location = new System.Drawing.Point(105, 410);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(68, 35);
            this.btnAlterar.TabIndex = 6;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Visible = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.Location = new System.Drawing.Point(12, 411);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(87, 35);
            this.btnRemover.TabIndex = 7;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Visible = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnTatuagens
            // 
            this.btnTatuagens.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTatuagens.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTatuagens.Location = new System.Drawing.Point(347, 410);
            this.btnTatuagens.Name = "btnTatuagens";
            this.btnTatuagens.Size = new System.Drawing.Size(98, 35);
            this.btnTatuagens.TabIndex = 8;
            this.btnTatuagens.Text = "Tatuagens";
            this.btnTatuagens.UseVisualStyleBackColor = true;
            this.btnTatuagens.Visible = false;
            this.btnTatuagens.Click += new System.EventHandler(this.btnTatuagens_Click);
            // 
            // btnVisualizarPesquisa
            // 
            this.btnVisualizarPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVisualizarPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisualizarPesquisa.Location = new System.Drawing.Point(179, 410);
            this.btnVisualizarPesquisa.Name = "btnVisualizarPesquisa";
            this.btnVisualizarPesquisa.Size = new System.Drawing.Size(162, 35);
            this.btnVisualizarPesquisa.TabIndex = 10;
            this.btnVisualizarPesquisa.Text = "Visualizar Pesquisa";
            this.btnVisualizarPesquisa.UseVisualStyleBackColor = true;
            this.btnVisualizarPesquisa.Visible = false;
            this.btnVisualizarPesquisa.Click += new System.EventHandler(this.btnVisualizarPesquisa_Click);
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 458);
            this.Controls.Add(this.btnVisualizarPesquisa);
            this.Controls.Add(this.btnTatuagens);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.cmbOrdenacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.lsvClientes);
            this.Controls.Add(this.btnCadastrar);
            this.Controls.Add(this.msPricipal);
            this.MainMenuStrip = this.msPricipal;
            this.Name = "FormClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FormClientes_Resize);
            this.msPricipal.ResumeLayout(false);
            this.msPricipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msPricipal;
        private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem termoDeResponsabilidadeToolStripMenuItem;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.ListView lsvClientes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOrdenacao;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnTatuagens;
        private System.Windows.Forms.ToolStripMenuItem informaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perguntasEAlternativasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tatuagensToolStripMenuItem;
        private System.Windows.Forms.Button btnVisualizarPesquisa;
        private System.Windows.Forms.ToolStripMenuItem gerenciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuáriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarClientesToolStripMenuItem;
    }
}

