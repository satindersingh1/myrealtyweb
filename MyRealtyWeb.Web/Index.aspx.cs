using MyRealtyWeb.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyRealtyWeb.Data.Access;
using MyRealtyWeb.Data.DataModel;
using System.Text;
namespace MyRealtyWeb.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserDomainService userDomainService = new UserDomainService();
                var subDomain = Request.Url.GetSubDomain();
                Response.Write(subDomain);
                var q = userDomainService.GetUserDomain(subDomain);
                if (!string.IsNullOrEmpty(q.Domain))
                {
                    Response.Redirect("Crm/ViewPost/");
                }
                else
                {
                    Response.Write("Account/Login");
                }
            }
            catch(Exception ex)
            {
                
            }
        }
    }
    public enum GetSubDomainOption
    {
        ExcludeWWW,
        IncludeWWW
    };
    public static class Extentions
    {
        public static string GetSubDomain(this Uri uri,
            GetSubDomainOption getSubDomainOption = GetSubDomainOption.IncludeWWW)
        {
            var subdomain = new StringBuilder();
            for (var i = 0; i < uri.Host.Split(new char[] { '.' }).Length - 2; i++)
            {
                //Ignore any www values of ExcludeWWW option is set
                if (getSubDomainOption == GetSubDomainOption.ExcludeWWW && uri.Host.Split(new char[] { '.' })[i].ToLowerInvariant() == "www") continue;
                //I use a ternary operator here...this could easily be converted to an if/else if you are of the ternary operators are evil crowd
                subdomain.Append((i < uri.Host.Split(new char[] { '.' }).Length - 3 &&
                                  uri.Host.Split(new char[] { '.' })[i + 1].ToLowerInvariant() != "www") ?
                                       uri.Host.Split(new char[] { '.' })[i] + "." :
                                       uri.Host.Split(new char[] { '.' })[i]);
            }
            return subdomain.ToString();
        }
    }
}