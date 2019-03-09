namespace ClienteTatoo
{
    partial class FormVersao
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
            this.lblSistema = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBancoDados = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBancoEndereco = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sistema:";
            // 
            // lblSistema
            // 
            this.lblSistema.AutoSize = true;
            this.lblSistema.Location = new System.Drawing.Point(65, 9);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(35, 13);
            this.lblSistema.TabIndex = 1;
            this.lblSistema.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Banco de dados:";
            // 
            // lblBancoDados
            // 
            this.lblBancoDados.AutoSize = true;
            this.lblBancoDados.Location = new System.Drawing.Point(106, 37);
            this.lblBancoDados.Name = "lblBancoDados";
            this.lblBancoDados.Size = new System.Drawing.Size(35, 13);
            this.lblBancoDados.TabIndex = 3;
            this.lblBancoDados.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Banco de endereços:";
            // 
            // lblBancoEndereco
            // 
            this.lblBancoEndereco.AutoSize = true;
            this.lblBancoEndereco.Location = new System.Drawing.Point(127, 63);
            this.lblBancoEndereco.Name = "lblBancoEndereco";
            this.lblBancoEndereco.Size = new System.Drawing.Size(35, 13);
            this.lblBancoEndereco.TabIndex = 5;
            this.lblBancoEndereco.Text = "label6";
            // 
            // FormVersao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 90);
            this.Controls.Add(this.lblBancoEndereco);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblBancoDados);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSistema);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVersao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Versão";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSistema;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBancoDados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBancoEndereco;
    }
}