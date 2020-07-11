namespace ClienteTatoo
{
    partial class FormTermoResponsabilidade
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
            this.rtbTermoResponsabilidade = new System.Windows.Forms.RichTextBox();
            this.cbxConcordo = new System.Windows.Forms.CheckBox();
            this.btnAvancar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbTermoResponsabilidade
            // 
            this.rtbTermoResponsabilidade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTermoResponsabilidade.Location = new System.Drawing.Point(12, 12);
            this.rtbTermoResponsabilidade.Name = "rtbTermoResponsabilidade";
            this.rtbTermoResponsabilidade.ReadOnly = true;
            this.rtbTermoResponsabilidade.Size = new System.Drawing.Size(776, 392);
            this.rtbTermoResponsabilidade.TabIndex = 0;
            this.rtbTermoResponsabilidade.TabStop = false;
            this.rtbTermoResponsabilidade.Text = "";
            // 
            // cbxConcordo
            // 
            this.cbxConcordo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxConcordo.AutoSize = true;
            this.cbxConcordo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxConcordo.Location = new System.Drawing.Point(12, 413);
            this.cbxConcordo.Name = "cbxConcordo";
            this.cbxConcordo.Size = new System.Drawing.Size(407, 24);
            this.cbxConcordo.TabIndex = 0;
            this.cbxConcordo.Text = "Li e concordo com o termo de responsabilidade acima";
            this.cbxConcordo.UseVisualStyleBackColor = true;
            this.cbxConcordo.CheckedChanged += new System.EventHandler(this.cbxConcordo_CheckedChanged);
            // 
            // btnAvancar
            // 
            this.btnAvancar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAvancar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAvancar.Enabled = false;
            this.btnAvancar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvancar.Location = new System.Drawing.Point(713, 410);
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.Size = new System.Drawing.Size(75, 28);
            this.btnAvancar.TabIndex = 1;
            this.btnAvancar.Text = "Avançar";
            this.btnAvancar.UseVisualStyleBackColor = true;
            // 
            // FormTermoResponsabilidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAvancar);
            this.Controls.Add(this.cbxConcordo);
            this.Controls.Add(this.rtbTermoResponsabilidade);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTermoResponsabilidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Termo de Responsabilidade";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTermoResponsabilidade;
        private System.Windows.Forms.CheckBox cbxConcordo;
        private System.Windows.Forms.Button btnAvancar;
    }
}