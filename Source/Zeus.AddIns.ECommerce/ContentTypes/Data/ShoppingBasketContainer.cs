using Zeus.AddIns.ECommerce.ContentTypes.Pages;
using Zeus.Integrity;
using Zeus.Templates.ContentTypes;

namespace Zeus.AddIns.ECommerce.ContentTypes.Data
{
	[ContentType("Shopping Basket Container")]
	[RestrictParents(typeof(Shop))]
	public class ShoppingBasketContainer : BaseContentItem
	{
		public override string IconUrl
		{
			get { return GetIconUrl(typeof(ShoppingBasketContainer), "Zeus.AddIns.ECommerce.Icons.basket_go.png"); }
		}
	}
}