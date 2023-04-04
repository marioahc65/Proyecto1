using System;
using System.Collections.Generic;

namespace Proyecto1API.Models;

public partial class Paciente
{
    public long PacienteId { get; set; }

    public long Identificacion { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public DateTime Fnacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public long Telefono { get; set; }

    public string Pais { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Distrito { get; set; }

    public string Ecivil { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Ocupacion { get; set; } = null!;

    public virtual ICollection<Enfermedade> Enfermedades { get; } = new List<Enfermedade>();

    public virtual ICollection<Informe> Informes { get; } = new List<Informe>();

    public virtual ICollection<Vacuna> Vacunas { get; } = new List<Vacuna>();
}
