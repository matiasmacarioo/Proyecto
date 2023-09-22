using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using static Proyecto.Controllers.ReportController;
using System.Reflection;
using System.Text;
using Proyecto.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{reportName}")]
        // Inside your ReportController
        public ActionResult Get(string reportName)
        {

            List<Profesor> profesoresList = _context.Profesores?.ToList();
            //List<Profesor> profesoresList = _context.Profesores?.Include(a => a.Carrera).ToList();

            ViewBag.ProfesoresList = profesoresList;

            var returnString = GenerateReportAsync(reportName, profesoresList);

            return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".pdf");
        }

        public byte[] GenerateReportAsync(string reportName, List<Profesor> profesoresList)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("ReportAPI.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}ReportFiles\\{1}.rdlc", fileDirPath, reportName);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
            LocalReport report = new LocalReport(rdlcFilePath);
            report.AddDataSource("DataSetProfesor", profesoresList);

            //var result = report.Execute(GetRenderType("pdf"), 0, parameters);
            //var result = report.Execute(RenderType.Pdf, 1);
            var result = report.Execute(RenderType.Pdf);
            return result.MainStream;
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;
            switch (reportType.ToLower())
            {
                default:
                case "pdf":
                    renderType = RenderType.Pdf;
                    break;
                case "word":
                    renderType = RenderType.Word;
                    break;
                case "excel":
                    renderType = RenderType.Excel;
                    break;
            }

            return renderType;
        }
    }
}

