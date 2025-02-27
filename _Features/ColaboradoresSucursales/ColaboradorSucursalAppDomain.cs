using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales
{
    public class ColaboradorSucursalAppDomain
    {
        
       
            public ErrorOr<bool> ValidateAssignment(ColaboradorSucursalDto colaboradorSucursalDto, Colaborador? colaborador, Sucursal? sucursal)
            {
                if (colaborador == null || sucursal == null)
                {
                    return Error.Failure("AsignarColaborador.Failure", "Colaborador o Sucursal no encontrada.");
                }

               
                if (!colaborador.Activo)
                {
                    return Error.Failure("AsignarColaborador.Failure", "El colaborador debe estar activo para ser asignado.");
                }

                return true;
            }

            public ErrorOr<decimal> ValidateDistance(DistanceMatrixResponse distanceKm)
            {
                if (distanceKm?.Rows?.Length == 0 || distanceKm?.Rows[0]?.Elements?.Length == 0)
                {
                    return Error.Failure("AsignarColaborador.Failure", "Error al calcular la distancia.");
                }

                string distanceText = distanceKm.Rows[0].Elements[0].Distance.Text ?? "0";
                if (!decimal.TryParse(distanceText.Replace("km", "").Trim(), out decimal distance))
                {
                    return Error.Failure("AsignarColaborador.Failure", "No se pudo convertir la distancia a un valor numérico.");
                }

                
                if (distance > 100) 
                {
                    return Error.Failure("AsignarColaborador.Failure", "La distancia excede el límite permitido de 100 km.");
                }

                return distance;
            }
     }
}


