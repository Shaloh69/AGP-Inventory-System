namespace AppClient.Helpers;

public static class AppConfig
{
    // run-dev.bat sets API_BASE_URL=http://localhost:3000/ for local testing.
    // Otherwise the app uses the live Render server. The URL MUST end with "/".
    public static readonly string ApiBaseUrl =
        Environment.GetEnvironmentVariable("API_BASE_URL")
        ?? "https://agp-api.onrender.com/";
}
