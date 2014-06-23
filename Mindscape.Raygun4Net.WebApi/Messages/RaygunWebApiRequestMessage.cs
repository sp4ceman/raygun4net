using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Mindscape.Raygun4Net.Messages;

namespace Mindscape.Raygun4Net.WebApi.Messages
{
  public class RaygunWebApiRequestMessage : IRaygunRequestMessage
  {
    public RaygunWebApiRequestMessage(HttpRequestMessage request, IEnumerable<string> ignoredFormNames)
    {
      HostName = request.RequestUri.Host;
      Url = request.RequestUri.AbsolutePath;
      HttpMethod = request.Method.ToString();
      IPAddress = GetIPAddress(request);

      var ignored = ignoredFormNames.ToLookup(i => i);
      Headers = new Dictionary<string, string>();

      foreach (var header in request.Headers.Where(h => !ignored.Contains(h.Key)))
      {
          Headers.Add(header.Key, string.Join(",", header.Value));
      }

      if (request.Content.Headers.ContentLength.HasValue && request.Content.Headers.ContentLength.Value > 0)
      {
        foreach (var header in request.Content.Headers)
        {
          Headers.Add(header.Key, string.Join(",", header.Value));
        }

        try
        {
          RawData = request.Content.ReadAsStringAsync().Result;
        }
        catch (Exception) {}
      }
    }

    private const string HttpContext = "MS_HttpContext";
    private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

    private string GetIPAddress(HttpRequestMessage request)
    {
      if (request.Properties.ContainsKey(HttpContext))
      {
        dynamic ctx = request.Properties[HttpContext];
        if (ctx != null)
        {
          return ctx.Request.UserHostAddress;
        }
      }

      if (request.Properties.ContainsKey(RemoteEndpointMessage))
      {
        dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
        if (remoteEndpoint != null)
        {
          return remoteEndpoint.Address;
        }
      }
      return null;
    }

    public string HostName { get; set; }

    public string Url { get; set; }

    public string HttpMethod { get; set; }

    public string IPAddress { get; set; }


    public string RawData { get; set; }

    public IDictionary Headers { get; set; }

  }
}