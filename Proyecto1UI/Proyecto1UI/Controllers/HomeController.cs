using Microsoft.AspNetCore.Mvc;
using Proyecto1UI.Models;
using System.Diagnostics;
using Proyecto1UI.Servicios;
using Proyecto1API.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.FlowAnalysis;

using DinkToPdf;
using DinkToPdf.Contracts;
using Proyecto1UI.Extension;
using Microsoft.AspNetCore.Http.Extensions;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Html;

namespace Proyecto1UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicioApi;

        private readonly IConverter _converter;

        public HomeController(IServicio_API servicioApi, IConverter converter)
        {
            _servicioApi = servicioApi;
            _converter = converter;
        }

        Paciente paciente = new Paciente();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HistorialFamiliar()
        {
            return View();
        }

        public IActionResult CheckClinica()
        {
            return View();
        }
        public IActionResult CheckPaciente()
        {
            return View();
        }

        public IActionResult ImprimirDocumento(Informe informe)
        {
            return View(informe);
        }

        public async Task<ActionResult> DescargarPDF()
        {
            Informe resInforme = new Informe();
            long MedicoId = long.Parse((string)TempData["Medico"]);
            long Paciente = long.Parse((string)TempData["Paciente"]);
            long PacienteI = long.Parse((string)TempData["PacienteI"]);
            long ClinicaId = 0;


            resInforme = await _servicioApi.ObtenerInforme(Paciente);
            resInforme.paciente = await _servicioApi.ObtenerPaciente(PacienteI);
            resInforme.medico = await _servicioApi.ObtenerMedico(MedicoId);
            if (!string.IsNullOrEmpty((string)TempData["Clinica"]))
            {
                ClinicaId = long.Parse((string)TempData["Clinica"]);
                resInforme.clinica = await _servicioApi.ObtenerClinica(ClinicaId);
                resInforme.ClinicaId = resInforme.clinica.ClinicaId;
            }

            /*string pagina_actual = HttpContext.Request.Path;
            String url_pagina = HttpContext.Request.GetEncodedUrl();
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = url_pagina + "/Home/ImprimirDocumento/"+ (new {informe = resInforme});*/

            string pagina_actual = HttpContext.Request.Path;
            String url_pagina =  ConvertUserListToHtml(resInforme);

            var pdf = new  HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = url_pagina,
                        
                    }
                }
            };

            var archivoPDf = _converter.Convert( pdf );
            string nombrePDF = "reporte_" + DateTime.Now.ToString("ddMMyyyyHHss") + ".pdf";

            return File(archivoPDf, "application/pdf", nombrePDF);



        }

        private string ConvertUserListToHtml(Informe informe)
        {
            string html = @$"<html><head></head><body><div><dl class='row'>
                <dt class='col-sm-3'>Nombre del Medico</dt>
                <dd class='col-sm-9' >{informe.medico.Nombre} {informe.medico.Apellido1} {informe.medico.Apellido2}</dd>

                <dt class='col-sm-3'>Codigo del Medico</dt>
                <dd class='col-sm-9'>{informe.medico.Cprofesional}</dd>

                <dt class='col-sm-3'>Nombre del Paciente</dt>
                <dd class='col-sm-9'>{informe.paciente.Nombre} {informe.paciente.Apellido1} {informe.paciente.Apellido2}</dd>

                <dt class='col-sm-3'>Cedula del Paciente</dt>
                <dd class='col-sm-9'>{informe.paciente.Identificacion}</dd>";

            if (informe.clinica != null)
            {
                html += @$"<dt class='col-sm-3'>Datos de la Clinica</dt>
                    <dd class='col-sm-9'>
                        <p>{informe.clinica.Nombre}</p>
                        <p>{informe.clinica.Cjuridica}</p>
                        <p>{informe.clinica.Pais}</p>
                        <p>{informe.clinica.Distrito}</p>
                    </dd>";
            }
            html += @$"<dt class='col-sm-3'>Datos de la Consulta</dt>";
            html += @$"<dt class='col-sm-3'>Inyecciones</dt>";

            if (informe.paciente.Vacunas.First().IsTetano)
            {
                html += $@"<dd class='col-sm-9'>
                     <p>Tetano</p>";
            }
            if (informe.paciente.Vacunas.First().IsInfluenza)
            {
                html += $@"<dd class='col-sm-9'>
                     <p>Influenza</p>";
            }
            if (informe.paciente.Vacunas.First().IsCovid)
            {
                html += $@"<dd class='col-sm-9'>
                     <p>COVID</p>";
            }
            if (informe.paciente.Vacunas.First().IsRubeola)
            {
                html += $@"<dd class='col-sm-9'>
                     <p>Rubeola</p>";
            }
            if (informe.paciente.Vacunas.First().IsSarampion)
            {
                html += $@"<dd class='col-sm-9'>
                     <p>Sarampeon</p>";
            }
            if (informe.paciente.Vacunas.First().IsParatiditis)
            {
                html += $@"<dd class='col-sm-9'>
                     <p>Paratiditis</p>";
            }
            if(informe.paciente.Vacunas.First().RazonCovid != "")
            {
                html += $@"<dt class='col-sm-3'>Razon por la cual no vacunarse del COVID</dt>
                        <dd><p>{informe.paciente.Vacunas.First().RazonCovid}</p></dd>";
            }
            if(informe.paciente.Vacunas.First().DosisCovid > 0)
            {
                html += $@"<dt class='col-sm-3'>Cantidad de Dosis COVID</dt>
                        <dd><p>{informe.paciente.Vacunas.First().DosisCovid}</p></dd>";
            }
            html += $@"</dd>";

            if(informe.paciente.Enfermedades.LastOrDefault().Sintomas !="[]")
            {
                html += $@"<dt class=""col-sm-3"">Sintomas</dt>
                    <dd class=""col-sm-9"">
                            {informe.paciente.Enfermedades.LastOrDefault().Sintomas}
                    </dd>";
            }
            if (informe.paciente.Enfermedades.LastOrDefault().NuevasEnfermedades != "[]")
            {
                html += $@"<dt class=""col-sm-3"">Nuevas Enfermedades</dt>
                    <dd class=""col-sm-9"">
                            {informe.paciente.Enfermedades.LastOrDefault().NuevasEnfermedades}
                    </dd>";
            }
            if (informe.paciente.Enfermedades.LastOrDefault().Alergias != "[]")
            {
                html += $@"<dt class=""col-sm-3"">Alergias</dt>
                    <dd class=""col-sm-9"">
                            {informe.paciente.Enfermedades.LastOrDefault().Alergias}
                    </dd>";
            }
            if (informe.paciente.Enfermedades.LastOrDefault().OtrosSintomas != "[]")
            {
                html += $@"<dt class=""col-sm-3"">Otros Sintomas</dt>
                    <dd class=""col-sm-9"">
                            {informe.paciente.Enfermedades.LastOrDefault().OtrosSintomas}
                    </dd>";
            }
            if (informe.paciente.Enfermedades.LastOrDefault().OtrasCondiciones != "")
            {
                html += $@"<dt class=""col-sm-3"">Otras Condiciones</dt>
                    <dd class=""col-sm-9"">
                            {informe.paciente.Enfermedades.LastOrDefault().OtrasCondiciones}
                    </dd>";
            }
            if (informe.paciente.Enfermedades.LastOrDefault().RCancer != "")
            {
                html += $@"<dt class=""col-sm-3"">Reaparicion de Cancer</dt>
                    <dd class=""col-sm-9"">
                            {informe.paciente.Enfermedades.LastOrDefault().RCancer}
                    </dd>";
            }

            html += $@"</div></body></html>";

            return html;

        }
                

    public async Task<IActionResult> Clinica(long CJuridica)
        {
            Clinica modelo_clinica = new Clinica();

            ViewBag.Accion = "Agregar Clínica";

            if (CJuridica != 0)
            {

                modelo_clinica = await _servicioApi.ObtenerClinica(CJuridica);
                ViewBag.Accion = "Editar Clínica";
            }

            return View(modelo_clinica);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarClinica(long ClinicaId, Clinica ob_clinica)
        {
            Clinica resClinica = new Clinica();

            if (ob_clinica.ClinicaId == 0)
            {
                resClinica = await _servicioApi.GuardarClinica(ob_clinica);
            }
            else
            {
                resClinica = await _servicioApi.ModificarClinica(ClinicaId, ob_clinica);
            }

            if (resClinica != null)
            {
                TempData["Clinica"] = resClinica.Cjuridica.ToString();
                return RedirectToAction("CheckPaciente");
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Medico(long CProfesional)
        {
            Medico modelo_medico = new Medico();

            ViewBag.Accion = "Agregar Médico";

            if (CProfesional != 0)
            {

                modelo_medico = await _servicioApi.ObtenerMedico(CProfesional);
                if (modelo_medico.MedicoId != 0)
                {
                    ViewBag.Accion = "Editar Médico";
                }
            }

            return View(modelo_medico);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarMedico(long MedicoId, Medico ob_medico)
        {
            Medico resMedico = new Medico();

            if (ob_medico.MedicoId == 0)
            {
                resMedico = await _servicioApi.GuardarMedico(ob_medico); ;
            }
            else
            {
                resMedico = await _servicioApi.ModificarMedico(MedicoId, ob_medico);
            }

            if (resMedico != null)
            {
                TempData["Medico"] = resMedico.Cprofesional.ToString();
                return RedirectToAction("CheckClinica");
            }
            else
            {
                return NoContent();
            }
        }


        public async Task<IActionResult> Paciente(long Identificacion)
        {
            Paciente modelo_paciente = new Paciente();

            ViewBag.Accion = "Agregar Paciente";

            if (Identificacion != 0)
            {

                modelo_paciente = await _servicioApi.ObtenerPaciente(Identificacion);
                paciente = modelo_paciente;
            }

            return View(modelo_paciente);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarPaciente(long PacienteId, Paciente ob_paciente)
        {
            Paciente resPaciente = new Paciente();

            if (ob_paciente.PacienteId == 0)
            {
                resPaciente = await _servicioApi.GuardarPaciente(ob_paciente);
            }
            else
            {
                resPaciente = await _servicioApi.ModificarPaciente(PacienteId, ob_paciente);
            }

            if (resPaciente != null)
            {
                TempData["Paciente"] = resPaciente.PacienteId.ToString();
                TempData["PacienteI"] = resPaciente.Identificacion.ToString();
                return RedirectToAction("Vacuna");
            }
            else
            {
                return NoContent();
            }
        }


        public async Task<IActionResult> Vacuna()
        {
            long PacienteId = long.Parse((string)TempData["Paciente"]);
            TempData["Paciente"] = PacienteId.ToString();

            Vacuna modelo_vacuna = new Vacuna();

            ViewBag.Accion = "Agregar Vacunas";

            //PacienteId = test.PacienteId;

            if (PacienteId != 0)
            {

                modelo_vacuna = await _servicioApi.ObtenerVacuna(PacienteId);
                if (modelo_vacuna.VacunasId != 0)
                {
                    ViewBag.Accion = "Editar Vacunas";
                }

            }

            return View(modelo_vacuna);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarVacuna(long VacunasId, Vacuna ob_vacuna)
        {
            long PacienteId = long.Parse((string)TempData["Paciente"]);
            Vacuna resVacuna = new Vacuna();
            ob_vacuna.PacienteId = PacienteId;

            if(ob_vacuna.RazonCovid == null)
            {
                resVacuna.RazonCovid = "";
            }

            if (ob_vacuna.VacunasId == 0)
            {
                resVacuna = await _servicioApi.GuardarVacuna(ob_vacuna);
            }
            else
            {
                resVacuna = await _servicioApi.ModificarVacuna(VacunasId, ob_vacuna);
            }

            if (resVacuna != null)
            {
                return RedirectToAction("Enfermedad");
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Enfermedad(long PacienteId)
        {
            Enfermedade modelo_enfermedad = new Enfermedade();

            ViewBag.Accion = "Agregar Enfermedad";

            if (PacienteId != 0)
            {

                modelo_enfermedad = await _servicioApi.ObtenerEnfermedad(PacienteId);
                ViewBag.Accion = "Editar Enfermedad";
            }

            return View(modelo_enfermedad);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarEnfermedad(string[] Sintomas, string[] Alergias, string[] NuevasEnfermedades, string[] OtrosSintomas, string OtrasCondiciones, string RCancer)
        {
            long EnfermedadId = 0;
            long PacienteId = long.Parse((string)TempData["Paciente"]);
            Enfermedade resEnfermedad = new Enfermedade();
            Enfermedade ob_enfermedad = new Enfermedade();
            Informe resInforme = new Informe();
            ob_enfermedad.Sintomas = JsonConvert.SerializeObject(Sintomas);
            ob_enfermedad.Alergias = JsonConvert.SerializeObject(Alergias);
            ob_enfermedad.NuevasEnfermedades = JsonConvert.SerializeObject(NuevasEnfermedades);
            ob_enfermedad.OtrasCondiciones = !string.IsNullOrEmpty(OtrasCondiciones) ? OtrasCondiciones : "";
            ob_enfermedad.RCancer = !string.IsNullOrEmpty(RCancer) ? RCancer : "";
            ob_enfermedad.OtrosSintomas = JsonConvert.SerializeObject(OtrosSintomas);

            ob_enfermedad.PacienteId = PacienteId;


            if (ob_enfermedad.EnfermedadesId == 0)
            {
                resEnfermedad = await _servicioApi.GuardarEnfermedad(ob_enfermedad);

                long MedicoId = long.Parse((string)TempData["Medico"]);
                long PacienteI = long.Parse((string)TempData["PacienteI"]);
                long ClinicaId = 0;

                resInforme.paciente = await _servicioApi.ObtenerPaciente(PacienteI);
                resInforme.medico = await _servicioApi.ObtenerMedico(MedicoId);
                if (!string.IsNullOrEmpty((string)TempData["Clinica"]))
                {
                    ClinicaId = long.Parse((string)TempData["Clinica"]);
                    resInforme.clinica = await _servicioApi.ObtenerClinica(ClinicaId);
                    resInforme.ClinicaId = resInforme.clinica.ClinicaId;

                }

                resInforme.MedicoId = resInforme.medico.MedicoId;
                resInforme.PacienteId = resInforme.paciente.PacienteId;

                resInforme.Fregistro = DateTime.Now;

                resInforme = await _servicioApi.GuardarInforme(resInforme);

            }
            else
            {
                resEnfermedad = await _servicioApi.ModificarEnfermedad(EnfermedadId, ob_enfermedad);
            }

            if (resEnfermedad != null)
            {
                return RedirectToAction("Informe", new { PacienteId = resInforme.PacienteId });
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> Informe(long PacienteId)
        {

            Informe modelo_informe = new Informe();

            ViewBag.Accion = "Agregar Informe";

            if (PacienteId != 0)
            {
                long MedicoId = long.Parse((string)TempData["Medico"]);
                long PacienteI = long.Parse((string)TempData["PacienteI"]);

                long ClinicaId = 0;

                if (!string.IsNullOrEmpty((string)TempData["Clinica"]))
                {
                    ClinicaId = long.Parse((string)TempData["Clinica"]);

                }

                if (ClinicaId != 0)
                {

                    modelo_informe.clinica = await _servicioApi.ObtenerClinica(ClinicaId);
                    TempData["Clinica"] = ClinicaId.ToString();
                }
                modelo_informe = await _servicioApi.ObtenerInforme(PacienteId);
                modelo_informe.paciente = await _servicioApi.ObtenerPaciente(PacienteI);
                modelo_informe.medico = await _servicioApi.ObtenerMedico(MedicoId);

                TempData["Medico"] = MedicoId.ToString();
                TempData["PacienteI"] = PacienteI.ToString();

                ViewBag.Accion = "Generar Informe";
            }

            return View(modelo_informe);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarInforme(long InformeId, Informe ob_informe)
        {
            Informe resInforme = new Informe();

            if (InformeId == 0)
            {
                resInforme = await _servicioApi.GuardarInforme(ob_informe);
            }
            else
            {
                resInforme = await _servicioApi.ModificarInforme(InformeId, ob_informe);
            }

            if (resInforme != null)
            {
                return RedirectToAction("Informe", resInforme.PacienteId);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
    public class FileDto
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public FileDto(string fileName, byte[] fileBytes)
        {
            FileName = fileName;
            FileBytes = fileBytes;
        }
    }