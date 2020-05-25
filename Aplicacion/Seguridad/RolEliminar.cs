using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolEliminar
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
                if (role == null) 
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new {mensaje = "No existe el rol"});
                }

                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) 
                {
                    return Unit.Value;
                }

                throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "no se puede eliminar el rol" });

            }
        }
    }
}
