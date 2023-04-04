using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto1API.Models;

public partial class Informe
{
    public long MedicoPacienteId { get; set; }

    public long? PacienteId { get; set; }

    public long? MedicoId { get; set; }

    public long? ClinicaId { get; set; }

    public Paciente? paciente { get; set; }

    public Medico? medico { get; set; }

    public Clinica? clinica { get; set; }  

    public DateTime Fregistro { get; set; }
}
