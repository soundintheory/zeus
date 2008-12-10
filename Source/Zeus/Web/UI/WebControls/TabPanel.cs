﻿using System;
using System.Web.UI.WebControls;

namespace Zeus.Web.UI.WebControls
{
	public class TabPanel : Panel
	{
		public TabPanel()
		{
			this.CssClass = "tabPanel";
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			this.Page.ClientScript.RegisterClientScriptInclude("JQueryUICore", ResolveClientUrl("~/admin/assets/js/plugins/ui.core.js"));
			this.Page.ClientScript.RegisterClientScriptInclude("JQueryUITabs", ResolveClientUrl("~/admin/assets/js/plugins/ui.tabs.js"));
			this.Page.ClientScript.RegisterClientScriptInclude("JQueryTabPanel", ResolveClientUrl("~/admin/assets/js/plugins/jquery.tabPanel.js"));

			string script = "$(document).ready(function() { $('.tabPanel').tabPanel(); });";
			this.Page.ClientScript.RegisterStartupScript(typeof(TabPanel), "InitTabPanels", script, true);
		}
	}
}
