namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos
{
    public class DistanceMatrixResponse
    {

        public string DestinationAddresses { get; set; }


        public string OriginAddresses { get; set; }


        public Row[] Rows { get; set; }


        public string Status { get; set; }

    }
}
