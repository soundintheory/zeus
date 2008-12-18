﻿using System;
using System.Web.UI.WebControls;
using Isis.Web;
using Zeus.ContentTypes;

namespace Zeus.Admin
{
	public partial class ViewDetail : AdminPage
	{
		protected override void OnInit(EventArgs e)
		{
			zeusItemEditView.CurrentItem = this.SelectedItem;
			this.Title = "Edit \"" + zeusItemEditView.CurrentItem.Title + "\"";

			base.OnInit(e);
		}

		protected void btnSave_Command(object sender, CommandEventArgs e)
		{
			if (!this.Page.IsValid)
				return;

			zeusItemEditView.Save();

			//  register the script to close the popup
			this.Page.ClientScript.RegisterStartupScript(typeof(ViewDetail), "closeThickBox", "self.parent.updated();", true);
		}
	}
}