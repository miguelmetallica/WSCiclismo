using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Seguridad
{
    public class UsuarioData
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Imagen { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string OtrasReferencias { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string TikTok { get; set; }
        public bool Publico { get; set; }
    }
}
