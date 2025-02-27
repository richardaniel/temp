namespace Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes.Dtos
{
    public class AprobacionSolicitudDto
    {
        public int AprobacionSolicitudId { get; set; }

        public int SolicitudId { get; set; }

        public int GerenteId { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public int EstadoSolicitudId { get; set; }

        public string? Comentario { get; set; }
    }
}
