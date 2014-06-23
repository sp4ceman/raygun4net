using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web;
using Mindscape.Raygun4Net.Messages;

namespace Mindscape.Raygun4Net
{
  public class RaygunMessageBuilder : RaygunMessageBuilderBase
  {
    public static RaygunMessageBuilder New
    {
      get { return new RaygunMessageBuilder(); }
    }

    public IRaygunMessageBuilder SetHttpDetails(HttpContext context, List<string> ignoredFormNames = null)
    {
      if (context != null)
      {
        HttpRequest request;
        try
        {
          request = context.Request;
        }
        catch (HttpException)
        {
          return this;
        }
        _raygunMessage.Details.Request = new RaygunRequestMessage(request, ignoredFormNames);
      }

      return this;
    }

    public override IRaygunMessageBuilder SetExceptionDetails(Exception exception)
    {
      HttpException error = exception as HttpException;
      if (error != null)
      {
        int code = error.GetHttpCode();
        string description = null;
        if (Enum.IsDefined(typeof(HttpStatusCode), code))
        {
          description = ((HttpStatusCode)code).ToString();
        }
        _raygunMessage.Details.Response = new RaygunResponseMessage { StatusCode = code, StatusDescription = description };
      }

      return base.SetExceptionDetails(exception);
    }
  }
}