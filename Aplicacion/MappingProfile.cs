using Aplicacion.Clientes;
using AutoMapper;

namespace Aplicacion
{
    
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Dominio.Clientes, ClientesDto>()
                .ForMember(x => x.TipoDocumento, y => y.MapFrom(y => y.TipoDocumento))
                .ForMember(x => x.Nacionalidad, y => y.MapFrom(y => y.Nacionalidad))
                .ForMember(x => x.EstadoCivil, y => y.MapFrom(y => y.EstadoCivil))
                .ForMember(x => x.Provincia, y => y.MapFrom(y => y.Provincia))
            ;
            CreateMap<Dominio.TiposDocumentos, TipoDocumentoDto>();
            CreateMap<Dominio.EstadosCiviles, EstadoCivilDto>();
            CreateMap<Dominio.Provincias, ProvinciaDto>();
            CreateMap<Dominio.Nacionalidades, NacionalidadDto>();
        }
    }
}
