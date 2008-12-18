﻿using System;
using Zeus;
using Zeus.Integrity;
using Zeus.ContentTypes.Properties;

namespace Bermedia.Gibbons.Web.Items
{
	[ContentType("Product Size Container", Description = "Container for product sizes")]
	[RestrictParents(typeof(RootItem))]
	public class ProductSizeContainer : BaseContentItem
	{
		public ProductSizeContainer()
		{
			this.Name = "ProductSizes";
			this.Title = "Product Sizes";
		}

		protected override string IconName
		{
			get { return "arrow_out"; }
		}

		public override string TemplateUrl
		{
			get { return "~/Admin/View.aspx?selected=" + this.Path; }
		}
	}
}