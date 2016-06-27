using SwaggerWcf;
using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace SwaggerWcfBug
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("api-docs", new WebServiceHostFactory(), typeof(SwaggerWcfEndpoint)));
        }
    }
}