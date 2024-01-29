using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

public class PhishingImageFunction
{
    [Function(nameof(PhishingImageFunction))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, Route = "phishing-image")] HttpRequestData req, FunctionContext context)
    {
        if (!req.Headers.TryGetValues("Referer", out var referers))
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        if (referers.Any(referer => referer == "https://login.microsoftonline.com/"))
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        var svg = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<svg xmlns=""http://www.w3.org/2000/svg""
    xmlns:xlink=""http://www.w3.org/1999/xlink""
    version=""1.1"" baseProfile=""full""
    width=""400px"" height=""300px""
    viewBox=""0 0 400 300"">
    <title>Titel der Datei</title>
    <desc>Beschreibung/Textalternative zum Inhalt.</desc>

<!--Inhalt der Datei -->
";
        for (int i = 0; i < 10; i++)
        {
            svg += $"<text x=\"0\" y=\"{i*30}\">!!! HACKED !!! HACKED !!! HACKED !!! HACKED !!!</text>\n";
        }
        svg += "</svg>\n";

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "image/svg+xml");
        await response.WriteStringAsync(svg, System.Text.Encoding.UTF8);

        return response;
    }
}
