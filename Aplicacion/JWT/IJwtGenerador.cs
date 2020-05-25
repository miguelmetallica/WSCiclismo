using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.JWT
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuarios usuarios,List<string> roles);

    }
}
