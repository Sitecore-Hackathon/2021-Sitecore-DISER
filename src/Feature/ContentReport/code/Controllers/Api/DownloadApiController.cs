using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Feature.ContentReport.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace SitecoreDiser.Feature.ContentReport.Controllers.Api
{
    public class DownloadApiController
    {
        private readonly IContentReportRepository _contentReportRepository;

        public DownloadApiController(IContentReportRepository contentReportRepository)
        {
            _contentReportRepository = contentReportRepository;
        }

        [HttpPost]
        public IHttpActionResult DownloadReport(RequestModel request)
        {
            var csvFileName = "Results.csv";
            var csvContent = GenerateCsv(_contentReportRepository.GetResults(request));
            switch (request.Type)
            {
                case "CreatedItem":
                    csvFileName = "CreatedItems.csv";
                    break;
                case "UpdatedItem":
                    csvFileName = "UpdatedItems.csv";
                    break;
                case "ArchivedItem":
                    csvFileName = "ArchivedItems.csv";
                    break;
                default:
                    break;
            }

            //Download the CSV file.
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + csvFileName);
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.Output.Write(csvContent.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            return null;
        }

        private StringBuilder GenerateCsv(List<ResultModel> results)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Item Id,Item Name,Item Path,Created/Updated User,Language,Version");
            foreach (var result in results)
            {
                csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.ItemName, result.ItemPath, result.User, result.Language, result.Version));
            }
            return csv;
        }
    }
}