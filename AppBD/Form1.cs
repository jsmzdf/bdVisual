﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBD.Clases;

namespace AppBD
{
    public partial class Form1 : Form
    {
        public string ruta;
        public string rutaexCOn;
        public string rutaExpres;
        public string rutaExDev;

        public Form1(string con,string excontra,string excepres,string excedev)
        {

            this.ruta = con;
            this.rutaexCOn = excontra;
            this.rutaExDev = excedev;
            
            this.rutaExpres = excepres;
            

            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form prestamos = new Form4(ruta);
            prestamos.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // Application.EnableVisualStyles();
           
            
            Form prestamos= new Form2(ruta,rutaExpres);
            prestamos.ShowDialog();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form contrato = new Form3(ruta,rutaexCOn);
            contrato.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
