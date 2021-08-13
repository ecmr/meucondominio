
using System;

namespace MeuCondominio
{
    partial class FrmGestaoSedex
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCelular = new System.Windows.Forms.MaskedTextBox();
            this.ckbEntregue = new System.Windows.Forms.CheckBox();
            this.cboApto = new System.Windows.Forms.ComboBox();
            this.cboBloco = new System.Windows.Forms.ComboBox();
            this.listViewMoradoresApto = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMsgMorador = new System.Windows.Forms.Label();
            this.txtNomeMoraador = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrateleira = new System.Windows.Forms.TextBox();
            this.txtQrcode = new System.Windows.Forms.TextBox();
            this.txtCodBarras = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEtiquetaLocal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.incluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cnsultarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reciboDeEntregaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.cknTodos = new System.Windows.Forms.CheckBox();
            this.ckbMail = new System.Windows.Forms.CheckBox();
            this.ckbZap = new System.Windows.Forms.CheckBox();
            this.ckbSms = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCelular);
            this.groupBox1.Controls.Add(this.ckbEntregue);
            this.groupBox1.Controls.Add(this.cboApto);
            this.groupBox1.Controls.Add(this.cboBloco);
            this.groupBox1.Controls.Add(this.listViewMoradoresApto);
            this.groupBox1.Controls.Add(this.lblMsgMorador);
            this.groupBox1.Controls.Add(this.txtNomeMoraador);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(20, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 370);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registrar";
            // 
            // txtCelular
            // 
            this.txtCelular.Location = new System.Drawing.Point(9, 231);
            this.txtCelular.Mask = "(99) 00000-0000";
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(86, 20);
            this.txtCelular.TabIndex = 5;
            this.txtCelular.Leave += new System.EventHandler(this.ValidarCelular);
            // 
            // ckbEntregue
            // 
            this.ckbEntregue.AutoSize = true;
            this.ckbEntregue.Location = new System.Drawing.Point(372, 60);
            this.ckbEntregue.Name = "ckbEntregue";
            this.ckbEntregue.Size = new System.Drawing.Size(69, 17);
            this.ckbEntregue.TabIndex = 14;
            this.ckbEntregue.Text = "Entregue";
            this.ckbEntregue.UseVisualStyleBackColor = true;
            // 
            // cboApto
            // 
            this.cboApto.FormattingEnabled = true;
            this.cboApto.Location = new System.Drawing.Point(117, 35);
            this.cboApto.MaxLength = 3;
            this.cboApto.Name = "cboApto";
            this.cboApto.Size = new System.Drawing.Size(104, 21);
            this.cboApto.TabIndex = 2;
            this.cboApto.SelectedIndexChanged += new System.EventHandler(this.cboApto_SelectedIndexChanged);
            this.cboApto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CarregaNomes);
            // 
            // cboBloco
            // 
            this.cboBloco.FormattingEnabled = true;
            this.cboBloco.Location = new System.Drawing.Point(9, 35);
            this.cboBloco.MaxLength = 2;
            this.cboBloco.Name = "cboBloco";
            this.cboBloco.Size = new System.Drawing.Size(104, 21);
            this.cboBloco.TabIndex = 1;
            this.cboBloco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboBlocoKeyPress);
            // 
            // listViewMoradoresApto
            // 
            this.listViewMoradoresApto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewMoradoresApto.HideSelection = false;
            this.listViewMoradoresApto.Location = new System.Drawing.Point(9, 60);
            this.listViewMoradoresApto.Name = "listViewMoradoresApto";
            this.listViewMoradoresApto.Size = new System.Drawing.Size(338, 121);
            this.listViewMoradoresApto.TabIndex = 3;
            this.listViewMoradoresApto.UseCompatibleStateImageBehavior = false;
            this.listViewMoradoresApto.View = System.Windows.Forms.View.List;
            this.listViewMoradoresApto.SelectedIndexChanged += new System.EventHandler(this.listViewMoradoresApto_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome Morador";
            this.columnHeader1.Width = 300;
            // 
            // lblMsgMorador
            // 
            this.lblMsgMorador.AutoSize = true;
            this.lblMsgMorador.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMsgMorador.Location = new System.Drawing.Point(9, 292);
            this.lblMsgMorador.Name = "lblMsgMorador";
            this.lblMsgMorador.Size = new System.Drawing.Size(280, 25);
            this.lblMsgMorador.TabIndex = 16;
            this.lblMsgMorador.Text = "labelMensagemEnvioMorador";
            this.lblMsgMorador.Visible = false;
            // 
            // txtNomeMoraador
            // 
            this.txtNomeMoraador.Location = new System.Drawing.Point(9, 198);
            this.txtNomeMoraador.Name = "txtNomeMoraador";
            this.txtNomeMoraador.Size = new System.Drawing.Size(477, 20);
            this.txtNomeMoraador.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Apartamento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bloco";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Destinatario";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(9, 270);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(477, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "E-mail";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Celular";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(521, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "QRCode";
            // 
            // txtPrateleira
            // 
            this.txtPrateleira.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtPrateleira.Location = new System.Drawing.Point(520, 189);
            this.txtPrateleira.Multiline = true;
            this.txtPrateleira.Name = "txtPrateleira";
            this.txtPrateleira.Size = new System.Drawing.Size(103, 31);
            this.txtPrateleira.TabIndex = 10;
            // 
            // txtQrcode
            // 
            this.txtQrcode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtQrcode.Location = new System.Drawing.Point(521, 94);
            this.txtQrcode.Multiline = true;
            this.txtQrcode.Name = "txtQrcode";
            this.txtQrcode.Size = new System.Drawing.Size(281, 39);
            this.txtQrcode.TabIndex = 8;
            // 
            // txtCodBarras
            // 
            this.txtCodBarras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtCodBarras.Location = new System.Drawing.Point(521, 49);
            this.txtCodBarras.Name = "txtCodBarras";
            this.txtCodBarras.Size = new System.Drawing.Size(281, 29);
            this.txtCodBarras.TabIndex = 7;
            this.txtCodBarras.TextChanged += new System.EventHandler(this.txtCodBarras_TextChanged);
            this.txtCodBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VerificarBanco);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(521, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "CodigoBarras";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(521, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Prateleira";
            // 
            // txtEtiquetaLocal
            // 
            this.txtEtiquetaLocal.Location = new System.Drawing.Point(520, 151);
            this.txtEtiquetaLocal.Name = "txtEtiquetaLocal";
            this.txtEtiquetaLocal.Size = new System.Drawing.Size(280, 20);
            this.txtEtiquetaLocal.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(521, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Etiqueta Local";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incluirToolStripMenuItem,
            this.cnsultarToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.carregarExcelToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(845, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // incluirToolStripMenuItem
            // 
            this.incluirToolStripMenuItem.Name = "incluirToolStripMenuItem";
            this.incluirToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.incluirToolStripMenuItem.Text = "Incluir";
            // 
            // cnsultarToolStripMenuItem
            // 
            this.cnsultarToolStripMenuItem.Name = "cnsultarToolStripMenuItem";
            this.cnsultarToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.cnsultarToolStripMenuItem.Text = "Consultar";
            this.cnsultarToolStripMenuItem.Click += new System.EventHandler(this.cnsultarToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvarToolStripMenuItem_Click);
            // 
            // carregarExcelToolStripMenuItem
            // 
            this.carregarExcelToolStripMenuItem.Name = "carregarExcelToolStripMenuItem";
            this.carregarExcelToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.carregarExcelToolStripMenuItem.Text = "Carregar Excel";
            this.carregarExcelToolStripMenuItem.Click += new System.EventHandler(this.carregarExcelToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reciboDeEntregaToolStripMenuItem});
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // reciboDeEntregaToolStripMenuItem
            // 
            this.reciboDeEntregaToolStripMenuItem.Name = "reciboDeEntregaToolStripMenuItem";
            this.reciboDeEntregaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reciboDeEntregaToolStripMenuItem.Text = "Recibo de entrega";
            this.reciboDeEntregaToolStripMenuItem.Click += new System.EventHandler(this.reciboDeEntregaToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(645, 189);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Alimento";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(714, 189);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(90, 17);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "Medicamento";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEnviar);
            this.groupBox2.Controls.Add(this.cknTodos);
            this.groupBox2.Controls.Add(this.ckbMail);
            this.groupBox2.Controls.Add(this.ckbZap);
            this.groupBox2.Controls.Add(this.ckbSms);
            this.groupBox2.Location = new System.Drawing.Point(523, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 87);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ENVIO";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(159, 55);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(83, 23);
            this.btnEnviar.TabIndex = 13;
            this.btnEnviar.Text = "SALVAR";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // cknTodos
            // 
            this.cknTodos.AutoSize = true;
            this.cknTodos.Location = new System.Drawing.Point(36, 55);
            this.cknTodos.Name = "cknTodos";
            this.cknTodos.Size = new System.Drawing.Size(56, 17);
            this.cknTodos.TabIndex = 14;
            this.cknTodos.Text = "Todos";
            this.cknTodos.UseVisualStyleBackColor = true;
            // 
            // ckbMail
            // 
            this.ckbMail.AutoSize = true;
            this.ckbMail.Location = new System.Drawing.Point(191, 22);
            this.ckbMail.Name = "ckbMail";
            this.ckbMail.Size = new System.Drawing.Size(55, 17);
            this.ckbMail.TabIndex = 13;
            this.ckbMail.Text = "E-Mail";
            this.ckbMail.UseVisualStyleBackColor = true;
            // 
            // ckbZap
            // 
            this.ckbZap.AutoSize = true;
            this.ckbZap.Location = new System.Drawing.Point(102, 22);
            this.ckbZap.Name = "ckbZap";
            this.ckbZap.Size = new System.Drawing.Size(76, 17);
            this.ckbZap.TabIndex = 12;
            this.ckbZap.Text = "WhatsApp";
            this.ckbZap.UseVisualStyleBackColor = true;
            // 
            // ckbSms
            // 
            this.ckbSms.AutoSize = true;
            this.ckbSms.Location = new System.Drawing.Point(36, 22);
            this.ckbSms.Name = "ckbSms";
            this.ckbSms.Size = new System.Drawing.Size(49, 17);
            this.ckbSms.TabIndex = 11;
            this.ckbSms.Text = "SMS";
            this.ckbSms.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.LimparMensagem);
            // 
            // FrmGestaoSedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 413);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEtiquetaLocal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCodBarras);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtQrcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrateleira);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmGestaoSedex";
            this.Text = "Gestão de Sedex";
            this.Load += new System.EventHandler(this.FrmGestaoSedex_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private EventHandler CboApto_GotFocus()
        {
            //throw new NotImplementedException();
            return null;
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNomeMoraador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrateleira;
        private System.Windows.Forms.TextBox txtQrcode;
        private System.Windows.Forms.TextBox txtCodBarras;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEtiquetaLocal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem incluirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cnsultarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
        private System.Windows.Forms.Label lblMsgMorador;
        private System.Windows.Forms.ToolStripMenuItem carregarExcelToolStripMenuItem;
        private System.Windows.Forms.ListView listViewMoradoresApto;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox cboApto;
        private System.Windows.Forms.ComboBox cboBloco;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.CheckBox cknTodos;
        private System.Windows.Forms.CheckBox ckbMail;
        private System.Windows.Forms.CheckBox ckbZap;
        private System.Windows.Forms.CheckBox ckbSms;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox ckbEntregue;
        private System.Windows.Forms.MaskedTextBox txtCelular;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reciboDeEntregaToolStripMenuItem;
    }
}

