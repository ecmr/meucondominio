
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
            this.CRBloco = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRApto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRNome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRMatricula = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CREvento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtCartao = new System.Windows.Forms.TextBox();
            this.btnCarregarCartao = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEvento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCarregarEventos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.dTPickerEvento = new System.Windows.Forms.DateTimePicker();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CRBloco,
            this.CRApto,
            this.CRNome,
            this.CRMatricula,
            this.CREvento});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(225, 91);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(563, 349);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // CRBloco
            // 
            this.CRBloco.Text = "BLOCO";
            // 
            // CRApto
            // 
            this.CRApto.Text = "Apartamento";
            this.CRApto.Width = 80;
            // 
            // CRNome
            // 
            this.CRNome.Text = "NOME";
            this.CRNome.Width = 170;
            // 
            // CRMatricula
            // 
            this.CRMatricula.Text = "MATRICULA";
            this.CRMatricula.Width = 90;
            // 
            // CREvento
            // 
            this.CREvento.Text = "DATA-HORA";
            this.CREvento.Width = 140;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExecutar);
            this.groupBox1.Controls.Add(this.dTPickerEvento);
            this.groupBox1.Controls.Add(this.txtMatricula);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 149);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(114, 120);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 4;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // dTPickerEvento
            // 
            this.dTPickerEvento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPickerEvento.Location = new System.Drawing.Point(7, 84);
            this.dTPickerEvento.Name = "dTPickerEvento";
            this.dTPickerEvento.Size = new System.Drawing.Size(170, 20);
            this.dTPickerEvento.TabIndex = 3;
            // 
            // txtMatricula
            // 
            this.txtMatricula.Location = new System.Drawing.Point(13, 36);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(100, 20);
            this.txtMatricula.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Data-Hora";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Matricula";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "*";
            // 
            // FrmRelatorioAcademia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCarregarEventos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEvento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCarregarCartao);
            this.Controls.Add(this.txtCartao);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelatorioAcademia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRelatorioAcademia";
            this.Load += new System.EventHandler(this.FrmRelatorioAcademia_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ColumnHeader CRBloco;
        private System.Windows.Forms.ColumnHeader CRApto;
        private System.Windows.Forms.ColumnHeader CRNome;
        private System.Windows.Forms.ColumnHeader CRMatricula;
        private System.Windows.Forms.ColumnHeader CREvento;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.DateTimePicker dTPickerEvento;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}