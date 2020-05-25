using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Cliente
{
    public class ClienteRepositorio : ICliente
    {
        private readonly IFactoryConnection factoryConnection;

        public ClienteRepositorio(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }
        public Task<int> Actualiza(ClienteModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task<int> Elimina(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Nuevo(ClienteModel cliente)
        {
            var sp = "ClientesObtener";
            try
            {
                var connexion = factoryConnection.GetConnection();
                var resultado = await connexion.ExecuteAsync(sp, new
                {
                    Apellido = cliente.Apellido,
                    Nombre = cliente.Nombre
                }, commandType: System.Data.CommandType.StoredProcedure);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el registro", ex);
            }
            finally 
            {
                factoryConnection.CloseConnection();
            }
        }

        public Task<ClienteModel> ObtenerId()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClienteModel>> ObtenerLista()
        {
            IEnumerable<ClienteModel> ClienteListado = null;
            var sp = "ClientesObtener";
            try
            {
                var connexion = factoryConnection.GetConnection();
                ClienteListado = await connexion.QueryAsync<ClienteModel>(sp, null, commandType: System.Data.CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta de datos",ex);
            }
            finally 
            {
                factoryConnection.CloseConnection();
            }
            return ClienteListado;
        }
    }
}
