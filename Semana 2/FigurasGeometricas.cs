using System;

namespace RomboYTrapecio
{
    // =========================================================
    // CLASE ROMBO
    // =========================================================
    public class Rombo
    {
        // Atributos privados que encapsulan los datos del rombo
        private double diagonalMayor;
        private double diagonalMenor;
        private double lado;

        // Constructor para asignar valores iniciales al rombo
        public Rombo(double dMayor, double dMenor, double ladoRombo)
        {
            diagonalMayor = dMayor;
            diagonalMenor = dMenor;
            lado = ladoRombo;
        }

        // CalcularArea es un método que devuelve un valor double.
        // Se utiliza para calcular el área del rombo con la fórmula:
        // (DiagonalMayor * DiagonalMenor) / 2
        public double CalcularArea()
        {
            return (diagonalMayor * diagonalMenor) / 2;
        }

        // CalcularPerimetro es un método que devuelve un valor double.
        // Se utiliza para calcular el perímetro del rombo
        // multiplicando el valor del lado por 4.
        public double CalcularPerimetro()
        {
            return 4 * lado;
        }
    }

    // =========================================================
    // CLASE TRAPECIO
    // =========================================================
    public class Trapecio
    {
        // Atributos privados que encapsulan los datos del trapecio
        private double baseMayor;
        private double baseMenor;
        private double altura;
        private double lado1;
        private double lado2;

        // Constructor para asignar valores iniciales al trapecio
        public Trapecio(double bMayor, double bMenor, double h, double l1, double l2)
        {
            baseMayor = bMayor;
            baseMenor = bMenor;
            altura = h;
            lado1 = l1;
            lado2 = l2;
        }

        // CalcularArea es un método que devuelve un valor double.
        // Se utiliza para calcular el área del trapecio con la fórmula:
        // (BaseMayor + BaseMenor) * Altura / 2
        public double CalcularArea()
        {
            return (baseMayor + baseMenor) * altura / 2;
        }

        // CalcularPerimetro es un método que devuelve un valor double.
        // Se utiliza para calcular el perímetro del trapecio
        // sumando todos sus lados.
        public double CalcularPerimetro()
        {
            return baseMayor + baseMenor + lado1 + lado2;
        }
    }

    // =========================================================
    // CLASE PRINCIPAL DE PRUEBA
    // =========================================================
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un objeto Rombo
            Rombo miRombo = new Rombo(8, 6, 5);

            Console.WriteLine("ROMBO");
            Console.WriteLine("Área: " + miRombo.CalcularArea());
            Console.WriteLine("Perímetro: " + miRombo.CalcularPerimetro());

            Console.WriteLine("----------------------------");

            // Crear un objeto Trapecio
            Trapecio miTrapecio = new Trapecio(10, 6, 4, 5, 5);

            Console.WriteLine("TRAPECIO");
            Console.WriteLine("Área: " + miTrapecio.CalcularArea());
            Console.WriteLine("Perímetro: " + miTrapecio.CalcularPerimetro());

            Console.ReadKey();
        }
    }
}
