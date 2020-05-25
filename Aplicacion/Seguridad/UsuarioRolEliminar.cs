using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioRolEliminar
    {
        public class Ejecuta : IRequest 
        {
            public string Username { get; set; }
            public string RolNombre { get; set; }
        }

        public class ValidarEjecuta : AbstractValidator<Ejecuta> 
        {
            public ValidarEjecuta()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuarios> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public Manejador(UserManager<Usuarios> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await roleManager.FindByNameAsync(request.RolNombre);
                if (role == null) 
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new {mensaje = "El rol no existe"});
                }

                var usuario = await userManager.FindByNameAsync(request.Username);
                if (usuario == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El usuario no existe" });
                }

                var result = await userManager.RemoveFromRoleAsync(usuario,role.Name);
                if (result.Succeeded) 
                {
                    return Unit.Value;
                }

                throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "error al agregar el rol al usuario" });

            }
        }
    }
}
