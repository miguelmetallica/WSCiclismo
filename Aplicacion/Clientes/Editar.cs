using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Aplicacion.Clientes
{
    using Aplicacion.ManejadorError;
    using Dominio;
    using FluentValidation;
    using System.Net;

    public class Editar
    {
        public class Ejecuta : IRequest 
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Apellido { get; set; }
            public string Nombre { get; set; }
            public string RazonSocial { get; set; }
            public int? TipoDocumentoId { get; set; }
            public string NroDocumento { get; set; }
            public DateTime? FechaNacimiento { get; set; }
            public int? EstadoCivilId { get; set; }
            public int? NacionalidadId { get; set; }
            public bool? EsPersonaJuridica { get; set; }
            public int? ProvinciaId { get; set; }
            public string Localidad { get; set; }
            public string CodigoPostal { get; set; }
            public string NroCalle { get; set; }
            public string OtrasReferencias { get; set; }
            public string Telefono { get; set; }
            public string Celular { get; set; }
            public string Email { get; set; }
            public bool Estado { get; set; }
            public string UsuarioAlta { get; set; }
            public DateTime FechaAlta { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly GestionContext context;

            public Manejador(GestionContext context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var cliente = await context.clientes.FindAsync(request.Id);
                if (cliente == null)
                {
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "El registro no existe" });
                }

                cliente.Codigo = request.Codigo ?? cliente.Codigo;
                cliente.Apellido = request.Apellido ?? cliente.Codigo;
                cliente.Nombre = request.Nombre ?? cliente.Codigo;
                cliente.RazonSocial = request.RazonSocial ?? cliente.Codigo;
                cliente.TipoDocumentoId = request.TipoDocumentoId ?? cliente.TipoDocumentoId;
                cliente.NroDocumento = request.NroDocumento ?? cliente.NroDocumento;
                cliente.FechaNacimiento = request.FechaNacimiento ?? cliente.FechaNacimiento;
                cliente.EstadoCivilId = request.EstadoCivilId ?? cliente.EstadoCivilId;
                cliente.NacionalidadId = request.NacionalidadId ?? cliente.NacionalidadId;
                cliente.EsPersonaJuridica = request.EsPersonaJuridica ?? cliente.EsPersonaJuridica;
                cliente.ProvinciaId = request.ProvinciaId ?? cliente.ProvinciaId;
                cliente.Localidad = request.Localidad ?? cliente.Localidad;
                cliente.CodigoPostal = request.CodigoPostal ?? cliente.CodigoPostal;
                cliente.NroCalle = request.NroCalle ?? cliente.NroCalle;
                cliente.OtrasReferencias = request.OtrasReferencias ?? cliente.OtrasReferencias;
                cliente.Telefono = request.Telefono ?? cliente.Telefono;
                cliente.Celular = request.Celular ?? cliente.Celular;
                cliente.Email = request.Email ?? cliente.Email;
                cliente.Estado = request.Estado;
                
                context.clientes.Update(cliente);
                var result = await context.SaveChangesAsync();
                if (result > 0) {
                    return Unit.Value;
                }

                throw new Exception("No se pudo editar el registro");
            }
        }

    }
}
