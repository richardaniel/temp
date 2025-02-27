using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Monedas;

public partial class Moneda
{
    public int MonedaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Simbolo { get; set; } = null!;

    public string CodigoIso { get; set; } = null!;

    public int PaisId { get; set; }

    public virtual Pais Pais { get; set; } = null!;

    public virtual ICollection<Transportista> Transportista { get; set; } = new List<Transportista>();

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
