using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.EstadosCiviles
{
    using Dominio;
    public class Consulta
    {
        public class Listado : IRequest<List<EstadosCiviles>>
        { 
        
        }

        public class Manejador : IRequestHandler<Listado, List<EstadosCiviles>>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public Task<List<EstadosCiviles>> Handle(Listado request, CancellationToken cancellationToken)
            {
                var estadosCiviles = context.ParamEstadosCiviles.ToListAsync();
                return estadosCiviles;
            }
        }

    }
}
