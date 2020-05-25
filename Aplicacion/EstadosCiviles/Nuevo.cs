using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Aplicacion.EstadosCiviles
{
    using Dominio;
    using FluentValidation;

    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }            
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Codigo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
            }
        }
        
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var estadosCiviles = new EstadosCiviles {
                                            Codigo = request.Codigo,
                                            Descripcion = request.Descripcion,
                                            Estado = true
                                            };

                context.ParamEstadosCiviles.Add(estadosCiviles);
                var result = await context.SaveChangesAsync();
                if (result > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el registro");
            }
        }

    }
}
 