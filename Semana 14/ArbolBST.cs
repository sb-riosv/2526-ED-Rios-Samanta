using System;

namespace _2526_ED_Rios_Samanta
{
    public class ArbolBST
    {
        public Nodo Raiz;

        // INSERTAR
        public void Insertar(int valor)
        {
            if (Buscar(valor))
            {
                Console.WriteLine("El valor ya existe en el árbol.");
                return;
            }

            Raiz = InsertarRecursivo(Raiz, valor);
            Console.WriteLine("Valor insertado correctamente.");
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);

            return nodo;
        }

        // BUSCAR
        public bool Buscar(int valor)
        {
            return BuscarRecursivo(Raiz, valor);
        }

        private bool BuscarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return false;

            if (valor == nodo.Valor)
                return true;

            if (valor < nodo.Valor)
                return BuscarRecursivo(nodo.Izquierdo, valor);

            return BuscarRecursivo(nodo.Derecho, valor);
        }

        // ELIMINAR
        public void Eliminar(int valor)
        {
            if (!Buscar(valor))
            {
                Console.WriteLine("El valor no existe en el árbol.");
                return;
            }

            Raiz = EliminarRecursivo(Raiz, valor);
            Console.WriteLine("Valor eliminado correctamente.");
        }

        private Nodo EliminarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return nodo;

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
            }
            else
            {
                // Caso 1: sin hijos
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                    return null;

                // Caso 2: un hijo
                if (nodo.Izquierdo == null)
                    return nodo.Derecho;

                if (nodo.Derecho == null)
                    return nodo.Izquierdo;

                // Caso 3: dos hijos
                Nodo sucesor = EncontrarMin(nodo.Derecho);
                nodo.Valor = sucesor.Valor;
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Valor);
            }

            return nodo;
        }

        // RECORRIDOS
        public void Inorden()
        {
            InordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                InordenRecursivo(nodo.Izquierdo);
                Console.Write(nodo.Valor + " ");
                InordenRecursivo(nodo.Derecho);
            }
        }

        public void Preorden()
        {
            PreordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void PreordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.Valor + " ");
                PreordenRecursivo(nodo.Izquierdo);
                PreordenRecursivo(nodo.Derecho);
            }
        }

        public void Postorden()
        {
            PostordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void PostordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                PostordenRecursivo(nodo.Izquierdo);
                PostordenRecursivo(nodo.Derecho);
                Console.Write(nodo.Valor + " ");
            }
        }

        // MÍNIMO
        public int Minimo()
        {
            if (Raiz == null)
                throw new Exception("El árbol está vacío.");

            Nodo actual = Raiz;
            while (actual.Izquierdo != null)
                actual = actual.Izquierdo;

            return actual.Valor;
        }

        // MÁXIMO
        public int Maximo()
        {
            if (Raiz == null)
                throw new Exception("El árbol está vacío.");

            Nodo actual = Raiz;
            while (actual.Derecho != null)
                actual = actual.Derecho;

            return actual.Valor;
        }

        // ALTURA
        public int Altura()
        {
            return AlturaRecursiva(Raiz);
        }

        private int AlturaRecursiva(Nodo nodo)
        {
            if (nodo == null)
                return -1;

            int izquierda = AlturaRecursiva(nodo.Izquierdo);
            int derecha = AlturaRecursiva(nodo.Derecho);

            return Math.Max(izquierda, derecha) + 1;
        }

        // LIMPIAR
        public void Limpiar()
        {
            Raiz = null;
        }

        // CONTAR NODOS (EXTRA)
        public int ContarNodos()
        {
            return ContarRecursivo(Raiz);
        }

        private int ContarRecursivo(Nodo nodo)
        {
            if (nodo == null)
                return 0;

            return 1 + ContarRecursivo(nodo.Izquierdo) + ContarRecursivo(nodo.Derecho);
        }

        // MÉTODO AUXILIAR
        private Nodo EncontrarMin(Nodo nodo)
        {
            while (nodo.Izquierdo != null)
                nodo = nodo.Izquierdo;

            return nodo;
        }
    }
}