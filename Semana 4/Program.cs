using System;
using System.Globalization;

namespace AET_UEA_Aportes
{
    // ================= REGISTROS / ESTRUCTURAS =================

    public struct Empleado
    {
        public int Id;
        public string Nombre;
        public string Area;
        public decimal Sueldo;

        public Empleado(int id, string nombre, string area, decimal sueldo)
        {
            Id = id;
            Nombre = nombre;
            Area = area;
            Sueldo = sueldo;
        }
    }

    public struct Aporte
    {
        public int IdEmpleado;
        public int Anio;
        public int Mes;
        public decimal Monto;
        public string Metodo;
        public string CodigoComprobante;
        public DateTime Fecha;

        public Aporte(int idEmp, int anio, int mes, decimal monto, string metodo, string codigo)
        {
            IdEmpleado = idEmp;
            Anio = anio;
            Mes = mes;
            Monto = monto;
            Metodo = metodo;
            CodigoComprobante = codigo;
            Fecha = DateTime.Now;
        }
    }

    // ================= CLASE PRINCIPAL =================

    public class SistemaAportes
    {
        private const string INSTITUCION = "Universidad Estatal Amazónica (UEA)";
        private const string ASOCIACION = "Asociación de Empleados y Trabajadores UEA (AET-UEA)";

        private Empleado[] empleados;
        private Aporte[] aportes;

        private decimal[,] matrizAportes;   // [empleado, mes]
        private bool[,] matrizEstado;       // [empleado, mes]

        private int contadorEmpleados;
        private int contadorAportes;
        private int secuenciaComprobante;

        private int anioActual;

        public SistemaAportes(int capacidad)
        {
            empleados = new Empleado[capacidad];
            aportes = new Aporte[capacidad * 12];
            matrizAportes = new decimal[capacidad, 12];
            matrizEstado = new bool[capacidad, 12];

            contadorEmpleados = 0;
            contadorAportes = 0;
            secuenciaComprobante = 1;
            anioActual = DateTime.Now.Year;
        }

        // ================= FUNCIONALIDADES =================

        public void RegistrarEmpleado(int id, string nombre, string area, decimal sueldo)
        {
            empleados[contadorEmpleados] = new Empleado(id, nombre, area, sueldo);
            contadorEmpleados++;
            Console.WriteLine("Empleado registrado correctamente.");
        }

        public void RegistrarAporte(int idEmpleado, int mes, decimal monto, string metodo)
        {
            int idx = BuscarEmpleado(idEmpleado);
            if (idx == -1)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            string codigo = GenerarComprobante();
            aportes[contadorAportes] = new Aporte(idEmpleado, anioActual, mes, monto, metodo, codigo);
            contadorAportes++;

            matrizAportes[idx, mes - 1] = monto;
            matrizEstado[idx, mes - 1] = true;

            Console.WriteLine($"Aporte registrado. Comprobante: {codigo}");
        }

        public void ReporteEmpleado(int idEmpleado)
        {
            int idx = BuscarEmpleado(idEmpleado);
            if (idx == -1)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            Console.WriteLine($"\n{ASOCIACION}");
            Console.WriteLine($"{INSTITUCION}");
            Console.WriteLine($"Empleado: {empleados[idx].Nombre} - Área: {empleados[idx].Area}");
            Console.WriteLine("Mes | Estado | Monto");

            decimal total = 0;

            for (int m = 0; m < 12; m++)
            {
                string estado = matrizEstado[idx, m] ? "Pagado" : "Pendiente";
                decimal monto = matrizAportes[idx, m];
                total += monto;
                Console.WriteLine($"{m + 1,3} | {estado,-9} | {monto,8:F2}");
            }

            Console.WriteLine($"Total anual aportado: {total:F2}");
        }

        public void ResumenAsociacion()
        {
            Console.WriteLine($"\nResumen anual de aportes - {ASOCIACION}");
            for (int m = 0; m < 12; m++)
            {
                decimal totalMes = 0;
                for (int e = 0; e < contadorEmpleados; e++)
                {
                    totalMes += matrizAportes[e, m];
                }
                Console.WriteLine($"Mes {m + 1}: {totalMes:F2}");
            }
        }

        // ================= MÉTODOS AUXILIARES =================

        private int BuscarEmpleado(int id)
        {
            for (int i = 0; i < contadorEmpleados; i++)
                if (empleados[i].Id == id) return i;
            return -1;
        }

        private string GenerarComprobante()
        {
            string codigo = $"AET-UEA-{anioActual}-{secuenciaComprobante:D6}";
            secuenciaComprobante++;
            return codigo;
        }
    }

    // ================= PROGRAMA =================

    class Program
    {
        static void Main()
        {
            CultureInfo.CurrentCulture = new CultureInfo("es-EC");

            SistemaAportes sistema = new SistemaAportes(40);

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- SISTEMA AET-UEA ---");
                Console.WriteLine("1. Registrar empleado");
                Console.WriteLine("2. Registrar aporte");
                Console.WriteLine("3. Reporte por empleado");
                Console.WriteLine("4. Resumen anual asociación");
                Console.WriteLine("0. Salir");

                Console.Write("Opción: ");
                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Área: ");
                        string area = Console.ReadLine();
                        Console.Write("Sueldo: ");
                        decimal sueldo = decimal.Parse(Console.ReadLine());
                        sistema.RegistrarEmpleado(id, nombre, area, sueldo);
                        break;

                    case 2:
                        Console.Write("ID Empleado: ");
                        int idEmp = int.Parse(Console.ReadLine());
                        Console.Write("Mes (1-12): ");
                        int mes = int.Parse(Console.ReadLine());
                        Console.Write("Monto: ");
                        decimal monto = decimal.Parse(Console.ReadLine());
                        Console.Write("Método (Nómina/Efectivo): ");
                        string metodo = Console.ReadLine();
                        sistema.RegistrarAporte(idEmp, mes, monto, metodo);
                        break;

                    case 3:
                        Console.Write("ID Empleado: ");
                        sistema.ReporteEmpleado(int.Parse(Console.ReadLine()));
                        break;

                    case 4:
                        sistema.ResumenAsociacion();
                        break;

                    case 0:
                        salir = true;
                        break;
                }
            }
        }
    }
}
