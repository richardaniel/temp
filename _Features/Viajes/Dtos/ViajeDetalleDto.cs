namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos
{
    public class ViajeDetalleDto
    {
        public int ViajeId { get; set; }
        public DateTime FechaViaje { get; set; }
        public int SucursalId { get; set; }
        public decimal DistanciaTotalKm { get; set; }
        public decimal CostoTotal { get; set; }
        public int MonedaId { get; set; }
    }
}
