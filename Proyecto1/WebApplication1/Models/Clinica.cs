using System;
using System.Collections.Generic;

namespace Proyecto1API.Models;

public partial class Clinica
{
    public long ClinicaId { get; set; }

    public long Cjuridica { get; set; }

    public string Nombre { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Distrito { get; set; }

    public long Telefono { get; set; }

    public string Correo { get; set; } = null!;

    public string SitioWeb { get; set; } = null!;
    public virtual ICollection<MedicoPaciente> MedicoPacientes { get; } = new List<MedicoPaciente>();
}
