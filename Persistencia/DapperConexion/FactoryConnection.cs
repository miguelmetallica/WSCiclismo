using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection connection;
        private readonly IOptions<ConexionConfiguracion> options;

        public FactoryConnection(IOptions<ConexionConfiguracion> options)
        {
            this.options = options;
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open) {
                connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (connection == null) {
                connection = new SqlConnection(options.Value.DefaultConnection);
            }

            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }
            return connection;
        }
    }
}
