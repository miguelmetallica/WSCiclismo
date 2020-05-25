using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Nacionalidades
{
    using Dominio;
    public class Consulta
    {
        public class Listado : IRequest<List<Nacionalidades>>
        { 
        
        }

        public class Manejador : IRequestHandler<Listado, List<Nacionalidades>>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public Task<List<Nacionalidades>> Handle(Listado request, CancellationToken cancellationToken)
            {
                var nacionalidades = context.ParamNacionalidades.ToListAsync();
                return nacionalidades;
            }
        }

    }
}
