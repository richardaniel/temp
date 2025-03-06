using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores
{
    public class ColaboradorDomainRequeriment
    {

        public static ColaboradorDomainRequeriment Fill(
            bool paisExiste ,
            bool estadoExiste  , 
            bool ciudadExiste ,
            bool correoExiste 
            ) => new ColaboradorDomainRequeriment
        {
            IsPaisExiste = paisExiste ,
            IsEstadoExiste = estadoExiste,
            IsCiudadExiste = ciudadExiste ,
            IsCorreoExiste = correoExiste,

        };
       
        public bool IsPaisExiste { get; private set; }
        public bool IsEstadoExiste { get; private set; }
        public bool IsCiudadExiste { get; private set; }
        public bool IsCorreoExiste { get; private set; }
        

        //public ErrorOr<bool> Validar()
        //{
        //    if (!IsPaisExiste)
        //    {
        //        Error.Conflict("General.Conflict", string.Format(MensajesGlobales.ObjetoNoExiste, "pais"));
        //    }
               
            
        //    if(!IsEstadoExiste)
        //    {
        //        Error.Conflict("General.Conflict", string.Format(MensajesGlobales.ObjetoNoExiste, "estado"));
        //    }
              

        //    if(!IsCiudaExiste)
        //    {
        //        Error.Conflict("General.Conflict", string.Format(MensajesGlobales.ObjetoNoExiste, "ciudad"));
        //    }
               

        //    if (!IsCorreoExiste)
        //    {
        //        Error.Conflict("General.Conflict", string.Format(MensajesGlobales.EmailYaExiste));
        //    }
               


        //    return true;

        //}

    }
}
