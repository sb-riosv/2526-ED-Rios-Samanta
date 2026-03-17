using System;

namespace _2526_ED_Rios_Samanta
{
    public class Nodo
    {
        // Propiedades
        public int Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        // Constructor
        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }

        // Método para saber si es hoja (extra)
        public bool EsHoja()
        {
            return Izquierdo == null && Derecho == null;
        }
    }
}