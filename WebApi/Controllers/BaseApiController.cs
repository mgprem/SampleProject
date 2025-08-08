using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public HttpResponseMessage Found(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public HttpResponseMessage Found()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DoesNotExist()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
        }
        public HttpResponseMessage RequestBad(string errMsg)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, errMsg);
        }
        public HttpResponseMessage AlreadyExists(string Msg)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.Conflict, Msg );
        }


    }
}