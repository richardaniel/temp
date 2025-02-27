using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Evaluaciones;

public partial class Evaluacion
{
    public int EvaluacionId { get; set; }

    public string Evaluador { get; set; } = null!;

    public string? Evaluacion_ { get; set; }

    public int Calificacion { get; set; }

    public int? ColaboradorId { get; set; }

    public int? ViajeId { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual Colaborador? Colaborador { get; set; }

    public virtual Viaje? Viaje { get; set; }
}
