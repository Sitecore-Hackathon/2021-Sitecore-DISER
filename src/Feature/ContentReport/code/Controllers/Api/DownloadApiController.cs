using Newtonsoft.Json;
using SitecoreDiser.Extensions.Attributes;
using SitecoreDiser.Feature.ContentReport.Models;
using SitecoreDiser.Feature.ContentReport.Repositories;
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


        /// <summary>
        /// Download the csv file for each type
        /// </summary>
        /// <param name="request">request model</param>
        /// <returns>csv file in http response</returns>
        [HttpPost]
        [AuthorizeApi(AllowAnonymous = false, AllowDatabase = AuthorizeApiAttribute.SitecoreDatabase.Master)]
        public IHttpActionResult DownloadReport(ReportModel request)
        {
            if (!request.IsValid())
            {
                string error = JsonConvert.SerializeObject(request.ErrorMessage, Formatting.Indented);
                return Json(error);
            }

            var _contentReportRepository = new ContentReportRepository();
            var csvFileName = request.Type + ".csv";
            var csvContent = GenerateCsv(_contentReportRepository.GetResults(request), request.Type);

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

        /// <summary>
        /// This method generates CSV file content to be downloaded
        /// </summary>
        /// <param name="reportDatamodel">data model with result items</param>
        /// <param name="type">type of requeest</param>
        /// <returns>CSV string</returns>
        private StringBuilder GenerateCsv(ReportDataModel reportDatamodel, string type)
        {
            var csv = new StringBuilder();
            if (type == Constants.ArchivedType)
            {
                csv.AppendLine("Archive Item Id,Archive Item Name,Archive By,Archive Date, Original Path");
                if (reportDatamodel == null || reportDatamodel.ArchivedItems == null || reportDatamodel.ArchivedItems.Count <= 0) return csv;
                foreach (var result in reportDatamodel.ArchivedItems)
                {
                    csv.AppendLine(string.Format("{0},{1},{2},{3},{4}", result.ItemId, result.ItemName, result.UpdatedBy, result.UpdatedDate, result.FullPath));
                }
            }
            if (type == Constants.CreatedType)
            {
                csv.AppendLine("Item Id,Item Name,Item Path,Created/Updated User,Language,Version");
                if (reportDatamodel == null || reportDatamodel.CreatedResults == null || reportDatamodel.CreatedResults.Count <= 0) return csv;
                foreach (var result in reportDatamodel.CreatedResults)
                {
                    csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.FullPath, result.FullPath, result.UpdatedBy, result.Language, result.Version));
                }
            }
            if (type == Constants.UpdatedType)
            {
                csv.AppendLine("Item Id,Item Name,Item Path,Created/Updated User,Language,Version");
                if (reportDatamodel == null || reportDatamodel.UpdatedResults == null || reportDatamodel.UpdatedResults.Count <= 0) return csv;
                foreach (var result in reportDatamodel.UpdatedResults)
                {
                    csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.FullPath, result.FullPath, result.UpdatedBy, result.Language, result.Version));
                }
            }

            return csv;
        }
    }
}