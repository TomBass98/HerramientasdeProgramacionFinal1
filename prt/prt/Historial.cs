using finalHerramientas1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prt
{
    public partial class Historial : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;
        public Historial()
        {
            InitializeComponent();
            this.biblioteca = new biblioteca(new List<biblioteca.material>(), new List<biblioteca.persona>(), new List<biblioteca.movimiento>());
            this.conexion = new conexionSQLcs();
            CargarHistorial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void CargarHistorial()
        {
            listView1.Items.Clear();

            string query = "SELECT * FROM historial"; // Consulta para obtener todos los registros del historial
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand command = new SqlCommand(query, conexion.ObtenerConexion()))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["isbn"].ToString()); // Primera columna (isbn)
                        item.SubItems.Add(reader["nombre"].ToString());
                        item.SubItems.Add(reader["fechaCreacion"].ToString());
                        item.SubItems.Add(reader["cantidadRegistrada"].ToString());
                        item.SubItems.Add(reader["cantidadActual"].ToString());

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
