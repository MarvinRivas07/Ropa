using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropaa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al catálogo de prendas de vestir.\n");

            Console.WriteLine("Por favor, ingrese su nombre:");
            string nombreUsuario = Console.ReadLine();

            Console.WriteLine("\nPor favor, ingrese su edad:");
            int edadUsuario = int.Parse(Console.ReadLine());

            Console.WriteLine("\nHola " + nombreUsuario + ", usted tiene " + edadUsuario + " años.");

            Console.WriteLine("\nElija el tipo de prenda que desea:");
            Console.WriteLine("1. Camisas");
            Console.WriteLine("2. Pantalones");

            int tipoPrenda = int.Parse(Console.ReadLine());

            if (tipoPrenda < 1 || tipoPrenda > 2)
            {
                Console.WriteLine("\nTipo de prenda inválido, intente de nuevo.");
            }
            else
            {
                Prenda[] prendas;

                if (tipoPrenda == 1)
                {
                    prendas = new Prenda[]
                    {
                        new CamisaMangaLarga("Camisa de manga larga azul", "Azul", "Algodón", new string[] {"S", "M", "L"}),
                        new CamisaMangaLarga("Camisa de manga larga blanca", "Blanco", "Algodón", new string[] {"S", "M", "L"}),
                        new CamisaMangaCorta("Camisa de manga corta roja", "Rojo", "Algodón", new string[] {"S", "M", "L"}),
                        new CamisaMangaCorta("Camisa de manga corta amarilla", "Amarillo", "Algodón", new string[] {"S", "M", "L"})
                    };
                }
                else
                {
                    prendas = new Prenda[]
                    {
                        new Pantalon("Pantalón negro", "Negro", "Mezclilla", new string[] {"30", "32", "34"}),
                        new Pantalon("Pantalón gris", "Gris", "Mezclilla", new string[] {"30", "32", "34"}),
                        new Pantalon("Pantalón café", "Café", "Mezclilla", new string[] {"30", "32", "34"}),
                        new Pantalon("Pantalón azul marino", "Azul marino", "Mezclilla", new string[] {"30", "32", "34"})
                    };
                }

                Console.WriteLine("\nEstos son los productos disponibles:");

                int contadorPrendas = 1;

                foreach (Prenda prenda in prendas)
                {
                    Console.WriteLine(contadorPrendas + ". " + prenda.nombre);
                    contadorPrendas++;
                }

                Console.WriteLine("\nIngrese el número de la prenda que desea:");
                int numeroPrenda = int.Parse(Console.ReadLine());

                if (numeroPrenda < 1 || numeroPrenda > prendas.Length)
                {
                    Console.WriteLine("\nPrenda inválida, intente de nuevo.");
                }
                else
                {
                    Prenda prendaSeleccionada = prendas[numeroPrenda - 1];

                    Console.WriteLine("\nHa seleccionado la siguiente prenda:");
                    Console.WriteLine("------------------------------------\n");

                    prendaSeleccionada.MostrarInformacion();

                    Console.WriteLine("\nIngrese su talla:");
                    string talla = Console.ReadLine();

                    if (!prendaSeleccionada.TallaValida(talla))
                    {
                        Console.WriteLine("\nTalla inválida, intente de nuevo.");
                    }
                    else
                    {
                        Console.WriteLine("\n¡Excelente elección! La prenda ha sido agregada a su carrito de compras.");


                        CarritoCompras carrito = new CarritoCompras(nombreUsuario, edadUsuario);
                        carrito.AgregarPrenda(prendaSeleccionada, talla);
                    }
                }
            }

            Console.WriteLine("\n¡Gracias por visitar nuestro catálogo de prendas de vestir!");
            Console.WriteLine("Presione cualquier tecla para salir.");
            Console.ReadKey();
        }
    }

    abstract class Prenda
    {
        public string nombre { get; set; }
        public string color { get; set; }
        public string material { get; set; }
        public string[] tallasDisponibles { get; set; }

        public Prenda(string nombre, string color, string material, string[] tallasDisponibles)
        {
            this.nombre = nombre;
            this.color = color;
            this.material = material;
            this.tallasDisponibles = tallasDisponibles;
        }

        public abstract void MostrarInformacion();

        public bool TallaValida(string talla)
        {
            foreach (string tallaDisponible in tallasDisponibles)
            {
                if (talla == tallaDisponible)
                {
                    return true;
                }
            }

            return false;
        }
    }

    class CamisaMangaLarga : Prenda
    {
        public CamisaMangaLarga(string nombre, string color, string material, string[] tallasDisponibles) : base(nombre, color, material, tallasDisponibles)
        {
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Color: " + color);
            Console.WriteLine("Material: " + material);
            Console.WriteLine("Tallas disponibles: " + string.Join(", ", tallasDisponibles));
            Console.WriteLine("Tipo: Camisa de manga larga");
        }
    }

    class CamisaMangaCorta : Prenda
    {
        public CamisaMangaCorta(string nombre, string color, string material, string[] tallasDisponibles) : base(nombre, color, material, tallasDisponibles)
        {
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Color: " + color);
            Console.WriteLine("Material: " + material);
            Console.WriteLine("Tallas disponibles: " + string.Join(", ", tallasDisponibles));
            Console.WriteLine("Tipo: Camisa de manga corta");
        }
    }

    class Pantalon : Prenda
    {
        public Pantalon(string nombre, string color, string material, string[] tallasDisponibles) : base(nombre, color, material, tallasDisponibles)
        {
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Color: " + color);
            Console.WriteLine("Material: " + material);
            Console.WriteLine("Tallas disponibles: " + string.Join(", ", tallasDisponibles));
            Console.WriteLine("Tipo: Pantalón");
        }
    }

    class CarritoCompras
    {
        private string nombreUsuario;
        private int edadUsuario;
        private Prenda[] prendasCompradas;

        private string[] tallasSeleccionadas;

        public CarritoCompras(string nombreUsuario, int edadUsuario)
        {
            this.nombreUsuario = nombreUsuario;
            this.edadUsuario = edadUsuario;
            prendasCompradas = new Prenda[0];
            tallasSeleccionadas = new string[0];
        }

        public void AgregarPrenda(Prenda prenda, string tallaSeleccionada)
        {

            Array.Resize(ref prendasCompradas, prendasCompradas.Length + 1);
            Array.Resize(ref tallasSeleccionadas, tallasSeleccionadas.Length + 1);


            prendasCompradas[prendasCompradas.Length - 1] = prenda;
            tallasSeleccionadas[tallasSeleccionadas.Length - 1] = tallaSeleccionada;
        }

        public void MostrarCarrito()
        {
            Console.WriteLine("\nCarrito de compras de " + nombreUsuario + " (Edad: " + edadUsuario + "):");


            for (int i = 0; i < prendasCompradas.Length; i++)
            {
                Console.WriteLine("\nPrenda #" + (i + 1) + ":");
                prendasCompradas[i].MostrarInformacion();
                Console.WriteLine("Talla seleccionada: " + tallasSeleccionadas[i]);
            }

            Console.WriteLine("\nTotal de prendas en el carrito: " + prendasCompradas.Length);
        }
    }
}
