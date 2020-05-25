using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Clientes
{
    using Aplicacion.ManejadorError;
    using AutoMapper;
    using Dominio;
    using Microsoft.EntityFrameworkCore;
    using System.Net;

    public class ConsultaId
    {
        public class ClienteUnico : IRequest<ClientesDto>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<ClienteUnico, ClientesDto>
        {
            private readonly GestionContext context;
            private readonly IMapper mapper;

            public Manejador(GestionContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<ClientesDto> Handle(ClienteUnico request, CancellationToken cancellationToken)
            {
                var cliente = await context.clientes
                    .Include(d => d.TipoDocumento)
                    .Include(x => x.Nacionalidad)
                    .Include(x => x.EstadoCivil)
                    .Include(x => x.Provincia)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (cliente == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                var clienteDto = mapper.Map<Clientes, ClientesDto>(cliente);

                return clienteDto;                
            }
        }

    }
}
