using System;

namespace ComparacionPorContenidoYTamano
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
        public int Cantidad { get; private set; }

        public ListaEnlazada()
        {
            cabeza = null;
            Cantidad = 0;
        }

        // Carga por el INICIO
        public void InsertarAlInicio(int valor)
        {
            Nodo nuevo = new Nodo(valor);
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
            Cantidad++;
        }

        public Nodo ObtenerCabeza() => cabeza;

        public void Imprimir(string titulo)
        {
            Console.WriteLine($"\n{titulo}");
            if (cabeza == null)
            {
                Console.WriteLine("(lista vacía)");
                return;
            }

            Nodo actual = cabeza;
            while (actual != null)
            {
                Console.Write(actual.Valor);
                if (actual.Siguiente != null) Console.Write(" -> ");
                actual = actual.Siguiente;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Comparación de dos Listas Enlazadas (carga por el inicio) ===\n");

            int n1 = LeerEnteroPositivo("Ingrese la cantidad de datos para la PRIMERA lista: ");
            ListaEnlazada lista1 = new ListaEnlazada();
            CargarListaPorInicio(lista1, n1, "PRIMERA");

            int n2 = LeerEnteroPositivo("Ingrese la cantidad de datos para la SEGUNDA lista: ");
            ListaEnlazada lista2 = new ListaEnlazada();
            CargarListaPorInicio(lista2, n2, "SEGUNDA");

            lista1.Imprimir("Lista 1 (orden final en la lista, carga por inicio):");
            lista2.Imprimir("Lista 2 (orden final en la lista, carga por inicio):");

            bool mismoTamano = (lista1.Cantidad == lista2.Cantidad);
            bool mismoContenidoYOrden = SonIgualesEnContenidoYOrden(lista1, lista2);

            Console.WriteLine("\n--- Resultado de verificación ---");

            if (mismoTamano && mismoContenidoYOrden)
            {
                // a. iguales en tamaño y en contenido (mismo orden)
                Console.WriteLine("a) Las listas son iguales en tamaño y en contenido (mismo orden).");
            }
            else if (mismoTamano && !mismoContenidoYOrden)
            {
                // b. iguales en tamaño pero no en contenido (o no en el mismo orden)
                Console.WriteLine("b) Las listas son iguales en tamaño, pero no son iguales en contenido y/o orden.");
            }
            else
            {
                // c. no tienen mismo tamaño ni contenido (por requisito, se reporta así)
                Console.WriteLine("c) No tienen el mismo tamaño ni contenido.");
            }

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }

        static void CargarListaPorInicio(ListaEnlazada lista, int cantidad, string etiqueta)
        {
            Console.WriteLine($"\n--- Carga de la lista {etiqueta} (se inserta por el INICIO) ---");
            for (int i = 1; i <= cantidad; i++)
            {
                int valor = LeerEntero($"Ingrese el dato #{i}: ");
                lista.InsertarAlInicio(valor);
            }
        }

        static bool SonIgualesEnContenidoYOrden(ListaEnlazada a, ListaEnlazada b)
        {
            if (a.Cantidad != b.Cantidad) return false;

            Nodo actualA = a.ObtenerCabeza();
            Nodo actualB = b.ObtenerCabeza();

            while (actualA != null && actualB != null)
            {
                if (actualA.Valor != actualB.Valor)
                    return false;

                actualA = actualA.Siguiente;
                actualB = actualB.Siguiente;
            }

            // Si llegaron ambos a null a la vez, son iguales
            return actualA == null && actualB == null;
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

        static int LeerEnteroPositivo(string mensaje)
        {
            while (true)
            {
                int valor = LeerEntero(mensaje);
                if (valor >= 0) return valor;

                Console.WriteLine("La cantidad no puede ser negativa. Intenta nuevamente.");
            }
        }
    }
}
