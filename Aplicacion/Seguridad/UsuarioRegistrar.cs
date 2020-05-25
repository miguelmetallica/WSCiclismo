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
    public class UsuarioRegistrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string TipoDocumento { get; set; }
            public string NroDocumento { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.TipoDocumento).NotEmpty();
                RuleFor(x => x.NroDocumento).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();                
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData> 
        {
            private readonly GestionContext context;
            private readonly UserManager<Usuarios> userManager;
            private readonly IJwtGenerador jwtGenerador;

            public Manejador(GestionContext context,UserManager<Usuarios> userManager, IJwtGenerador jwtGenerador) 
            {
                this.context = context;
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existeEmail = await context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if(existeEmail) {
                    throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con ese Email" });
                }

                var existeUser = await context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeUser)
                {
                    throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con ese UserName" });
                }

                var existeDni = await context.Users.Where(x => x.TipoDocumento == request.TipoDocumento && x.NroDocumento == request.NroDocumento).AnyAsync();
                if (existeDni)
                {
                    throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con ese Tipo y Nro de Documento" });
                }

                var usuario = new Usuarios
                {
                    NombreCompleto = request.Nombre + " " + request.Apellido,
                    UserName = request.UserName,
                    TipoDocumento = request.TipoDocumento,
                    NroDocumento = request.NroDocumento,
                    Email = request.Email,
                    Publico = true
                };

                var result = await userManager.CreateAsync(usuario, request.Password);
                if (result.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = jwtGenerador.CrearToken(usuario,null),
                        Username = usuario.UserName,                        
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

                throw new Exception("No se pudo agregar al Usuario");

            }
        }

    }
}
