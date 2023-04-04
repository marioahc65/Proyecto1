using Microsoft.EntityFrameworkCore;
using Proyecto1API.Data;
using Proyecto1API.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();
builder.WebHost.UseNLog();

var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection");
builder.Services.AddDbContext<ContextDb>(options =>
   options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


/////Medicos

app.MapPost("/medicos/", async (Medico medico, ContextDb db) =>
{
    app.Logger.LogInformation("Medico creado");

    db.Medicos.Add(medico);
    await db.SaveChangesAsync();

    return Results.Created($"/medicos/{medico.Cprofesional}", medico);
});

app.MapGet("/medicos/{Cprofesional:long}", async (long Cprofesional, ContextDb db) => {
    app.Logger.LogInformation("Medico encontrado");

    return await db.Medicos.Where(x => x.Cprofesional == Cprofesional).Include(x=> x.MedicoPacientes).FirstOrDefaultAsync() 
    is Medico medico ? Results.Ok(medico)
    : Results.NotFound();
});

app.MapGet("/medicos", async (ContextDb db) => await db.Medicos.Include(x => x.MedicoPacientes).ToListAsync());

app.MapPut("/medicos/{id:long}", async (long id,Medico medico, ContextDb db) => {

    app.Logger.LogInformation("Medico actualizado");
    if(id != medico.MedicoId)
        return Results.BadRequest();

    var medicoResult = await db.Medicos.FindAsync(id);

    if(medicoResult is null) return Results.NotFound();

    medicoResult.Identificacion = medico.Identificacion;
    medicoResult.Cprofesional = medico.Cprofesional;
    medicoResult.Nombre = medico.Nombre;
    medicoResult.Apellido1 = medico.Apellido1;
    medicoResult.Apellido2 = medico.Apellido2;
    medicoResult.Email = medico.Email;
    medicoResult.Pais = medico.Pais;
    medicoResult.Estado = medico.Estado;

    await db.SaveChangesAsync();

    return Results.Ok(medicoResult);
});

///////Clinicas

app.MapPost("/clinicas/", async (Clinica clinica, ContextDb db) =>
{
    app.Logger.LogInformation("Clinica creada");
    db.Clinicas.Add(clinica);
    await db.SaveChangesAsync();

    return Results.Created($"/clinicas/{clinica.Cjuridica}", clinica);

});

app.MapGet("/clinicas/{Cjuridica:long}", async (long Cjuridica, ContextDb db) => {
    app.Logger.LogInformation("Clinica encontrada");
    return await db.Clinicas.Where(x=> x.Cjuridica == Cjuridica).FirstOrDefaultAsync()
    is Clinica clinica ? Results.Ok(clinica)
    : Results.NotFound();
});

app.MapGet("/clinicas", async (ContextDb db) => await db.Clinicas.ToListAsync());

app.MapPut("/clinicas/{id:long}", async (long id, Clinica clinica, ContextDb db) => {
    app.Logger.LogInformation("Clinica actualizada");
    if (id != clinica.ClinicaId)
        return Results.BadRequest();

    var clinicaResult = await db.Clinicas.FindAsync(id);

    if (clinicaResult is null) return Results.NotFound();

    clinicaResult.Cjuridica = clinica.Cjuridica;
    clinicaResult.Nombre = clinica.Nombre;
    clinicaResult.Pais = clinica.Pais;
    clinicaResult.Estado = clinica.Estado;
    clinicaResult.Distrito = clinica.Distrito;
    clinicaResult.Telefono = clinica.Telefono;
    clinicaResult.Correo = clinica.Correo;
    clinicaResult.SitioWeb = clinica.SitioWeb;

    await db.SaveChangesAsync();

    return Results.Ok(clinicaResult);
});

//////////Vacuna
app.MapPost("/vacunas/", async (Vacuna vacuna, ContextDb db) =>
{
    try { 
    app.Logger.LogInformation("Vacuna creada");
    db.Vacunas.Add(vacuna);
    await db.SaveChangesAsync();

    return Results.Created($"/vacunas/{vacuna.PacienteId}", vacuna);
    }
    catch(Exception ex) 
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

app.MapGet("/vacunas/{PacienteId:long}", async (long PacienteId, ContextDb db) => {
    app.Logger.LogInformation("Vacuna encontrada");
    try { 
    return await db.Vacunas.Where (x => x.PacienteId == PacienteId).FirstOrDefaultAsync()
    is Vacuna vacuna ? Results.Ok(vacuna)
    : Results.NotFound();
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString()); return Results.NotFound();
    }
});

app.MapGet("/vacunas", async (ContextDb db) => await db.Vacunas.ToListAsync());

app.MapPut("/vacunas/{id:long}", async (long id, Vacuna vacuna, ContextDb db) => {
    try { 
    app.Logger.LogInformation("Vacuna actualizada");
    if (id != vacuna.VacunasId)
        return Results.BadRequest();

    var vacunaResult = await db.Vacunas.FindAsync(id);

    if (vacunaResult is null) return Results.NotFound();

    vacunaResult.IsSarampion = vacuna.IsSarampion;
    vacunaResult.IsRubeola = vacuna.IsRubeola;
    vacunaResult.IsParatiditis = vacuna.IsParatiditis;
    vacunaResult.IsTetano = vacuna.IsTetano;
    vacunaResult.IsHepatitisAb = vacuna.IsHepatitisAb;
    vacunaResult.IsInfluenza = vacuna.IsInfluenza;
    vacunaResult.IsCovid = vacuna.IsCovid;
    vacunaResult.DosisCovid = vacuna.DosisCovid;
    vacunaResult.RazonCovid = vacuna.RazonCovid;

    await db.SaveChangesAsync();

    return Results.Ok(vacuna);
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest();
    }
});

//////////Enfermedades
app.MapPost("/enfermedades/", async (Enfermedade enfermedad, ContextDb db) =>
{
    try
    {
        app.Logger.LogInformation("Enfermedad creada");
        db.Enfermedades.Add(enfermedad);
        await db.SaveChangesAsync();

        return Results.Created($"/enfermedades/{enfermedad.PacienteId}", enfermedad);
    }catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString()); return Results.BadRequest();
    }

});

