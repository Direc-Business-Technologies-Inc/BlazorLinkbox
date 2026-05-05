using Ardalis.GuardClauses;
using Domain.Commons;
using Domain.Markers;

namespace Domain.Entities.Entities.Configuration.Setup.Api;

public class ApiDEM : AuditableDEM, IAggregateRoot, IActivateable
{
    public string ApiCode { get; private set; }
    public string Method { get; private set; }
    public string Module { get; private set; }
    public string Url { get; private set; }
    public string? ApiKey { get; private set; }
    public string? ApiSecretKey { get; private set; }
    public string? ApiToken { get; private set; }
    public string? ApiLoginUrl { get; private set; }
    public string? ApiLoginBody { get; private set; }
    public bool Active { get; private set; }

    ApiDEM() { }

    ApiDEM(
        string apiCode,
        string method,
        string module,
        string url,
        string apiKey,
        string apiSecretKey,
        string apiToken,
        string apiLoginUrl,
        string apiLoginBody,
        bool active = true)
    {
        ApiCode = Guard.Against.NullOrEmpty(apiCode, nameof(apiCode), "API code cannot be null or empty");
        Method = Guard.Against.NullOrEmpty(method, nameof(method), "Api method cannot be null or empty");
        Module = Guard.Against.NullOrEmpty(module, nameof(module), "Api Module cannot be null or empty");
        Url = Guard.Against.NullOrEmpty(url, nameof(url), "Api URL cannot be null or empty");
        ApiKey = apiKey;
        ApiSecretKey = apiSecretKey;
        ApiToken = apiToken;
        ApiLoginUrl = apiLoginUrl;
        ApiLoginBody = apiLoginBody;
        Active = active;
    }

    public static ApiDEM Create(
        string apiCode,
        string method,
        string module,
        string url,
        string apiKey,
        string apiSecretKey,
        string apiToken,
        string apiLoginUrl,
        string apiLoginBody,
        bool active = true)
    {
        return new(
            apiCode,
            method,
            module,
            url,
            apiKey,
            apiSecretKey,
            apiToken,
            apiLoginUrl,
            apiLoginBody,
            active);
    }

    public ApiDEM Update(
        string apiCode,
        string method,
        string module,
        string url,
        string apiKey,
        string apiSecretKey,
        string apiToken,
        string apiLoginUrl,
        string apiLoginBody,
        bool active)
    {
        ApiCode = Guard.Against.NullOrEmpty(apiCode, nameof(apiCode), "API code cannot be null or empty");
        Method = Guard.Against.NullOrEmpty(method, nameof(method), "Api method cannot be null or empty");
        Module = Guard.Against.NullOrEmpty(module, nameof(module), "Api Module cannot be null or empty");
        Url = Guard.Against.NullOrEmpty(url, nameof(url), "Api URL cannot be null or empty");
        ApiKey = apiKey;
        ApiSecretKey = apiSecretKey;
        ApiToken = apiToken;
        ApiLoginUrl = apiLoginUrl;
        ApiLoginBody = apiLoginBody;
        Active = active;

        return this;
    }

    public ApiDEM Activate()
    {
        Active = true;
        return this;
    }

    public ApiDEM Deactivate()
    {
        Active = false;
        return this;
    }
}
