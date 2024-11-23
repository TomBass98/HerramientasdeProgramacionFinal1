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
    public partial class IncrementarMaterial : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;
        public IncrementarMaterial()
        {
            InitializeComponent();
            this.biblioteca = new biblioteca(new List<biblioteca.material>(), new List<biblioteca.persona>(), new List<biblioteca.movimiento>());
            this.conexion = new conexionSQLcs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cantAumentar = int.Parse(cantidadCampo.Text);
            int prestIsbn = int.Parse(ISBNcampo.Text);
            biblioteca.IncrementarMaterial(prestIsbn, cantAumentar);
            biblioteca.ConsultarFilaPorISBN(prestIsbn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
