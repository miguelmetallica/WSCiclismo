using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.EstadosCiviles
{
    using Aplicacion.ManejadorError;
    using Dominio;
    using System.Net;

    public class ConsultaId
    {
        public class Unico : IRequest<EstadosCiviles>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Unico, EstadosCiviles>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public async Task<EstadosCiviles> Handle(Unico request, CancellationToken cancellationToken)
            {
                var estadosCiviles = await context.ParamEstadosCiviles.FindAsync(request.Id);
                if (estadosCiviles == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                return estadosCiviles;
            }
        }

    }
}
