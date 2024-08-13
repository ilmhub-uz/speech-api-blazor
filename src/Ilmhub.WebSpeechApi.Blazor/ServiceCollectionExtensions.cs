using Microsoft.Extensions.DependencyInjection;

namespace Ilmhub.WebSpeechApi.Blazor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeechServices(this IServiceCollection services)
        => services.AddSingleton<ISpeechApi, SpeechApi>();
}