using Coolite.Ext.Web;
using Zeus.Persistence;

namespace Zeus.Web
{
	public class WidgetContentItem : ContentItem
	{
		/// <summary>Gets or sets zone name which is associated with data items and their placement on a page.</summary>
		[Copy]
		public virtual string ZoneName { get; set; }

		public override string IconUrl
		{
			get { return Utility.GetCooliteIconUrl(Icon.PageWhite); }
		}

		public override bool IsPage
		{
			get { return false; }
		}
	}
}