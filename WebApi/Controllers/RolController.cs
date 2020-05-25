using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RolController : MiControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }

        [HttpGet("listar")]
        public async Task<ActionResult<List<IdentityRole>>> Listar(RolLista.Ejecuta ejecuta) 
        {
            return await Mediator.Send(ejecuta);
        }

        [HttpPost("AgregarRolUsuario")]
        public async Task<ActionResult<Unit>> AgregarRolUsuario(UsuarioRolAgregar.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }

        [HttpPost("EliminarRolUsuario")]
        public async Task<ActionResult<Unit>> EliminarRolUsuario(UsuarioRolEliminar.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }

        [HttpGet("{Username}")]
        public async Task<ActionResult<List<string>>> ObtenerRolesPorUsuarios(ObtenerRolesPorUsuario.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }
    }


}