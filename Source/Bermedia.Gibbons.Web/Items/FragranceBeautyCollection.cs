﻿using Zeus;
using Zeus.Integrity;
using Zeus.ContentTypes.Properties;

namespace Bermedia.Gibbons.Web.Items
{
	[ContentType("Collection", Description = "e.g. Burberry London")]
	[RestrictParents(typeof(FragranceBeautyBrandCategory))]
	public class FragranceBeautyCollection : FragranceBeautyCategory
	{
		[LinkedItemDropDownListEditor("Brand", 200, TypeFilter = typeof(Brand), ContainerName = Tabs.General)]
		public Brand Brand
		{
			get { return GetDetail<Brand>("Brand", null); }
			set { SetDetail<Brand>("Brand", value); }
		}
	}
}