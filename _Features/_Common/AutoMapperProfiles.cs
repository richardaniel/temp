using AutoMapper;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.Dtos;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{
    public class AutoMapperProfiles: Profile 
    {
        public AutoMapperProfiles()
        {
            
           
            CreateMap<Colaborador , ColaboradorDtoRequest>().ReverseMap();

            CreateMap<Colaborador, ColaboradorDtoResponse>()
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => new PaisDto
            {
                PaisId = src.PaisId,
                Nombre = src.Pais != null ? src.Pais.Nombre : null
             }))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => new EstadoDto
            {
                EstadoId = src.EstadoId,
                Nombre = src.Estado != null ? src.Estado.Nombre : null
            }))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => new CiudadDto
            {
                CiudadId = src.CiudadId,
                Nombre = src.Ciudad != null ? src.Ciudad.Nombre : null
              }));




            CreateMap<ColaboradorSucursal, ColaboradorSucursalDtoRequest>().ReverseMap();

            CreateMap<ColaboradorSucursal, ColaboradorSucursalDtoResponse>()
                .ForMember(dest => dest.Colaborador, opt => opt.MapFrom(src => new ColaboradorDtoResponse
                {
                    ColaboradorId = src.Colaborador.ColaboradorId,
                    Nombre = src.Colaborador.Nombre
                }))
                .ForMember(dest => dest.Sucursal, opt => opt.MapFrom(src => new SucursalDtoResponse
                {
                    SucursalId = src.Sucursal.SucursalId,
                    Nombre = src.Sucursal.Nombre
                }));


            CreateMap<Usuario,UsuarioDto>().ReverseMap();
            CreateMap<Pais, PaisDto>().ReverseMap();
            CreateMap<Estado, EstadoDto>().ReverseMap();
            CreateMap<Ciudad, CiudadDto>().ReverseMap();

        }
    }
   
}
