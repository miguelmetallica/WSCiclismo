using Aplicacion.ManejadorError;
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
    public class RolNuevo
    {
        public class Ejecuta : IRequest 
        {
            public string Nombre { get; set; }
        }

        public class ValidarEjecuta : AbstractValidator<Ejecuta> 
        {
            public ValidarEjecuta()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> roleManager;

            public Manejador(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await roleManager.FindByNameAsync(request.Nombre);
                if (role != null) 
                {
                    throw new ManejadorException(HttpStatusCode.BadRequest, new {mensaje = "Ya existe el rol"});
                }

                var result = await roleManager.CreateAsync(new IdentityRole(request.Nombre));
                if (result.Succeeded) 
                {
                    return Unit.Value;
                }

                throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "no se puede insertar el rol" });

            }
        }
    }
}
