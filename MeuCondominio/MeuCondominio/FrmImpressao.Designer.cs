
namespace MeuCondominio
{
    partial class FrmImpressao
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImpressao));
            this.cboImpressora = new System.Windows.Forms.ComboBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.lblImprimir = new System.Windows.Forms.Label();
            this.chkVisualizaImpressao = new System.Windows.Forms.CheckBox();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraFinal = new System.Windows.Forms.DateTimePicker();
            this.lblMsgMorador = new System.Windows.Forms.Label();
            this.MoradorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ckBPeriodo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MoradorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cboImpressora
            // 
            this.cboImpressora.FormattingEnabled = true;
            this.cboImpressora.Location = new System.Drawing.Point(-12, 3);
            this.cboImpressora.Name = "cboImpressora";
            this.cboImpressora.Size = new System.Drawing.Size(18, 21);
            this.cboImpressora.TabIndex = 0;
            this.cboImpressora.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(237, 184);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(93, 38);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // lblImprimir
            // 
            this.lblImprimir.AutoSize = true;
            this.lblImprimir.Location = new System.Drawing.Point(12, 3);
            this.lblImprimir.Name = "lblImprimir";
            this.lblImprimir.Size = new System.Drawing.Size(58, 13);
            this.lblImprimir.TabIndex = 2;
            this.lblImprimir.Text = "Impressora";
            this.lblImprimir.Visible = false;
            // 
            // chkVisualizaImpressao
            // 
            this.chkVisualizaImpressao.AutoSize = true;
            this.chkVisualizaImpressao.Location = new System.Drawing.Point(260, 146);
            this.chkVisualizaImpressao.Name = "chkVisualizaImpressao";
            this.chkVisualizaImpressao.Size = new System.Drawing.Size(70, 17);
            this.chkVisualizaImpressao.TabIndex = 4;
            this.chkVisualizaImpressao.Text = "Visualizar";
            this.chkVisualizaImpressao.UseVisualStyleBackColor = true;
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataInicial.Location = new System.Drawing.Point(122, 85);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(101, 20);
            this.dtpDataInicial.TabIndex = 5;
            // 
            // dtpHoraInicial
            // 
            this.dtpHoraInicial.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicial.Location = new System.Drawing.Point(229, 85);
            this.dtpHoraInicial.Name = "dtpHoraInicial";
            this.dtpHoraInicial.Size = new System.Drawing.Size(101, 20);
            this.dtpHoraInicial.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Período inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Período Final";
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(123, 120);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(100, 20);
            this.dtpDataFinal.TabIndex = 9;
            // 
            // dtpHoraFinal
            // 
            this.dtpHoraFinal.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFinal.Location = new System.Drawing.Point(230, 119);
            this.dtpHoraFinal.Name = "dtpHoraFinal";
            this.dtpHoraFinal.Size = new System.Drawing.Size(100, 20);
            this.dtpHoraFinal.TabIndex = 10;
            // 
            // lblMsgMorador
            // 
            this.lblMsgMorador.AutoSize = true;
            this.lblMsgMorador.Location = new System.Drawing.Point(46, 32);
            this.lblMsgMorador.Name = "lblMsgMorador";
            this.lblMsgMorador.Size = new System.Drawing.Size(0, 13);
            this.lblMsgMorador.TabIndex = 11;
            // 
            // MoradorBindingSource
            // 
            this.MoradorBindingSource.DataSource = typeof(MeuCondominio.Model.Morador);
            // 
            // ckBPeriodo
            // 
            this.ckBPeriodo.AutoSize = true;
            this.ckBPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckBPeriodo.Location = new System.Drawing.Point(49, 62);
            this.ckBPeriodo.Name = "ckBPeriodo";
            this.ckBPeriodo.Size = new System.Drawing.Size(139, 17);
            this.ckBPeriodo.TabIndex = 13;
            this.ckBPeriodo.Text = "Imprimir por período";
            this.ckBPeriodo.UseVisualStyleBackColor = true;
            // 
            // FrmImpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 234);
            this.Controls.Add(this.ckBPeriodo);
            this.Controls.Add(this.lblMsgMorador);
            this.Controls.Add(this.dtpHoraFinal);
            this.Controls.Add(this.dtpDataFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpHoraInicial);
            this.Controls.Add(this.dtpDataInicial);
            this.Controls.Add(this.chkVisualizaImpressao);
            this.Controls.Add(this.lblImprimir);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.cboImpressora);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImpressao";
            this.Text = "Impressora";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmImpressao_FormClosed);
            this.Load += new System.EventHandler(this.FrmImpressao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MoradorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboImpressora;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label lblImprimir;
        private System.Windows.Forms.BindingSource MoradorBindingSource;
        private System.Windows.Forms.CheckBox chkVisualizaImpressao;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.DateTimePicker dtpHoraInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpHoraFinal;
        private System.Windows.Forms.Label lblMsgMorador;
        private System.Windows.Forms.CheckBox ckBPeriodo;
    }
}