using Newtonsoft.Json;
using System.Text;

namespace DevTaskFlow.Repository_pattern.Service.Services
{
    //      For gemini-1.5-flash, Google’s free tier quotas(as of now) are:
    //      15 requests per minute
    //      Up to 1,500 requests per day
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly PortalRoleService _portalRoleService;
        private readonly string _apiKey;
        private readonly string _model;
        private readonly string _basicPrompt;

        public ApiService(PortalRoleService portalRoleService,IConfiguration configuration, ILogger<ApiService> logger)
        {
            _httpClient = new HttpClient();
            _portalRoleService = portalRoleService;
            _apiKey = configuration["GeminiSettings:ApiKey"];
            _model = configuration["GeminiSettings:Model"];
            _basicPrompt = configuration["GeminiSettings:basicPrompt"];
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var api = _portalRoleService.GetApiDetail();
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_model}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        parts = new[] {
                            new { text =  _basicPrompt +" "+ prompt }
                        }
                    }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"ErrorResponse: {response.StatusCode} - {json}";
            else
            {
                api.UsageCount++;
                _portalRoleService.UpdateApiDetail(api);
            }

            dynamic parsed = JsonConvert.DeserializeObject(json);
            return parsed?.candidates?[0]?.content?.parts?[0]?.text ?? "No response received.";
        }
    }
}
