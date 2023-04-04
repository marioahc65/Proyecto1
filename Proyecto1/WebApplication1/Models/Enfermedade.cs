using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto1API.Models;

public partial class Enfermedade
{
    public long EnfermedadesId { get; set; }

    public string Sintomas { get; set; } = null!;
    public string Alergias { get; set; } = null!;
    public string NuevasEnfermedades { get; set; } = null!;

    public string OtrasCondiciones { get; set; } = null!;

    public string RCancer { get; set; } = null!;

    public string OtrosSintomas { get; set; } = null!;

    public long? PacienteId { get; set; }

    [JsonIgnore]
    public virtual Paciente? Paciente { get; set; }
}
