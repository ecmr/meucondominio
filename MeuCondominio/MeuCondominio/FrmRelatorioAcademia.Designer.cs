
namespace MeuCondominio
{
    partial class FrmRelatorioAcademia
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.txtCartao = new System.Windows.Forms.TextBox();
            this.btnCarregarCartao = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEvento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCarregarEventos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 89);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(776, 349);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // txtCartao
            // 
            this.txtCartao.Location = new System.Drawing.Point(12, 26);
            this.txtCartao.Name = "txtCartao";
            this.txtCartao.Size = new System.Drawing.Size(563, 20);
            this.txtCartao.TabIndex = 1;
            // 
            // btnCarregarCartao
            // 
            this.btnCarregarCartao.Location = new System.Drawing.Point(581, 26);
            this.btnCarregarCartao.Name = "btnCarregarCartao";
            this.btnCarregarCartao.Size = new System.Drawing.Size(100, 23);
            this.btnCarregarCartao.TabIndex = 2;
            this.btnCarregarCartao.Text = "Carregar arquivos...";
            this.btnCarregarCartao.UseVisualStyleBackColor = true;
            this.btnCarregarCartao.Click += new System.EventHandler(this.btnCarregarCartao_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cartão";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEvento
            // 
            this.txtEvento.Location = new System.Drawing.Point(15, 65);
            this.txtEvento.Name = "txtEvento";
            this.txtEvento.Size = new System.Drawing.Size(560, 20);
            this.txtEvento.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Eventos";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCarregarEventos
            // 
            this.btnCarregarEventos.Location = new System.Drawing.Point(581, 60);
            this.btnCarregarEventos.Name = "btnCarregarEventos";
            this.btnCarregarEventos.Size = new System.Drawing.Size(100, 23);
            this.btnCarregarEventos.TabIndex = 6;
            this.btnCarregarEventos.Text = "Carregar...";
            this.btnCarregarEventos.UseVisualStyleBackColor = true;
            this.btnCarregarEventos.Click += new System.EventHandler(this.btnCarregarEventos_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(688, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 61);
            this.button1.TabIndex = 7;
            this.button1.Text = "Executar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmRelatorioAcademia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCarregarEventos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEvento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCarregarCartao);
            this.Controls.Add(this.txtCartao);
            this.Controls.Add(this.listView1);
            this.Name = "FrmRelatorioAcademia";
            this.Text = "FrmRelatorioAcademia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox txtCartao;
        private System.Windows.Forms.Button btnCarregarCartao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEvento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCarregarEventos;
        private System.Windows.Forms.Button button1;
    }
}