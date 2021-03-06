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
    public class DownloadApiController : ApiController
    {
        public DownloadApiController()
        {
        }
       
        [HttpPost]
        public IHttpActionResult DownloadReport(RequestModel request)
        {
            var _contentReportRepository = new ContentReportRepository();
            var csvFileName = request.Type + ".csv";
            var csvContent = GenerateCsv(_contentReportRepository.GetResults(request));

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
            if (results == null) return csv;
            foreach (var result in results)
            {
                csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.ItemName, result.ItemPath, result.User, result.Language, result.Version));
            }
            return csv;
        }
    }
}