using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Provincias
{
    using Dominio;
    public class Consulta
    {
        public class Listado : IRequest<List<Provincias>>
        { 
        
        }

        public class Manejador : IRequestHandler<Listado, List<Provincias>>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public Task<List<Provincias>> Handle(Listado request, CancellationToken cancellationToken)
            {
                var provincias = context.ParamProvincias.ToListAsync();
                return provincias;
            }
        }

    }
}
