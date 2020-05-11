using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBD.Clases;
using SpreadsheetLight;
namespace AppBD
{
    public partial class Form2 : Form
    {
        public DataSet TABLA;
        Conexion con = new Conexion();
        Prestamo prestamo = new Prestamo();
        Contrato contrato = new Contrato();
        PrestamoContrato presCon = new PrestamoContrato();
        List<ExtrarExcePres> ls;
        string ruta;
        private string patch;
        public Form2(string ruta,string expres)
        {
            this.patch = expres;
           
            this.ruta = ruta;
            contrato.con.ruta = this.ruta;
            prestamo.con.ruta = this.ruta;
            presCon.con.ruta = this.ruta;
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {


            foreach (DataGridViewRow dgvRenglon in dataGridView1.Rows)
            {
                
                int indice = dgvRenglon.Index;
               
                try
                {
                    contrato.consultarC(ls[indice].cod);
                    if (contrato.cod == ls[indice].cod)
                    {
                        prestamo.consultarP(ls[indice].cod);
                        Console.WriteLine("contratoexiste");
                        if (prestamo.estado == "PRESTADO")
                        {
                            MessageBox.Show("El contrato se encuentra prestado se puede hacer una devolución"+
                                "\n retírelo de la lista y vuelva a intentar");

                        }
                        else
                        {
                            Console.WriteLine("contrato se va a prestar");
                            prestamo.obetenerUltimoID();
                            Console.WriteLine("aeriguando id");
                            prestamo.generarPR(double.Parse(prestamo.id),
                                Convert.ToString(dgvRenglon.Cells[1].Value),
                                Convert.ToString(dgvRenglon.Cells[2].Value),
                                Convert.ToString(dgvRenglon.Cells[3].Value),
                                Convert.ToString(dgvRenglon.Cells[4].Value),
                                Convert.ToString(dgvRenglon.Cells[5].Value),
                                Convert.ToString(dgvRenglon.Cells[6].Value));
                            Console.WriteLine("guardando pres");
                            presCon.addConPRes(double.Parse(prestamo.id),
                                ls[indice].cod);
                            Console.WriteLine("guardandorela");

                            MessageBox.Show("Préstamo realizado");

                        }
                    }


               }
                catch (NullReferenceException)
                {
                    MessageBox.Show("campo vacio en la fila " + indice.ToString());
                }
            }





        }

        private void button8_Click(object sender, EventArgs e)
        {
            contrato.consultarC(textBox1.Text);
            try
            {
                prestamo.consultarP(textBox1.Text);
                if (contrato.cod == textBox1.Text && contrato.cod!="")
                {



                    button3.Enabled = true;
                    prestamo.TABLA = new DataSet();
                    prestamo.ORDEN.Fill(prestamo.TABLA, "Prestamo");
                    dataGridView2.DataSource = prestamo.TABLA;
                    dataGridView2.DataMember = "Prestamo";
                    if (prestamo.estado == "PRESTADO")
                    {
                        MessageBox.Show("El contrato se encuentra prestado se puede hacer una devolución");


                    }
                    else
                    {
                        MessageBox.Show("Puede prestar el contrato");

                    }
                }
                else
                {

                    MessageBox.Show("El Contrato no existe");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que no esten modificando propiedades de la base o que no hayan movido en archcivo de lugar");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewRow dgvRenglon in dataGridView3.Rows)
                {
                    int indice = dgvRenglon.Index;
                    try
                    {
                        contrato.consultarC(Convert.ToString(dgvRenglon.Cells[0].Value));
                        prestamo.consultarP(Convert.ToString(dgvRenglon.Cells[0].Value));
                        if (contrato.cod == Convert.ToString(dgvRenglon.Cells[0].Value) && contrato.cod != "")
                        {

                            if (prestamo.estado == "PRESTADO")
                            {
                                prestamo.updateP(double.Parse(prestamo.idRef));
                                dataGridView3.Rows.RemoveAt(dgvRenglon.Index);
                                MessageBox.Show("Se ha hecho la devolución");

                            }
                            else
                            {
                                MessageBox.Show("No esta prestado");
                                break;
                            }
                        }


                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("campo vacio en la fila " + indice.ToString());
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que no esten modificando propiedades de la base o que no hayan movido en archcivo de lugar");
            }



        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int fila = 2;
            ls = new List<ExtrarExcePres>();
          try
            {
                SLDocument sl = new SLDocument(@patch);
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(fila, 1)))
                {
                    ExtrarExcePres ex = new ExtrarExcePres();
                    ex.cod = sl.GetCellValueAsString(fila, 1);
                    ex.fusContratis = sl.GetCellValueAsString(fila, 2);
                    ex.dependencia = sl.GetCellValueAsString(fila, 3);
                    ex.quienprssta = sl.GetCellValueAsString(fila, 4);
                    ex.motivoPres = sl.GetCellValueAsString(fila, 5);
                    ex.numerocarp = sl.GetCellValueAsString(fila, 6);
                    ex.observa = sl.GetCellValueAsString(fila, 7);
                    

                    fila++;
                    ls.Add(ex);
                }
                dataGridView1.DataSource = ls;
            }
            catch (Exception)
            {
                MessageBox.Show("valide datos o verifique que el documento no se encuentre abierto");
            }
        }
    }
}
