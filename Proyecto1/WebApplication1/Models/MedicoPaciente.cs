using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto1API.Models;

public partial class MedicoPaciente
{
    public long MedicoPacienteId { get; set; }

    public long? PacienteId { get; set; }

    public long? MedicoId { get; set; }

    public long? ClinicaId { get; set; }

    public DateTime Fregistro { get; set; }
    [JsonIgnore]
    public virtual Medico? Medico { get; set; }
    [JsonIgnore]
    public virtual Paciente? Paciente { get; set; }
    [JsonIgnore]
    public virtual Clinica? Clinica { get; set; }
}
