using System;
using System.Collections.Generic;

class Abecedario
{
    static void Main()
    {
        // Crear la lista del abecedario
        List<char> letras = new List<char>();

        for (char c = 'A'; c <= 'Z'; c++)
        {
            letras.Add(c);
        }

        // Eliminar letras en posiciones múltiplos de 3 (posiciones desde 1)
        for (int i = letras.Count; i >= 1; i--)
        {
            if (i % 3 == 0)
            {
                letras.RemoveAt(i - 1);
            }
        }

        // Mostrar la lista resultante
        Console.WriteLine("Abecedario resultante:\n");

        foreach (char letra in letras)
        {
            Console.Write(letra + " ");
        }

        Console.WriteLine("\n\nPresione cualquier tecla para finalizar...");
        Console.ReadKey();
    }
}
