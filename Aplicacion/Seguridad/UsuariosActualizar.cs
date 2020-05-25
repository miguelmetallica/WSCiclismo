using Aplicacion.JWT;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuariosActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
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

        public class ValidarEjecuta : AbstractValidator<Ejecuta> 
        {
            public ValidarEjecuta()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.TipoDocumento).NotEmpty();
                RuleFor(x => x.NroDocumento).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly GestionContext gestionContext;
            private readonly UserManager<Usuarios> userManager;
            private readonly IJwtGenerador jwtGenerador;
            private IPasswordHasher<Usuarios> passwordHasher;

            public Manejador(GestionContext gestionContext, UserManager<Usuarios> userManager, IJwtGenerador jwtGenerador, IPasswordHasher<Usuarios> passwordHasher)
            {
                this.gestionContext = gestionContext;
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
                this.passwordHasher= passwordHasher;
            }
            
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByNameAsync(request.Username);
                if (usuario == null) 
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new {mensaje = "El usuario no existe"});
                }

                var existeEmail = await gestionContext.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();
                if (existeEmail)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El email pertenece a otro usuario" });
                }

                var existeDni = await gestionContext.Users.Where(x => x.TipoDocumento == request.TipoDocumento && x.NroDocumento == request.NroDocumento && x.UserName != request.Username).AnyAsync();
                if (existeDni)
                {
                    throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con ese Tipo y Nro de Documento" });
                }

                usuario.NombreCompleto = request.Nombre + ' ' + request.Apellido;
                usuario.PasswordHash = passwordHasher.HashPassword(usuario, request.Password) ?? usuario.PasswordHash;
                usuario.Email = request.Email ?? usuario.Email;

                usuario.TipoDocumento = request.TipoDocumento;
                usuario.NroDocumento = request.NroDocumento;
                usuario.Nacionalidad = request.Nacionalidad ?? usuario.Nacionalidad;
                usuario.FechaNacimiento = request.FechaNacimiento ?? usuario.FechaNacimiento;
                usuario.Provincia = request.Provincia ?? usuario.Provincia;
                usuario.Localidad = request.Localidad ?? usuario.Localidad;
                usuario.CodigoPostal = request.CodigoPostal ?? usuario.CodigoPostal;
                usuario.Calle = request.Calle ?? usuario.Calle;
                usuario.OtrasReferencias = request.OtrasReferencias ?? usuario.OtrasReferencias;
                usuario.Facebook = request.Facebook ?? usuario.Facebook;
                usuario.Instagram = request.Instagram ?? usuario.Instagram;
                usuario.Twitter = request.Twitter ?? usuario.Twitter;
                usuario.TikTok= request.TikTok ?? usuario.TikTok;
                usuario.Publico = request.Publico;

                var resultRoles = await userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultRoles);

                var updateUsuario = await userManager.UpdateAsync(usuario);
                if (updateUsuario.Succeeded) {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Username = usuario.UserName,
                        Email = usuario.Email,
                        Token = jwtGenerador.CrearToken(usuario, listaRoles),
                        Imagen = null,
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

                throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "no se puede actualizar el usuario" });

            }

            
        }
    }
}
