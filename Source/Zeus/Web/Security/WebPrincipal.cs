﻿using System.Security.Principal;
using System.Web.Security;

namespace Zeus.Web.Security
{
	public class WebPrincipal : GenericPrincipal
	{
		public WebPrincipal(IUser membershipUser, FormsAuthenticationTicket ticket)
			: base(new WebIdentity(ticket), membershipUser.Roles)
		{
			MembershipUser = membershipUser;
		}

		public IUser MembershipUser
		{
			get;
			private set;
		}
	}
}