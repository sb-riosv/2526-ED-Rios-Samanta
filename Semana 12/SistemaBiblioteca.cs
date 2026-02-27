// SistemaBiblioteca.cs
// Aplicación de consola: Registro y gestión básica de libros usando Mapas (Dictionary) y Conjuntos (HashSet)

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SistemaBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            biblioteca.AgregarLibro(new Libro("978-0132350884", "Clean Code", "Robert C. Martin", "Programación"));
            biblioteca.AgregarLibro(new Libro("978-0201633610", "Design Patterns", "Erich Gamma", "Programación"));
            biblioteca.AgregarLibro(new Libro("978-8499924217", "El Quijote", "Miguel de Cervantes", "Literatura"));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== SISTEMA BIBLIOTECA ==========");
                Console.WriteLine("1. Registrar libro");
                Console.WriteLine("2. Listar todos los libros");
                Console.WriteLine("3. Buscar libro por ISBN");
                Console.WriteLine("4. Listar libros por autor");
                Console.WriteLine("5. Listar libros por categoría");
                Console.WriteLine("6. Prestar libro");
                Console.WriteLine("7. Devolver libro");
                Console.WriteLine("8. Ver libros disponibles");
                Console.WriteLine("9. Ver libros prestados");
                Console.WriteLine("10. Reporte general");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine()?.Trim() ?? "";

                switch (opcion)
                {
                    case "1":
                        RegistrarLibroUI(biblioteca);
                        break;
                    case "2":
                        MedirYMostrar("Listar todos los libros", () => biblioteca.ListarTodos());
                        Pausa();
                        break;
                    case "3":
                        BuscarPorIsbnUI(biblioteca);
                        break;
                    case "4":
                        ListarPorAutorUI(biblioteca);
                        break;
                    case "5":
                        ListarPorCategoriaUI(biblioteca);
                        break;
                    case "6":
                        PrestarUI(biblioteca);
                        break;
                    case "7":
                        DevolverUI(biblioteca);
                        break;
                    case "8":
                        MedirYMostrar("Listar disponibles", () => biblioteca.ListarDisponibles());
                        Pausa();
                        break;
                    case "9":
                        MedirYMostrar("Listar prestados", () => biblioteca.ListarPrestados());
                        Pausa();
                        break;
                    case "10":
                        MedirYMostrar("Reporte general", () => biblioteca.ReporteGeneral());
                        Pausa();
                        break;
                    case "0":
                        Console.WriteLine("Saliendo...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Pausa();
                        break;
                }
            }
        }

        // ------------------- UI (Interfaz consola) -------------------

        static void RegistrarLibroUI(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== Registrar libro ===");

            string isbn = LeerNoVacio("ISBN: ");
            string titulo = LeerNoVacio("Título: ");
            string autor = LeerNoVacio("Autor: ");
            string categoria = LeerNoVacio("Categoría: ");

            Libro libro = new Libro(isbn, titulo, autor, categoria);

            MedirYMostrar("Registrar libro", () =>
            {
                bool ok = biblioteca.AgregarLibro(libro);
                Console.WriteLine(ok ? "Libro registrado correctamente." : "No se registró: ISBN duplicado o inválido.");
            });

            Pausa();
        }

        static void BuscarPorIsbnUI(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== Buscar por ISBN ===");

            string isbn = LeerNoVacio("Ingrese ISBN: ");

            MedirYMostrar("Buscar por ISBN", () =>
            {
                if (biblioteca.BuscarPorIsbn(isbn, out Libro? libro))
                {
                    Console.WriteLine("Resultado:");
                    Console.WriteLine(libro!.ToString());
                    Console.WriteLine("Estado: " + (biblioteca.EstaPrestado(isbn) ? "PRESTADO" : "DISPONIBLE"));
                }
                else
                {
                    Console.WriteLine("No se encontró un libro con ese ISBN.");
                }
            });

            Pausa();
        }

        static void ListarPorAutorUI(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== Listar por autor ===");

            string autor = LeerNoVacio("Autor: ");

            MedirYMostrar("Listar por autor", () =>
            {
                biblioteca.ListarPorAutor(autor);
            });

            Pausa();
        }

        static void ListarPorCategoriaUI(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== Listar por categoría ===");

            string categoria = LeerNoVacio("Categoría: ");

            MedirYMostrar("Listar por categoría", () =>
            {
                biblioteca.ListarPorCategoria(categoria);
            });

            Pausa();
        }

        static void PrestarUI(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== Prestar libro ===");

            string isbn = LeerNoVacio("ISBN del libro a prestar: ");
            string usuario = LeerNoVacio("Nombre del usuario: ");

            MedirYMostrar("Prestar libro", () =>
            {
                bool ok = biblioteca.Prestar(isbn, usuario, out string mensaje);
                Console.WriteLine(mensaje);
            });

            Pausa();
        }

        static void DevolverUI(Biblioteca biblioteca)
        {
            Console.Clear();
            Console.WriteLine("=== Devolver libro ===");

            string isbn = LeerNoVacio("ISBN del libro a devolver: ");

            MedirYMostrar("Devolver libro", () =>
            {
                bool ok = biblioteca.Devolver(isbn, out string mensaje);
                Console.WriteLine(mensaje);
            });

            Pausa();
        }

        static string LeerNoVacio(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? valor = Console.ReadLine();
                valor = valor?.Trim();

                if (!string.IsNullOrWhiteSpace(valor))
                    return valor;

                Console.WriteLine("Este campo no puede estar vacío.");
            }
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.Write("Presione ENTER para continuar...");
            Console.ReadLine();
        }

        static void MedirYMostrar(string nombreOperacion, Action accion)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            accion();

            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"[Tiempo] {nombreOperacion}: {sw.Elapsed.TotalMilliseconds:F3} ms");
        }
    }

    // ------------------- Modelo -------------------

    internal class Libro
    {
        public string Isbn { get; }
        public string Titulo { get; }
        public string Autor { get; }
        public string Categoria { get; }

        public Libro(string isbn, string titulo, string autor, string categoria)
        {
            Isbn = (isbn ?? "").Trim();
            Titulo = (titulo ?? "").Trim();
            Autor = (autor ?? "").Trim();
            Categoria = (categoria ?? "").Trim();
        }

        public override string ToString()
        {
            return $"ISBN: {Isbn}\nTítulo: {Titulo}\nAutor: {Autor}\nCategoría: {Categoria}";
        }
    }

    // ------------------- Lógica de negocio / Estructuras -------------------

    internal class Biblioteca
    {
        // MAPA principal: ISBN -> Libro
        private readonly Dictionary<string, Libro> librosPorIsbn =
            new Dictionary<string, Libro>(StringComparer.OrdinalIgnoreCase);

        // Índices (MAPAS + CONJUNTOS) para búsquedas por autor/categoría
        private readonly Dictionary<string, HashSet<string>> isbnsPorAutor =
            new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, HashSet<string>> isbnsPorCategoria =
            new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        // CONJUNTOS para estados
        private readonly HashSet<string> disponibles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> prestados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // MAPA extra (mejora): ISBN -> Usuario que lo tiene prestado
        private readonly Dictionary<string, string> prestamoPorIsbn =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public bool AgregarLibro(Libro libro)
        {
            if (libro == null) return false;
            if (string.IsNullOrWhiteSpace(libro.Isbn)) return false;

            // Evita duplicados (propio de un mapa con clave única)
            if (librosPorIsbn.ContainsKey(libro.Isbn))
                return false;

            librosPorIsbn[libro.Isbn] = libro;

            // Actualiza índices por autor
            if (!isbnsPorAutor.ContainsKey(libro.Autor))
                isbnsPorAutor[libro.Autor] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            isbnsPorAutor[libro.Autor].Add(libro.Isbn);

            // Actualiza índices por categoría
            if (!isbnsPorCategoria.ContainsKey(libro.Categoria))
                isbnsPorCategoria[libro.Categoria] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            isbnsPorCategoria[libro.Categoria].Add(libro.Isbn);

            // Por defecto queda disponible
            disponibles.Add(libro.Isbn);
            prestados.Remove(libro.Isbn);
            prestamoPorIsbn.Remove(libro.Isbn);

            return true;
        }

        public bool BuscarPorIsbn(string isbn, out Libro? libro)
        {
            return librosPorIsbn.TryGetValue((isbn ?? "").Trim(), out libro);
        }

        public bool EstaPrestado(string isbn)
        {
            isbn = (isbn ?? "").Trim();
            return prestados.Contains(isbn);
        }

        public void ListarTodos()
        {
            if (librosPorIsbn.Count == 0)
            {
                Console.WriteLine("No hay libros registrados.");
                return;
            }

            Console.WriteLine($"Total de libros: {librosPorIsbn.Count}\n");

            foreach (var par in librosPorIsbn)
            {
                string isbn = par.Key;
                Libro libro = par.Value;

                Console.WriteLine("------------------------------------");
                Console.WriteLine(libro.ToString());
                Console.WriteLine("Estado: " + (EstaPrestado(isbn) ? "PRESTADO" : "DISPONIBLE"));
            }
            Console.WriteLine("------------------------------------");
        }

        public void ListarDisponibles()
        {
            if (disponibles.Count == 0)
            {
                Console.WriteLine("No hay libros disponibles.");
                return;
            }

            Console.WriteLine($"Disponibles: {disponibles.Count}\n");

            foreach (string isbn in disponibles)
            {
                if (librosPorIsbn.TryGetValue(isbn, out Libro? libro))
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine(libro!.ToString());
                    Console.WriteLine("Estado: DISPONIBLE");
                }
            }
            Console.WriteLine("------------------------------------");
        }

        public void ListarPrestados()
        {
            if (prestados.Count == 0)
            {
                Console.WriteLine("No hay libros prestados.");
                return;
            }

            Console.WriteLine($"Prestados: {prestados.Count}\n");

            foreach (string isbn in prestados)
            {
                if (librosPorIsbn.TryGetValue(isbn, out Libro? libro))
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine(libro!.ToString());
                    Console.WriteLine("Estado: PRESTADO");
                    if (prestamoPorIsbn.TryGetValue(isbn, out string? usuario))
                        Console.WriteLine("Usuario: " + usuario);
                }
            }
            Console.WriteLine("------------------------------------");
        }

        public void ListarPorAutor(string autor)
        {
            autor = (autor ?? "").Trim();

            if (!isbnsPorAutor.TryGetValue(autor, out HashSet<string>? isbns) || isbns.Count == 0)
            {
                Console.WriteLine("No se encontraron libros para ese autor.");
                return;
            }

            Console.WriteLine($"Libros del autor \"{autor}\": {isbns.Count}\n");

            foreach (string isbn in isbns)
            {
                if (librosPorIsbn.TryGetValue(isbn, out Libro? libro))
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine(libro!.ToString());
                    Console.WriteLine("Estado: " + (EstaPrestado(isbn) ? "PRESTADO" : "DISPONIBLE"));
                }
            }
            Console.WriteLine("------------------------------------");
        }

        public void ListarPorCategoria(string categoria)
        {
            categoria = (categoria ?? "").Trim();

            if (!isbnsPorCategoria.TryGetValue(categoria, out HashSet<string>? isbns) || isbns.Count == 0)
            {
                Console.WriteLine("No se encontraron libros para esa categoría.");
                return;
            }

            Console.WriteLine($"Libros de la categoría \"{categoria}\": {isbns.Count}\n");

            foreach (string isbn in isbns)
            {
                if (librosPorIsbn.TryGetValue(isbn, out Libro? libro))
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine(libro!.ToString());
                    Console.WriteLine("Estado: " + (EstaPrestado(isbn) ? "PRESTADO" : "DISPONIBLE"));
                }
            }
            Console.WriteLine("------------------------------------");
        }

        public bool Prestar(string isbn, string usuario, out string mensaje)
        {
            isbn = (isbn ?? "").Trim();
            usuario = (usuario ?? "").Trim();

            if (!librosPorIsbn.ContainsKey(isbn))
            {
                mensaje = "No se puede prestar: el ISBN no existe en el sistema.";
                return false;
            }

            if (prestados.Contains(isbn))
            {
                mensaje = "No se puede prestar: el libro ya está prestado.";
                return false;
            }

            // Cambia estado usando conjuntos
            disponibles.Remove(isbn);
            prestados.Add(isbn);

            // Registra a quién se prestó (mejora)
            prestamoPorIsbn[isbn] = usuario;

            mensaje = $"Préstamo registrado correctamente. Libro prestado a: {usuario}";
            return true;
        }

        public bool Devolver(string isbn, out string mensaje)
        {
            isbn = (isbn ?? "").Trim();

            if (!librosPorIsbn.ContainsKey(isbn))
            {
                mensaje = "No se puede devolver: el ISBN no existe en el sistema.";
                return false;
            }

            if (!prestados.Contains(isbn))
            {
                mensaje = "No se puede devolver: el libro no está marcado como prestado.";
                return false;
            }

            // Cambia estado usando conjuntos
            prestados.Remove(isbn);
            disponibles.Add(isbn);

            prestamoPorIsbn.Remove(isbn);

            mensaje = "Devolución registrada correctamente. El libro vuelve a estar disponible.";
            return true;
        }

        public void ReporteGeneral()
        {
            Console.WriteLine("=== REPORTE GENERAL ===");
            Console.WriteLine($"Total registrados: {librosPorIsbn.Count}");
            Console.WriteLine($"Disponibles: {disponibles.Count}");
            Console.WriteLine($"Prestados: {prestados.Count}");

            Console.WriteLine("\n=== Categorías registradas ===");
            if (isbnsPorCategoria.Count == 0)
            {
                Console.WriteLine("No hay categorías.");
            }
            else
            {
                foreach (var cat in isbnsPorCategoria)
                    Console.WriteLine($"- {cat.Key}: {cat.Value.Count} libro(s)");
            }

            Console.WriteLine("\n=== Autores registrados ===");
            if (isbnsPorAutor.Count == 0)
            {
                Console.WriteLine("No hay autores.");
            }
            else
            {
                foreach (var aut in isbnsPorAutor)
                    Console.WriteLine($"- {aut.Key}: {aut.Value.Count} libro(s)");
            }
        }
    }
}