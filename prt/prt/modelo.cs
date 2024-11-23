using prt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace finalHerramientas1
{

    public class biblioteca
    {

        private List<material> catalogo;
        private List<persona> registroPersonas;
        private List<movimiento> movimientos;
        private conexionSQLcs conexion;
        public biblioteca(List<material> catalogo, List<persona> registroPersonas, List<movimiento> movimientos)
        {
            this.Catalogo = catalogo;
            this.RegistroPersonas = registroPersonas;
            this.Movimientos = movimientos;
            this.conexion = new conexionSQLcs(); // Inicialización de la conexión
        }

        public List<material> Catalogo { get => catalogo; set => catalogo = value; }
        public List<persona> RegistroPersonas { get => registroPersonas; set => registroPersonas = value; }
        public List<movimiento> Movimientos { get => movimientos; set => movimientos = value; }


        public void registrarMaterial(material nuevoMaterial)
        {
            if (catalogo.Exists(m => m.ISBN == nuevoMaterial.ISBN))
            {
                MessageBox.Show("El material con este ISBN ya existe.");
                return;
            }

            catalogo.Add(nuevoMaterial);

            // Guardar en la base de datos
            string query = $"INSERT INTO material (iSBN, Nombre,  CantidadRegistrada, CantidadActual) " +
                           $"VALUES ({nuevoMaterial.ISBN}, '{nuevoMaterial.Nombre}', {nuevoMaterial.CantidadReg}, {nuevoMaterial.CantidadAct})";
            conexion.AbrirConexion();
            conexion.EjecutarConsulta(query);

            MessageBox.Show("Material registrado con éxito.");
            Historial();
            conexion.CerrarConexion();
        }

        public void CargarHistorial(System.Windows.Forms.ListView lvwHistorial)
        {
            lvwHistorial.Items.Clear();

            string query = "SELECT * FROM historial";

            try
            {
                // Abrir la conexión
                conexion.AbrirConexion();

                // Ejecutar consulta
                using (SqlCommand comando = new SqlCommand(query, conexion.ObtenerConexion()))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        // Crear un nuevo elemento para el ListView
                        System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem(lector["isbn"].ToString());

                        // Agregar los subelementos correspondientes
                        item.SubItems.Add(lector["nombre"].ToString());
                        item.SubItems.Add(Convert.ToDateTime(lector["fechaCreacion"]).ToString("yyyy-MM-dd HH:mm:ss"));
                        item.SubItems.Add(lector["cantidadRegistrada"].ToString());
                        item.SubItems.Add(lector["cantidadActual"].ToString());

                        // Añadir el elemento al ListView
                        lvwHistorial.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del historial: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }


        public bool respuesta(string nombre)
        {
            // Definimos la consulta
            string query = "SELECT cc FROM persona WHERE nombre = @nombre";

            try
            {
                // Abrir la conexión
                conexion.AbrirConexion();

                // Crear el comando con la consulta
                using (SqlCommand comando = new SqlCommand(query, conexion.ObtenerConexion()))
                {
                    // Agregar el parámetro para el nombre
                    comando.Parameters.AddWithValue("@nombre", nombre);

                    // Ejecutar la consulta y leer el resultado
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read()) // Si encuentra un registro
                        {
                            CCGlobal.Cedula = lector["cc"].ToString(); // Asignar a la variable global
                            return true; // El nombre existe
                        }
                        else
                        {
                            CCGlobal.Cedula = null; // Asegurarse de que esté vacía si no se encuentra
                            return false; // El nombre no existe
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                MessageBox.Show($"Error al validar el nombre: {ex.Message}");
                CCGlobal.Cedula = null; // Limpiar la variable global en caso de error
                return false;
            }
            finally
            {
                // Cerrar la conexión
                conexion.CerrarConexion();
            }
        }

        public void prestamo(int isbn)
        {
            // Verificar que el ISBN sea válido
            if (isbn <= 0)
            {
                MessageBox.Show("Por favor, ingrese un ISBN válido.");
                return;
            }


            string queryVerificar = "SELECT COUNT(*) FROM material WHERE iSBN = @isbn";


            string queryActualizar = "UPDATE material SET CantidadActual = CantidadActual - 1 WHERE iSBN = @isbn AND CantidadActual > 0";

            try
            {


                // Verificar si el material existe
                using (SqlCommand comandoVerificar = new SqlCommand(queryVerificar, conexion.ObtenerConexion()))
                {
                    comandoVerificar.Parameters.AddWithValue("@isbn", isbn);

                    conexion.AbrirConexion();
                    int cantidad = (int)comandoVerificar.ExecuteScalar(); // Devuelve la cantidad de registros encontrados

                    if (cantidad == 0) // Si no existe el material
                    {
                        MessageBox.Show("El material no existe en la base de datos.");
                        return;
                    }
                }

                // Actualizar la cantidad del material
                using (SqlCommand comandoActualizar = new SqlCommand(queryActualizar, conexion.ObtenerConexion()))
                {
                    comandoActualizar.Parameters.AddWithValue("@isbn", isbn);

                    int filasAfectadas = comandoActualizar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Material actualizado con éxito. Cantidad disminuida correctamente.");
                        Historial();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el material. Verifica la cantidad disponible.");
                    }
                }

            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al consultar o actualizar el material: {ex.Message}");
            }
        }

        public void Devolucion(int isbn)
        {
            // Verificar que el ISBN sea válido
            if (isbn <= 0)
            {
                MessageBox.Show("Por favor, ingrese un ISBN válido.");
                return;
            }


            string queryVerificar = "SELECT COUNT(*) FROM material WHERE iSBN = @isbn";


            string queryActualizar = "UPDATE material SET CantidadActual = CantidadActual + 1 WHERE iSBN = @isbn AND CantidadActual > 0";

            try
            {


                // Verificar si el material existe
                using (SqlCommand comandoVerificar = new SqlCommand(queryVerificar, conexion.ObtenerConexion()))
                {
                    comandoVerificar.Parameters.AddWithValue("@isbn", isbn);

                    conexion.AbrirConexion();
                    int cantidad = (int)comandoVerificar.ExecuteScalar(); // Devuelve la cantidad de registros encontrados

                    if (cantidad == 0) // Si no existe el material
                    {
                        MessageBox.Show("El material no existe en la base de datos.");
                        return;
                    }
                }

                // Actualizar la cantidad del material
                using (SqlCommand comandoActualizar = new SqlCommand(queryActualizar, conexion.ObtenerConexion()))
                {
                    comandoActualizar.Parameters.AddWithValue("@isbn", isbn);

                    int filasAfectadas = comandoActualizar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Material actualizado con éxito. Cantidad aumentada correctamente.");
                        Historial();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el material. Verifica la cantidad disponible.");
                    }
                }

            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al consultar o actualizar el material: {ex.Message}");
            }
        }

        public void Historial()
        {
            string queryInsertarHistorial = @"
        INSERT INTO historial (isbn, nombre, fechaCreacion, cantidadRegistrada, cantidadActual)
        SELECT isbn, nombre, fechaCreacion, cantidadRegistrada, cantidadActual
        FROM material";

            using (SqlCommand comandoHistorial = new SqlCommand(queryInsertarHistorial, conexion.ObtenerConexion()))
            {
                comandoHistorial.ExecuteNonQuery();
                conexion.CerrarConexion();

            }
        }

        public void IncrementarMaterial(int isbn, int cantidad)
        {

            if (isbn <= 0)
            {
                MessageBox.Show("Por favor, ingrese un ISBN válido.");
                return;
            }

            string queryVerificar = "SELECT COUNT(*) FROM material WHERE isbn = @isbn";
            string queryActualizar = "UPDATE material SET cantidadActual = cantidadActual + @cantidad, cantidadRegistrada = cantidadRegistrada + @cantidad WHERE isbn = @isbn";

            try
            {
                conexion.AbrirConexion();

                // Verificar si el material existe
                using (SqlCommand comandoVerificar = new SqlCommand(queryVerificar, conexion.ObtenerConexion()))
                {
                    comandoVerificar.Parameters.AddWithValue("@isbn", isbn);

                    int cantidadExistente = (int)comandoVerificar.ExecuteScalar();

                    if (cantidadExistente == 0)
                    {
                        MessageBox.Show("El material no existe en la base de datos.");
                        return;
                    }
                }

                // Actualizar la cantidad del material
                using (SqlCommand comandoActualizar = new SqlCommand(queryActualizar, conexion.ObtenerConexion()))
                {
                    comandoActualizar.Parameters.AddWithValue("@isbn", isbn);
                    comandoActualizar.Parameters.AddWithValue("@cantidad", cantidad);

                    int filasAfectadas = comandoActualizar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Material actualizado con éxito. Cantidad aumentada correctamente.");
                        Historial();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el material.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar o actualizar el material: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public bool validarMenos(string cedula)
        {
            // Definimos la consulta para obtener el valor actual de cantMaxPrestamo
            string querySelect = "SELECT cantMaxPrestamo FROM persona WHERE cc = @cedula";
            string queryUpdate = "UPDATE persona SET cantMaxPrestamo = cantMaxPrestamo + 1 WHERE cc = @cedula";

            try
            {
                // Abrir la conexión
                conexion.AbrirConexion();

                // Crear el comando para seleccionar el valor
                using (SqlCommand comandoSelect = new SqlCommand(querySelect, conexion.ObtenerConexion()))
                {
                    // Agregar el parámetro para la cédula
                    comandoSelect.Parameters.AddWithValue("@cedula", cedula);

                    // Ejecutar la consulta y leer el resultado
                    object resultado = comandoSelect.ExecuteScalar();

                    if (resultado != null && int.TryParse(resultado.ToString(), out int cantMaxPrestamo))
                    {
                        // Si el valor existe y es un número, incrementamos
                        // No es necesario validar si es mayor a 0 porque estamos sumando 1
                        using (SqlCommand comandoUpdate = new SqlCommand(queryUpdate, conexion.ObtenerConexion()))
                        {
                            // Agregar el parámetro para la cédula
                            comandoUpdate.Parameters.AddWithValue("@cedula", cedula);

                            // Ejecutar la actualización
                            comandoUpdate.ExecuteNonQuery();
                        }

                        return true; // Operación exitosa
                    }
                    else
                    {
                        MessageBox.Show("La cédula no existe en la base de datos o el valor de cantMaxPrestamo es inválido.");
                        return false; // Valor inválido o no encontrado
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                MessageBox.Show($"Error al actualizar el valor de cantMaxPrestamo: {ex.Message}");
                return false; // Retornar false en caso de error
            }
            finally
            {
                // Cerrar la conexión
                conexion.CerrarConexion();
            }
        }


        public bool Validar(string cedula)
        {
            // Definimos la consulta para obtener el valor actual de cantMaxPrestamo
            string querySelect = "SELECT cantMaxPrestamo FROM persona WHERE cc = @cedula";
            string queryUpdate = "UPDATE persona SET cantMaxPrestamo = cantMaxPrestamo - 1 WHERE cc = @cedula";

            try
            {
                // Abrir la conexión
                conexion.AbrirConexion();

                // Crear el comando para seleccionar el valor
                using (SqlCommand comandoSelect = new SqlCommand(querySelect, conexion.ObtenerConexion()))
                {
                    // Agregar el parámetro para la cédula
                    comandoSelect.Parameters.AddWithValue("@cedula", cedula);

                    // Ejecutar la consulta y leer el resultado
                    object resultado = comandoSelect.ExecuteScalar();

                    if (resultado != null && int.TryParse(resultado.ToString(), out int cantMaxPrestamo))
                    {
                        if (cantMaxPrestamo > 0) // Validar si el valor es mayor a 0
                        {
                            // Crear el comando para actualizar el valor
                            using (SqlCommand comandoUpdate = new SqlCommand(queryUpdate, conexion.ObtenerConexion()))
                            {
                                // Agregar el parámetro para la cédula
                                comandoUpdate.Parameters.AddWithValue("@cedula", cedula);

                                // Ejecutar la actualización
                                comandoUpdate.ExecuteNonQuery();
                            }

                            return true; // Operación exitosa
                        }
                        else
                        {
                            return false; // Valor es 0, no se modifica
                        }
                    }
                    else
                    {
                        MessageBox.Show("La cédula no existe en la base de datos o el valor de cantMaxPrestamo es inválido.");
                        return false; // Valor inválido o no encontrado
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                MessageBox.Show($"Error al actualizar el valor de cantMaxPrestamo: {ex.Message}");
                return false; // Retornar false en caso de error
            }
            finally
            {
                // Cerrar la conexión
                conexion.CerrarConexion();
            }
        }



        public void ConsultarFilaPorISBN(int isbn)
        {
            string query = "SELECT * FROM material WHERE isbn = @isbn";

            try
            {
                conexion.AbrirConexion();

                using (SqlCommand command = new SqlCommand(query, conexion.ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@isbn", isbn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string fechaCreacion = reader["fechaCreacion"].ToString();
                            string cantidadRegistrada = reader["cantidadRegistrada"].ToString();
                            string cantidadActual = reader["cantidadActual"].ToString();

                            string resultado = $"ISBN: {isbn}\nNombre: {nombre}\nFecha Creación: {fechaCreacion}\nCantidad Registrada: {cantidadRegistrada}\nCantidad Actual: {cantidadActual}";
                            MessageBox.Show(resultado, "Información del Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el material con el ISBN especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar la fila: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Incrementar Cantidad de Material
        public void incrementarCantidad(int isbn, int cantidad)
        {
            material mat = catalogo.FirstOrDefault(m => m.ISBN == isbn);
            if (mat == null)
            {
                MessageBox.Show("Material no encontrado.");
                return;
            }

            mat.CantidadAct += cantidad;

            // Actualizar en la base de datos
            string query = $"UPDATE Material SET CantidadAct = {mat.CantidadAct} WHERE ISBN = {isbn}";
            conexion.EjecutarConsulta(query);

            MessageBox.Show("Cantidad incrementada con éxito.");
            Historial();
        }

        // Registrar Persona
        public void registrarPersona(persona nuevaPersona)
        {
            if (registroPersonas.Exists(p => p.Cc == nuevaPersona.Cc))
            {
                MessageBox.Show("Ya existe una persona registrada con esta cédula.");
                return;
            }

            registroPersonas.Add(nuevaPersona);

            // Guardar en la base de datos
            string query = $"INSERT INTO Persona (CC, Nombre, Rol, CantMaxPrestamo) " +
                           $"VALUES ({nuevaPersona.Cc}, '{nuevaPersona.Nombre}', '{nuevaPersona.Rol}', {nuevaPersona.CantMaxPrestamo})";

            conexion.EjecutarConsulta(query);

            MessageBox.Show("Persona registrada con éxito.");
        }


        public void eliminarPersona(int cc)
        {
            persona per = consultaPersona(cc);

            if (per == null)
            {
                MessageBox.Show("La persona no está registrada.");
                return;
            }

            if (movimientos.Any(m => m.Persona.Cc == cc && m.TipoMovimiento == "Préstamo"))
            {
                MessageBox.Show("No se puede eliminar una persona con préstamos activos.");
                return;
            }

            registroPersonas.Remove(per);

            string query = "DELETE FROM persona WHERE cc = @cc";
            try
            {
                conexion.AbrirConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@cc", cc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Persona eliminada con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la persona de la base de datos: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public persona consultaPersona(int cc)
        {
            string query = "SELECT nombre, cc, rol, cantMaxPrestamo FROM persona WHERE cc = @cc";

            try
            {
                conexion.AbrirConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@cc", cc);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    persona persona = new persona(
                        reader["nombre"]?.ToString() ?? "Desconocido",
                        Convert.ToInt32(reader["cc"]),
                        reader["rol"]?.ToString() ?? "Sin rol",
                        reader["cantMaxPrestamo"] != DBNull.Value
                            ? Convert.ToInt32(reader["cantMaxPrestamo"])
                            : 3
                    );

                    reader.Close();
                    return persona;
                }
                else
                {

                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar la persona: {ex.Message}");
                return null;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public class movimiento
        {
            private persona persona;
            private material material;
            private DateTime fechaMovimiento;
            private string tipoMovimiento;

            public movimiento(persona persona, material material, DateTime fechaMovimiento, string tipoMovimiento)
            {
                this.Persona = persona;
                this.Material = material;
                this.FechaMovimiento = fechaMovimiento;
                this.TipoMovimiento = tipoMovimiento;
            }

            public persona Persona { get => persona; set => persona = value; }
            public material Material { get => material; set => material = value; }
            public DateTime FechaMovimiento { get => fechaMovimiento; set => fechaMovimiento = value; }
            public string TipoMovimiento { get => tipoMovimiento; set => tipoMovimiento = value; }
        }

        public class material
        {
            private int iSBN;
            private string nombre;
            private int cantidadReg;
            private int cantidadAct;
            private string tipo;

            public material(int iSBN, string nombre, int cantidadReg, int cantidadAct, string tipo)
            {
                this.ISBN = iSBN;
                this.Nombre = nombre;

                this.CantidadReg = cantidadReg;
                this.CantidadAct = cantidadAct;
                this.tipo = tipo;
            }

            public int ISBN { get => iSBN; set => iSBN = value; }
            public string Nombre { get => nombre; set => nombre = value; }

            public int CantidadReg { get => cantidadReg; set => cantidadReg = value; }
            public int CantidadAct { get => cantidadAct; set => cantidadAct = value; }

            public string Tipo { get => tipo; set => tipo = value; }
        }

        public class persona
        {
            // Campos privados
            private string nombre;
            private int cc;
            private string rol; // Corregido de "roll" a "rol"
            private int cantMaxPrestamo;

            // Constructor
            public persona(string nombre, int cc, string rol, int cantMaxPrestamo)
            {
                this.Nombre = nombre;
                this.Cc = cc;
                this.Rol = rol; // Corregido
                this.CantMaxPrestamo = cantMaxPrestamo;
            }

            // Propiedades públicas
            public string Nombre
            {
                get => nombre;
                set => nombre = value;
            }
            public int Cc
            {
                get => cc;
                set => cc = value;
            }
            public string Rol // Corregido
            {
                get => rol;
                set => rol = value;
            }
            public int CantMaxPrestamo
            {
                get => cantMaxPrestamo;
                set => cantMaxPrestamo = value;
            }
        }





    }
}
