using Aplicacion.Nacionalidades;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NacionalidadesController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Nacionalidades>>> Get()
        {
            return await Mediator.Send(new Consulta.Listado());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Nacionalidades>> Detalle(int id)
        {
            return await Mediator.Send(new ConsultaId.Unico{Id = id});
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
    }
}