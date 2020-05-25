using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Nacionalidades
{
    using Aplicacion.ManejadorError;
    using Dominio;
    using System.Net;

    public class ConsultaId
    {
        public class Unico : IRequest<Nacionalidades>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Unico, Nacionalidades>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public async Task<Nacionalidades> Handle(Unico request, CancellationToken cancellationToken)
            {
                var nacionalidades = await context.ParamNacionalidades.FindAsync(request.Id);
                if (nacionalidades == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                return nacionalidades;
            }
        }

    }
}
