using MediatR;
using Persistencia.Paginacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Clientes
{
    public class PaginacionClientes
    {
        public class Ejecuta : IRequest<PaginacionModel> {
            public string Nombre { get; set; }
            public int NumeroPagina { get; set; }
            public int CantidadElementos { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {
            private readonly IPaginacion paginacion;

            public Manejador(IPaginacion paginacion)
            {
                this.paginacion = paginacion;
            }
            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var sp = "ObtenerClientePaginacion";
                var ordenamiento = "NroDocumento";

                var parametros = new Dictionary<string, object>();
                parametros.Add("Nombre", request.Nombre);
                return await paginacion.devolverPaginacion(sp, request.NumeroPagina, request.CantidadElementos, parametros, ordenamiento);

            }
        }
    }
}
