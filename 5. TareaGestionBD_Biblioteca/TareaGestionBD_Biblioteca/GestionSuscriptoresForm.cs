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
    public partial class GestionSuscriptoresForm: Form
    {
        public GestionSuscriptoresForm()
        {
            InitializeComponent();
        }

        private void GestionSuscriptoresForm_Load(object sender, EventArgs e)
        {

        }

        private void MostrarSuscriptores()
        {
            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "SELECT * FROM suscriptores";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvSuscriptores.DataSource = dt;
                        dgvSuscriptores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar suscriptores: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtDocumento.Focus();
        }


        private void btnListar_Click(object sender, EventArgs e)
        {
            MostrarSuscriptores();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text) ||
        string.IsNullOrWhiteSpace(txtNombre.Text) ||
        string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Debes llenar todos los campos.");
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "INSERT INTO suscriptores (documento, nombre, direccion) VALUES (@doc, @nombre, @dir)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@doc", txtDocumento.Text.Trim());
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@dir", txtDireccion.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Suscriptor agregado correctamente.");
                MostrarSuscriptores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar suscriptor: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Selecciona un suscriptor.");
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "UPDATE suscriptores SET nombre=@nombre, direccion=@dir WHERE documento=@doc";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@doc", txtDocumento.Text.Trim());
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@dir", txtDireccion.Text.Trim());

                        int filas = cmd.ExecuteNonQuery();

                        if (filas == 0)
                            MessageBox.Show("No existe un suscriptor con ese documento.");
                        else
                            MessageBox.Show("Suscriptor actualizado correctamente.");
                    }
                }

                MostrarSuscriptores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Selecciona un suscriptor para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Seguro que deseas eliminar este suscriptor?",
                "Confirmación", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "DELETE FROM suscriptores WHERE documento=@doc";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@doc", txtDocumento.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Suscriptor eliminado.");
                MostrarSuscriptores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }
    }
}
