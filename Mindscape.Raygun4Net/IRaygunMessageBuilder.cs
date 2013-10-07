using System;
using System.Collections;
#if !WINRT && !WINDOWS_PHONE && !ANDROID && !IOS
using System.Web;
#endif

using Mindscape.Raygun4Net.Messages;

namespace Mindscape.Raygun4Net
{
  public interface IRaygunMessageBuilder
  {
    RaygunMessage Build();

    #if !WINRT && !WINDOWS_PHONE && !ANDROID && !IOS
    IRaygunMessageBuilder SetHttpDetails(HttpContext context);
    #endif

    IRaygunMessageBuilder SetMachineName(string machineName);

    IRaygunMessageBuilder SetExceptionDetails(Exception exception);

    IRaygunMessageBuilder SetClientDetails();

    IRaygunMessageBuilder SetEnvironmentDetails();

    IRaygunMessageBuilder SetVersion();

    IRaygunMessageBuilder SetUserCustomData(IDictionary userCustomData);

    IRaygunMessageBuilder SetContextIdentifier(string identifier);

    IRaygunMessageBuilder SetUserIdentifier(string identifier);
  }
}