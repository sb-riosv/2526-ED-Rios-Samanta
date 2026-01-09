using System;

class ConteoDeVocales
{
    static void Main()
    {
        Console.Write("Ingrese una palabra: ");
        string palabra = Console.ReadLine().ToLower();

        int contadorA = 0;
        int contadorE = 0;
        int contadorI = 0;
        int contadorO = 0;
        int contadorU = 0;

        foreach (char letra in palabra)
        {
            switch (letra)
            {
                case 'a':
                    contadorA++;
                    break;
                case 'e':
                    contadorE++;
                    break;
                case 'i':
                    contadorI++;
                    break;
                case 'o':
                    contadorO++;
                    break;
                case 'u':
                    contadorU++;
                    break;
            }
        }

        Console.WriteLine("\nResultado del conteo de vocales:");
        Console.WriteLine($"A: {contadorA}");
        Console.WriteLine($"E: {contadorE}");
        Console.WriteLine($"I: {contadorI}");
        Console.WriteLine($"O: {contadorO}");
        Console.WriteLine($"U: {contadorU}");

        Console.WriteLine("\nPresione cualquier tecla para finalizar...");
        Console.ReadKey();
    }
}
