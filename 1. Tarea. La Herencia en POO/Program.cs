using System;

namespace ProyectoFiguras
{
    // Clase base
    public abstract class Figura
    {
        public double PosicionX { get; set; }
        public double PosicionY { get; set; }

        protected Figura(double x = 0, double y = 0)
        {
            PosicionX = x;
            PosicionY = y;
        }

        public abstract double CalcularArea();
        public abstract double CalcularPerimetro();

        public virtual void MostrarPosicion()
        {
            Console.WriteLine($"Posición: (X={PosicionX}, Y={PosicionY})");
        }
    }

    public class Circulo : Figura
    {
        public double Radio { get; set; }

        public Circulo(double radio, double x = 0, double y = 0) : base(x, y)
        {
            if (radio <= 0) throw new ArgumentException("El radio debe ser mayor que cero.");
            Radio = radio;
        }

        public override double CalcularArea() => Math.PI * Radio * Radio;
        public override double CalcularPerimetro() => 2 * Math.PI * Radio;
    }

    public class Rectangulo : Figura
    {
        public double Ancho { get; set; }
        public double Alto { get; set; }

        public Rectangulo(double ancho, double alto, double x = 0, double y = 0) : base(x, y)
        {
            if (ancho <= 0 || alto <= 0) throw new ArgumentException("Ancho y alto deben ser mayores que cero.");
            Ancho = ancho;
            Alto = alto;
        }

        public override double CalcularArea() => Ancho * Alto;
        public override double CalcularPerimetro() => 2 * (Ancho + Alto);
    }

    public class Triangulo : Figura
    {
        public double LadoA { get; set; }
        public double LadoB { get; set; }
        public double LadoC { get; set; }

        public Triangulo(double a, double b, double c, double x = 0, double y = 0) : base(x, y)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentException("Los lados deben ser mayores que cero.");
            if (!(a + b > c && a + c > b && b + c > a)) throw new ArgumentException("Los lados no forman un triángulo válido.");

            LadoA = a;
            LadoB = b;
            LadoC = c;
        }

        public override double CalcularArea()
        {
            double s = (LadoA + LadoB + LadoC) / 2.0;
            return Math.Sqrt(s * (s - LadoA) * (s - LadoB) * (s - LadoC));
        }

        public override double CalcularPerimetro() => LadoA + LadoB + LadoC;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Proyecto - Figuras Geométricas";
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (opcion)
                    {
                        case "1": UsarCirculo(); break;
                        case "2": UsarRectangulo(); break;
                        case "3": UsarTriangulo(); break;
                        case "4":
                            Console.WriteLine("Gracias por usar el programa. Saliendo...");
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (!salir)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione Enter para volver al menú...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("=== MENÚ - Figuras Geométricas ===");
            Console.WriteLine("1) Círculo");
            Console.WriteLine("2) Rectángulo");
            Console.WriteLine("3) Triángulo");
            Console.WriteLine("4) Salir");
            Console.Write("Seleccione una opción (1-4): ");
        }

        static double PedirDouble(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Entrada inválida. " + mensaje);
            }
            return valor;
        }

        static (double x, double y) PedirPosicionOpcional()
        {
            Console.Write("¿Desea ingresar posición X,Y? (s/n): ");
            string r = Console.ReadLine().Trim().ToLower();
            if (r == "s" || r == "si")
            {
                double x = PedirDouble("Ingrese Posición X: ");
                double y = PedirDouble("Ingrese Posición Y: ");
                return (x, y);
            }
            return (0, 0);
        }

        static void UsarCirculo()
        {
            Console.WriteLine("--- CÍRCULO ---");
            double radio = PedirDouble("Ingrese el radio: ");
            var pos = PedirPosicionOpcional();
            Circulo c = new Circulo(radio, pos.x, pos.y);
            MostrarResultados(c);
        }

        static void UsarRectangulo()
        {
            Console.WriteLine("--- RECTÁNGULO ---");
            double ancho = PedirDouble("Ingrese el ancho: ");
            double alto = PedirDouble("Ingrese el alto: ");
            var pos = PedirPosicionOpcional();
            Rectangulo r = new Rectangulo(ancho, alto, pos.x, pos.y);
            MostrarResultados(r);
        }

        static void UsarTriangulo()
        {
            Console.WriteLine("--- TRIÁNGULO ---");
            Console.WriteLine("Ingrese las longitudes de los tres lados:");
            double a = PedirDouble("Lado A: ");
            double b = PedirDouble("Lado B: ");
            double c = PedirDouble("Lado C: ");
            var pos = PedirPosicionOpcional();
            Triangulo t = new Triangulo(a, b, c, pos.x, pos.y);
            MostrarResultados(t);
        }

        static void MostrarResultados(Figura f)
        {
            Console.WriteLine();
            Console.WriteLine("--- RESULTADOS ---");
            f.MostrarPosicion();
            Console.WriteLine($"Área: {f.CalcularArea():F4}");
            Console.WriteLine($"Perímetro: {f.CalcularPerimetro():F4}");
        }
    }
}
