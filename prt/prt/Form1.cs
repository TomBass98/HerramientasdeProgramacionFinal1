using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inicio inicio = new Inicio();
            inicio.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Personas personas = new Personas();
            personas.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegistrarMaterial registrar = new RegistrarMaterial();
            registrar.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Prestamos prestamo = new Prestamos();
            prestamo.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IncrementarMaterial incrementar = new IncrementarMaterial();
            incrementar.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Historial historial = new Historial();
            historial.Show();
            this.Hide();
        }
    }
}
