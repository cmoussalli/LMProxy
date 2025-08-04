using CMouss.IdentityFramework;
using CMouss.IdentityFramework.API.Serving;
using CMouss.IdentityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using static MLProxy.DTOs.OllamaDTOs;

namespace MLProxy.Controllers
{
    public class OllamaController : IDFBaseController
    {
        [HttpPost]
        [Route("api/chat")]
        [IDFAuthUser()]

        public async Task<IActionResult> ProxyChat([FromBody] OllamaChatRequest request, CancellationToken cancellationToken)
        {
            UserClaim claim = GetUserClaim();
            //Validate if the user has access to the model


            var client = new HttpClient();

            var backendUrl = "http://localhost:11434/api/chat"; // Ollama endpoint
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, backendUrl)
            {
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            if (request.Stream)
            {
                var response = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

                Response.ContentType = "application/x-ndjson";
                await stream.CopyToAsync(Response.Body, cancellationToken);
                return new EmptyResult(); // Streamed response
            }
            else
            {
                var response = await client.SendAsync(httpRequest, cancellationToken);
                var resultJson = await response.Content.ReadAsStringAsync(cancellationToken);
                return Content(resultJson, "application/json");
            }
        }
    }
}
