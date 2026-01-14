using System;

namespace ListaEnlazadaRangoValores
{
    class Nodo
    {
        public int Valor { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }

    class ListaEnlazada
    {
        private Nodo cabeza;
        private Nodo cola;
        public int Cantidad { get; private set; }

        public ListaEnlazada()
        {
            cabeza = null;
            cola = null;
            Cantidad = 0;
        }

        public void AgregarAlFinal(int valor)
        {
            Nodo nuevo = new Nodo(valor);

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.Siguiente = nuevo;
                cola = nuevo;
            }

            Cantidad++;
        }

        public int EliminarFueraDeRango(int minimo, int maximo)
        {
            if (cabeza == null) return 0;

            int eliminados = 0;

            // Eliminar desde el inicio mientras esté fuera de rango
            while (cabeza != null && (cabeza.Valor < minimo || cabeza.Valor > maximo))
            {
                cabeza = cabeza.Siguiente;
                Cantidad--;
                eliminados++;
            }

            // Si la lista quedó vacía
            if (cabeza == null)
            {
                cola = null;
                return eliminados;
            }

            // Eliminar en el resto de la lista
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Valor < minimo || actual.Siguiente.Valor > maximo)
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    Cantidad--;
                    eliminados++;
                }
                else
                {
                    actual = actual.Siguiente;
                }
            }

            // Actualizar cola
            cola = actual;

            return eliminados;
        }

        public void Imprimir(string titulo)
        {
            Console.WriteLine($"\n{titulo}");
            if (cabeza == null)
            {
                Console.WriteLine("(lista vacía)");
                return;
            }

            Nodo actual = cabeza;
            int i = 1;
            while (actual != null)
            {
                Console.Write(actual.Valor);
                if (actual.Siguiente != null) Console.Write(" -> ");
                actual = actual.Siguiente;
                i++;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            ListaEnlazada lista = new ListaEnlazada();
            Random rnd = new Random();

            // Crear lista enlazada con 50 números aleatorios entre 1 y 999
            for (int i = 0; i < 50; i++)
            {
                lista.AgregarAlFinal(rnd.Next(1, 1000)); // 1000 no incluido, por eso llega a 999
            }

            lista.Imprimir("Lista original (50 números aleatorios entre 1 y 999):");

            int minimo = LeerEntero("Ingrese el valor mínimo del rango: ");
            int maximo = LeerEntero("Ingrese el valor máximo del rango: ");

            // Asegurar que el rango sea válido (min <= max)
            if (minimo > maximo)
            {
                int temp = minimo;
                minimo = maximo;
                maximo = temp;
                Console.WriteLine($"Se ajustó el rango para que sea válido: [{minimo}, {maximo}]");
            }

            int eliminados = lista.EliminarFueraDeRango(minimo, maximo);

            Console.WriteLine($"\nRango aplicado: [{minimo}, {maximo}]");
            Console.WriteLine($"Nodos eliminados por estar fuera del rango: {eliminados}");
            Console.WriteLine($"Nodos restantes en la lista: {lista.Cantidad}");

            lista.Imprimir("Lista final (solo valores dentro del rango):");

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }

        static int LeerEntero(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int valor))
                    return valor;

                Console.WriteLine("Entrada inválida. Por favor ingresa un número entero.");
            }
        }
    }
}
