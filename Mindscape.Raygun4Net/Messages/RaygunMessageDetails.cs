using System.Collections;
using System.Collections.Generic;

namespace Mindscape.Raygun4Net.Messages
{
  public class RaygunMessageDetails
  {
    public RaygunMessageDetails()
    {
      Context = new RaygunContextMessage();
    }

    public string MachineName { get; set; }

    public string Version { get; set; }

    public RaygunContextMessage Context { get; set; }

    public RaygunErrorMessage Error { get; set; }

    public RaygunEnvironmentMessage Environment { get; set; }

    public RaygunClientMessage Client { get; set; }

    public RaygunUserMessage User { get; set; }

    public IList<string> Tags { get; set; }

    public IDictionary UserCustomData { get; set; }

#if !WINRT && !WINDOWS_PHONE && !ANDROID && !IOS
    public RaygunRequestMessage Request { get; set; }
#endif
  }
}