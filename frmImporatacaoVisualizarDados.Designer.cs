namespace ClienteTatoo
{
    partial class frmImporatacaoVisualizarDados
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
            this.lstDadosImportacao = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLinhasSucesso = new System.Windows.Forms.Label();
            this.lblLinhasProblema = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDadosImportacao
            // 
            this.lstDadosImportacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDadosImportacao.FullRowSelect = true;
            this.lstDadosImportacao.GridLines = true;
            this.lstDadosImportacao.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstDadosImportacao.Location = new System.Drawing.Point(0, 0);
            this.lstDadosImportacao.Name = "lstDadosImportacao";
            this.lstDadosImportacao.Size = new System.Drawing.Size(800, 450);
            this.lstDadosImportacao.TabIndex = 0;
            this.lstDadosImportacao.UseCompatibleStateImageBehavior = false;
            this.lstDadosImportacao.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLinhasProblema);
            this.panel1.Controls.Add(this.lblLinhasSucesso);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnConfirmar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 44);
            this.panel1.TabIndex = 1;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmar.Location = new System.Drawing.Point(713, 9);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 2;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Linhas que serão importadas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Linhas com problemas:";
            // 
            // lblLinhasSucesso
            // 
            this.lblLinhasSucesso.AutoSize = true;
            this.lblLinhasSucesso.Location = new System.Drawing.Point(163, 9);
            this.lblLinhasSucesso.Name = "lblLinhasSucesso";
            this.lblLinhasSucesso.Size = new System.Drawing.Size(13, 13);
            this.lblLinhasSucesso.TabIndex = 5;
            this.lblLinhasSucesso.Text = "0";
            // 
            // lblLinhasProblema
            // 
            this.lblLinhasProblema.AutoSize = true;
            this.lblLinhasProblema.Location = new System.Drawing.Point(133, 22);
            this.lblLinhasProblema.Name = "lblLinhasProblema";
            this.lblLinhasProblema.Size = new System.Drawing.Size(13, 13);
            this.lblLinhasProblema.TabIndex = 6;
            this.lblLinhasProblema.Text = "0";
            // 
            // frmImporatacaoVisualizarDados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lstDadosImportacao);
            this.MinimizeBox = false;
            this.Name = "frmImporatacaoVisualizarDados";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Imporatação - Visualizar Dados";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstDadosImportacao;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Label lblLinhasProblema;
        private System.Windows.Forms.Label lblLinhasSucesso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}