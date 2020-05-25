using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.TiposDocumentos
{
    using Aplicacion.ManejadorError;
    using Dominio;
    using System.Net;

    public class ConsultaId
    {
        public class Unico : IRequest<TiposDocumentos>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Unico, TiposDocumentos>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public async Task<TiposDocumentos> Handle(Unico request, CancellationToken cancellationToken)
            {
                var tiposDocumentos = await context.ParamTiposDocumentos.FindAsync(request.Id);
                if (tiposDocumentos == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                return tiposDocumentos;
            }
        }

    }
}
