using MeuCondominio.Bus;
using MeuCondominio.Model;
using System;
using System.Collections.Generic;
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
            SedexBus sedexBus = new SedexBus();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (!string.IsNullOrEmpty(txtCartao.Text))
                {
                    string[] Cartoes = System.IO.File.ReadAllLines(txtCartao.Text);

                    System.Console.WriteLine("Cartoes.txt = ");

                    label5.Text = "Carregando matrículas";
                    label5.Refresh();

                    foreach (string linhaCartao in Cartoes)
                    {
                        var linhaArray = linhaCartao.Split('[');
                        label6.Text = (string.Concat("\t", "Matrícula: ", int.Parse(linhaArray[2]).ToString(), " Nome:", linhaArray[18]));
                        //Console.WriteLine(string.Concat("\t", "Matrícula: ", int.Parse(linhaArray[2]).ToString(), " Nome:", linhaArray[18], " Bloco-Apto:", linhaArray[18].Substring(linhaArray[18].Length-5,2), "-", linhaArray[18].Substring(linhaArray[18].Length - 3, 3)));
                        label6.Refresh();
                        Academia academia = new Academia() { Matricula = int.Parse(linhaArray[2]).ToString(), Nome = linhaArray[18] };
                        var retorno = sedexBus.AdionarCadastroMorador(academia);
                    }
                }

                if (!string.IsNullOrEmpty(txtEvento.Text))
                {
                    string[] Eventos = System.IO.File.ReadAllLines(txtEvento.Text);

                    label5.Text = "Carregando Eventos";
                    label5.Refresh();

                    foreach (string linhaEvento in Eventos)
                    {
                        var eventoArray = linhaEvento.Split('[');
                        label6.Text = (string.Concat("\t", int.Parse(eventoArray[2]).ToString(), " Horário: ", eventoArray[3]));
                        label6.Refresh();
                        Catraca evento = new Catraca() { Matricula = int.Parse(eventoArray[1]).ToString(), Registro = DateTime.Parse(eventoArray[2]) };
                        var retorno = sedexBus.AdionarEvento(evento);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void FrmRelatorioAcademia_Load(object sender, EventArgs e)
        {
            dTPickerEvento.Text = DateTime.Now.ToShortDateString();
        }


        /// <summary>
        /// Carregar Lista para preencher listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                listView1.Items.Clear();
                listView1.Refresh();
                SedexBus sedexBus = new SedexBus();
                Catraca evento = new Catraca();

                if (!string.IsNullOrEmpty(txtMatricula.Text))
                    evento.Matricula = txtMatricula.Text.TrimEnd();
                if (dTPickerEvento.Text != "01/01/2000")
                    evento.Registro = dTPickerEvento.Value;


                List<AcademiaEvento> eventos = sedexBus.RetornarEventos(evento);

                if (eventos.Count < 1)
                {
                    MessageBox.Show("Nenhum registro encontrado!");
                    return;
                }

                foreach (AcademiaEvento _evento in eventos)
                {
                    ListViewItem item = new ListViewItem(_evento.Bloco);
                    item.SubItems.Add(_evento.Apartamento.ToString());
                    item.SubItems.Add(_evento.Nome.ToString());
                    item.SubItems.Add(_evento.Matricula.ToString());
                    item.SubItems.Add(_evento.Registro.ToString());
                    listView1.Items.Add(item);
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
            }


        }
    }
}
