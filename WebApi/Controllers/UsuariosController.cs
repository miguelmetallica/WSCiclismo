using Aplicacion.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuariosController : MiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(UsuarioRegistrar.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioData>> DevolverUsuario() 
        {
            return await Mediator.Send(new UsuarioActual.Ejecutar());
        }

        [HttpPut("Actualizar")]
        public async Task<ActionResult<UsuarioData>> Actualizar(UsuariosActualizar.Ejecuta ejecuta)
        {
            return await Mediator.Send(ejecuta);
        }
    }


}