namespace ClienteTatoo
{
    partial class FormDadosPessoaisCliente
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
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCpf = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDataNascimento = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.MaskedTextBox();
            this.txtCelular = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCep = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTipoLogradouro = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbCidade = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNome.Location = new System.Drawing.Point(12, 32);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(444, 26);
            this.txtNome.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "CPF";
            // 
            // txtCpf
            // 
            this.txtCpf.Location = new System.Drawing.Point(12, 94);
            this.txtCpf.Mask = "000\\.000\\.000-00";
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.PromptChar = ' ';
            this.txtCpf.Size = new System.Drawing.Size(120, 26);
            this.txtCpf.TabIndex = 2;
            this.txtCpf.Text = "99999999999";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(458, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data de Nascimento";
            // 
            // dtpDataNascimento
            // 
            this.dtpDataNascimento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDataNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataNascimento.Location = new System.Drawing.Point(462, 32);
            this.dtpDataNascimento.Name = "dtpDataNascimento";
            this.dtpDataNascimento.Size = new System.Drawing.Size(150, 26);
            this.dtpDataNascimento.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Telefone";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefone.Location = new System.Drawing.Point(344, 94);
            this.txtTelefone.Mask = "(00)\\ 0000-0000";
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.PromptChar = ' ';
            this.txtTelefone.Size = new System.Drawing.Size(131, 26);
            this.txtTelefone.TabIndex = 4;
            // 
            // txtCelular
            // 
            this.txtCelular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCelular.Location = new System.Drawing.Point(481, 94);
            this.txtCelular.Mask = "(00)\\ 0\\.0000-0000";
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.PromptChar = ' ';
            this.txtCelular.Size = new System.Drawing.Size(131, 26);
            this.txtCelular.TabIndex = 5;
            this.txtCelular.Text = "99999999999";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(481, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Celular";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "E-mail";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(138, 94);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 26);
            this.txtEmail.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "CEP";
            // 
            // txtCep
            // 
            this.txtCep.Location = new System.Drawing.Point(12, 153);
            this.txtCep.Mask = "00000-000";
            this.txtCep.Name = "txtCep";
            this.txtCep.PromptChar = ' ';
            this.txtCep.Size = new System.Drawing.Size(84, 26);
            this.txtCep.TabIndex = 6;
            this.txtCep.Text = "99999999";
            this.txtCep.TextChanged += new System.EventHandler(this.txtCep_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Logradouro";
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogradouro.Location = new System.Drawing.Point(254, 218);
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Size = new System.Drawing.Size(358, 26);
            this.txtLogradouro.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Tipo Logradouro";
            // 
            // cmbTipoLogradouro
            // 
            this.cmbTipoLogradouro.FormattingEnabled = true;
            this.cmbTipoLogradouro.Location = new System.Drawing.Point(12, 218);
            this.cmbTipoLogradouro.Name = "cmbTipoLogradouro";
            this.cmbTipoLogradouro.Size = new System.Drawing.Size(236, 28);
            this.cmbTipoLogradouro.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(508, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumero.Location = new System.Drawing.Point(512, 285);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 26);
            this.txtNumero.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 262);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 20);
            this.label11.TabIndex = 20;
            this.label11.Text = "Complemento";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(12, 285);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(173, 26);
            this.txtComplemento.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(187, 262);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 22;
            this.label12.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBairro.Location = new System.Drawing.Point(191, 285);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(315, 26);
            this.txtBairro.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(97, 132);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 20);
            this.label13.TabIndex = 24;
            this.label13.Text = "Estado";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(101, 153);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(258, 28);
            this.cmbEstado.TabIndex = 7;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(361, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 20);
            this.label14.TabIndex = 26;
            this.label14.Text = "Cidade";
            // 
            // cmbCidade
            // 
            this.cmbCidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCidade.FormattingEnabled = true;
            this.cmbCidade.Location = new System.Drawing.Point(365, 153);
            this.cmbCidade.Name = "cmbCidade";
            this.cmbCidade.Size = new System.Drawing.Size(247, 28);
            this.cmbCidade.TabIndex = 8;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(537, 345);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 35);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "Avançar";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbort.Location = new System.Drawing.Point(12, 345);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 35);
            this.btnAbort.TabIndex = 15;
            this.btnAbort.Text = "Voltar";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Visible = false;
            // 
            // FormDadosPessoaisCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 392);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbCidade);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtComplemento);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbTipoLogradouro);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtLogradouro);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCep);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCelular);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDataNascimento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDadosPessoaisCliente";
            this.Text = "Dados Pessoais";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDadosPessoaisCliente_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtCpf;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDataNascimento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtTelefone;
        private System.Windows.Forms.MaskedTextBox txtCelular;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txtCep;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbTipoLogradouro;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbCidade;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnAbort;
    }
}