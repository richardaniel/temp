
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{
    public interface ILocationService
    {
        public  Task<DistanceMatrixResponse> GetDistanceAsync(Point point);




    }
}
