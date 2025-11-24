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
    public partial class InformePrestamosForm: Form
    {
        public InformePrestamosForm()
        {
            InitializeComponent();
        }

        private void InformePrestamosForm_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerarInforme_Click(object sender, EventArgs e)
        {
            GenerarInforme();
        }


        private void GenerarInforme()
        {
            try
            {
                using (SqlConnection cn = DB.GetConnection())
                {
                    cn.Open();

                    string sql = @"
                SELECT 
                    s.nombre AS Suscriptor,
                    l.titulo AS Libro,
                    p.fechaprestamo AS FechaPrestamo,
                    p.fechadevolucion AS FechaDevolucion
                FROM prestamos p
                INNER JOIN suscriptores s ON p.docsuscriptor = s.documento
                INNER JOIN libros l ON p.codigolibro = l.codigo
            ";

                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvInforme.DataSource = dt;
                    dgvInforme.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: " + ex.Message);
            }
        }
    }
}
