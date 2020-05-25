﻿using System;

namespace Dominio
{
    public class Clientes
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string NroDocumento { get; set; }
        public string Foto { get; set; }
        public DateTime FechaNacimiento { get; set; }                
        public bool EsPersonaJuridica { get; set; }
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

        public int TipoDocumentoId { get; set; }
        public TiposDocumentos TipoDocumento { get; set; }

        public int EstadoCivilId { get; set; }
        public EstadosCiviles EstadoCivil { get; set; }

        public int NacionalidadId { get; set; }
        public Nacionalidades Nacionalidad { get; set; }

        public int ProvinciaId { get; set; }
        public Provincias Provincia { get; set; }        
    }
}
