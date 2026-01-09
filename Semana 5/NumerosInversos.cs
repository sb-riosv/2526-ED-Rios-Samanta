using System;
using System.Collections.Generic;

class NumerosInversos
{
    static void Main()
    {
        // Crear la lista de números
        List<int> numeros = new List<int>();

        // Agregar los números del 1 al 10
        for (int i = 1; i <= 10; i++)
        {
            numeros.Add(i);
        }

        // Mostrar los números en orden inverso
        for (int i = numeros.Count - 1; i >= 0; i--)
        {
            Console.Write(numeros[i]);

            // Evitar la coma al final
            if (i != 0)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine("\n\nPresione cualquier tecla para finalizar...");
        Console.ReadKey();
    }
}
