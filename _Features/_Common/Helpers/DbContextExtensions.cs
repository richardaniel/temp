using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Helpers
{
    public static class DbContextExtensions
    {
        public static string GetPrimaryKeyName<T>(this DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));

            if (entityType == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no está registrada en el DbContext.");

            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene una clave primaria definida.");

            return primaryKey.Properties.First().Name; // Devuelve el nombre de la PK
        }
    }
}
