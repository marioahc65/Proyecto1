using Newtonsoft.Json;
using System.Text;
using Proyecto1UI.Models;
using Proyecto1API.Models;
using System;

namespace Proyecto1UI.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _baseUrl;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<Clinica> ObtenerClinica(long CJuridica)
        {
            Clinica clinica = new Clinica();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"clinicas/{CJuridica}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Clinica>(json_respuesta);
                clinica = resultado;
            }

            return clinica;
        }

        public async Task<List<Clinica>> ObtenerClinicas()
        {
            List<Clinica> lista = new List<Clinica>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("clinicas");

            if(response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Clinica>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<Clinica> GuardarClinica(Clinica objeto)
        {
            Clinica clinica = new Clinica();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("clinicas/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Clinica>(json_respuesta);
                clinica = resultado;
            }

            return clinica;
        }

        public async Task<Clinica> ModificarClinica(long ClinicaId, Clinica objeto)
        {
            Clinica clinica = new Clinica();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"clinicas/{ClinicaId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Clinica>(json_respuesta);
                clinica = resultado;
            }

            return clinica;
        }

        public async Task<Enfermedade> ObtenerEnfermedad(long PacienteId)
        {
            Enfermedade enfermedad = new Enfermedade();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"enfermedades/{PacienteId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Enfermedade>(json_respuesta);
                enfermedad = resultado;
            }

            return enfermedad;
        }

        public async Task<List<Enfermedade>> ObtenerEnfermedades()
        {
            List<Enfermedade> lista = new List<Enfermedade>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("enfermedades");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Enfermedade>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<Enfermedade> GuardarEnfermedad(Enfermedade objeto)
        {
            Enfermedade enfermedad = new Enfermedade();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("enfermedades/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Enfermedade>(json_respuesta);
                enfermedad = resultado;
            }

            return enfermedad;
        }

        public async Task<Enfermedade> ModificarEnfermedad(long EnfermedadId, Enfermedade objeto)
        {
            Enfermedade enfermedad = new Enfermedade();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"enfermedades/{EnfermedadId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Enfermedade>(json_respuesta);
                enfermedad = resultado;
            }

            return enfermedad;
        }

        public async Task<Medico> ObtenerMedico(long CProfesional)
        {
            Medico medico = new Medico();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"medicos/{CProfesional}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Medico>(json_respuesta);
                medico = resultado;
            }

            return medico;
        }

        public async Task<List<Medico>> ObtenerMedicos()
        {
            List<Medico> lista = new List<Medico>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("medicos");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Medico>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<Medico> GuardarMedico(Medico objeto)
        {
            Medico medico = new Medico();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("medicos/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Medico>(json_respuesta);
                medico = resultado;
            }

            return medico;
        }

        public async Task<Medico> ModificarMedico(long MedicoId, Medico objeto)
        {
            Medico medico = new Medico();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"medicos/{MedicoId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Medico>(json_respuesta);
                medico = resultado;
            }

            return medico;
        }

        public async Task<Informe> ObtenerInforme(long PacienteId)
        {
            Informe informe = new Informe();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"informe/{PacienteId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Informe>(json_respuesta);
                informe = resultado;
            }

            return informe;
        }

        public async Task<List<Informe>> ObtenerInformes()
        {
            List<Informe> lista = new List<Informe>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("informe");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Informe>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<Informe> GuardarInforme(Informe objeto)
        {
            Informe informe = new Informe();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("informe/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Informe>(json_respuesta);
                informe = resultado;
            }

            return informe;
        }

        public async Task<Informe> ModificarInforme(long InformeId, Informe objeto)
        {
            Informe informe = new Informe();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"informe/{InformeId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Informe>(json_respuesta);
                informe = resultado;
            }

            return informe;
        }

        public async Task<Paciente> ObtenerPaciente(long Identificacion)
        {
            Paciente paciente = new Paciente();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"pacientes/{Identificacion}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Paciente>(json_respuesta);
                paciente = resultado;
            }

            return paciente;
        }

        public async Task<List<Paciente>> ObtenerPacientes()
        {
            List<Paciente> lista = new List<Paciente>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("pacientes");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Paciente>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<Paciente> GuardarPaciente(Paciente objeto)
        {
            Paciente paciente = new Paciente();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("pacientes/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Paciente>(json_respuesta);
                paciente = resultado;
            }

            return paciente;
        }

        public async Task<Paciente> ModificarPaciente(long PacienteId, Paciente objeto)
        {
            Paciente paciente = new Paciente();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"pacientes/{PacienteId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Paciente>(json_respuesta);
                paciente = resultado;
            }

            return paciente;
        }

        public async Task<Vacuna> ObtenerVacuna(long PacienteId)
        {
            Vacuna vacuna = new Vacuna();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"vacunas/{PacienteId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Vacuna>(json_respuesta);
                vacuna = resultado;
            }

            return vacuna;
        }

        public async Task<List<Vacuna>> ObtenerVacunas()
        {
            List<Vacuna> lista = new List<Vacuna>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("vacunas");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Vacuna>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<Vacuna> GuardarVacuna(Vacuna objeto)
        {
            Vacuna vacuna = new Vacuna();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("vacunas/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Vacuna>(json_respuesta);
                vacuna = resultado;
            }

            return vacuna;
        }

        public async Task<Vacuna> ModificarVacuna(long VacunaId, Vacuna objeto)
        {
            Vacuna vacuna = new Vacuna();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"vacunas/{VacunaId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Vacuna>(json_respuesta);
                vacuna = resultado;
            }

            return vacuna;
        }
    }
}
