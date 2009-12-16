﻿using Coolite.Ext.Web;
using Zeus.ContentTypes;
using Zeus.Design.Editors;

namespace Zeus.Web
{
	[UI.TabPanel("Content", "Content", 10)]
	[DefaultContainer("Content")]
	public abstract class PageContentItem : ContentItem
	{
		[TextBoxEditor("Title", 10, Required = true, Shared = false)]
		public override string Title
		{
			get { return base.Title; }
			set { base.Title = value; }
		}

		[ContentProperty("Page Title", 11, Description = "Used in the &lt;h1&gt; element on the page")]
		[PageTitleEditor]
		public virtual string PageTitle
		{
			get { return GetDetail("PageTitle", Title); }
			set { SetDetail("PageTitle", value); }
		}

		[NameEditor("URL", 20, Required = true, Shared = false)]
		public override string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}

		[ContentProperty("Visible in Menu", 25)]
		public override bool Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}

		public override string IconUrl
		{
			get { return Utility.GetCooliteIconUrl(Icon.Page); }
		}

		public override bool IsPage
		{
			get { return true; }
		}
	}
}