using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeuCondominio
{
    public partial class FrmRelatorioAcademia : Form
    {
        public FrmRelatorioAcademia()
        {
            InitializeComponent();
        }

        private void btnCarregarCartao_Click(object sender, EventArgs e)
        {
            DialogResult = openFileDialog1.ShowDialog();
            if (DialogResult == DialogResult.OK)
                txtCartao.Text = openFileDialog1.FileName;
        }

        private void btnCarregarEventos_Click(object sender, EventArgs e)
        {
            DialogResult = openFileDialog1.ShowDialog();
            if (DialogResult == DialogResult.OK)
                txtEvento.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             string[] Cartoes = System.IO.File.ReadAllLines(txtCartao.Text);

            System.Console.WriteLine("Cartoes.txt = ");

            foreach (string linhaCartao in Cartoes)
            {
                var linhaArray = linhaCartao.Split('[');
                Console.WriteLine(string.Concat("\t", "Matrícula: ", int.Parse(linhaArray[2]).ToString(), " Nome:", linhaArray[18]));
            }

            string[] Eventos = System.IO.File.ReadAllLines(txtEvento.Text);

            System.Console.WriteLine("Cartoes.txt = ");
            foreach (string linhaEvento in Eventos)
            {
                var eventoArray = linhaEvento.Split('[');
                Console.WriteLine(string.Concat("\t", "Matrícula: ", int.Parse(eventoArray[2]).ToString(), " Horário: ", eventoArray[3]));
            }


        }
    }
}
