using AutoMapper;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.Dtos;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{
    public class AutoMapperProfiles: Profile 
    {
        public AutoMapperProfiles()
        {
            
           // CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Colaborador , ColaboradorDto>().ReverseMap();
            CreateMap<Usuario,UsuarioDto>().ReverseMap();

        }
    }
   
}
