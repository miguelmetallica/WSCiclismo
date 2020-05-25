using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Aplicacion.Clientes
{
    using Aplicacion.ManejadorError;
    using Dominio;
    using System.Net;

    public class Eliminar
    {
        public class Ejecuta : IRequest 
        {
            public int Id { get; set; }
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
                var cliente = await context.clientes.FindAsync(request.Id);
                if (cliente == null) {                    
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                context.clientes.Remove(cliente);
                var result = await context.SaveChangesAsync();
                if (result > 0) {
                    return Unit.Value;
                }

                throw new ManejadorException(HttpStatusCode.BadRequest, new { mensaje = "No se pudo eliminar el registro" });
            }
        }

    }
}
