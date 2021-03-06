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
        public IHttpActionResult DownloadReport(ReportModel request)
        {
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

        private StringBuilder GenerateCsv(ReportDataModel tabItemModel, string type)
        {
            var csv = new StringBuilder();
            if (type == "ArchivedItems")
            {
                csv.AppendLine("Archive Item Id,Archive Item Name,Archive By,Archive Date, Original Path");
                if (tabItemModel == null || tabItemModel.ArchivedItems == null || tabItemModel.ArchivedItems.Count <= 0) return csv;
                foreach (var result in tabItemModel.ArchivedItems)
                {
                    csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.ItemName, result.UpdatedBy, result.UpdatedDate, result.FullPath));
                }
            }
            if (type == "CreatedItem")
            {
                csv.AppendLine("Item Id,Item Name,Item Path,Created/Updated User,Language,Version");
                if (tabItemModel == null || tabItemModel.CreatedResults == null || tabItemModel.CreatedResults.Count <= 0) return csv;
                foreach (var result in tabItemModel.CreatedResults)
                {
                    csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.FullPath, result.FullPath, result.UpdatedBy, result.Language, result.Version));
                }
            }
            if (type == "UpdatedItem")
            {
                csv.AppendLine("Item Id,Item Name,Item Path,Created/Updated User,Language,Version");
                if (tabItemModel == null || tabItemModel.UpdatedResults == null || tabItemModel.UpdatedResults.Count <= 0) return csv;
                foreach (var result in tabItemModel.UpdatedResults)
                {
                    csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", result.ItemId, result.FullPath, result.FullPath, result.UpdatedBy, result.Language, result.Version));
                }
            }

            if (type == "Summary" || string.IsNullOrEmpty(type))
            {
                csv.AppendLine("Total Items Created,Total Items Updated,Total Items Archived");
                if (tabItemModel == null) return csv;

                csv.AppendLine(string.Format("{0},{1},{2}", tabItemModel.CreatedPages, tabItemModel.UpdatedPages, tabItemModel.ArchivedPages));

            }

            return csv;
        }
    }
}