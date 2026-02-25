using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Diccionario inicial español -> inglés (puedo seguir agregando más)
        Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"tiempo", "time"},
            {"persona", "person"},
            {"año", "year"},
            {"camino", "way"},
            {"forma", "way"},
            {"día", "day"},
            {"cosa", "thing"},
            {"hombre", "man"},
            {"mundo", "world"},
            {"vida", "life"},
            {"mano", "hand"},
            {"parte", "part"},
            {"niño", "child"},
            {"niña", "child"},
            {"ojo", "eye"},
            {"mujer", "woman"},
            {"lugar", "place"},
            {"trabajo", "work"},
            {"semana", "week"},
            {"caso", "case"},
            {"punto", "point"},
            {"gobierno", "government"},
            {"empresa", "company"},
            {"compañía", "company"}
        };

        int opcion;

        // El programa se mantiene corriendo hasta que el usuario elija salir
        do
        {
            MostrarMenu();

            // Validación para evitar errores si ingresan letras
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida. Intente nuevamente.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    TraducirFrase(diccionario);
                    break;

                case 2:
                    AgregarPalabra(diccionario);
                    break;

                case 3:
                    VerDiccionario(diccionario);
                    break;

                case 4:
                    BuscarPalabra(diccionario);
                    break;

                case 5:
                    EliminarPalabra(diccionario);
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

    static void MostrarMenu()
    {
        Console.WriteLine("\n==================== MENÚ ====================");
        Console.WriteLine("1. Traducir una frase");
        Console.WriteLine("2. Agregar palabras al diccionario");
        Console.WriteLine("3. Ver palabras del diccionario");
        Console.WriteLine("4. Buscar una palabra");
        Console.WriteLine("5. Eliminar una palabra");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
    }

    // Aquí el usuario ingresa una frase y se traduce parcialmente
    static void TraducirFrase(Dictionary<string, string> diccionario)
    {
        Console.Write("\nIngrese una frase: ");
        string frase = Console.ReadLine() ?? "";

        int totalPalabras, traducidas;

        // Se traduce la frase y también se obtienen estadísticas
        string resultado = TraducirFraseConIndicador(frase, diccionario, out totalPalabras, out traducidas);

        Console.WriteLine("\nTraducción (parcial):");
        Console.WriteLine(resultado);

        MostrarIndicador(totalPalabras, traducidas);
    }

    // Esta función separa la frase en partes para no perder comas, puntos, etc.
    static string TraducirFraseConIndicador(string frase,
        Dictionary<string, string> diccionario,
        out int totalPalabras,
        out int traducidas)
    {
        totalPalabras = 0;
        traducidas = 0;

        // Divide la frase en palabras, espacios y signos
        var tokens = Regex.Matches(frase, @"\p{L}+|[^\p{L}\s]+|\s+")
                          .Select(m => m.Value)
                          .ToList();

        for (int i = 0; i < tokens.Count; i++)
        {
            string t = tokens[i];

            // Solo se procesan palabras (no signos)
            if (Regex.IsMatch(t, @"^\p{L}+$"))
            {
                totalPalabras++;

                // Si la palabra existe en el diccionario se reemplaza
                if (diccionario.TryGetValue(t, out string traduccion))
                {
                    tokens[i] = traduccion;
                    traducidas++;
                }
            }
        }

        // Se reconstruye la frase ya traducida
        return string.Concat(tokens);
    }

    // Muestra qué tan buena fue la traducción
    static void MostrarIndicador(int total, int traducidas)
    {
        double porcentaje = total == 0 ? 0 : (double)traducidas / total * 100;

        Console.WriteLine($"\nCalidad de traducción: {traducidas}/{total} palabras traducidas ({porcentaje:F2}%)");

        if (porcentaje < 50)
        {
            Console.WriteLine("Sugerencia: agregue más palabras al diccionario.");
        }
    }

    // Permite agregar nuevas palabras
    static void AgregarPalabra(Dictionary<string, string> diccionario)
    {
        Console.Write("\nIngrese palabra en español: ");
        string es = (Console.ReadLine() ?? "").Trim();

        Console.Write("Ingrese traducción en inglés: ");
        string en = (Console.ReadLine() ?? "").Trim();

        // Validación básica
        if (string.IsNullOrWhiteSpace(es) || string.IsNullOrWhiteSpace(en))
        {
            Console.WriteLine("No se puede agregar datos vacíos.");
            return;
        }

        // Si ya existe, se pregunta si se quiere actualizar
        if (diccionario.ContainsKey(es))
        {
            Console.WriteLine("La palabra ya existe.");
            Console.Write("¿Desea actualizarla? (S/N): ");
            string resp = Console.ReadLine().ToUpper();

            if (resp == "S")
            {
                diccionario[es] = en;
                Console.WriteLine("Actualizada correctamente.");
            }
        }
        else
        {
            diccionario.Add(es, en);
            Console.WriteLine("Palabra agregada.");
        }
    }

    // Muestra todo el diccionario ordenado
    static void VerDiccionario(Dictionary<string, string> diccionario)
    {
        Console.WriteLine("\n--- Diccionario ---");

        foreach (var item in diccionario.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{item.Key} -> {item.Value}");
        }

        Console.WriteLine($"Total de palabras: {diccionario.Count}");
    }

    // Busca una palabra específica
    static void BuscarPalabra(Dictionary<string, string> diccionario)
    {
        Console.Write("\nIngrese palabra a buscar: ");
        string palabra = Console.ReadLine();

        if (diccionario.TryGetValue(palabra, out string traduccion))
        {
            Console.WriteLine($"Traducción: {traduccion}");
        }
        else
        {
            Console.WriteLine("No existe en el diccionario.");
        }
    }

    // Elimina una palabra si existe
    static void EliminarPalabra(Dictionary<string, string> diccionario)
    {
        Console.Write("\nIngrese palabra a eliminar: ");
        string palabra = Console.ReadLine();

        if (diccionario.Remove(palabra))
        {
            Console.WriteLine("Eliminada correctamente.");
        }
        else
        {
            Console.WriteLine("La palabra no existe.");
        }
    }
}