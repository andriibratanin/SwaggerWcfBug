using SwaggerWcf.Attributes;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace SwaggerWcfBug
{
    //
    // Traces will be included into "Traces.svclog" file near the project file
    // Use an url like the following to see Swagger's ui (port number will probably change) http://localhost:58590/api-docs/
    //

    [ServiceContract]
    public interface IServiceContract
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ok")]
        void SwaggerOk(List<string> ok);

        //comment the following "SwaggerWcfHidden" attribute to reproduce the bug
        [SwaggerWcfHidden]
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fail")]
        void SwaggerFail(List<string> notok);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/arrayOk")]
        void SwaggerArrayOk(string[] parameter);

        // will be available on an url like the following (port number will probably change) http://localhost:58590/DemoService.svc/json/ping
        [OperationContract]
        [WebGet(UriTemplate = "/ping")]
        string Ping();
    }

    [SwaggerWcf("/")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DemoService : IServiceContract
    {
        public void SwaggerOk(List<string> ok)
        {
            var breakpointPlaceholder = ok;
        }

        public void SwaggerFail(List<string> notok)
        {
            var breakpointPlaceholder = notok;
        }

        public void SwaggerArrayOk(string[] parameter)
        {
            var breakpointPlaceholder = parameter;
        }

        public string Ping()
        {
            return "Pong";
        }
    }
}
