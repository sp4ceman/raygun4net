using System;
using System.Collections.Generic;
using System.Net.Http;
using Mindscape.Raygun4Net.Messages;
using Mindscape.Raygun4Net.WebApi.Messages;

namespace Mindscape.Raygun4Net.WebApi
{
  public class RaygunWebApiMessageBuilder : RaygunMessageBuilderBase
  {
    public static RaygunWebApiMessageBuilder New
    {
      get { return new RaygunWebApiMessageBuilder(); }
    }

    public IRaygunMessageBuilder SetHttpDetails(HttpRequestMessage message, List<string> ignoredFormNames = null)
    {
      if (message != null)
      {
        _raygunMessage.Details.Request = new RaygunWebApiRequestMessage(message, ignoredFormNames);
      }

      return this;
    }

  }
}