using SitecoreDiser.Feature.ContentReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitecoreDiser.Extensions.Extensions;
using Sitecore.Configuration;
using Sitecore;
using Sitecore.Data.Archiving;

namespace SitecoreDiser.Feature.ContentReport.Helper
{
    public static class ReportHelper
    {
        private static readonly string _localFolder = "_local";

        public static ReportDataModel GetReport(List<ReportSearchResultItemModel> updatedItems, ReportModel reportModel)
        {
            try
            {
                var reportDataModel = new ReportDataModel();

                var homePath = ItemExtensions.GetItemPathById(Settings.GetSetting("HomeItemId"));

                var approvedItems = updatedItems.Where(x => (x.FullPath.StartsWith(homePath, StringComparison.OrdinalIgnoreCase))
                                                        && x.WorkflowState == Settings.GetSetting("WorkflowItemId")).ToList();

                if (reportModel.Type == "CreatedItems" || reportModel.Type == "Summary" || string.IsNullOrEmpty(reportModel.Type))
                {
                    var createdApprovedItems = approvedItems.Where(x => x.ItemVersion == 1).ToList();
                    reportDataModel.CreatedResults = GetPageItems(createdApprovedItems, true);
                    reportDataModel.CreatedPages = reportDataModel.CreatedResults.Any() ? reportDataModel.CreatedResults.Count : 0;
                }

                if (reportModel.Type == "UpdatedItems" || reportModel.Type == "Summary" || string.IsNullOrEmpty(reportModel.Type))
                {
                    var updatedApprovedItems = approvedItems.Where(x => x.ItemVersion > 1 || (x.ItemVersion == 1 && x.FullPath.ToLower().Contains(_localFolder))).ToList();
                    reportDataModel.UpdatedResults = GetPageItems(updatedApprovedItems, false, reportDataModel.CreatedResults);
                    reportDataModel.UpdatedPages = reportDataModel.UpdatedResults.Any() ? reportDataModel.UpdatedResults.Count : 0;
                }

                if (reportModel.Type == "ArchivedItems" || reportModel.Type == "Summary" || string.IsNullOrEmpty(reportModel.Type))
                {
                    // get the archive database for the master database
                    var master = Factory.GetDatabase(Constants.Database);

                    Sitecore.Data.Archiving.Archive archive = master.Archives[Constants.Archive];

                    //get Archived items
                    var archivedItems = archive.GetEntries(0, int.MaxValue)
                                        .Where(entry =>
                                            entry.ArchiveDate >= reportModel.StartDateTime &&
                                            entry.ArchiveDate <= reportModel.EndDateTime &&
                                            (entry.OriginalLocation.StartsWith(homePath))
                                        ).ToList();

                    var archiveResults = MapArchiveItemToResult(archivedItems);
                    reportDataModel.ArchivedItems = archiveResults;
                    reportDataModel.ArchivedPages = archiveResults.Any() ? archiveResults.Count : 0;
                }
                return reportDataModel;
            }

            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Failed to get the report" + ex.Message, ex, typeof(ReportHelper));
                return null;
            }
        }

        private static List<ArchiveResultItem> MapArchiveItemToResult(List<ArchiveEntry> archiveItems)
        {
            var archiveResults = new List<ArchiveResultItem>();
            foreach(var archiveItem in archiveItems)
            {
                var reportSearchResultItem = new ArchiveResultItem()
                {
                    ItemId = archiveItem.ItemId,
                    ItemName = archiveItem.Name,
                    UpdatedBy = archiveItem.ArchivedBy,
                    UpdatedDate = archiveItem.ArchiveDate,
                    FullPath = archiveItem.OriginalLocation
                };
                archiveResults.Add(reportSearchResultItem);
            }
            return archiveResults;
        }

        private static List<ReportSearchResultItemModel> GetPageItems(List<ReportSearchResultItemModel> items, bool isCreatedPages = false, List<ReportSearchResultItemModel> createdPages = null)
        {

            var pages = items.Where(c => !c.FullPath.ToLower().Contains(_localFolder)).ToList();

            // For page creation ignore components. New page is created when version 1 of a page is published
            // Any update with the components after that will be considered as page update
            if (isCreatedPages)
                return pages;

            // Updated page and components
            var updatedComponents = items.Where(c => c.FullPath.ToLower().Contains(_localFolder)).ToList();

            foreach (var component in updatedComponents)
            {
                var pagePath = component.FullPath.ToLower().Split(new string[] { "/" + _localFolder }, StringSplitOptions.None)[0];

                if (!pages.Any(p => p.FullPath.ToLower().Equals(pagePath)))
                {
                    // If page of component is not in list and not created from branch template

                    if (createdPages != null && !createdPages.Any(x => DateUtil.ToServerTime(component.UpdatedDate) >= DateUtil.ToServerTime(x.UpdatedDate)
                                         && DateUtil.ToServerTime(component.UpdatedDate) <= DateUtil.ToServerTime(x.UpdatedDate.AddMinutes(2))))
                    {
                        pages.Add(component);
                    }
                }
                else
                {
                    // If page of component is in list, check for updated time - updates within 2 mins (Exp editor) will be considered single update to page
                    var pageVersions = pages.Where(p => p.FullPath.ToLower().Equals(pagePath)).ToList();

                    if (!pageVersions.Any(x => DateUtil.ToServerTime(component.UpdatedDate) >= DateUtil.ToServerTime(x.UpdatedDate)
                                         && DateUtil.ToServerTime(component.UpdatedDate) <= DateUtil.ToServerTime(x.UpdatedDate.AddMinutes(2))))
                    {
                        pages.Add(component);
                    }
                }
            }

            return pages;
        }


    }
}