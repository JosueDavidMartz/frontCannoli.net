using System.Net.Http.Headers;
using frontendnet.Models;

namespace frontendnet.Services;

public class ArchivosClientService(HttpClient client)
{
    public async Task<List<Archivo>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<Archivo>>("api/archivos");
    }

    public async Task<Archivo?> GetAsync(int id)
    {
        return await client.GetFromJsonAsync<Archivo>($"api/archivos/{id}/detalle");
    }

    public async Task<bool> PostAsync(Upload Archivo)
    {
        var memoryStream = new MemoryStream();
        await Archivo.Portada.CopyToAsync(memoryStream);
        var Contenido = memoryStream.ToArray();
        memoryStream.Close();
        var fileContent = new ByteArrayContent(Contenido);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(Archivo.Portada.ContentType);
        using var form = new MultipartFormDataContent
        {
            { fileContent, "file", Archivo.Portada.FileName! }
        };

        var response = await client.PostAsync($"api/archivos", form);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync(Upload Archivo)
    {
        var memoryStream = new MemoryStream();
        await Archivo.Portada.CopyToAsync(memoryStream);
        var Contenido = memoryStream.ToArray();
        memoryStream.Close();
        var fileContent = new ByteArrayContent(Contenido);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(Archivo.Portada.ContentType);
        using var form = new MultipartFormDataContent
        {
            { fileContent, "file", Archivo.Portada.FileName! }
        };

        var response = await client.PutAsync($"api/archivos/{Archivo.ArchivoId}", form);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await client.DeleteAsync($"api/archivos/{id}");
        return response.IsSuccessStatusCode;
    }
}