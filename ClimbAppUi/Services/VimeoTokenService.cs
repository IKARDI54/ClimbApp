//using System;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace ClimbApp.Services
//{
//    public class VimeoTokenService
//    {
//        private readonly HttpClient httpClient;
//        private string accessToken = "10feba93f1ee0e94d9830b7caff547ec";
//        private readonly string clientId = "59dd4406a2ffdc66631f62f3d4407f5b4fc31760";
//        private readonly string clientSecret = "RfLzh79Af5SKnk0lpAJRaYXjN/W4lMQcSmGtnEKwT+WUOabTfkYyFXhT2z0CmMQu2YMK4/agnod1jtPAUU9fuuDA7yABo7nxF0u3aAk7k64ybLRVcNjYJq5Spbrm7Jyw";
       
//        public VimeoTokenService(HttpClient httpClient)
//        {
//            this.httpClient = httpClient;
//        }

//        public string GetAccessToken()
//        {
//            return accessToken;
//        }

//        public async Task<string> RefreshAccessTokenAsync()
//        {
//            // Codifica el clientId y el clientSecret en base64
//            string authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

//            // Configura los encabezados
//            httpClient.DefaultRequestHeaders.Clear(); // Limpia los encabezados para evitar duplicados
//            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authHeader}");
//            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.vimeo.*+json;version=3.4");

//            // Configura el cuerpo de la solicitud
//            var requestBody = new
//            {
//                grant_type = "client_credentials",
//                scope = "public"
//            };

//            var requestBodyJson = JsonSerializer.Serialize(requestBody);
//            var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

//            try
//            {
//                // Realiza la solicitud POST
//                var response = await httpClient.PostAsync("https://api.vimeo.com/oauth/access_token", content);

//                if (response.IsSuccessStatusCode)
//                {
//                    var responseContent = await response.Content.ReadAsStringAsync();
//                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
//                    accessToken = tokenResponse.Access_token; // Almacena el nuevo token de acceso
//                    Console.WriteLine(responseContent);
//                    return accessToken;
//                }
//                else
//                {
//                    // Maneja errores aquí si es necesario
//                    // Registra la respuesta para depuración
//                    var errorResponseContent = await response.Content.ReadAsStringAsync();

//                    // Muestra información adicional sobre la respuesta de error
//                    Console.WriteLine("Error en la solicitud:");
//                    Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
//                    Console.WriteLine($"Response Content: {errorResponseContent}");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Maneja excepciones generales aquí
//                Console.WriteLine($"Excepción: {ex.Message}");
//            }

//            return null;
//        }
//    }

//    public class TokenResponse
//    {
//        public string Access_token { get; set; }
//    }
//}
