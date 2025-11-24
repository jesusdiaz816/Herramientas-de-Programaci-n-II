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

namespace TareaGestionBD_Biblioteca
{
    public partial class GestionLibrosForm: Form
    {
        public GestionLibrosForm()
        {
            InitializeComponent();
           
        }

        private void GestionLibrosForm_Load(object sender, EventArgs e)
        {

        }

        private void MostrarDatos()
        {
            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "SELECT * FROM libros";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvLibros.DataSource = dt; // agrega un DataGridView llamado dgvLibros
                        dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar libros: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtCodigo.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Debes seleccionar un libro para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Seguro que deseas eliminar este libro?",
                                "Confirmación",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();

                    string sql = "DELETE FROM libros WHERE codigo = @codigo";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", int.Parse(txtCodigo.Text));

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Libro eliminado correctamente.");
                MostrarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
        string.IsNullOrWhiteSpace(txtTitulo.Text) ||
        string.IsNullOrWhiteSpace(txtAutor.Text))
            {
                MessageBox.Show("Debes llenar todos los campos.");
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "INSERT INTO libros (codigo, titulo, autor) VALUES (@codigo, @titulo, @autor)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", int.Parse(txtCodigo.Text));
                        cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text.Trim());
                        cmd.Parameters.AddWithValue("@autor", txtAutor.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Libro agregado correctamente.");
                MostrarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Debes seleccionar un libro.");
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();

                    string sql = "UPDATE libros SET titulo = @titulo, autor = @autor WHERE codigo = @codigo";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", int.Parse(txtCodigo.Text));
                        cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text.Trim());
                        cmd.Parameters.AddWithValue("@autor", txtAutor.Text.Trim());

                        int filas = cmd.ExecuteNonQuery();

                        if (filas == 0)
                        {
                            MessageBox.Show("No existe un libro con ese código.");
                        }
                        else
                        {
                            MessageBox.Show("Libro actualizado correctamente.");
                        }
                    }
                }

                MostrarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }
    }
}