app.MapGet("/enfermedades/{PacienteId:long}", async (long PacienteId, ContextDb db) => {
    try { 
    app.Logger.LogInformation("Enfermedad encontrada");
    return await db.Enfermedades.Where(x => x.PacienteId == PacienteId).FirstOrDefaultAsync()
    is Enfermedade enfermedad ? Results.Ok(enfermedad)
    : Results.NotFound();
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

app.MapGet("/enfermedades", async (ContextDb db) => await db.Enfermedades.ToListAsync());

app.MapPut("/enfermedades/{id:long}", async (long id, Enfermedade enfermedad, ContextDb db) => {
    try { 
    app.Logger.LogInformation("Enfermedad actualizada");
    if (id != enfermedad.EnfermedadesId)
        return Results.BadRequest();

    var enfermedadResult = await db.Enfermedades.FindAsync(id);

    if (enfermedadResult is null) return Results.NotFound();

    enfermedadResult.Sintomas = enfermedad.Sintomas;

    await db.SaveChangesAsync();

    return Results.Ok(enfermedad);
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

////////MedicoPaciente

app.MapPost("/informe/", async (MedicoPaciente informe, ContextDb db) =>
{
    try { 
    app.Logger.LogInformation("Informe Creado");
    db.MedicoPacientes.Add(informe);
    await db.SaveChangesAsync();

    return Results.Created($"/informe/{informe.PacienteId}", informe);
    }
    catch(Exception ex) {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

app.MapGet("/informe/{PacienteId:long}", async (long PacienteId, ContextDb db) => {
    try { 
    app.Logger.LogInformation("Informe Encontrado");
    return await db.MedicoPacientes.Where(x => x.PacienteId == PacienteId).OrderByDescending(x => x.MedicoPacienteId).FirstOrDefaultAsync()
    is MedicoPaciente informe ? Results.Ok(informe)
    : Results.NotFound();
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

app.MapGet("/informe", async (ContextDb db) => await db.MedicoPacientes.ToListAsync());

app.MapPut("/informe/{id:long}", async (long id, MedicoPaciente informe, ContextDb db) => {
    try
    {  
    app.Logger.LogInformation("Informe actualizado");
    if (id != informe.MedicoPacienteId)
        return Results.BadRequest();

    var informeResult = await db.MedicoPacientes.FindAsync(id);

    if (informeResult is null) return Results.NotFound();

    informeResult.Fregistro = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.Ok(informe);
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

/////Paciente

app.MapPost("/pacientes/", async (Paciente paciente, ContextDb db) =>
{
    try { 
    app.Logger.LogInformation("Paciente creado");
    db.Pacientes.Add(paciente);
    await db.SaveChangesAsync();

    return Results.Created($"/pacientes/{paciente.Identificacion}", paciente);
    }catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString()); return Results.NotFound();
    }
});

app.MapGet("/pacientes/{Identificacion:long}", async (long Identificacion, ContextDb db) => {
    try { 
    app.Logger.LogInformation("Paciente encontrado");
    return await db.Pacientes.Where(x => x.Identificacion == Identificacion).Include(x=>x.Vacunas).Include(x=>x.Enfermedades).Include(x=> x.MedicoPacientes).FirstOrDefaultAsync()
    is Paciente paciente ? Results.Ok(paciente)
    : Results.NotFound();
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

app.MapGet("/pacientes", async (ContextDb db) => await db.Pacientes.Include(x => x.Vacunas).Include(x => x.Enfermedades).Include(x => x.MedicoPacientes).ToListAsync());

app.MapPut("/pacientes/{id:long}", async (long id, Paciente paciente, ContextDb db) => {
    try { 
    app.Logger.LogInformation("Paciente actualizado");
    if (id != paciente.PacienteId)
        return Results.BadRequest();

    var pacienteResult = await db.Pacientes.FindAsync(id);

    if (pacienteResult is null) return Results.NotFound();

    pacienteResult.Identificacion = paciente.Identificacion;
    pacienteResult.Nombre = paciente.Nombre;
    pacienteResult.Apellido1 = paciente.Apellido1;
    pacienteResult.Apellido2 = paciente.Apellido2;
    pacienteResult.Fnacimiento = paciente.Fnacimiento;
    pacienteResult.Sexo = paciente.Sexo;
    pacienteResult.Telefono = paciente.Telefono;
    pacienteResult.Pais = paciente.Pais;
    pacienteResult.Estado = paciente.Estado;
    pacienteResult.Distrito = paciente.Distrito;
    pacienteResult.Ecivil = paciente.Ecivil;
    pacienteResult.Email = paciente.Email;
    pacienteResult.Ocupacion = paciente.Ocupacion;

        await db.SaveChangesAsync();

    return Results.Ok(pacienteResult);
    }catch(Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.NotFound();
    }
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}