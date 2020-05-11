using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using AppBD.Clases;
using System.Collections;
using SpreadsheetLight;

namespace AppBD
{
    public partial class Form3 : Form
    {
        public DataSet TABLA;
        Conexion con = new Conexion();
        Prestamo prestamo = new Prestamo();
        Contrato contrato = new Contrato();
        ContratisContrato contricon = new ContratisContrato();
        Contratista contratista = new Contratista();
        string ruta;
        List<ExtraerExcel> ls;
        private string patch ;
        public Form3(string ruta,string excontra)
        {
            this.patch = @excontra;
            this.ruta = ruta;
            contratista.con.ruta = this.ruta;
            contrato.con.ruta = this.ruta;
            prestamo.con.ruta = this.ruta;
            contricon.con.ruta = this.ruta;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Enabled = false;
                button5.Enabled = false;
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                groupBox2.Enabled = false;
                groupBox3.Visible = false;

                contrato.consultarC(textBox1.Text);
                textBox17.Text = contrato.objeto;
                textBox22.Text = contrato.codigo;
                textBox25.Text = contrato.ano;
                textBox19.Text = contrato.carpetasNO;
                textBox24.Text = contrato.plazodias;
                
                textBox23.Text = contrato.interventr;
                textBox18.Text = contrato.ubicacion;
                textBox27.Text = contrato.sedno;
                textBox2.Text = contrato.numeFol;




                prestamo.consultarP(textBox1.Text);
                prestamo.TABLA = new DataSet();
                prestamo.ORDEN.Fill(prestamo.TABLA, "Prestamo");
                dataGridView2.DataSource = prestamo.TABLA;
                dataGridView2.DataMember = "Prestamo";


                contratista.consultarContratisa(textBox1.Text);
                textBox20.Text = contratista.nombre;
                textBox21.Text = contratista.id;
            }
            catch (Exception) { MessageBox.Show("Ingrese valoes correctos", "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            groupBox2.Enabled = true;
            dataGridView1.Enabled = true;
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;


            con.conectar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
            button5.Enabled = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;


            con.conectar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           

          
            foreach (DataGridViewRow dgvRenglon in dataGridView1.Rows)
            {
                int indice = dgvRenglon.Index;

                    try
                    {



                    try
                    {

                        contratista.consutaexistencia(ls[indice].identifContraits);
                        contrato.consultarC(ls[indice].cod);

                        bool exixtencia = true;

                        string cc = contratista.id;
                        ArrayList nombres = contratista.nombres;

                        if (contrato.cod == ls[indice].cod)
                        {
                            MessageBox.Show("El número" + contrato.cod + " de contrato ya exixte" + "\n" +
                                "por favor verifique el numero o quite el cotrato del excel");
                            break;
                        }
                        else
                        {
                            if (cc == ls[indice].identifContraits)
                            {
                                for (int i = 0; i < nombres.Count; i++)
                                {
                                    if (nombres[i].ToString() == ls[indice].nombrecontratis)
                                    {

                                        contratista.consultarID(ls[indice].nombrecontratis,
                                                                double.Parse(ls[indice].identifContraits));
                                        contrato.addC(ls[indice]);

                                        contricon.addConContri(double.Parse(contratista.cod),
                                                               ls[indice].cod);

                                        MessageBox.Show("Se Agregó un nuevo contrato");
                                        break;

                                    }
                                    else
                                    {
                                        exixtencia = false;
                                    }
                                }
                            }
                            else { exixtencia = false; }
                            if (exixtencia == false)
                            {
                                contrato.addC(ls[indice]);
                                contratista.obetenerUltimoID();
                                contratista.agregarContratista(double.Parse(ls[indice].identifContraits), ls[indice].nombrecontratis);
                                contricon.addConContri(double.Parse(contratista.cod),
                                                                ls[indice].cod);
                                MessageBox.Show("Se Agregó un nuevo contrato");



                            }
                        }


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Verifique que no esten modificando propiedades de la base o que no hayan movido en archcivo de lugar");

                    }




                   
                }

                catch (NullReferenceException)
                {
                    MessageBox.Show("campo vacio en la fila " + indice.ToString());
                }///////PROCESO DEGUARADOD temp



            }///////////////////////////////////////////////

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            button5.Enabled = false;

            MessageBoxButtons sino = MessageBoxButtons.YesNo;
            DialogResult accion = MessageBox.Show("¿Quiere realizar acción?", "", sino, MessageBoxIcon.Question);
            if (accion == DialogResult.Yes)
            {
                contrato.updateC(textBox16.Text, textBox15.Text, textBox14.Text);
                textBox14.Enabled = true;
                MessageBox.Show("Se ha actualizado correctamente");
            }
            else
            {
            }
            try
            {
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que no esten modificando propiedades de la base o que no hayan movido en archcivo de lugar");
            }
            textBox1.Text = "";
      
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                contrato.consultarC(textBox14.Text);

                contrato.TABLA = new DataSet();
                contrato.ORDEN.Fill(contrato.TABLA, "CONTRATO");
                dataGridView3.DataSource = contrato.TABLA;
                dataGridView3.DataMember = "CONTRATO";
                if (contrato.cod == textBox14.Text)
                {
                    textBox14.Enabled = false;
                    button5.Enabled = true;
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que no esten modificando propiedades de la base o que no hayan movido en archcivo de lugar");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            textBox17.Multiline = true;

            textBox17.ScrollBars = ScrollBars.Vertical;

            textBox17.AcceptsReturn = true;

            textBox17.AcceptsTab = true;

            textBox17.WordWrap = true;
            textBox17.Size = new Size(new Point(250, 46));

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
        

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int fila = 2;
             ls = new List<ExtraerExcel>();
            try { 
            SLDocument sl = new SLDocument(patch);
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(fila,1))) {
                ExtraerExcel ex = new ExtraerExcel();
                ex.cod = sl.GetCellValueAsString(fila,1);
                ex.codigo = sl.GetCellValueAsString(fila, 2);
                ex.ano = sl.GetCellValueAsInt32(fila, 3);
                ex.ubicacion =sl.GetCellValueAsString(fila, 4);
                ex.carpetasNO = sl.GetCellValueAsString(fila, 5);
                ex.sedno = sl.GetCellValueAsString(fila, 6);
                ex.numeFol = sl.GetCellValueAsString(fila, 7);
                ex.objeto = sl.GetCellValueAsString(fila, 8);
                ex.plazodias = sl.GetCellValueAsInt32(fila, 9);
                ex.contraVal = sl.GetCellValueAsDouble(fila, 10);
                ex.interventr = sl.GetCellValueAsString(fila, 11);
                ex.identifContraits = sl.GetCellValueAsString(fila, 12);
                ex.nombrecontratis = sl.GetCellValueAsString(fila, 13);

                fila ++;
                ls.Add(ex);
            }
            dataGridView1.DataSource = ls;
            }
            catch (Exception)
            {
                MessageBox.Show("valide datos o verifique que el documento no se encuentre abierto");
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
