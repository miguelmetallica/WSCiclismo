using Dapper;
using Persistencia.DapperConexion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Persistencia.Paginacion
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection factoryConnection;

        public PaginacionRepositorio(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            PaginacionModel paginacionModel = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;

            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                
                DynamicParameters parametros = new DynamicParameters();

                foreach (var param in parametrosFiltro) {
                    parametros.Add("@" + param.Key, param.Value);
                }
                
                parametros.Add("@NumeroPagina", numeroPagina);
                parametros.Add("@CantidadElementos", cantidadElementos);
                parametros.Add("@Ordenamiento", ordenamientoColumna);

                parametros.Add("@TotalRecords", totalRecords,System.Data.DbType.Int32,System.Data.ParameterDirection.Output);
                parametros.Add("@TotalPaginas", totalPaginas, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

                var connection = factoryConnection.GetConnection();
                var result = await connection.QueryAsync(storeProcedure, parametros, commandType: System.Data.CommandType.StoredProcedure );
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.ListaRecords = listaReporte;
                paginacionModel.NumeroPaginas = parametros.Get<int>("TotalPaginas");
                paginacionModel.TotalRecords = parametros.Get<int>("TotalRecords");
            }

            catch (Exception ex)
            {
                throw new Exception("no se pudo ejecutar el procedimiento almacenado",ex);
            }
            finally 
            {
                factoryConnection.CloseConnection();
            }

            return paginacionModel;
        }
    }
}
