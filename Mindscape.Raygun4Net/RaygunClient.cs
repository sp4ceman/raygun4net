using System.Web;

namespace Mindscape.Raygun4Net
{
  public class RaygunClient : RaygunClientBase
  {
    public RaygunClient(string apiKey) : base(apiKey) { }
    public RaygunClient() {}

    protected override IRaygunMessageBuilder BuildMessage()
    {
      return RaygunMessageBuilder.New
        .SetHttpDetails(HttpContext.Current, _ignoredFormNames);
    }
  }
}