using Aplicacion.JWT;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData> {}

        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<Usuarios> userManager;
            private readonly IJwtGenerador jwtGenerador;
            private readonly IUsuarioSesion usuarioSesion;

            public Manejador(UserManager<Usuarios> userManager,IJwtGenerador jwtGenerador,IUsuarioSesion usuarioSesion)
            {
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
                this.usuarioSesion = usuarioSesion;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByEmailAsync(usuarioSesion.ObtenerUsuarioSesion());
                var resultadoRoles = await userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);

                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    Username = usuario.UserName,
                    Token = jwtGenerador.CrearToken(usuario, listaRoles),
                    Imagen = null,
                    Email = usuario.Email,
                    TipoDocumento = usuario.TipoDocumento,
                    NroDocumento = usuario.NroDocumento,
                    FechaNacimiento = usuario.FechaNacimiento,
                    Nacionalidad = usuario.Nacionalidad,
                    Provincia = usuario.Provincia,
                    Localidad = usuario.Localidad,
                    CodigoPostal = usuario.CodigoPostal,
                    Calle = usuario.Calle,
                    OtrasReferencias = usuario.OtrasReferencias,
                    Facebook = usuario.Facebook,
                    Instagram = usuario.Instagram,
                    Twitter = usuario.Twitter,
                    TikTok = usuario.TikTok,
                    Publico = usuario.Publico
                };
            }
        }

    }
}
