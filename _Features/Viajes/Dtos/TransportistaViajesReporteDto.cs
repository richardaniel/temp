namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos
{
    public class TransportistaViajesReporteDto
    {

        public int TransportistaId { get; set; }
        public string NombreTransportista { get; set; }
        public List<ViajeDetalleDto> Viajes { get; set; } = new List<ViajeDetalleDto>();
        public decimal TotalAPagar { get; set; }
    }
}
