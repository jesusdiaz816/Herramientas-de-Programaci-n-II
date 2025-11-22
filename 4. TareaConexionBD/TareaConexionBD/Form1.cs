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

namespace TareaConexionBD
{
    public partial class Form1 : Form
    {
        string cadena = @"Server=JESUS-DIAZ\SQLEXPRESS;Database=TareaConexionBD;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) 
        {
        }
        private void label3_Click(object sender, EventArgs e) 
        {
        }
        private void Form1_Load(object sender, EventArgs e) 
        { 
        }
        private void btnListar_Click(object sender, EventArgs e) 
        { 
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();

                    SqlDataAdapter da =
                        new SqlDataAdapter("SELECT * FROM empleados", conexion);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvEmpleados.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar: " + ex.Message);
            }
        }


        // AGREGAR

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO empleados VALUES (@id,@ap,@nom,@dir,@mail)", conexion);

                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.Parameters.AddWithValue("@ap", txtApellido.Text);
                    cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@dir", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@mail", txtEmail.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Empleado registrado.");
                MostrarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }


        // ACTUALIZAR

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE empleados SET apellido=@ap, nombre=@nom, direccion=@dir, email=@mail " +
                        "WHERE idempleado=@id",
                        conexion
                    );

                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.Parameters.AddWithValue("@ap", txtApellido.Text);
                    cmd.Parameters.AddWithValue("@nom", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@dir", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@mail", txtEmail.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Empleado actualizado.");
                MostrarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }


        // ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM empleados WHERE idempleado=@id",
                        conexion
                    );

                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Empleado eliminado.");
                MostrarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
