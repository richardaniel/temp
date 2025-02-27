using Microsoft.Identity.Client;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos
{
    public class ViajeDto
    {
        
        public int SucursalId { get; set; }
        public int TransportistaId { get; set; }
        
        public DateTime FechaViaje { get; set; }

        public int UsuarioRegistroId { get; set; }

        public Decimal DistanciaTotalKm { get; set; }

        public Decimal CostoTotal { get; set; }

        //public int MonedaId { get; set; }


    }
}
