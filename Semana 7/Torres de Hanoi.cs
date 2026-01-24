using System;
using System.Collections.Generic;

/// <summary>
/// Torres de Hanoi representando torres como pilas (Stacks) y mostrando pasos.
/// </summary>
class Program
{
    static int paso = 0;

    static void Main()
    {
        Console.Write("Ingrese el número de discos: ");
        int n = int.Parse(Console.ReadLine());

        Stack<int> A = new Stack<int>();
        Stack<int> B = new Stack<int>();
        Stack<int> C = new Stack<int>();

        // Inicializar torre A con discos: n ... 3,2,1 (1 queda arriba)
        for (int i = n; i >= 1; i--)
            A.Push(i);

        Console.WriteLine("\nEstado inicial:");
        ImprimirTorres(A, B, C);

        // Resolver Hanoi
        Hanoi(n, A, C, B, 'A', 'C', 'B');

        Console.WriteLine("\nEstado final:");
        ImprimirTorres(A, B, C);
    }

    /// <summary>
    /// Resuelve Hanoi moviendo n discos desde origen hacia destino usando auxiliar.
    /// Usa pilas para representar las torres.
    /// </summary>
    static void Hanoi(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar,
                      char nomOrigen, char nomDestino, char nomAux)
    {
        if (n == 1)
        {
            MoverDisco(origen, destino, nomOrigen, nomDestino);
            return;
        }

        // Mover n-1 a auxiliar
        Hanoi(n - 1, origen, auxiliar, destino, nomOrigen, nomAux, nomDestino);

        // Mover el disco grande a destino
        MoverDisco(origen, destino, nomOrigen, nomDestino);

        // Mover n-1 desde auxiliar a destino
        Hanoi(n - 1, auxiliar, destino, origen, nomAux, nomDestino, nomOrigen);
    }

    /// <summary>
    /// Mueve el disco superior de una torre a otra (pop/push) y muestra el paso.
    /// </summary>
    static void MoverDisco(Stack<int> desde, Stack<int> hacia, char nombreDesde, char nombreHacia)
    {
        int disco = desde.Pop();

        // Validación opcional (por seguridad): no poner grande sobre pequeño
        if (hacia.Count > 0 && hacia.Peek() < disco)
        {
            throw new InvalidOperationException("Movimiento inválido: disco grande sobre disco pequeño.");
        }

        hacia.Push(disco);

        paso++;
        Console.WriteLine($"Paso {paso}: Mover disco {disco} de {nombreDesde} a {nombreHacia}");
    }

    /// <summary>
    /// Imprime el contenido de las tres torres (pila a pila).
    /// Nota: para imprimir sin destruir la pila, se convierte a arreglo.
    /// </summary>
    static void ImprimirTorres(Stack<int> A, Stack<int> B, Stack<int> C)
    {
        Console.WriteLine($"A: {FormatearPila(A)}");
        Console.WriteLine($"B: {FormatearPila(B)}");
        Console.WriteLine($"C: {FormatearPila(C)}");
        Console.WriteLine("----------------------------------");
    }

    /// <summary>
    /// Devuelve un string con el contenido de la pila de abajo hacia arriba.
    /// </summary>
    static string FormatearPila(Stack<int> torre)
    {
        int[] arr = torre.ToArray(); // top -> bottom
        Array.Reverse(arr);          // bottom -> top
        return "[" + string.Join(", ", arr) + "]";
    }
}
