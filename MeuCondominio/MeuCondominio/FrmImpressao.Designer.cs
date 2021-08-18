
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.MoradorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboImpressora = new System.Windows.Forms.ComboBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.lblImprimir = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chkVisualizaImpressao = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MoradorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MoradorBindingSource
            // 
            this.MoradorBindingSource.DataSource = typeof(MeuCondominio.Model.Morador);
            // 
            // cboImpressora
            // 
            this.cboImpressora.FormattingEnabled = true;
            this.cboImpressora.Location = new System.Drawing.Point(25, 30);
            this.cboImpressora.Name = "cboImpressora";
            this.cboImpressora.Size = new System.Drawing.Size(229, 21);
            this.cboImpressora.TabIndex = 0;
            this.cboImpressora.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(105, 57);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(86, 38);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // lblImprimir
            // 
            this.lblImprimir.AutoSize = true;
            this.lblImprimir.Location = new System.Drawing.Point(25, 11);
            this.lblImprimir.Name = "lblImprimir";
            this.lblImprimir.Size = new System.Drawing.Size(58, 13);
            this.lblImprimir.TabIndex = 2;
            this.lblImprimir.Text = "Impressora";
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.MoradorBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp1.Relatorio.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 391);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(487, 52);
            this.reportViewer1.TabIndex = 3;
            this.reportViewer1.Visible = false;
            // 
            // chkVisualizaImpressao
            // 
            this.chkVisualizaImpressao.AutoSize = true;
            this.chkVisualizaImpressao.Location = new System.Drawing.Point(207, 69);
            this.chkVisualizaImpressao.Name = "chkVisualizaImpressao";
            this.chkVisualizaImpressao.Size = new System.Drawing.Size(70, 17);
            this.chkVisualizaImpressao.TabIndex = 4;
            this.chkVisualizaImpressao.Text = "Visualizar";
            this.chkVisualizaImpressao.UseVisualStyleBackColor = true;
            // 
            // FrmImpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 455);
            this.Controls.Add(this.chkVisualizaImpressao);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.lblImprimir);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.cboImpressora);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmImpressao";
            this.Text = "Impressora";
            this.Load += new System.EventHandler(this.FrmImpressao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MoradorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboImpressora;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label lblImprimir;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource MoradorBindingSource;
        private System.Windows.Forms.CheckBox chkVisualizaImpressao;
    }
}