using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Raven.Abstractions.Exceptions;
using Raven.Client;

namespace WebApi.App_Start
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ContextInitializeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var container = GlobalConfiguration.Configuration.DependencyResolver;
            var method = actionExecutedContext.Request.Method;
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
            {
                var session = (IDocumentSession)container.GetService(typeof(IDocumentSession));
                // Skip SaveChanges if response is BadRequest (400) or NotFound (404)
                if (actionExecutedContext.Response != null &&
                    (actionExecutedContext.Response.StatusCode == HttpStatusCode.BadRequest ||
                     actionExecutedContext.Response.StatusCode == HttpStatusCode.NotFound))
                {
                    return;
                }

                    session.SaveChanges();

            }
        }
    }
}