using Aplicacion.Clientes;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Cliente;
using Persistencia.Paginacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : MiControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ClienteModel>>> Get()
        {
            return await Mediator.Send(new ConsultaSP.ListadoClientes());
        }
        //public async Task<ActionResult<List<ClientesDto>>> Get()
        //{
        //    return await Mediator.Send(new Consulta.ListadoClientes());
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesDto>> Detalle(int id)
        {
            return await Mediator.Send(new ConsultaId.ClienteUnico{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Nuevo.Ejecuta data)
        {
            return await Mediator.Send(data);            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Put(int id,Editar.Ejecuta data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Post(PaginacionClientes.Ejecuta data)
        {
            return await Mediator.Send(data);
        }
    }
}