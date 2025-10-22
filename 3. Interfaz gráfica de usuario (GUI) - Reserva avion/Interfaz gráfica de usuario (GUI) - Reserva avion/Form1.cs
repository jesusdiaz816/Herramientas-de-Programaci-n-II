using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz_gráfica_de_usuario__GUI____Reserva_avion
{
    public partial class Form1: Form
    {

        // Declaración global en la clase Form1
        private List<Asiento> listaAsientos;
        private string asientoSeleccionado = "";

        public Form1()
        {
            InitializeComponent();

        // Llamar a un método para inicializar los datos
            InicializarAsientos();
        }

        private void InicializarAsientos()
        {

            listaAsientos = new List<Asiento>();

            // Guardamos el identificador simple del asiento (ej. "A1")
            for (int i = 1; i <= 10; i++)
            {
                string idAsientoSimple = "A" + i;
                listaAsientos.Add(new Asiento(idAsientoSimple));

                //Asignar el Tag a los botones
                Control[] controles = this.Controls.Find("btn" + idAsientoSimple, true);
                if (controles.Length > 0 && controles[0] is Button btn)
                {
                    btn.Tag = idAsientoSimple; // Asignamos "A1", "A2", etc. al Tag
                    btn.BackColor = System.Drawing.Color.LimeGreen; // Asegurar el color inicial
                }
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Verificar que haya un asiento seleccionado
            if (string.IsNullOrEmpty(asientoSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione el asiento que desea cancelar.",
                                "Error de Cancelación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Buscar el asiento en la lista (nuestra "BD") usando el ID simple ("A1")
            Asiento asientoACancelar = listaAsientos.FirstOrDefault(a => a.NumeroAsiento == asientoSeleccionado);

            if (asientoACancelar != null)
            {
                // Verificar si el asiento está realmente reservado
                if (!asientoACancelar.EstaReservado)
                {
                    MessageBox.Show($"El asiento {asientoSeleccionado} no está reservado. Ya está Libre.",
                                    "Asiento Libre",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // CANCELACIÓN: Obtener datos del pasajero antes de limpiar
                string pasajero = asientoACancelar.NombrePasajero + " " + asientoACancelar.ApellidoPasajero;

                // Liberar asiento y limpiar datos de la reserva
                asientoACancelar.EstaReservado = false;
                asientoACancelar.NombrePasajero = "";
                asientoACancelar.ApellidoPasajero = "";

                // Actualizar la interfaz (el color del botón a verde)
                string nombreBoton = "btn" + asientoSeleccionado;
                ActualizarEstadoAsiento(nombreBoton, false); // false = libre (verde)

                // Limpiar la información de la interfaz
                asientoSeleccionado = "";
                lblAsiento.Text = "Asiento:";
                lblPasajero.Text = "Pasajero: "; // Ajustar si usas otro nombre para la etiqueta de pasajero

                MessageBox.Show($"Cancelación exitosa. Asiento liberado. Pasajero: {pasajero}.",
                                "Cancelación confirmada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void btnAsiento_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Tag != null)
            {
                // Almacena el valor del Tag (ej. "A1")
                string idAsientoSimple = btn.Tag.ToString();

                // Almacenamos el ID simple globalmente
                asientoSeleccionado = idAsientoSimple;

                // Mostramos el ID simple en la etiqueta
                lblAsiento.Text = "Asiento: " + idAsientoSimple;

                // Buscar la información del asiento en la lista (nuestra "BD")
                Asiento asientoActual = listaAsientos.FirstOrDefault(a => a.NumeroAsiento == idAsientoSimple);

                if (asientoActual != null)
                {
                    if (asientoActual.EstaReservado)
                    {
                        // Si está reservado, mostrar el nombre del pasajero
                        lblPasajero.Text = $"Pasajero: {asientoActual.NombrePasajero} {asientoActual.ApellidoPasajero}";

                        // Opcional: Limpiar los campos de Nombre/Apellido para evitar reservas accidentales sobre el mismo asiento
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                    }
                    else
                    {
                        // Si está libre, mostrar que no hay pasajero y limpiar el campo de la interfaz
                        lblPasajero.Text = "Pasajero: ";
                    }
                }
            }
        }


        private void btnReservar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(asientoSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un asiento primero.", "Error de Reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener datos de la interfaz
            string nombre = txtNombre.Text.Trim(); // Usa el nombre de tu TextBox de Nombre
            string apellido = txtApellido.Text.Trim(); // Usa el nombre de tu TextBox de Apellido

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
            {
                MessageBox.Show("Debe ingresar el nombre y el apellido del pasajero.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscar el asiento en la lista (nuestra "BD")
            Asiento asientoAReservar = listaAsientos.FirstOrDefault(a => a.NumeroAsiento == asientoSeleccionado);

            if (asientoAReservar != null)
            {
                if (asientoAReservar.EstaReservado)
                {
                    MessageBox.Show("El asiento " + asientoSeleccionado + " ya está reservado.", "Asiento Ocupado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Realizar la reserva
                asientoAReservar.EstaReservado = true;
                asientoAReservar.NombrePasajero = nombre;
                asientoAReservar.ApellidoPasajero = apellido;

                // Actualizar la interfaz: Buscamos el control con el nombre 'btn' + ID
                string nombreBoton = "btn" + asientoSeleccionado;
                ActualizarEstadoAsiento(nombreBoton, true); // True = Reservado (Rojo)

                MessageBox.Show("Reserva exitosa para " + nombre + " " + apellido + " en el asiento " + asientoSeleccionado, "Reserva Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos después de reservar
                txtNombre.Text = "";
                txtApellido.Text = "";
                asientoSeleccionado = "";
                lblAsiento.Text = "Asiento:";
            }
        }

        private void ActualizarEstadoAsiento(string numeroAsiento, bool reservado)
        {
            // Buscar el control (botón) en el formulario por su nombre
            Control[] controlesEncontrados = this.Controls.Find(numeroAsiento, true);

            if (controlesEncontrados.Length > 0 && controlesEncontrados[0] is Button btn)
            {
                if (reservado)
                {
                    // Cambiar a color Rojo (Reservado)
                    btn.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    // Cambiar a color Verde (Disponible)
                    btn.BackColor = System.Drawing.Color.LimeGreen;
                }
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            // Pedir confirmación al usuario para evitar reinicios accidentales
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cancelar TODAS las reservas y reiniciar el sistema?",
                "Confirmar Reinicio",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return; // Detiene la ejecución si el usuario cancela
            }

            // Recorrer y limpiar la Base de Datos (listaAsientos)
            foreach (Asiento asiento in listaAsientos)
            {
                // Cancelar la reserva en la data
                asiento.EstaReservado = false;
                asiento.NombrePasajero = "";
                asiento.ApellidoPasajero = "";

                // Actualizar la interfaz: Cambiar el botón a color verde (Libre)
                string nombreBoton = "btn" + asiento.NumeroAsiento; // Reconstruye el nombre completo (ej. "btnA1")
                ActualizarEstadoAsiento(nombreBoton, false); // false = libre (verde)
            }

            // Limpiar el estado de selección actual en la interfaz
            asientoSeleccionado = "";
            lblAsiento.Text = "Asiento:";
            lblPasajero.Text = "Pasajero: ";
            txtNombre.Text = "";
            txtApellido.Text = "";

            MessageBox.Show("El sistema se ha reiniciado correctamente. Todos los puestos están disponibles.",
                            "Sistema Reiniciado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }


        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void lblDisponible_Click(object sender, EventArgs e)
        {

        }

        private void lblReservado_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
