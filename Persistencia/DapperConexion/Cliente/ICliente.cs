using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Cliente
{
    public interface ICliente
    {
        Task<IEnumerable<ClienteModel>> ObtenerLista();
        Task<ClienteModel> ObtenerId();
        Task<int> Nuevo(ClienteModel cliente);
        Task<int> Actualiza(ClienteModel cliente);
        Task<int> Elimina(int id);
    }
}
