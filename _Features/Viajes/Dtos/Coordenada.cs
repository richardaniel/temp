namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos
{
    public class Coordenada
    {
        public Decimal Latitud { get; set; }
        public Decimal Longitud { get; set; }
        public string Nombre { get; set; }
        public bool Visitado { get; set; } // Para DBSCAN

        public Coordenada(Decimal lat, Decimal lon, string nombre)
        {
            Latitud = lat;
            Longitud = lon;
            Nombre = nombre;
            Visitado = false;
        }

        public override string ToString() => $"{Latitud},{Longitud}";
    }
}
