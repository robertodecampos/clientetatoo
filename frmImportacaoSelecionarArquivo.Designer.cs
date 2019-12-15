namespace ClienteTatoo
{
    partial class frmImportacaoSelecionarArquivo
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
            this.edtArquivo = new System.Windows.Forms.TextBox();
            this.btnProcurarArquivo = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.lblClientesCarregados = new System.Windows.Forms.Label();
            this.lblQtdeClientesCarregados = new System.Windows.Forms.Label();
            this.ofdArquivo = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arquivo";
            // 
            // edtArquivo
            // 
            this.edtArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edtArquivo.Location = new System.Drawing.Point(15, 25);
            this.edtArquivo.Name = "edtArquivo";
            this.edtArquivo.ReadOnly = true;
            this.edtArquivo.Size = new System.Drawing.Size(433, 20);
            this.edtArquivo.TabIndex = 1;
            // 
            // btnProcurarArquivo
            // 
            this.btnProcurarArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcurarArquivo.Location = new System.Drawing.Point(454, 23);
            this.btnProcurarArquivo.Name = "btnProcurarArquivo";
            this.btnProcurarArquivo.Size = new System.Drawing.Size(27, 23);
            this.btnProcurarArquivo.TabIndex = 2;
            this.btnProcurarArquivo.Text = "...";
            this.btnProcurarArquivo.UseVisualStyleBackColor = true;
            this.btnProcurarArquivo.Click += new System.EventHandler(this.btnProcurarArquivo_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportar.Location = new System.Drawing.Point(406, 103);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(75, 23);
            this.btnImportar.TabIndex = 3;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // lblClientesCarregados
            // 
            this.lblClientesCarregados.AutoSize = true;
            this.lblClientesCarregados.Location = new System.Drawing.Point(12, 65);
            this.lblClientesCarregados.Name = "lblClientesCarregados";
            this.lblClientesCarregados.Size = new System.Drawing.Size(103, 13);
            this.lblClientesCarregados.TabIndex = 4;
            this.lblClientesCarregados.Text = "Clientes carregados:";
            this.lblClientesCarregados.Visible = false;
            // 
            // lblQtdeClientesCarregados
            // 
            this.lblQtdeClientesCarregados.AutoSize = true;
            this.lblQtdeClientesCarregados.Location = new System.Drawing.Point(121, 65);
            this.lblQtdeClientesCarregados.Name = "lblQtdeClientesCarregados";
            this.lblQtdeClientesCarregados.Size = new System.Drawing.Size(13, 13);
            this.lblQtdeClientesCarregados.TabIndex = 5;
            this.lblQtdeClientesCarregados.Text = "0";
            this.lblQtdeClientesCarregados.Visible = false;
            // 
            // ofdArquivo
            // 
            this.ofdArquivo.Filter = "Arquivo Excel|*.xlsx";
            // 
            // frmImportacaoSelecionarArquivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 138);
            this.Controls.Add(this.lblQtdeClientesCarregados);
            this.Controls.Add(this.lblClientesCarregados);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnProcurarArquivo);
            this.Controls.Add(this.edtArquivo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportacaoSelecionarArquivo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importação - Selecionar Arquivo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtArquivo;
        private System.Windows.Forms.Button btnProcurarArquivo;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label lblClientesCarregados;
        private System.Windows.Forms.Label lblQtdeClientesCarregados;
        private System.Windows.Forms.OpenFileDialog ofdArquivo;
    }
}