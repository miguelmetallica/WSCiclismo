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
    using Persistencia.DapperConexion.Cliente;
    using System.Linq;

    public class ConsultaSP
    {
        public class ListadoClientes : IRequest<List<ClienteModel>>
        { 
        
        }

        public class Manejador : IRequestHandler<ListadoClientes, List<ClienteModel>>
        {
            private readonly ICliente cliente;

            public Manejador(ICliente cliente)
            {                
                this.cliente = cliente;
            }
            public async Task<List<ClienteModel>> Handle(ListadoClientes request, CancellationToken cancellationToken)
            {
                var result = await cliente.ObtenerLista();
                return result.ToList();
            }
        }

    }
}
