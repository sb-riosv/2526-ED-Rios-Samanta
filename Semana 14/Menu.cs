using System;

namespace _2526_ED_Rios_Samanta
{
    class Menu
    {
        static void Main(string[] args)
        {
            ArbolBST arbol = new ArbolBST();
            int opcion;

            do
            {
                Console.WriteLine("\n===== MENÚ ÁRBOL BST =====");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Buscar valor");
                Console.WriteLine("3. Eliminar valor");
                Console.WriteLine("4. Recorridos (Preorden, Inorden, Postorden)");
                Console.WriteLine("5. Mostrar mínimo, máximo y altura");
                Console.WriteLine("6. Limpiar árbol");
                Console.WriteLine("7. Contar nodos (extra)");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Entrada inválida.");
                    opcion = -1;
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese valor: ");
                        if (int.TryParse(Console.ReadLine(), out int valorInsertar))
                        {
                            arbol.Insertar(valorInsertar);
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case 2:
                        Console.Write("Ingrese valor a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorBuscar))
                        {
                            bool encontrado = arbol.Buscar(valorBuscar);
                            Console.WriteLine(encontrado ? "Valor encontrado." : "Valor no encontrado.");
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case 3:
                        Console.Write("Ingrese valor a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int valorEliminar))
                        {
                            arbol.Eliminar(valorEliminar);
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case 4:
                        Console.WriteLine("\nRecorrido Inorden:");
                        arbol.Inorden();

                        Console.WriteLine("\nRecorrido Preorden:");
                        arbol.Preorden();

                        Console.WriteLine("\nRecorrido Postorden:");
                        arbol.Postorden();
                        break;

                    case 5:
                        Console.WriteLine("Valor mínimo: " + arbol.Minimo());
                        Console.WriteLine("Valor máximo: " + arbol.Maximo());
                        Console.WriteLine("Altura del árbol: " + arbol.Altura());
                        break;

                    case 6:
                        arbol.Limpiar();
                        Console.WriteLine("El árbol ha sido limpiado.");
                        break;

                    case 7:
                        Console.WriteLine("Total de nodos: " + arbol.ContarNodos());
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 0);
        }
    }
}