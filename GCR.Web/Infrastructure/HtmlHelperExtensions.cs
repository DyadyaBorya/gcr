using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using GCR.Core;

namespace GCR.Web.Infrastructure
{
    public static class HtmlHelperExtensions
    {

        /// <summary>
        /// Merges multiple Anonymous Html Attribute Objects into one Dictionary&lt;string, object&gt;
        /// </summary>
        /// <param name="htmlHelper">Current HtmlHelper.</param>
        /// <param name="sources">Anonymous Html Attribute Objects to merge.</param>
        public static IDictionary<string, object> MergeAttributes(this HtmlHelper htmlHelper, params object[] sources)
        {
            var d = new IDictionary<string, object>[sources.Length];

            for (int i = 0; i < sources.Length; i++)
            {
                d[i] = HtmlHelper.AnonymousObjectToHtmlAttributes(sources[i]);
            }

            return MergeAttributes(htmlHelper, d);
        }

        /// <summary>
        /// Merges multiple Dictionary&lt;string, object&gt; Html Attribute Objects into one Dictionary&lt;string, object&gt;
        /// </summary>
        /// <param name="htmlHelper">Current HtmlHelper.</param>
        /// <param name="sources">Dictionary&lt;string, object&gt; Html Attribute Objects to merge.</param>
        public static IDictionary<string, object> MergeAttributes(this HtmlHelper htmlHelper, params IDictionary<string, object>[] sources)
        {
            var newAttrs = new SortedDictionary<string, object>(StringComparer.Ordinal);

            foreach (var source in sources)
            {
                if (source != null)
                {
                    foreach (var entry in source)
                    {
                        if (newAttrs.ContainsKey(entry.Key))
                        {
                            newAttrs[entry.Key] = entry.Value;
                        }
                        else
                        {
                            newAttrs.Add(entry.Key, entry.Value);
                        }
                    }
                }
            }

            return newAttrs;
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper)
        {
            // false = excludePropertyErrors
            return Message(htmlHelper, false);
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            // null = message
            return Message(htmlHelper, excludePropertyErrors, null);
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, string message)
        {
            return Message(htmlHelper, false /* excludePropertyErrors */, message, MessageMode.Error);
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message)
        {
            return Message(htmlHelper, excludePropertyErrors, message, MessageMode.Error);
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, string message, MessageMode mode)
        {
            return Message(htmlHelper, false /* excludePropertyErrors */, message, mode);
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, bool excludePropertyErrors, MessageMode mode)
        {
            return Message(htmlHelper, excludePropertyErrors, null, mode);
        }

        public static MvcHtmlString Message(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, MessageMode mode)
        {
            var htmlAttributes = new Dictionary<string, object>();
            switch (mode)
            {
                case MessageMode.Error:
                    htmlAttributes.Add("class", "error");
                    break;
                case MessageMode.Notice:
                    htmlAttributes.Add("class", "notice");
                    break;
                case MessageMode.Success:
                    htmlAttributes.Add("class", "success");
                    break;
                case MessageMode.Info:
                    htmlAttributes.Add("class", "info");
                    break;
                default:
                    htmlAttributes.Add("class", "error");
                    break;
            }

            return System.Web.Mvc.Html.ValidationExtensions.ValidationSummary(htmlHelper, excludePropertyErrors, message, htmlAttributes);
        }
    }

    public enum MessageMode
    {
        Error = 0,
        Notice,
        Success,
        Info
    }
}