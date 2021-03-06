using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SitecoreDiser.Extensions.Extensions
{

    public static class ItemExtensions
    {

        public const string EntityNameValidCharsRegex = "[^a-zA-Z0-9 ]";

        public static Item GetPageContext()
        {
            return Context.Item;
        }

        public static Item TargetItem(this Item item, ID linkFieldId)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (item.Fields[linkFieldId] == null || !item.Fields[linkFieldId].HasValue)
                return null;
            return ((LinkField)item.Fields[linkFieldId]).TargetItem ?? ((ReferenceField)item.Fields[linkFieldId]).TargetItem;
        }

        public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateId)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return item.IsDerived(templateId) ? item : item.Axes.GetAncestors().LastOrDefault(i => i.IsDerived(templateId));
        }

        public static bool HasLayout(this Item item)
        {
            return item?.Visualization?.Layout != null;
        }

        public static bool HasPageChildrens(this Item item)
        {

            foreach (var it in item.Children.ToList())
            {
                var value = it.IsPageOrHasPageChildrens();
                if (value) return true;

            }
            return false;
        }

        public static bool IsPageOrHasPageChildrens(this Item item)
        {
            if (item.IsPage()) return true;

            foreach (var it in item.Children.ToList())
            {
                var value = it.IsPageOrHasPageChildrens();
                if (value) return true;

            }
            return false;
        }

        public static bool IsPage(this Item item)
        {
            return !string.IsNullOrWhiteSpace(item?.Fields[FieldIDs.LayoutField]?.Value) || !string.IsNullOrWhiteSpace(item?.Fields[FieldIDs.FinalLayoutField]?.Value);
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            if (item == null)
                return false;

            return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
        }

        private static bool IsDerived(this Item item, Item templateItem)
        {
            if (item == null)
                return false;

            if (templateItem == null)
                return false;

            var itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
        }

        private static Database GetDatabase(bool useMaster = false)
        {
            if (useMaster)
                return Factory.GetDatabase(Constants.Site.MasterDb);

            return Context.Database ?? null;
        }

        /// <summary>
        /// Get Sitecore Item by item Id
        /// </summary>
        /// <param name="itemId">Id of the Item to get</param>
        /// <returns></returns>
        public static Item GetItemById(string itemId, bool useMaster = false)
        {
            if (!string.IsNullOrEmpty(itemId))
                return GetDatabase(useMaster).GetItem(itemId);
            return null;
        }

        /// <summary>
        /// Get Sitecore Template Item by template Id
        /// </summary>
        /// <param name="templateId">Template Id</param>
        /// <returns>Template Item</returns>
        public static TemplateItem GetTemplateById(ID templateId, bool useMaster = false)
        {
            return GetDatabase(useMaster).GetTemplate(templateId);
        }

        public static Item GetItemByPath(string itemPath, bool useMaster = false)
        {
            if (!string.IsNullOrEmpty(itemPath))
                return GetDatabase(useMaster).GetItem(itemPath);
            return null;
        }

        public static string GetItemPathById(string itemId)
        {
            var item = GetItemById(itemId);
            return item.Paths.Path.ToString();
        }

        public static string GetItemDisplayNameById(string itemId, bool useMaster = false)
        {
            if (!string.IsNullOrEmpty(itemId))
            {
                var item = GetDatabase(useMaster).GetItem(itemId);
                return item == null ? string.Empty : !string.IsNullOrEmpty(item.DisplayName) ? item.DisplayName : item.Name;
            }
            return string.Empty;
        }

        public static string GetItemUrl(Item item)
        {
            var linkOptions = new ItemUrlBuilderOptions
            {
                SiteResolving = true,
                LanguageEmbedding = LanguageEmbedding.Never,
                LowercaseUrls = true,
                EncodeNames = true
            };
            return LinkManager.GetItemUrl(item, linkOptions).Replace(",-w-,", "");
        }

        /// <summary>
        /// Check if the item already exists under a specific parent
        /// </summary>
        /// <param name="itemName">Item name to check</param>
        /// <param name="parentId">parent item id to check for items</param>
        /// <returns>true if item exists</returns>
        public static bool CheckIfItemExists(string itemName, ID parentId)
        {
            var parent = GetItemById(parentId.ToString());
            return parent.Children.Any(i => i.Name.ToLower().Equals(itemName.ToLower()));
        }

        /// <summary>
        /// Get all child items under parent item by given template type
        /// </summary>
        /// <param name="parentItem">Parent item</param>
        /// <param name="templateId">Template Id for children</param>
        /// <returns></returns>
        public static List<Item> GetChildrenByTemplateUsingQuery(Item parentItem, ID templateId, bool useMaster = false)
        {
            List<Item> items = null;
            var query = $"{parentItem.Paths.FullPath}//*[@@TemplateId='{templateId}']";

            if (useMaster)
                items = GetDatabase(useMaster).SelectItems(query).ToList();
            else if (parentItem.Database != null)
                items = parentItem.Database.SelectItems(query).ToList();
            else
                items = GetDatabase(useMaster).SelectItems(query).ToList();

            return items;
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="parentItemId">Parent Item Id under which the new item gets created</param>
        /// <param name="templateId">Template Id to define the item template to be used</param>
        /// <param name="itemName">name of the item to be created</param>
        /// <returns></returns>
        public static Item CreateNewItem(ID parentItemId, ID templateId, string itemName)
        {
            if (User.Exists(Constants.User.AdminUser))
            {
                User scUser = User.FromName(Constants.User.AdminUser, false);
                using (new UserSwitcher(scUser))
                {
                    var parentItem = GetItemById(parentItemId.ToString(), true);

                    return parentItem.Add(itemName.Replace("-", " "), GetTemplateById(templateId, true));
                }
            }

            return null;
        }

        public static string FormatItemName(string name)
        {
            //valid characters are any word characters, dots, spaces.
            var regex = new Regex(EntityNameValidCharsRegex);
            return regex.Replace(name, string.Empty).Trim();
        }

        public static string GetLinkUrl(this LinkField linkField)
        {
            if (linkField.GetFriendlyUrl().Contains("#")) return linkField.GetFriendlyUrl();
            var anchor = !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : "";
            return linkField.GetFriendlyUrl() + anchor;
        }

    }
}