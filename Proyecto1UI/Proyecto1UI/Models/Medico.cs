using System;
using System.Collections.Generic;

namespace Proyecto1API.Models;

public partial class Medico
{
    public long MedicoId { get; set; }

    public long Identificacion { get; set; }

    public long Cprofesional { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Informe> MedicoPacientes { get; } = new List<Informe>();
}
