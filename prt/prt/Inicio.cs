using finalHerramientas1;
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
    public partial class Inicio : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;
        public Inicio()
        {
            InitializeComponent();
            this.biblioteca = new biblioteca(new List<biblioteca.material>(), new List<biblioteca.persona>(), new List<biblioteca.movimiento>());
            this.conexion = new conexionSQLcs();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var response = biblioteca.respuesta(campoNombre.Text);
            if (response == true)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else MessageBox.Show("Nombre no encontrado");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Personas persona = new Personas();
            persona.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
