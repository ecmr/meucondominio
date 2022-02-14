
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
            this.txtEmail1 = new System.Windows.Forms.TextBox();
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
            this.carregarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatórioAcademiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mensagemSMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reciboDeEntregaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testeTelegramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cknTodos = new System.Windows.Forms.CheckBox();
            this.btnEnviarSms = new System.Windows.Forms.Button();
            this.ckbMail = new System.Windows.Forms.CheckBox();
            this.ckbZap = new System.Windows.Forms.CheckBox();
            this.ckbSms = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnExcluir = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstHistorico = new System.Windows.Forms.ListView();
            this.Bloco = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Apartamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NomeMorador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EnviadoPara = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataSms = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CodigoBarrasProduto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdbDesenvolvedor = new System.Windows.Forms.RadioButton();
            this.rdbAdminstracao = new System.Windows.Forms.RadioButton();
            this.grpBoxConfigTelegram = new System.Windows.Forms.GroupBox();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.buttonSendCode = new System.Windows.Forms.Button();
            this.labelCode = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxApiHash = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxApiID = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpBoxConfigTelegram.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.txtEmail1);
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
            this.ckbEntregue.Location = new System.Drawing.Point(278, 35);
            this.ckbEntregue.Name = "ckbEntregue";
            this.ckbEntregue.Size = new System.Drawing.Size(69, 17);
            this.ckbEntregue.TabIndex = 14;
            this.ckbEntregue.Text = "Entregue";
            this.ckbEntregue.UseVisualStyleBackColor = true;
            this.ckbEntregue.Visible = false;
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
            this.cboBloco.SelectedIndexChanged += new System.EventHandler(this.cboBloco_SelectedIndexChanged);
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
            this.lblMsgMorador.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMsgMorador.Location = new System.Drawing.Point(7, 291);
            this.lblMsgMorador.Name = "lblMsgMorador";
            this.lblMsgMorador.Size = new System.Drawing.Size(479, 79);
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
            // txtEmail1
            // 
            this.txtEmail1.Location = new System.Drawing.Point(9, 270);
            this.txtEmail1.Name = "txtEmail1";
            this.txtEmail1.Size = new System.Drawing.Size(477, 20);
            this.txtEmail1.TabIndex = 6;
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
            this.label5.Location = new System.Drawing.Point(521, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "QRCode";
            // 
            // txtPrateleira
            // 
            this.txtPrateleira.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.txtPrateleira.Enabled = false;
            this.txtPrateleira.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtPrateleira.Location = new System.Drawing.Point(520, 179);
            this.txtPrateleira.Multiline = true;
            this.txtPrateleira.Name = "txtPrateleira";
            this.txtPrateleira.Size = new System.Drawing.Size(103, 31);
            this.txtPrateleira.TabIndex = 10;
            // 
            // txtQrcode
            // 
            this.txtQrcode.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtQrcode.Enabled = false;
            this.txtQrcode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtQrcode.Location = new System.Drawing.Point(521, 84);
            this.txtQrcode.Multiline = true;
            this.txtQrcode.Name = "txtQrcode";
            this.txtQrcode.Size = new System.Drawing.Size(281, 39);
            this.txtQrcode.TabIndex = 8;
            // 
            // txtCodBarras
            // 
            this.txtCodBarras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtCodBarras.Location = new System.Drawing.Point(521, 39);
            this.txtCodBarras.Name = "txtCodBarras";
            this.txtCodBarras.Size = new System.Drawing.Size(281, 29);
            this.txtCodBarras.TabIndex = 7;
            this.txtCodBarras.TextChanged += new System.EventHandler(this.txtCodBarras_TextChanged);
            this.txtCodBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VerificarBanco);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(521, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "CodigoBarras";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(521, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Prateleira";
            // 
            // txtEtiquetaLocal
            // 
            this.txtEtiquetaLocal.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.txtEtiquetaLocal.Enabled = false;
            this.txtEtiquetaLocal.Location = new System.Drawing.Point(520, 141);
            this.txtEtiquetaLocal.Name = "txtEtiquetaLocal";
            this.txtEtiquetaLocal.Size = new System.Drawing.Size(280, 20);
            this.txtEtiquetaLocal.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(521, 125);
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
            this.carregarExcelToolStripMenuItem,
            this.relatórioAcademiaToolStripMenuItem,
            this.configuraçõesToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.imprimirToolStripMenuItem,
            this.testeTelegramToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(829, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // incluirToolStripMenuItem
            // 
            this.incluirToolStripMenuItem.Name = "incluirToolStripMenuItem";
            this.incluirToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.incluirToolStripMenuItem.Text = "Incluir";
            this.incluirToolStripMenuItem.Visible = false;
            // 
            // cnsultarToolStripMenuItem
            // 
            this.cnsultarToolStripMenuItem.Name = "cnsultarToolStripMenuItem";
            this.cnsultarToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.cnsultarToolStripMenuItem.Text = "Consultar";
            this.cnsultarToolStripMenuItem.Visible = false;
            this.cnsultarToolStripMenuItem.Click += new System.EventHandler(this.cnsultarToolStripMenuItem_Click);
            // 
            // carregarExcelToolStripMenuItem
            // 
            this.carregarExcelToolStripMenuItem.Name = "carregarExcelToolStripMenuItem";
            this.carregarExcelToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.carregarExcelToolStripMenuItem.Text = "Carregar Excel";
            this.carregarExcelToolStripMenuItem.Click += new System.EventHandler(this.carregarExcelToolStripMenuItem_Click);
            // 
            // relatórioAcademiaToolStripMenuItem
            // 
            this.relatórioAcademiaToolStripMenuItem.Name = "relatórioAcademiaToolStripMenuItem";
            this.relatórioAcademiaToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.relatórioAcademiaToolStripMenuItem.Text = "Relatório Academia";
            this.relatórioAcademiaToolStripMenuItem.Click += new System.EventHandler(this.relatórioAcademiaToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mensagemSMSToolStripMenuItem});
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            // 
            // mensagemSMSToolStripMenuItem
            // 
            this.mensagemSMSToolStripMenuItem.Name = "mensagemSMSToolStripMenuItem";
            this.mensagemSMSToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.mensagemSMSToolStripMenuItem.Text = "Mensagem SMS";
            this.mensagemSMSToolStripMenuItem.Click += new System.EventHandler(this.mensagemSMSToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Visible = false;
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvarToolStripMenuItem_Click);
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
            this.reciboDeEntregaToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.reciboDeEntregaToolStripMenuItem.Text = "Recibo de entrega";
            this.reciboDeEntregaToolStripMenuItem.Visible = false;
            this.reciboDeEntregaToolStripMenuItem.Click += new System.EventHandler(this.reciboDeEntregaToolStripMenuItem_Click);
            // 
            // testeTelegramToolStripMenuItem
            // 
            this.testeTelegramToolStripMenuItem.Name = "testeTelegramToolStripMenuItem";
            this.testeTelegramToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.testeTelegramToolStripMenuItem.Text = "Teste Telegram";
            this.testeTelegramToolStripMenuItem.Visible = false;
            this.testeTelegramToolStripMenuItem.Click += new System.EventHandler(this.testeTelegramToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(645, 179);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Alimento";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(714, 179);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(90, 17);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.Text = "Medicamento";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cknTodos);
            this.groupBox2.Controls.Add(this.btnEnviarSms);
            this.groupBox2.Controls.Add(this.ckbMail);
            this.groupBox2.Controls.Add(this.ckbZap);
            this.groupBox2.Controls.Add(this.ckbSms);
            this.groupBox2.Location = new System.Drawing.Point(523, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 87);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ENVIO";
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
            this.cknTodos.Visible = false;
            // 
            // btnEnviarSms
            // 
            this.btnEnviarSms.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarSms.Location = new System.Drawing.Point(122, 45);
            this.btnEnviarSms.Name = "btnEnviarSms";
            this.btnEnviarSms.Size = new System.Drawing.Size(149, 32);
            this.btnEnviarSms.TabIndex = 26;
            this.btnEnviarSms.Text = "Enviar Mensagem";
            this.btnEnviarSms.UseVisualStyleBackColor = true;
            this.btnEnviarSms.Click += new System.EventHandler(this.btnEnviarSms_Click);
            // 
            // ckbMail
            // 
            this.ckbMail.AutoSize = true;
            this.ckbMail.Checked = true;
            this.ckbMail.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.ckbZap.Visible = false;
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
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Location = new System.Drawing.Point(696, 222);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(101, 36);
            this.btnSalvar.TabIndex = 13;
            this.btnSalvar.Text = "SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.LimparMensagem);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Location = new System.Drawing.Point(528, 222);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(95, 36);
            this.btnExcluir.TabIndex = 26;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstHistorico);
            this.groupBox3.Location = new System.Drawing.Point(20, 510);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(784, 218);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Histórico";
            // 
            // lstHistorico
            // 
            this.lstHistorico.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Bloco,
            this.Apartamento,
            this.NomeMorador,
            this.Email,
            this.EnviadoPara,
            this.DataSms,
            this.CodigoBarrasProduto});
            this.lstHistorico.HideSelection = false;
            this.lstHistorico.Location = new System.Drawing.Point(6, 20);
            this.lstHistorico.MultiSelect = false;
            this.lstHistorico.Name = "lstHistorico";
            this.lstHistorico.Size = new System.Drawing.Size(767, 193);
            this.lstHistorico.TabIndex = 0;
            this.lstHistorico.UseCompatibleStateImageBehavior = false;
            this.lstHistorico.View = System.Windows.Forms.View.Details;
            // 
            // Bloco
            // 
            this.Bloco.Text = "Bloco";
            this.Bloco.Width = 40;
            // 
            // Apartamento
            // 
            this.Apartamento.Text = "Apto";
            this.Apartamento.Width = 45;
            // 
            // NomeMorador
            // 
            this.NomeMorador.Text = "Morador";
            this.NomeMorador.Width = 150;
            // 
            // Email
            // 
            this.Email.Text = "E-Mail Enviado";
            this.Email.Width = 180;
            // 
            // EnviadoPara
            // 
            this.EnviadoPara.Text = "Núm. Enviado";
            this.EnviadoPara.Width = 90;
            // 
            // DataSms
            // 
            this.DataSms.Text = "Data Envio SMS";
            this.DataSms.Width = 120;
            // 
            // CodigoBarrasProduto
            // 
            this.CodigoBarrasProduto.Text = "Cód. Barras Produto";
            this.CodigoBarrasProduto.Width = 120;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdbDesenvolvedor);
            this.groupBox4.Controls.Add(this.rdbAdminstracao);
            this.groupBox4.Location = new System.Drawing.Point(528, 371);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(274, 33);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Chave Sedex";
            // 
            // rdbDesenvolvedor
            // 
            this.rdbDesenvolvedor.AutoSize = true;
            this.rdbDesenvolvedor.Location = new System.Drawing.Point(148, 11);
            this.rdbDesenvolvedor.Name = "rdbDesenvolvedor";
            this.rdbDesenvolvedor.Size = new System.Drawing.Size(97, 17);
            this.rdbDesenvolvedor.TabIndex = 1;
            this.rdbDesenvolvedor.Text = "Desenvolvedor";
            this.rdbDesenvolvedor.UseVisualStyleBackColor = true;
            // 
            // rdbAdminstracao
            // 
            this.rdbAdminstracao.AutoSize = true;
            this.rdbAdminstracao.Checked = true;
            this.rdbAdminstracao.Location = new System.Drawing.Point(51, 11);
            this.rdbAdminstracao.Name = "rdbAdminstracao";
            this.rdbAdminstracao.Size = new System.Drawing.Size(91, 17);
            this.rdbAdminstracao.TabIndex = 0;
            this.rdbAdminstracao.TabStop = true;
            this.rdbAdminstracao.Text = "Administração";
            this.rdbAdminstracao.UseVisualStyleBackColor = true;
            // 
            // grpBoxConfigTelegram
            // 
            this.grpBoxConfigTelegram.Controls.Add(this.textBoxPhone);
            this.grpBoxConfigTelegram.Controls.Add(this.buttonSendCode);
            this.grpBoxConfigTelegram.Controls.Add(this.labelCode);
            this.grpBoxConfigTelegram.Controls.Add(this.textBoxCode);
            this.grpBoxConfigTelegram.Controls.Add(this.label10);
            this.grpBoxConfigTelegram.Controls.Add(this.linkLabel);
            this.grpBoxConfigTelegram.Controls.Add(this.label11);
            this.grpBoxConfigTelegram.Controls.Add(this.textBoxApiHash);
            this.grpBoxConfigTelegram.Controls.Add(this.label12);
            this.grpBoxConfigTelegram.Controls.Add(this.textBoxApiID);
            this.grpBoxConfigTelegram.Controls.Add(this.buttonLogin);
            this.grpBoxConfigTelegram.Location = new System.Drawing.Point(22, 407);
            this.grpBoxConfigTelegram.Name = "grpBoxConfigTelegram";
            this.grpBoxConfigTelegram.Size = new System.Drawing.Size(778, 91);
            this.grpBoxConfigTelegram.TabIndex = 30;
            this.grpBoxConfigTelegram.TabStop = false;
            this.grpBoxConfigTelegram.Text = "Configurações Telegram";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MeuCondominio.Properties.Settings.Default, "phone_number", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPhone.Location = new System.Drawing.Point(186, 50);
            this.textBoxPhone.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(135, 19);
            this.textBoxPhone.TabIndex = 23;
            this.textBoxPhone.Text = global::MeuCondominio.Properties.Settings.Default.phone_number;
            // 
            // buttonSendCode
            // 
            this.buttonSendCode.Location = new System.Drawing.Point(674, 51);
            this.buttonSendCode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSendCode.Name = "buttonSendCode";
            this.buttonSendCode.Size = new System.Drawing.Size(61, 22);
            this.buttonSendCode.TabIndex = 22;
            this.buttonSendCode.Text = "Verificar";
            this.buttonSendCode.UseVisualStyleBackColor = true;
            this.buttonSendCode.Visible = false;
            this.buttonSendCode.Click += new System.EventHandler(this.buttonSendCode_Click);
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode.Location = new System.Drawing.Point(440, 50);
            this.labelCode.Margin = new System.Windows.Forms.Padding(0);
            this.labelCode.Name = "labelCode";
            this.labelCode.Padding = new System.Windows.Forms.Padding(2);
            this.labelCode.Size = new System.Drawing.Size(139, 17);
            this.labelCode.TabIndex = 21;
            this.labelCode.Text = "Código de verificação:";
            this.labelCode.Visible = false;
            // 
            // textBoxCode
            // 
            this.textBoxCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCode.Location = new System.Drawing.Point(581, 53);
            this.textBoxCode.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(76, 19);
            this.textBoxCode.TabIndex = 20;
            this.textBoxCode.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(21, 50);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(2);
            this.label10.Size = new System.Drawing.Size(163, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "Use o Número de telefone:";
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.LinkArea = new System.Windows.Forms.LinkArea(13, 28);
            this.linkLabel.Location = new System.Drawing.Point(443, 25);
            this.linkLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Padding = new System.Windows.Forms.Padding(2);
            this.linkLabel.Size = new System.Drawing.Size(214, 21);
            this.linkLabel.TabIndex = 18;
            this.linkLabel.TabStop = true;
            this.linkLabel.Tag = "https://my.telegram.org/apps";
            this.linkLabel.Text = "get these at https://my.telegram.org/apps";
            this.linkLabel.UseCompatibleTextRendering = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(154, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(2);
            this.label11.Size = new System.Drawing.Size(66, 17);
            this.label11.TabIndex = 17;
            this.label11.Text = "api_hash:";
            // 
            // textBoxApiHash
            // 
            this.textBoxApiHash.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MeuCondominio.Properties.Settings.Default, "api_hash", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxApiHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxApiHash.Location = new System.Drawing.Point(222, 24);
            this.textBoxApiHash.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxApiHash.Name = "textBoxApiHash";
            this.textBoxApiHash.Size = new System.Drawing.Size(211, 19);
            this.textBoxApiHash.TabIndex = 16;
            this.textBoxApiHash.Text = global::MeuCondominio.Properties.Settings.Default.api_hash;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 24);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(2);
            this.label12.Size = new System.Drawing.Size(49, 17);
            this.label12.TabIndex = 15;
            this.label12.Text = "api_id:";
            // 
            // textBoxApiID
            // 
            this.textBoxApiID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MeuCondominio.Properties.Settings.Default, "api_id", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxApiID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxApiID.Location = new System.Drawing.Point(71, 24);
            this.textBoxApiID.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxApiID.Name = "textBoxApiID";
            this.textBoxApiID.Size = new System.Drawing.Size(76, 19);
            this.textBoxApiID.TabIndex = 14;
            this.textBoxApiID.Text = global::MeuCondominio.Properties.Settings.Default.api_id;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(325, 50);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(84, 22);
            this.buttonLogin.TabIndex = 13;
            this.buttonLogin.Text = "Conectar/Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // FrmGestaoSedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 739);
            this.Controls.Add(this.grpBoxConfigTelegram);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnSalvar);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGestaoSedex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestão de Sedex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGestaoSedex_FormClosing);
            this.Load += new System.EventHandler(this.FrmGestaoSedex_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpBoxConfigTelegram.ResumeLayout(false);
            this.grpBoxConfigTelegram.PerformLayout();
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
        private System.Windows.Forms.TextBox txtEmail1;
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
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.CheckBox cknTodos;
        private System.Windows.Forms.CheckBox ckbMail;
        private System.Windows.Forms.CheckBox ckbZap;
        private System.Windows.Forms.CheckBox ckbSms;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox ckbEntregue;
        private System.Windows.Forms.MaskedTextBox txtCelular;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reciboDeEntregaToolStripMenuItem;
        private System.Windows.Forms.Button btnEnviarSms;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lstHistorico;
        private System.Windows.Forms.ColumnHeader DataSms;
        private System.Windows.Forms.ColumnHeader CodigoBarrasProduto;
        private System.Windows.Forms.ColumnHeader Bloco;
        private System.Windows.Forms.ColumnHeader Apartamento;
        private System.Windows.Forms.ColumnHeader NomeMorador;
        private System.Windows.Forms.ColumnHeader EnviadoPara;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdbDesenvolvedor;
        private System.Windows.Forms.RadioButton rdbAdminstracao;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mensagemSMSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testeTelegramToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader Email;
        private System.Windows.Forms.ToolStripMenuItem relatórioAcademiaToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpBoxConfigTelegram;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Button buttonSendCode;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxApiHash;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxApiID;
        private System.Windows.Forms.Button buttonLogin;
    }
}

