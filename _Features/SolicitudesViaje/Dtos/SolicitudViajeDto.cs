namespace Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje.Dtos
{
    public class SolicitudViajeDto
    {

        public int SolicitudViajeId { get; set; }

        public int ColaboradorId { get; set; }

        public int SucursalId { get; set; }

        public int EstadoSolicitudId { get; set; }

        public string? Comentario { get; set; }

        public DateTime FechaSolicitud { get; set; }

    }
}