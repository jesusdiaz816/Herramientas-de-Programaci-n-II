// Taller: Aplicación herencia y polimorfismo
// Jesús David Diaz Henao

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Taller.Aplicación_herencia_y_polimorfismo
{
    // Clase base: Persona

    public abstract class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public string EstadoCivil { get; set; }

        public Persona(string nombre, string apellido, string id, string estadoCivil)
        {
            Nombre = nombre;
            Apellido = apellido;
            Identificacion = id;
            EstadoCivil = estadoCivil;
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre} {Apellido}");
            Console.WriteLine($"Identificacion: {Identificacion}");
            Console.WriteLine($"Estado civil: {EstadoCivil}");
        }

        public void CambiarEstadoCivil(string nuevoEstado)
        {
            EstadoCivil = nuevoEstado;
            Console.WriteLine($"El estado civil de {Nombre} fue cambiado a {nuevoEstado}.");
        }
    }


    // Clase abstracta Empleado

    public abstract class Empleado : Persona
    {
        public int AnioIncorporacion { get; set; }

        public Empleado(string nombre, string apellido, string id, string estadoCivil, int anio)
            : base(nombre, apellido, id, estadoCivil)
        {
            AnioIncorporacion = anio;
        }
    }


    // Clase Estudiante

    public class Estudiante : Persona
    {
        public string Curso { get; set; }

        public Estudiante(string nombre, string apellido, string id, string estadoCivil, string curso)
            : base(nombre, apellido, id, estadoCivil)
        {
            Curso = curso;
        }

        public void MatricularNuevoCurso(string nuevoCurso)
        {
            Curso = nuevoCurso;
            Console.WriteLine($"{Nombre} ha sido matriculado en el curso {nuevoCurso}.");
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Curso actual: {Curso}\n");
        }
    }


    // Clase Profesor

    public class Profesor : Empleado
    {
        public string Facultad { get; set; }

        public Profesor(string nombre, string apellido, string id, string estadoCivil, int anio, string facultad)
            : base(nombre, apellido, id, estadoCivil, anio)
        {
            Facultad = facultad;
        }

        public void CambiarFacultad(string nuevaFacultad)
        {
            Facultad = nuevaFacultad;
            Console.WriteLine($"{Nombre} ahora pertenece a la facultad de {nuevaFacultad}.");
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Facultad: {Facultad}");
            Console.WriteLine($"Año de incorporacion: {AnioIncorporacion}\n");
        }
    }


    // Clase Administrativo

    public class Administrativo : Empleado
    {
        public string Dependencia { get; set; }

        public Administrativo(string nombre, string apellido, string id, string estadoCivil, int anio, string dependencia)
            : base(nombre, apellido, id, estadoCivil, anio)
        {
            Dependencia = dependencia;
        }

        public void CambiarDependencia(string nuevaDependencia)
        {
            Dependencia = nuevaDependencia;
            Console.WriteLine($"{Nombre} ahora pertenece a la dependencia {nuevaDependencia}.");
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Dependencia: {Dependencia}");
            Console.WriteLine($"Año de incorporacion: {AnioIncorporacion}\n");
        }
    }


    // Clase Servicios varios

    public class ServiciosVarios : Empleado
    {
        public string Labor { get; set; }

        public ServiciosVarios(string nombre, string apellido, string id, string estadoCivil, int anio, string labor)
            : base(nombre, apellido, id, estadoCivil, anio)
        {
            Labor = labor;
        }

        public void CambiarLabor(string nuevaLabor)
        {
            Labor = nuevaLabor;
            Console.WriteLine($"{Nombre} ahora desempeña la labor de {nuevaLabor}.");
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Labor: {Labor}");
            Console.WriteLine($"Año de incorporacion: {AnioIncorporacion}\n");
        }
    }


    // Programa principal

    class ProgramaInstitucion
    {
        static List<Persona> personas = new List<Persona>();

        static void Main()
        {
            // Registros iniciales
            personas.Add(new Estudiante("Jesus David", "Diaz", "101", "Soltero", "Herramientas de programacion 2"));
            personas.Add(new Profesor("Carlos", "Gomez", "202", "Casado", 2015, "Ingenieria"));
            personas.Add(new Administrativo("Laura", "Ramirez", "303", "Soltero", 2018, "Recursos Humanos"));
            personas.Add(new ServiciosVarios("Jose", "Torres", "404", "Casado", 2020, "Mantenimiento"));

            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("Sistema de gestion institucional - Aplicacion herencia y polimorfismo");
                Console.WriteLine("1. Ver personas registradas");
                Console.WriteLine("2. Modificar informacion");
                Console.WriteLine("3. Registrar nueva persona");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opcion: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MostrarPersonasPorTipo();
                        break;
                    case "2":
                        ModificarPersona();
                        break;
                    case "3":
                        RegistrarPersona();
                        break;
                    case "4":
                        continuar = false;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }
            }
        }


        // Muestra las personas clasificadas por tipo

        static void MostrarPersonasPorTipo()
        {
            Console.WriteLine("\n--- ESTUDIANTES ---");
            foreach (var p in personas.OfType<Estudiante>()) p.MostrarInformacion();

            Console.WriteLine("\n--- PROFESORES ---");
            foreach (var p in personas.OfType<Profesor>()) p.MostrarInformacion();

            Console.WriteLine("\n--- ADMINISTRATIVOS ---");
            foreach (var p in personas.OfType<Administrativo>()) p.MostrarInformacion();

            Console.WriteLine("\n--- SERVICIOS VARIOS ---");
            foreach (var p in personas.OfType<ServiciosVarios>()) p.MostrarInformacion();
        }

        // Modificar información según tipo de persona

        static void ModificarPersona()
        {
            Console.Write("Ingrese el número de identificacion: ");
            string id = Console.ReadLine();
            Persona persona = personas.Find(p => p.Identificacion == id);

            if (persona == null)
            {
                Console.WriteLine("No se encontro una persona con esa identificacion.");
                return;
            }

            if (persona is Estudiante estudiante)
            {
                estudiante.MatricularNuevoCurso(SeleccionarCurso());
            }
            else if (persona is Profesor profesor)
            {
                profesor.CambiarFacultad(SeleccionarFacultad());
            }
            else if (persona is Administrativo admin)
            {
                admin.CambiarDependencia(SeleccionarDependencia());
            }
            else if (persona is ServiciosVarios servicio)
            {
                servicio.CambiarLabor(SeleccionarLabor());
            }

            Console.Write("¿Desea cambiar tambien el estado civil? (s/n): ");
            if (Console.ReadLine().ToLower() == "s")
            {
                persona.CambiarEstadoCivil(SeleccionarEstadoCivil());
            }
        }


        // Registrar una nueva persona según el tipo seleccionado

        static void RegistrarPersona()
        {
            Console.WriteLine("\nSeleccione el tipo de persona a registrar:");
            Console.WriteLine("1. Estudiante");
            Console.WriteLine("2. Profesor");
            Console.WriteLine("3. Administrativo");
            Console.WriteLine("4. Servicios varios");
            Console.Write("Opcion: ");
            string tipo = Console.ReadLine();

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Identificacion: ");
            string id = Console.ReadLine();
            string estadoCivil = SeleccionarEstadoCivil();

            switch (tipo)
            {
                case "1":
                    personas.Add(new Estudiante(nombre, apellido, id, estadoCivil, SeleccionarCurso()));
                    break;
                case "2":
                    Console.Write("Año de incorporacion: ");
                    int anioProf = int.Parse(Console.ReadLine());
                    personas.Add(new Profesor(nombre, apellido, id, estadoCivil, anioProf, SeleccionarFacultad()));
                    break;
                case "3":
                    Console.Write("Año de incorporacion: ");
                    int anioAdm = int.Parse(Console.ReadLine());
                    personas.Add(new Administrativo(nombre, apellido, id, estadoCivil, anioAdm, SeleccionarDependencia()));
                    break;
                case "4":
                    Console.Write("Año de incorporacion: ");
                    int anioServ = int.Parse(Console.ReadLine());
                    personas.Add(new ServiciosVarios(nombre, apellido, id, estadoCivil, anioServ, SeleccionarLabor()));
                    break;
                default:
                    Console.WriteLine("Tipo no valido.");
                    break;
            }

            Console.WriteLine("Persona registrada exitosamente.");
        }


        // Menús de seleccion

        static string SeleccionarEstadoCivil()
        {
            string[] opciones = { "Soltero", "Casado", "Union libre", "Divorciado" };
            return SeleccionarDeLista("estado civil", opciones);
        }

        static string SeleccionarFacultad()
        {
            string[] opciones = { "Ingenieria", "Ciencias", "Artes" };
            return SeleccionarDeLista("facultad", opciones);
        }

        static string SeleccionarDependencia()
        {
            string[] opciones = { "Recursos Humanos", "Contabilidad", "Secretaria" };
            return SeleccionarDeLista("dependencia", opciones);
        }

        static string SeleccionarCurso()
        {
            string[] opciones = { "Algebra Lineal", "Base de datos 1", "Herramientas de programacion 2", "Cálculo diferencial", "Circuitos digitales" };
            return SeleccionarDeLista("curso", opciones);
        }

        static string SeleccionarLabor()
        {
            string[] opciones = { "Aseo", "Seguridad", "Jardineria", "Mantenimiento" };
            return SeleccionarDeLista("labor", opciones);
        }

        // Método genérico para evitar repetir código
        static string SeleccionarDeLista(string titulo, string[] opciones)
        {
            Console.WriteLine($"\nSeleccione {titulo}:");
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {opciones[i]}");
            }
            Console.Write("Opción: ");
            int seleccion;
            if (int.TryParse(Console.ReadLine(), out seleccion) && seleccion >= 1 && seleccion <= opciones.Length)
            {
                return opciones[seleccion - 1];
            }
            Console.WriteLine("Seleccion no valida, se usara la primera opcion por defecto.");
            return opciones[0];
        }
    }
}

