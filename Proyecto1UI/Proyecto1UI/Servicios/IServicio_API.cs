using Proyecto1API.Models;
using Proyecto1UI.Models;

namespace Proyecto1UI.Servicios
{
    public interface IServicio_API
    {
        //Clinica
        Task<Clinica> ObtenerClinica(long ClinicaId);
        Task<List<Clinica>> ObtenerClinicas();
        Task<Clinica> GuardarClinica(Clinica objeto);
        Task<Clinica> ModificarClinica(long ClinicaId,  Clinica objeto);
        //Enfermedades
        Task<Enfermedade> ObtenerEnfermedad(long PacienteId);
        Task<List<Enfermedade>> ObtenerEnfermedades();
        Task<Enfermedade> GuardarEnfermedad(Enfermedade objeto);
        Task<Enfermedade> ModificarEnfermedad(long EnfermedadId, Enfermedade objeto);
        //Medico
        Task<Medico> ObtenerMedico(long CProfesional);
        Task<List<Medico>> ObtenerMedicos();
        Task<Medico> GuardarMedico(Medico objeto);
        Task<Medico> ModificarMedico(long MedicoId, Medico objeto);
        //Informe
        Task<Informe> ObtenerInforme(long PacienteId);
        Task<List<Informe>> ObtenerInformes();
        Task<Informe> GuardarInforme(Informe objeto);
        Task<Informe> ModificarInforme(long InformeId, Informe objeto);
        //Paciente
        Task<Paciente> ObtenerPaciente (long Identificacion);
        Task<List<Paciente>> ObtenerPacientes();
        Task<Paciente> GuardarPaciente(Paciente objeto);
        Task<Paciente> ModificarPaciente(long PacienteId, Paciente objeto);

        //Vacuna
        Task<Vacuna> ObtenerVacuna (long VacunaId);
        Task<List<Vacuna>> ObtenerVacunas();
        Task<Vacuna> GuardarVacuna(Vacuna objeto);
        Task<Vacuna> ModificarVacuna(long VacunaId, Vacuna objeto);

    }
}
