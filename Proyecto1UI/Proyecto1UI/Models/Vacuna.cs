using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto1API.Models;

public partial class Vacuna
{
    public long VacunasId { get; set; }

    public bool IsSarampion { get; set; }

    public bool IsRubeola { get; set; }

    public bool IsParatiditis { get; set; }

    public bool IsTetano { get; set; }

    public bool IsHepatitisAb { get; set; }

    public bool IsInfluenza { get; set; }

    public bool IsCovid { get; set; }

    public int DosisCovid { get; set; }

    public string? RazonCovid { get; set; }

    public long? PacienteId { get; set; }

}
