using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.TiposDocumentos
{
    using Dominio;
    public class Consulta
    {
        public class Listado : IRequest<List<TiposDocumentos>>
        { 
        
        }

        public class Manejador : IRequestHandler<Listado, List<TiposDocumentos>>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public Task<List<TiposDocumentos>> Handle(Listado request, CancellationToken cancellationToken)
            {
                var tiposDocumentos = context.ParamTiposDocumentos.ToListAsync();
                return tiposDocumentos;
            }
        }

    }
}
