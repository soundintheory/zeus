using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Zeus.Web.Mvc.Html
{
	public static class ValidationExtensions
	{
		public static bool HasModelError(this HtmlHelper htmlHelper, string modelName)
		{
			return !MvcHtmlString.IsNullOrEmpty(htmlHelper.ValidationMessage(modelName));
		}

		public static string ValidationSummaryBox(this HtmlHelper htmlHelper, string message)
		{
			if (htmlHelper.ViewData.ModelState.IsValid)
				return null;

			var divBuilder = new TagBuilder("div");
			divBuilder.Attributes["id"] = "errors";

			if (!string.IsNullOrEmpty(message))
			{
				var messageBuilder = new TagBuilder("p");
				messageBuilder.SetInnerText(message);
				divBuilder.InnerHtml += messageBuilder.ToString(TagRenderMode.Normal) + Environment.NewLine;
			}

			var builder = new StringBuilder();
			var listBuilder = new TagBuilder("ul");
			foreach (var state in htmlHelper.ViewData.ModelState.Values)
			{
				foreach (var error in state.Errors)
				{
					var errorMessage = GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, error, null);
					if (!string.IsNullOrEmpty(errorMessage))
					{
						var liBuilder = new TagBuilder("li");
						liBuilder.SetInnerText(errorMessage);
						builder.AppendLine(liBuilder.ToString(TagRenderMode.Normal));
					}
				}
			}
			listBuilder.InnerHtml = builder.ToString();
			divBuilder.InnerHtml += listBuilder.ToString(TagRenderMode.Normal);
			return divBuilder.ToString(TagRenderMode.Normal);
		}

		private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
		{
			if (!string.IsNullOrEmpty(error.ErrorMessage))
				return error.ErrorMessage;

			if (modelState == null)
				return null;

			var value = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
			return string.Format(CultureInfo.CurrentCulture, "The value '{0}' is invalid.", value);
		}

		public const string FormNameKey = "FormName";

		public static string ValidationSummaryBox(this HtmlHelper htmlHelper, string name, string message)
		{
			if (htmlHelper.ViewData[FormNameKey] != null
				&& !string.IsNullOrEmpty(htmlHelper.ViewData[FormNameKey].ToString())
				&& (htmlHelper.ViewData[FormNameKey].ToString() == name))
				return ValidationSummaryBox(htmlHelper, message);
			return string.Empty;
		}
	}
}