﻿using System;
using Isis.Web;

namespace Zeus.Admin
{
	public partial class Move : AdminPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int sourceID = Request.GetRequiredInt("sourceid");
			ContentItem sourceContentItem = Zeus.Context.Persister.Get(sourceID);

			int destinationID = Request.GetRequiredInt("destinationid");
			ContentItem destinationContentItem = Zeus.Context.Persister.Get(destinationID);

			// Change parent if necessary.
			if (sourceContentItem.Parent.ID != destinationContentItem.ID)
				Zeus.Context.Persister.Move(sourceContentItem, destinationContentItem);

			// Update sort order based on new pos.
			int pos = Request.GetRequiredInt("pos");
			Zeus.Context.Persister.UpdateSortOrder(sourceContentItem, pos);

			Refresh(sourceContentItem, false);
		}
	}
}