using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBD.Clases;

namespace AppBD
{
    public partial class Form5 : Form
    {
        Prestamo presta = new Prestamo();
        public Contrasena contra = new Contrasena();
        int validar = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            contra.con.ruta = textBox3.Text;
            contra.consultarCrede(textBox1.Text);
            string pas1= contra.CLA;
            string pas2= contra.USU;
            if (pas1.Equals(textBox2.Text) && pas2.Equals(textBox1.Text)) {
                
                button1.Enabled = false;
                Form opciones = new Form1(textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text);
                
                opciones.ShowDialog();

            }
            else
            {
                MessageBox.Show("error de cédula o contraseña");
                textBox1.Text = "";
                textBox2.Text = "";
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            contra.con.ruta = textBox3.Text;
            contra.con.conectar();
            
            if (contra.con.result.Equals("ok")) {
                try {
                    presta.con.ruta = textBox3.Text;
                    presta.con.conectar();
                    presta.actualizarfechaactual();
                    validar++;
                } catch (Exception) {
                    MessageBox.Show("Verifique quesea el archivo correcto\n " +
                        "o que no este en modo edicion");
                }
                
                
                }
            else { MessageBox.Show("EL archivo De BD no existe"); }
            if (File.Exists(textBox4.Text))
            {
                validar++;
            }
            else
            {
                MessageBox.Show("Ubicación incorerecta de archivo Contrato");
            }
            if (File.Exists(textBox5.Text))
            {
                validar++;
            }
            else
            {
                MessageBox.Show("Ubicación incorerecta de archivo préstamo");
            }
            if (File.Exists(textBox4.Text))
            {
                validar++;
            }
            else
            {
                MessageBox.Show("Ubicación incorerecta de archivo Devolución");
            }
            if (validar == 4) {
                MessageBox.Show("Ubicación encontrada");
                button1.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
