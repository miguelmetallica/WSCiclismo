using MediatR;
using Persistencia;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Aplicacion.Clientes
{
    using Dominio;
    using FluentValidation;

    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {            
            public string Codigo { get; set; }
            public string Apellido { get; set; }
            public string Nombre { get; set; }
            public string RazonSocial { get; set; }
            public int TipoDocumentoId { get; set; }
            public string NroDocumento { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public int EstadoCivilId { get; set; }
            public int NacionalidadId { get; set; }
            public bool EsPersonaJuridica { get; set; }
            public int ProvinciaId { get; set; }
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

        public class EjecutaValidacion : AbstractValidator<Ejecuta> {
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
                var cliente = new Clientes {
                                            Codigo = request.Codigo,
                                            Apellido = request.Apellido,
                                            Nombre = request.Nombre,
                                            RazonSocial = request.RazonSocial,
                                            TipoDocumentoId = request.TipoDocumentoId,
                                            NroDocumento = request.NroDocumento,
                                            FechaNacimiento = request.FechaNacimiento,
                                            EstadoCivilId = request.EstadoCivilId,
                                            NacionalidadId = request.NacionalidadId,
                                            EsPersonaJuridica = request.EsPersonaJuridica,
                                            ProvinciaId = request.ProvinciaId,
                                            Localidad = request.Localidad,
                                            CodigoPostal = request.CodigoPostal,
                                            NroCalle = request.NroCalle,
                                            OtrasReferencias = request.OtrasReferencias,
                                            Telefono = request.Telefono,
                                            Celular = request.Celular,
                                            Email = request.Email,
                                            Estado = request.Estado,
                                            UsuarioAlta = request.UsuarioAlta,
                                            FechaAlta = request.FechaAlta
                                            };

                context.clientes.Add(cliente);
                var result = await context.SaveChangesAsync();
                if (result > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el registro");
            }
        }

    }
}
 