using Sitecore.Data;
using System.Web;
using System.Web.Mvc;

namespace SitecoreDiser.Extensions.Attributes
{
    public class AuthorizeApiAttribute : AuthorizeAttribute
    {

        public enum SitecoreDatabase
        {
            All,
            Master,
            Web
        }

        public SitecoreDatabase AllowDatabase { get; set; } = SitecoreDatabase.All;
        public bool AllowAnonymous { get; set; } = false;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AllowAnonymous && !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (AllowDatabase != SitecoreDatabase.All)
            {
                Database db = null;
                if (AllowDatabase == SitecoreDatabase.Web)
                    db = Database.GetDatabase("web");
                else if (AllowDatabase == SitecoreDatabase.Master)
                    db = Database.GetDatabase("master");
                if (db == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    return;
                }
            }

            base.OnAuthorization(filterContext);
        }
    }
}