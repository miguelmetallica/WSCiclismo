using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Provincias
{
    using Aplicacion.ManejadorError;
    using Dominio;
    using System.Net;

    public class ConsultaId
    {
        public class Unico : IRequest<Provincias>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Unico, Provincias>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public async Task<Provincias> Handle(Unico request, CancellationToken cancellationToken)
            {
                var provincias = await context.ParamProvincias.FindAsync(request.Id);
                if (provincias == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                return provincias;
            }
        }

    }
}
