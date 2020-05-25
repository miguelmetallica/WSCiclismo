using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Clientes
{
    using AutoMapper;
    using Dominio;
    public class Consulta
    {
        public class ListadoClientes : IRequest<List<ClientesDto>>
        { 
        
        }

        public class Manejador : IRequestHandler<ListadoClientes, List<ClientesDto>>
        {
            private readonly GestionContext context;
            private readonly IMapper mapper;
            public Manejador(GestionContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<List<ClientesDto>> Handle(ListadoClientes request, CancellationToken cancellationToken)
            {
                var clientes = await context.clientes
                    .Include(d => d.TipoDocumento)
                    .Include(x => x.Nacionalidad)
                    .Include(x => x.EstadoCivil)
                    .Include(x => x.Provincia)
                    .ToListAsync();
                
                var clientesDto =  mapper.Map<List<Clientes>, List<ClientesDto>>(clientes);

                return clientesDto;
            }
        }

    }
}
