using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Resources.Media;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SitecoreDiser.Extensions.Extensions
{
    public static class FieldExtensions
    {
        public static string LinkFieldUrl(this Item item, ID fieldID)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (ID.IsNullOrEmpty(fieldID))
            {
                throw new ArgumentNullException(nameof(fieldID));
            }
            var field = item.Fields[fieldID];
            if (field == null || !(FieldTypeManager.GetField(field) is LinkField))
            {
                return string.Empty;
            }
            else
            {
                LinkField linkField = (LinkField)field;
                var options = new ItemUrlBuilderOptions() { EncodeNames = true, LanguageEmbedding = LanguageEmbedding.Never, SiteResolving = true };
                switch (linkField.LinkType.ToLower())
                {
                    case "internal":
                        // Use LinkMananger for internal links, if link is not empty
                        return linkField.TargetItem != null ? LinkManager.GetItemUrl(linkField.TargetItem, options) : string.Empty;
                    case "media":
                        // Use MediaManager for media links, if link is not empty
                        return linkField.TargetItem != null ? HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(linkField.TargetItem)) : string.Empty;
                    case "external":
                        // Just return external links
                        return linkField.Url;
                    case "anchor":
                        // Prefix anchor link with # if link if not empty
                        return !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
                    case "mailto":
                        // Just return mailto link
                        return linkField.Url;
                    case "javascript":
                        // Just return javascript
                        return linkField.Url;
                    default:
                        // Just please the compiler, this
                        // condition will never be met
                        return linkField.Url;
                }
            }
        }

        public static string ImageUrl(this Item item, ID imageFieldId, MediaUrlBuilderOptions options = null)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var imageField = (ImageField)item.Fields[imageFieldId];
            return imageField?.MediaItem == null ? string.Empty : imageField.ImageUrl(options);
        }

        public static string GetProtectedMediaUrl(string mediaUrl)
        {
            return HashingUtils.ProtectAssetUrl(mediaUrl);
        }

        public static string ImageUrl(this ImageField imageField, MediaUrlBuilderOptions options)
        {
            if (imageField?.MediaItem == null)
            {
                throw new ArgumentNullException(nameof(imageField));
            }

            return options == null ? imageField.ImageUrl() : GetProtectedMediaUrl(MediaManager.GetMediaUrl(imageField.MediaItem, options));
        }

        public static string Url(this Item item, ItemUrlBuilderOptions options = null)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (options == null)
                options = new ItemUrlBuilderOptions { SiteResolving = true, EncodeNames = true, LanguageEmbedding = LanguageEmbedding.Never };
            return !item.Paths.IsMediaItem ? LinkManager.GetItemUrl(item, options).Replace(":443", string.Empty).Replace(",-w-,", "") : GetProtectedMediaUrl(MediaManager.GetMediaUrl(item)).Replace(":443", string.Empty);
        }

        public static string ImageUrl(this ImageField imageField)
        {
            if (imageField?.MediaItem == null)
            {
                throw new ArgumentNullException(nameof(imageField));
            }

            var options = MediaUrlBuilderOptions.Empty;
            int width, height;

            if (int.TryParse(imageField.Width, out width))
            {
                options.Width = width;
            }

            if (int.TryParse(imageField.Height, out height))
            {
                options.Height = height;
            }
            return imageField.ImageUrl(options);
        }

        public static string ImageAltText(this Item item, ID imageFieldId, MediaUrlBuilderOptions options = null)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var imageField = (ImageField)item.Fields[imageFieldId];
            return imageField?.MediaItem == null ? string.Empty : imageField.Alt;
        }

        public static bool IsChecked(this Field checkboxField)
        {
            if (checkboxField == null)
            {
                throw new ArgumentNullException(nameof(checkboxField));
            }
            return MainUtil.GetBool(checkboxField.Value, false);
        }

        public static string MediaUrl(this Item item, ID mediaFieldId, MediaUrlBuilderOptions options = null)
        {
            var targetItem = item.TargetItem(mediaFieldId);
            return targetItem == null ? string.Empty : (GetProtectedMediaUrl(MediaManager.GetMediaUrl(targetItem)) ?? string.Empty);
        }

        public static string MediaMimeType(this Item item, ID mediaFieldId)
        {
            var targetItem = item.TargetItem(mediaFieldId);
            if (targetItem == null)
                return string.Empty;

            var media = MediaManager.GetMedia(targetItem);
            return (media == null ? string.Empty : media.MimeType);
        }

        /// <summary>
        /// The tokenise.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Tokenise(this string input)
        {
            var output = input;

            output = output.ToLowerInvariant();

            output = output.Trim();

            // Split by white space or non word charactor
            // If non word charactor or white space is encountered, break the remaining string into a new array.
            var segments = Regex.Split(output, @"(\s|\W)");

            segments = segments
                // Remove non letter or digit char from array element.
                .Select(q => new string(q.ToCharArray().Where(char.IsLetterOrDigit).ToArray()))
                // Remove empty element from array.
                .Where(q => !string.IsNullOrEmpty(q)).ToArray();

            output = string.Join("-", segments);

            return output;
        }

        public static string FormatHtag(string tag, HtmlString value)
        {
            if (!string.IsNullOrEmpty(tag) && !string.IsNullOrEmpty(value.ToString()))
                return string.Format(@"<{0}>{1}</{0}>", tag, value);
            return string.Empty;
        }
    }
}