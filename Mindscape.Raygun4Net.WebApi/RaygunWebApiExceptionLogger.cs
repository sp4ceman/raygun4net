using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Mindscape.Raygun4Net.WebApi
{
  public class RaygunWebApiExceptionLogger : ExceptionLogger
  {
    private Func<RaygunWebApiClient> _generateRaygunClient;

    public static void Init(HttpConfiguration config, Func<RaygunWebApiClient> generateRaygunClient = null)
    {
      new RaygunWebApiExceptionLogger().Attach(config, generateRaygunClient);
    }

    private void Attach(HttpConfiguration config, Func<RaygunWebApiClient> generateRaygunClient = null)
    {
      config.Services.Add(typeof(IExceptionLogger), this);
      _generateRaygunClient = generateRaygunClient;
    }

    public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
    {
      return Task.Factory.StartNew(() => GetClient().CurrentHttpRequest(context.Request).Send(context.Exception), cancellationToken);
    }

    private RaygunWebApiClient GetClient()
    {
      return _generateRaygunClient == null ? new RaygunWebApiClient() : _generateRaygunClient();
    }
  }
}

