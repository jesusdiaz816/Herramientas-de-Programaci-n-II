using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TareaGestionBD_Biblioteca
{
    public partial class MainMenuForm: Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }

        // Botones o menú que abra formularios:
  

        private void btnGestionPrestamos_Click(object sender, EventArgs e)
        {
            using (GestionPrestamosForm f = new GestionPrestamosForm())
            {
                f.ShowDialog();
            }
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            using (InformePrestamosForm f = new InformePrestamosForm())
            {
                f.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Como usamos "using" en cada operación, no hay conexión global abierta.
            // Si en algún momento mantienes una conexión global, aquí la cierras.
            Application.Exit();
        }

        private void btnGestionLibros_Click_1(object sender, EventArgs e)
        {
            using (GestionLibrosForm f = new GestionLibrosForm())
            {
                f.ShowDialog();
            }
        }

        private void btnGestionSuscriptores_Click_1(object sender, EventArgs e)
        {
            using (GestionSuscriptoresForm f = new GestionSuscriptoresForm())
            {
                f.ShowDialog();
            }
        }

        private void btnGestionPrestamos_Click_1(object sender, EventArgs e)
        {
            using (GestionPrestamosForm f = new GestionPrestamosForm())
            {
                f.ShowDialog();
            }
        }

        private void btnInforme_Click_1(object sender, EventArgs e)
        {
            using (InformePrestamosForm f = new InformePrestamosForm())
            {
                f.ShowDialog();
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
