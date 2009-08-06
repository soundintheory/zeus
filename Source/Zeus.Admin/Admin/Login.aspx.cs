using System;
using System.Configuration;
using Isis.ExtensionMethods.Web.UI;
using Isis.Web.Hosting;
using Isis.Web.Security;
using Zeus.Configuration;
using Zeus.Web.UI;

[assembly: EmbeddedResourceFile("Zeus.Admin.Login.aspx", "Zeus.Admin")]
namespace Zeus.Admin
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ltlAdminName.Text = ((AdminSection) ConfigurationManager.GetSection("zeus/admin")).Name;
		}

		protected void loginButton_Click(object sender, EventArgs e)
		{
			if (!IsValid)
				return;

			try
			{
				if (WebSecurityEngine.Get<ICredentialContextService>().GetCurrentService().ValidateUser(UserName.Text, Password.Text))
				{
					string username = WebSecurityEngine.Get<ICredentialContextService>().GetCurrentService().GetUser(UserName.Text).Username;
					WebSecurityEngine.Get<IAuthenticationContextService>().GetCurrentService().RedirectFromLoginPage(username, false);
				}
				else
					FailureText.Text = "Invalid username or password";
			}
			catch (Exception ex)
			{
				Trace.Warn(ex.ToString());
				FailureText.Text = "Error logging in: " + ex;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			Page.ClientScript.RegisterJQuery();
			Page.ClientScript.RegisterCssResource(typeof(Login), "Zeus.Admin.Assets.Css.reset.css");
			Page.ClientScript.RegisterCssResource(typeof(Login), "Zeus.Admin.Assets.Css.login.css");
			base.OnPreRender(e);
		}
	}
}
