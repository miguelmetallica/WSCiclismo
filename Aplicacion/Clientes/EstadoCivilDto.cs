using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Clientes
{
    public class EstadoCivilDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
