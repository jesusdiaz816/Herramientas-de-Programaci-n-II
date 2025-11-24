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
    public partial class GestionPrestamosForm: Form
    {
        public GestionPrestamosForm()
        {
            InitializeComponent();
        }


        private void GestionPrestamosForm_Load(object sender, EventArgs e)
        {
            CargarSuscriptores();
            CargarLibros();
        }

        private void CargarSuscriptores()
        {
            using (SqlConnection cn = DB.GetConnection())
            {
                cn.Open();
                string sql = "SELECT documento FROM suscriptores";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cmbSuscriptor.Items.Add(dr["documento"].ToString());
                    }
                }
            }
        }

        private void CargarLibros()
        {
            using (SqlConnection cn = DB.GetConnection())
            {
                cn.Open();
                string sql = "SELECT codigo FROM libros";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cmbLibro.Items.Add(dr["codigo"].ToString());
                    }
                }
            }
        }

        private void MostrarPrestamos()
        {
            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "SELECT * FROM prestamos";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvPrestamos.DataSource = dt;
                        dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar préstamos: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            cmbLibro.SelectedIndex = -1;
            cmbSuscriptor.SelectedIndex = -1;
            chkTieneDevolucion.Checked = false;
            dtpPrestamo.Value = DateTime.Now;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            MostrarPrestamos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbSuscriptor.SelectedIndex == -1 || cmbLibro.SelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar suscriptor y libro.");
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql =
                        "INSERT INTO prestamos (docsuscriptor, codigolibro, fechaprestamo, fechadevolucion) " +
                        "VALUES (@doc, @lib, @fp, @fd)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@doc", cmbSuscriptor.Text);
                        cmd.Parameters.AddWithValue("@lib", int.Parse(cmbLibro.Text));
                        cmd.Parameters.AddWithValue("@fp", dtpPrestamo.Value.Date);

                        if (chkTieneDevolucion.Checked)
                            cmd.Parameters.AddWithValue("@fd", dtpDevolucion.Value.Date);
                        else
                            cmd.Parameters.AddWithValue("@fd", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Préstamo registrado.");
                MostrarPrestamos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar préstamo: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql =
                        "UPDATE prestamos SET docsuscriptor=@doc, fechadevolucion=@fd " +
                        "WHERE codigolibro=@lib AND fechaprestamo=@fp";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@doc", cmbSuscriptor.Text);
                        cmd.Parameters.AddWithValue("@lib", int.Parse(cmbLibro.Text));
                        cmd.Parameters.AddWithValue("@fp", dtpPrestamo.Value.Date);

                        if (chkTieneDevolucion.Checked)
                            cmd.Parameters.AddWithValue("@fd", dtpDevolucion.Value.Date);
                        else
                            cmd.Parameters.AddWithValue("@fd", DBNull.Value);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas == 0)
                            MessageBox.Show("No existe un préstamo con esos datos.");
                        else
                            MessageBox.Show("Préstamo actualizado.");
                    }
                }

                MostrarPrestamos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar préstamo: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cmbLibro.SelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar un préstamo.");
                return;
            }

            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();
                    string sql = "DELETE FROM prestamos WHERE codigolibro=@lib AND fechaprestamo=@fp";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@lib", int.Parse(cmbLibro.Text));
                        cmd.Parameters.AddWithValue("@fp", dtpPrestamo.Value.Date);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Préstamo eliminado.");
                MostrarPrestamos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void chkTieneDevolucion_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
