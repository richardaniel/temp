namespace Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos
{
    public class ColaboradorAsignadoDto
    {
        public int ColaboradorId { get; set; }
        public string NombreColaborador { get; set; } 

        public int SucursalId { get; set; }

        public string NombreSucursal { get; set; }

        public string DistanciaVivienda { get; set; }

        public string DireccionColaborador { get; set; }

        public string DireccionSucursal { get; set; }

    }
}
