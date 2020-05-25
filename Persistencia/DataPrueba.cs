using Dominio;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(GestionContext context, UserManager<Usuarios> userManager) 
        {
            if (!userManager.Users.Any()) {
                var usuario = new Usuarios { NombreCompleto = "Miguel Elias", UserName = "admin", Email = "miguel.a.elias@gmail.com"  };
                await userManager.CreateAsync(usuario, "Me123456$");
            }
        }
    }
}
