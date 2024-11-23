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
    public partial class Prestamos : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;

        public Prestamos()
        {
            InitializeComponent();
            this.biblioteca = new biblioteca(new List<biblioteca.material>(), new List<biblioteca.persona>(), new List<biblioteca.movimiento>());
            this.conexion = new conexionSQLcs();


        }

        private void botonPrestamo_Click(object sender, EventArgs e)
        {
            var response = biblioteca.Validar(CCGlobal.Cedula.ToString());
            if (response == true)
            {
                int prestIsbn = int.Parse(campoISBN.Text);

                biblioteca.prestamo(prestIsbn);

                biblioteca.ConsultarFilaPorISBN(prestIsbn);
            }else MessageBox.Show("No tiene mas prestamos disponibles");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            var response = biblioteca.validarMenos(CCGlobal.Cedula.ToString());
            int prestIsbn = int.Parse(campoISBN.Text);
            biblioteca.Devolucion(prestIsbn);
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
