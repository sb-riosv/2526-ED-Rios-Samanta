using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CongresoAuditorio
{
    // -----------------------------
    // MODELO (POO)
    // -----------------------------
    public class Asistente
    {
        public int Id { get; }
        public string Nombre { get; }
        public int Registrador { get; }          // 1 o 2
        public long OrdenLlegada { get; }        // Timestamp lógico (ticks)
        public int? AsientoAsignado { get; set; }

        public Asistente(int id, string nombre, int registrador, long ordenLlegada)
        {
            Id = id;
            Nombre = nombre;
            Registrador = registrador;
            OrdenLlegada = ordenLlegada;
            AsientoAsignado = null;
        }

        public override string ToString()
        {
            string asiento = AsientoAsignado.HasValue ? AsientoAsignado.Value.ToString() : "SIN ASIENTO";
            return $"ID:{Id} | {Nombre} | Reg:{Registrador} | Llegada:{OrdenLlegada} | Asiento:{asiento}";
        }
    }

    public class Auditorio
    {
        private readonly int _capacidad;
        private int _siguienteAsiento;

        public int Capacidad => _capacidad;
        public int AsientosAsignados => _siguienteAsiento - 1;
        public int AsientosDisponibles => _capacidad - AsientosAsignados;

        public Auditorio(int capacidad)
        {
            if (capacidad <= 0) throw new ArgumentException("La capacidad debe ser mayor a 0.");
            _capacidad = capacidad;
            _siguienteAsiento = 1;
        }

        public bool HayCupo()
        {
            return _siguienteAsiento <= _capacidad;
        }

        public int? AsignarAsiento()
        {
            if (!HayCupo()) return null;
            return _siguienteAsiento++;
        }
    }

    /// <summary>
    /// Administra dos líneas de registro y una cola final (FIFO) que respeta el orden de llegada.
    /// Incluye reportería y medición de tiempo.
    /// </summary>
    public class SistemaCongreso
    {
        private readonly Queue<Asistente> _colaIngreso;          // Cola principal (FIFO)
        private readonly List<Asistente> _asistentesRegistrados; // Historial de registro
        private readonly List<Asistente> _asistentesConAsiento;  // Historial de asignación

        private readonly Auditorio _auditorio;

        private int _contadorId;
        private long _contadorLlegada; // Orden lógico incremental para asegurar FIFO global

        public SistemaCongreso(int capacidadAuditorio)
        {
            _colaIngreso = new Queue<Asistente>();
            _asistentesRegistrados = new List<Asistente>();
            _asistentesConAsiento = new List<Asistente>();
            _auditorio = new Auditorio(capacidadAuditorio);

            _contadorId = 1;
            _contadorLlegada = 1;
        }

        // -----------------------------
        // REGISTRO (2 líneas)
        // -----------------------------
        public void RegistrarAsistente(string nombre, int registrador)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Nombre inválido.");
                return;
            }

            if (registrador != 1 && registrador != 2)
            {
                Console.WriteLine("Registrador inválido. Debe ser 1 o 2.");
                return;
            }

            // OrdenLlegada incremental asegura el orden global aunque existan dos registradores
            var asistente = new Asistente(_contadorId++, nombre.Trim(), registrador, _contadorLlegada++);
            _colaIngreso.Enqueue(asistente);
            _asistentesRegistrados.Add(asistente);

            Console.WriteLine($" Registrado y encolado: {asistente.Nombre} (Reg {registrador})");
        }

        // -----------------------------
        // PROCESO: Asignación de asientos
        // -----------------------------
        public void AsignarAsientosHastaLlenar(out long ticksConsumidos, out int procesados)
        {
            var sw = Stopwatch.StartNew();

            procesados = 0;

            while (_colaIngreso.Count > 0 && _auditorio.HayCupo())
            {
                var asistente = _colaIngreso.Dequeue();
                int? asiento = _auditorio.AsignarAsiento();

                if (asiento.HasValue)
                {
                    asistente.AsientoAsignado = asiento.Value;
                    _asistentesConAsiento.Add(asistente);
                    procesados++;
                }
            }

            sw.Stop();
            ticksConsumidos = sw.ElapsedTicks;
        }

        // -----------------------------
        // REPORTERÍA (consultas)
        // -----------------------------
        public void MostrarEstadoGeneral()
        {
            Console.WriteLine("\n===== ESTADO GENERAL =====");
            Console.WriteLine($"Capacidad auditorio: {_auditorio.Capacidad}");
            Console.WriteLine($"Asientos asignados:  {_auditorio.AsientosAsignados}");
            Console.WriteLine($"Asientos disponibles:{_auditorio.AsientosDisponibles}");
            Console.WriteLine($"En cola de ingreso:  {_colaIngreso.Count}");
            Console.WriteLine($"Total registrados:   {_asistentesRegistrados.Count}");
            Console.WriteLine("==========================\n");
        }

        public void MostrarColaIngreso()
        {
            Console.WriteLine("\n===== COLA DE INGRESO (FIFO) =====");

            if (_colaIngreso.Count == 0)
            {
                Console.WriteLine("No hay asistentes en cola.");
                return;
            }

            // Para no perder la cola, la recorremos sin desencolar
            foreach (var a in _colaIngreso)
                Console.WriteLine(a);

            Console.WriteLine("==================================\n");
        }

        public void MostrarAsistentesConAsiento()
        {
            Console.WriteLine("\n===== ASISTENTES CON ASIENTO =====");

            if (_asistentesConAsiento.Count == 0)
            {
                Console.WriteLine("Aún no se han asignado asientos.");
                return;
            }

            foreach (var a in _asistentesConAsiento.OrderBy(x => x.AsientoAsignado))
                Console.WriteLine(a);

            Console.WriteLine("==================================\n");
        }

        public void BuscarPorId(int id)
        {
            var encontrado = _asistentesRegistrados.FirstOrDefault(x => x.Id == id);

            Console.WriteLine("\n===== BÚSQUEDA POR ID =====");
            if (encontrado == null)
            {
                Console.WriteLine($"No existe un asistente con ID {id}.");
            }
            else
            {
                Console.WriteLine(encontrado);
            }
            Console.WriteLine("===========================\n");
        }

        public void BuscarPorNombre(string nombre)
        {
            Console.WriteLine("\n===== BÚSQUEDA POR NOMBRE =====");

            var coincidencias = _asistentesRegistrados
                .Where(x => x.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            if (coincidencias.Count == 0)
            {
                Console.WriteLine("No hay coincidencias.");
            }
            else
            {
                foreach (var a in coincidencias)
                    Console.WriteLine(a);
            }

            Console.WriteLine("================================\n");
        }
    }

    // -----------------------------
    // PROGRAMA PRINCIPAL (MENÚ)
    // -----------------------------
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Sistema: Congreso - Asignación de 100 asientos ===");
            var sistema = new SistemaCongreso(capacidadAuditorio: 100);

            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("1) Registrar asistente (Registrador 1)");
                Console.WriteLine("2) Registrar asistente (Registrador 2)");
                Console.WriteLine("3) Asignar asientos hasta llenar / hasta vaciar cola");
                Console.WriteLine("4) Reporte: Estado general");
                Console.WriteLine("5) Reporte: Ver cola de ingreso");
                Console.WriteLine("6) Reporte: Ver asistentes con asiento");
                Console.WriteLine("7) Consultar por ID");
                Console.WriteLine("8) Consultar por nombre");
                Console.WriteLine("9) Cargar datos de prueba (rápido)");
                Console.WriteLine("0) Salir");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Registrar(sistema, 1);
                        break;

                    case "2":
                        Registrar(sistema, 2);
                        break;

                    case "3":
                        sistema.AsignarAsientosHastaLlenar(out long ticks, out int procesados);
                        Console.WriteLine($" Proceso completado. Asistentes procesados: {procesados}");
                        Console.WriteLine($" Tiempo (ticks): {ticks}");
                        break;

                    case "4":
                        sistema.MostrarEstadoGeneral();
                        break;

                    case "5":
                        sistema.MostrarColaIngreso();
                        break;

                    case "6":
                        sistema.MostrarAsistentesConAsiento();
                        break;

                    case "7":
                        Console.Write("Ingrese ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                            sistema.BuscarPorId(id);
                        else
                            Console.WriteLine("ID inválido.");
                        break;

                    case "8":
                        Console.Write("Ingrese nombre (o parte): ");
                        string nombre = Console.ReadLine() ?? "";
                        sistema.BuscarPorNombre(nombre);
                        break;

                    case "9":
                        CargarPrueba(sistema);
                        break;

                    case "0":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }

            Console.WriteLine("Programa finalizado.");
        }

        static void Registrar(SistemaCongreso sistema, int registrador)
        {
            Console.Write("Nombre del asistente: ");
            string nombre = Console.ReadLine() ?? "";
            sistema.RegistrarAsistente(nombre, registrador);
        }

        static void CargarPrueba(SistemaCongreso sistema)
        {
            Console.Write("¿Cuántos asistentes de prueba desea registrar? (Ej: 120): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Cantidad inválida.");
                return;
            }

            // Alterna registradores 1 y 2 para simular dos líneas
            for (int i = 1; i <= n; i++)
            {
                int reg = (i % 2 == 0) ? 2 : 1;
                sistema.RegistrarAsistente($"Asistente_{i}", reg);
            }

            Console.WriteLine($" Se registraron {n} asistentes de prueba en dos líneas.");
        }
    }
}
