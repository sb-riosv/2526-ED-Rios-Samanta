using System;
using System.Collections.Generic;

class ProductoDeMatrices
{
    static void Main()
    {
        // Matriz A (2x3)
        List<List<int>> A = new List<List<int>>
        {
            new List<int> { 1, 2, 3 },
            new List<int> { 4, 5, 6 }
        };

        // Matriz B (3x2)
        List<List<int>> B = new List<List<int>>
        {
            new List<int> { -1, 0 },
            new List<int> { 0, 1 },
            new List<int> { 1, 1 }
        };

        int filasA = A.Count;
        int columnasA = A[0].Count;
        int columnasB = B[0].Count;

        // Matriz resultado (2x2)
        List<List<int>> resultado = new List<List<int>>();

        for (int i = 0; i < filasA; i++)
        {
            List<int> filaResultado = new List<int>();

            for (int j = 0; j < columnasB; j++)
            {
                int suma = 0;

                for (int k = 0; k < columnasA; k++)
                {
                    suma += A[i][k] * B[k][j];
                }

                filaResultado.Add(suma);
            }

            resultado.Add(filaResultado);
        }

        // Mostrar la matriz resultado
        Console.WriteLine("Resultado del producto A x B:\n");

        foreach (var fila in resultado)
        {
            foreach (var valor in fila)
            {
                Console.Write(valor + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nPresione cualquier tecla para finalizar...");
        Console.ReadKey();
    }
}
