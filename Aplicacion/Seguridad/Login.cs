using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.JWT;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData> 
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuarios> userManager;
            private readonly SignInManager<Usuarios> signInManager;
            private readonly IJwtGenerador jwtGenerador;

            public Manejador(UserManager<Usuarios> userManager,SignInManager<Usuarios> signInManager,IJwtGenerador jwtGenerador)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    throw new ManejadorException(HttpStatusCode.Unauthorized);
                }
                var result = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                var resultadoRoles = await userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);

                if (result.Succeeded)
                {
                    return new UsuarioData {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = jwtGenerador.CrearToken(usuario, listaRoles) ,
                        Username = usuario.UserName,
                        Email = usuario.Email,
                        Imagen = null
                    };
                }
                
                throw new ManejadorException(HttpStatusCode.Unauthorized);

            }
        }
    }
}
