
namespace MeuCondominio
{
    partial class FrmConfiguracoes
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCaracteres = new System.Windows.Forms.Label();
            this.txtMensagemSms = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblTextoFinal = new System.Windows.Forms.Label();
            this.txtFinal = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTextoFinal);
            this.groupBox1.Controls.Add(this.btnSalvar);
            this.groupBox1.Controls.Add(this.txtFinal);
            this.groupBox1.Controls.Add(this.lblCaracteres);
            this.groupBox1.Controls.Add(this.txtMensagemSms);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "texo para envio do SMS";
            // 
            // lblCaracteres
            // 
            this.lblCaracteres.AutoSize = true;
            this.lblCaracteres.Location = new System.Drawing.Point(192, 97);
            this.lblCaracteres.Name = "lblCaracteres";
            this.lblCaracteres.Size = new System.Drawing.Size(0, 13);
            this.lblCaracteres.TabIndex = 1;
            // 
            // txtMensagemSms
            // 
            this.txtMensagemSms.Location = new System.Drawing.Point(7, 20);
            this.txtMensagemSms.Multiline = true;
            this.txtMensagemSms.Name = "txtMensagemSms";
            this.txtMensagemSms.Size = new System.Drawing.Size(332, 59);
            this.txtMensagemSms.TabIndex = 0;
            this.txtMensagemSms.MultilineChanged += new System.EventHandler(this.PreenchimentoSms);
            this.txtMensagemSms.TextChanged += new System.EventHandler(this.PreenchimentoSms);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(264, 124);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 1;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblTextoFinal
            // 
            this.lblTextoFinal.AutoSize = true;
            this.lblTextoFinal.Location = new System.Drawing.Point(619, 124);
            this.lblTextoFinal.Name = "lblTextoFinal";
            this.lblTextoFinal.Size = new System.Drawing.Size(59, 13);
            this.lblTextoFinal.TabIndex = 2;
            this.lblTextoFinal.Text = "Texto Final";
            // 
            // txtFinal
            // 
            this.txtFinal.BackColor = System.Drawing.Color.AliceBlue;
            this.txtFinal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFinal.Enabled = false;
            this.txtFinal.Location = new System.Drawing.Point(345, 20);
            this.txtFinal.Multiline = true;
            this.txtFinal.Name = "txtFinal";
            this.txtFinal.Size = new System.Drawing.Size(333, 96);
            this.txtFinal.TabIndex = 3;
            // 
            // FrmConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 292);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfiguracoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConfiguracoes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmConfiguracoes_FormClosed);
            this.Load += new System.EventHandler(this.FrmConfiguracoes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCaracteres;
        private System.Windows.Forms.TextBox txtMensagemSms;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblTextoFinal;
        private System.Windows.Forms.TextBox txtFinal;
    }
}