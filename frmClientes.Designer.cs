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
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 458);
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
    }
}

