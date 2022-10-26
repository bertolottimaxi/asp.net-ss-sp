using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Agente
    {
        public int Dni { get; set; }
        public string NombreyApellido { get; set; }

        public Agente() { }
        public Agente(int dni, string nya)
        {
            Dni = dni;
            NombreyApellido = nya;
        }
    }
}