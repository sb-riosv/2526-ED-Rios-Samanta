using System;

namespace RegistroEstudiantes
{
    // La clase Estudiante modela la información básica de un estudiante.
    public class Estudiante
    {
        // Propiedades que almacenan los datos principales.
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        // Arreglo que almacena exactamente tres números de teléfono.
        public string[] Telefonos { get; set; }

        // Constructor que inicializa todos los datos del estudiante.
        public Estudiante(int id, string nombres, string apellidos, string direccion, string[] telefonos)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefonos = telefonos;
        }

        // Método que muestra un resumen de la información del estudiante en consola.
        public void MostrarResumen()
        {
            Console.WriteLine("=========== FICHA DEL ESTUDIANTE ===========");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombres: {Nombres}");
            Console.WriteLine($"Apellidos: {Apellidos}");
            Console.WriteLine($"Dirección: {Direccion}");
            Console.WriteLine("Teléfonos registrados:");

            // Recorremos el arreglo de teléfonos para mostrarlos uno por uno.
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"  Teléfono #{i + 1}: {Telefonos[i]}");
            }

            Console.WriteLine("============================================");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== REGISTRO BÁSICO DE ESTUDIANTE ===\n");

            // Solicitar los datos del estudiante al usuario.
            int id = LeerEntero("Ingrese el ID del estudiante: ");
            string nombres = LeerTextoObligatorio("Ingrese los nombres del estudiante: ");
            string apellidos = LeerTextoObligatorio("Ingrese los apellidos del estudiante: ");
            string direccion = LeerTextoObligatorio("Ingrese la dirección del estudiante: ");

            // Creamos un arreglo para almacenar los tres números de teléfono.
            string[] telefonos = new string[3];

            for (int i = 0; i < telefonos.Length; i++)
            {
                telefonos[i] = LeerTextoObligatorio($"Ingrese el teléfono #{i + 1}: ");
            }

            // Crear el objeto Estudiante con los datos proporcionados.
            Estudiante estudiante = new Estudiante(id, nombres, apellidos, direccion, telefonos);

            // Limpiamos la pantalla para mostrar el resumen de forma más clara.
            Console.Clear();
            Console.WriteLine("Los datos del estudiante han sido registrados correctamente.\n");

            // Mostrar la información recopilada.
            estudiante.MostrarResumen();

            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
        }

        // Método auxiliar para leer un número entero con validación.
        private static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Entrada no válida. Ingrese un número entero: ");
            }

            return valor;
        }

        // Método auxiliar para leer texto obligatorio (no vacío).
        private static string LeerTextoObligatorio(string mensaje)
        {
            string? texto;

            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    Console.WriteLine("El valor no puede estar vacío. Intente nuevamente.");
                }

            } while (string.IsNullOrWhiteSpace(texto));

            return texto.Trim();
        }
    }
}
