using assessment.Application.Interfaces.ExternalWS;
using System.Text;
using System.Text.Json;

namespace assessment.Infraestructure.Persistence.ExternalWS;

public class AnalizaSentimientosAPI(HttpClient httpClient) : IAnalizaSentimientosAPI
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<T> analizarSentimientosPOST<T>(object request)
    {
        //_httpClient.DefaultRequestHeaders.Add("ocp-apim-subscription-key", "");

        var jsonContent = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

        HttpResponseMessage response = null!;
        try
        {
            response = await _httpClient.PostAsync("text/analytics/v3.1/sentiment", jsonContent);

            response.EnsureSuccessStatusCode();

        }
        catch (HttpRequestException ex)
        {
            if (response != null)
            {
                string errorContent = await response.Content.ReadAsStringAsync();

                //throw new Exception($"La petición falló con el código {response.StatusCode}. Detalles: {errorContent}", ex);
            }
            else
            {
                // Si la respuesta es null, fue un error de conexión
                //throw new Exception($"La petición falló sin una respuesta. Detalles: {ex.Message}", ex);
            }
        }

        var responseData = await response!.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseData)!;
    }
}
