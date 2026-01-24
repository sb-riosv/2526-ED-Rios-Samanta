using System;
using System.Collections.Generic;

/// <summary>
/// Programa para verificar si una expresión tiene paréntesis, llaves y corchetes balanceados.
/// </summary>
class Program
{
    static void Main()
    {
        Console.WriteLine("Ingrese una expresión matemática:");
        string expresion = Console.ReadLine();

        bool balanceada = EstaBalanceada(expresion);

        if (balanceada)
            Console.WriteLine("Salida esperada: Fórmula balanceada.");
        else
            Console.WriteLine("Salida esperada: Fórmula NO balanceada.");
    }

    /// <summary>
    /// Verifica si los símbolos (), {}, [] están correctamente balanceados usando una pila.
    /// </summary>
    /// <param name="texto">Expresión a evaluar</param>
    /// <returns>true si está balanceada, false si no</returns>
    static bool EstaBalanceada(string texto)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char c in texto)
        {
            // 1) Si es un símbolo de apertura, se apila
            if (c == '(' || c == '{' || c == '[')
            {
                pila.Push(c);
            }
            // 2) Si es un símbolo de cierre, se valida contra el tope de la pila
            else if (c == ')' || c == '}' || c == ']')
            {
                // Si no hay nada que cerrar, está mal
                if (pila.Count == 0) return false;

                char tope = pila.Pop();

                // Validar si el cierre corresponde al símbolo de apertura
                if (!EsParCorrecto(tope, c)) return false;
            }
        }

        // Si al final quedaron aperturas sin cerrar, no está balanceada
        return pila.Count == 0;
    }

    /// <summary>
    /// Determina si el par apertura-cierre es correcto.
    /// </summary>
    static bool EsParCorrecto(char apertura, char cierre)
    {
        return (apertura == '(' && cierre == ')') ||
               (apertura == '{' && cierre == '}') ||
               (apertura == '[' && cierre == ']');
    }
}
