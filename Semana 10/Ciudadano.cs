using System;
using System.Collections.Generic;
using System.Linq;

public class Ciudadano
{
    public int Id { get; init; }
    public string Cedula { get; init; } = "";
    public string Nombre { get; init; } = "";
    public string Apellido { get; init; } = "";
    public string Ciudad { get; init; } = "";

    // HashSet: igualdad por Id (único)
    public override bool Equals(object? obj) => obj is Ciudadano c && c.Id == Id;
    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString()
        => $"{Nombre} {Apellido} | Cédula: {Cedula} | ID: {Id} | {Ciudad}";
}

public class Program
{
    public static void Main()
    {
        const int TOTAL_CIUDADANOS = 500;
        const int TOTAL_PFIZER = 75;
        const int TOTAL_ASTRA = 75;

        var rng = new Random();

        // 1) Universo (U): 500 ciudadanos ficticios
        HashSet<Ciudadano> universo = GenerarUniverso(TOTAL_CIUDADANOS, rng);

        // 2) Pfizer (P): 75 ciudadanos del universo
        HashSet<Ciudadano> pfizer = TomarAleatorios(universo, TOTAL_PFIZER, rng);

        // 3) AstraZeneca (A): 75 ciudadanos del universo, pero EXCLUYENDO Pfizer (sin intersección)
        HashSet<Ciudadano> astra = TomarAleatorios(universo.Except(pfizer).ToHashSet(), TOTAL_ASTRA, rng);

        // ===== Operaciones de teoría de conjuntos =====

        // P ∪ A
        var unionVacunados = new HashSet<Ciudadano>(pfizer);
        unionVacunados.UnionWith(astra);

        // No vacunados: U \ (P ∪ A)
        var noVacunados = new HashSet<Ciudadano>(universo);
        noVacunados.ExceptWith(unionVacunados);

        // Ambas dosis: P ∩ A (aquí será 0 porque hicimos conjuntos disjuntos)
        var ambasDosis = new HashSet<Ciudadano>(pfizer);
        ambasDosis.IntersectWith(astra);

        // Solo Pfizer: P \ A (será 75)
        var soloPfizer = new HashSet<Ciudadano>(pfizer);
        soloPfizer.ExceptWith(astra);

        // Solo AstraZeneca: A \ P (será 75)
        var soloAstra = new HashSet<Ciudadano>(astra);
        soloAstra.ExceptWith(pfizer);

        // ===== Reporte =====
        ImprimirListado("No vacunados (U \\ (P ∪ A))", noVacunados, 12);
        ImprimirListado("Ambas dosis (P ∩ A)", ambasDosis, 12);
        ImprimirListado("Solo Pfizer (P \\ A)", soloPfizer, 12);
        ImprimirListado("Solo AstraZeneca (A \\ P)", soloAstra, 12);

       
        Console.WriteLine("\n=== RESUMEN (Validación) ===");
        Console.WriteLine($"Total ciudadanos (U): {universo.Count}");
        Console.WriteLine($"Pfizer (P): {pfizer.Count}");
        Console.WriteLine($"AstraZeneca (A): {astra.Count}");
        Console.WriteLine($"Ambas dosis (P ∩ A): {ambasDosis.Count}");
        Console.WriteLine($"Vacunados (P ∪ A): {unionVacunados.Count}");
        Console.WriteLine($"No vacunados (U \\ (P ∪ A)): {noVacunados.Count}");
    }

    private static HashSet<Ciudadano> GenerarUniverso(int total, Random rng)
    {
        var nombres = new[]
        {
            "Ana","Luis","Carlos","María","Sofía","Pedro","Valentina","Jorge","Diana","Andrés",
            "Camila","Mateo","Daniela","Gabriel","Paula","Sebastián","Alejandra","Diego","Fernanda","Kevin"
        };

        var apellidos = new[]
        {
            "Ríos","García","Sánchez","Mendoza","Paredes","Villacís","Moreira","Zambrano","Cedeño","Torres",
            "Ortiz","López","Vera","Martínez","Castro","Silva","Navarro","Chávez","Romero","Salazar"
        };

        var ciudades = new[]
        {
            "Quito","Guayaquil","Cuenca","Ambato","Manta","Portoviejo","Loja","Machala",
            "Tena","Puyo","Lago Agrio","Ibarra","Riobamba","Santo Domingo"
        };

        var universo = new HashSet<Ciudadano>();

        for (int id = 1; id <= total; id++)
        {
            string nombre = nombres[rng.Next(nombres.Length)];
            string apellido = apellidos[rng.Next(apellidos.Length)];
            string ciudad = ciudades[rng.Next(ciudades.Length)];

            // Cédula ficticia tipo EC + 8 dígitos + control simple
            string cedula = GenerarCedulaFicticia(id, rng);

            universo.Add(new Ciudadano
            {
                Id = id,
                Nombre = nombre,
                Apellido = apellido,
                Ciudad = ciudad,
                Cedula = cedula
            });
        }

        return universo;
    }

    private static string GenerarCedulaFicticia(int id, Random rng)
    {
        
        int ochoDigitos = rng.Next(10_000_000, 99_999_999);
        int digitoControl = (id + (ochoDigitos % 10)) % 10;
        return $"EC-{ochoDigitos}-{digitoControl}";
    }

    // Toma "cantidad" elementos aleatorios de un conjunto
    private static HashSet<Ciudadano> TomarAleatorios(HashSet<Ciudadano> fuente, int cantidad, Random rng)
    {
        if (cantidad > fuente.Count)
            throw new ArgumentException("La cantidad solicitada excede el tamaño del conjunto fuente.");

        return fuente
            .OrderBy(_ => rng.Next())
            .Take(cantidad)
            .ToHashSet();
    }

    private static void ImprimirListado(string titulo, HashSet<Ciudadano> conjunto, int maxMuestra)
    {
        Console.WriteLine($"\n--- {titulo} | Total: {conjunto.Count} ---");

        foreach (var c in conjunto.OrderBy(x => x.Apellido).ThenBy(x => x.Nombre).Take(maxMuestra))
            Console.WriteLine(c);

        if (conjunto.Count > maxMuestra)
            Console.WriteLine($"... (mostrando {maxMuestra} de {conjunto.Count})");
    }
}