using System;
using System.Collections.Generic;

class RegistroDeNotasTIC
{
    static void Main()
    {
        // Lista de asignaturas de Tecnologías de la Información
        List<string> asignaturas = new List<string>
        {
            "Programación",
            "Fundamentos de Tecnologías de la Información",
            "Bases de Datos",
            "Sistemas Operativos",
            "Redes de Computadoras",
            "Ingeniería de Software",
            "Seguridad Informática",
            "Desarrollo Web"
        };

        // Lista para almacenar las notas
        List<double> notas = new List<double>();

        Console.WriteLine("REGISTRO DE NOTAS - TECNOLOGÍAS DE LA INFORMACIÓN\n");

        // Solicitar la nota para cada asignatura
        foreach (string asignatura in asignaturas)
        {
            double nota;
            bool notaValida;

            do
            {
                Console.Write($"Ingrese la nota obtenida en {asignatura}: ");
                notaValida = double.TryParse(Console.ReadLine(), out nota);

                if (!notaValida)
                {
                    Console.WriteLine("Entrada inválida. Por favor ingrese un número.\n");
                }

            } while (!notaValida);

            notas.Add(nota);
        }

        Console.WriteLine("\nRESULTADOS\n");

        // Mostrar los resultados
        for (int i = 0; i < asignaturas.Count; i++)
        {
            Console.WriteLine($"En {asignaturas[i]} has sacado {notas[i]}");
        }

        Console.WriteLine("\nPresione cualquier tecla para finalizar...");
        Console.ReadKey();
    }
}
