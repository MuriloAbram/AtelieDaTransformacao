// =============================================================================
// AtelieDaTransformacao.Desktop - Helpers/HttpClientHelper.cs
// =============================================================================
// CONCEITO: HttpClient para consumo da API
//
// HttpClient é a classe do .NET usada para fazer requisições HTTP.
//
// Para consumir a API, usamos:
//   - GET:    buscar dados
//   - POST:   criar dados
//   - PUT:    atualizar dados
//   - DELETE: excluir dados
//
// IMPORTANTE sobre a autenticação:
//   A API usa Cookie Authentication (não JWT).
//   Isso significa que após o login, a API envia um cookie de sessão.
//   O HttpClient precisa ARMAZENAR e REENVIAR esse cookie automaticamente.
//   Para isso, usamos CookieContainer no HttpClientHandler.
//
// IMPORTANTE sobre HttpClient:
//   Não crie um HttpClient novo para cada requisição.
//   O HttpClient é reutilizado através de uma instância Singleton.
//
// IMPORTANTE sobre a URL da API:
//   A URL base é obtida através do AppConfig.
//   Não há portas hardcoded nesta classe.
// =============================================================================

using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AtelieDaTransformacao.Desktop.Helpers
{
    /// <summary>
    /// Helper centralizado para comunicação HTTP com a API.
    /// Gerencia cookies de sessão, serialização JSON e tratamento de erros.
    /// </summary>
    public sealed class HttpClientHelper
    {
        // Instância Singleton
        private static readonly Lazy<HttpClientHelper> _instance =
            new(() => new HttpClientHelper());

        /// <summary>
        /// Ponto de acesso global ao HttpClientHelper.
        /// </summary>
        public static HttpClientHelper Instance => _instance.Value;

        // =====================================================================
        // CAMPOS PRIVADOS
        // =====================================================================

        /// <summary>
        /// Armazena os cookies recebidos da API.
        /// Após o login, o cookie de autenticação fica armazenado aqui.
        /// </summary>
        private readonly CookieContainer _cookieContainer;

        /// <summary>
        /// Handler responsável pelo gerenciamento dos cookies e conexão HTTP.
        /// </summary>
        private readonly HttpClientHandler _handler;

        /// <summary>
        /// Cliente HTTP utilizado para realizar as requisições à API.
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Configurações utilizadas para serialização e desserialização JSON.
        /// </summary>
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // =====================================================================
        // CONSTRUTOR
        // =====================================================================

        private HttpClientHelper()
        {
            // Cria o container responsável pelos cookies da sessão
            _cookieContainer = new CookieContainer();

            // Configura o Handler
            _handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,

                // Permite que o Handler gerencie os cookies automaticamente
                UseCookies = true,

                // Não segue redirects automaticamente
                AllowAutoRedirect = false,

                // Aceita certificados SSL inválidos durante o desenvolvimento.
                // Em produção, essa configuração deve ser removida.
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            // Obtém a URL da API através do AppConfig
            var baseUrl = AppConfig.ApiBaseUrl;

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                // Cria o HttpClient sem BaseAddress caso a URL não esteja configurada
                _client = new HttpClient(_handler)
                {
                    Timeout = TimeSpan.FromSeconds(AppConfig.Timeout)
                };
            }
            else
            {
                // Garante que a URL termine com "/"
                if (!baseUrl.EndsWith('/'))
                    baseUrl += "/";

                _client = new HttpClient(_handler)
                {
                    BaseAddress = new Uri(baseUrl),
                    Timeout = TimeSpan.FromSeconds(AppConfig.Timeout)
                };
            }

            // Define que esperamos respostas em JSON
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(
                    "application/json"));
        }

        // =====================================================================
        // VERIFICAÇÃO DA API
        // =====================================================================

        /// <summary>
        /// Verifica se a API do AtelieDaTransformacao está disponível.
        /// </summary>
        /// <returns>
        /// Retorna true caso a API responda e false caso ocorra um erro
        /// de conexão.
        /// </returns>
        public async Task<(bool IsAvailable, string ErrorMessage)> PingApiAsync()
        {
            if (_client.BaseAddress == null)
            {
                return (
                    false,
                    "URL da API não configurada. " +
                    "Verifique o AppConfig e as configurações da API."
                );
            }

            try
            {
                // O endpoint de produtos pertence ao projeto atual.
                // Ele é utilizado apenas para verificar se a API está online.
                using var cts = new CancellationTokenSource(
                    TimeSpan.FromSeconds(5));

                var response = await _client.GetAsync(
                    "/api/products",
                    cts.Token);

                // Qualquer resposta HTTP indica que a API está funcionando.
                // Isso inclui respostas como 401 ou 403.
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (
                    false,
                    CategorizeConnectionError(
                        ex,
                        _client.BaseAddress?.ToString() ?? "")
                );
            }
        }

        // =====================================================================
        // MÉTODOS HTTP
        // =====================================================================

        /// <summary>
        /// Realiza uma requisição GET e transforma a resposta em T.
        /// </summary>
        /// <typeparam name="T">Tipo esperado na resposta.</typeparam>
        /// <param name="endpoint">Endpoint da API.</param>
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>(
                        _jsonOptions);
                }

                return default;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[GET] Erro em {endpoint}: {ex.Message}");

                throw;
            }
        }

        /// <summary>
        /// Realiza uma requisição POST com corpo JSON.
        /// </summary>
        /// <typeparam name="T">Tipo esperado na resposta.</typeparam>
        /// <param name="endpoint">Endpoint da API.</param>
        /// <param name="body">Objeto que será enviado em JSON.</param>
        public async Task<(bool Success, T? Data, string ErrorMessage)> PostAsync<T>(
            string endpoint,
            object body)
        {
            try
            {
                var json = JsonSerializer.Serialize(body);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await _client.PostAsync(
                    endpoint,
                    content);

                var responseBody =
                    await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<T>(
                        responseBody,
                        _jsonOptions);

                    return (true, data, string.Empty);
                }

                var error = TryExtractErrorMessage(responseBody);

                return (false, default, error);
            }
            catch (Exception ex)
            {
                var friendly = CategorizeConnectionError(
                    ex,
                    endpoint);

                return (false, default, friendly);
            }
        }

        /// <summary>
        /// Realiza uma requisição PUT com corpo JSON.
        /// </summary>
        /// <typeparam name="T">Tipo esperado na resposta.</typeparam>
        /// <param name="endpoint">Endpoint da API.</param>
        /// <param name="body">Objeto que será enviado em JSON.</param>
        public async Task<(bool Success, T? Data, string ErrorMessage)> PutAsync<T>(
            string endpoint,
            object body)
        {
            try
            {
                var json = JsonSerializer.Serialize(body);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await _client.PutAsync(
                    endpoint,
                    content);

                var responseBody =
                    await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<T>(
                        responseBody,
                        _jsonOptions);

                    return (true, data, string.Empty);
                }

                var error = TryExtractErrorMessage(responseBody);

                return (false, default, error);
            }
            catch (Exception ex)
            {
                var friendly = CategorizeConnectionError(
                    ex,
                    endpoint);

                return (false, default, friendly);
            }
        }

        /// <summary>
        /// Realiza uma requisição DELETE.
        /// </summary>
        /// <param name="endpoint">Endpoint da API.</param>
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(
            string endpoint)
        {
            try
            {
                var response = await _client.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                    return (true, string.Empty);

                var body =
                    await response.Content.ReadAsStringAsync();

                return (
                    false,
                    TryExtractErrorMessage(body)
                );
            }
            catch (Exception ex)
            {
                return (
                    false,
                    CategorizeConnectionError(ex, endpoint)
                );
            }
        }

        /// <summary>
        /// Realiza um POST sem corpo.
        /// É utilizado principalmente para o logout.
        /// </summary>
        /// <param name="endpoint">Endpoint da API.</param>
        public async Task<(bool Success, string ErrorMessage)> PostEmptyAsync(
            string endpoint)
        {
            try
            {
                var response = await _client.PostAsync(
                    endpoint,
                    null);

                if (response.IsSuccessStatusCode)
                    return (true, string.Empty);

                var body =
                    await response.Content.ReadAsStringAsync();

                return (
                    false,
                    TryExtractErrorMessage(body)
                );
            }
            catch (Exception ex)
            {
                return (
                    false,
                    CategorizeConnectionError(ex, endpoint)
                );
            }
        }

        // =====================================================================
        // COOKIES / SESSÃO
        // =====================================================================

        /// <summary>
        /// Limpa os cookies da sessão atual.
        /// </summary>
        public void ClearCookies()
        {
            var baseUri = _client.BaseAddress;

            if (baseUri != null)
            {
                var cookies =
                    _cookieContainer.GetCookies(baseUri);

                foreach (Cookie cookie in cookies)
                {
                    cookie.Expired = true;
                }
            }
        }

        // =====================================================================
        // TRATAMENTO DE ERROS
        // =====================================================================

        /// <summary>
        /// Tenta obter a mensagem de erro retornada pela API.
        ///
        /// A API do AtelieDaTransformacao retorna mensagens
        /// no formato:
        ///
        /// {
        ///     "message": "Mensagem do erro"
        /// }
        /// </summary>
        private string TryExtractErrorMessage(string json)
        {
            try
            {
                var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty(
                    "message",
                    out var msg))
                {
                    return msg.GetString()
                           ?? "Erro desconhecido.";
                }

                if (doc.RootElement.TryGetProperty(
                    "title",
                    out var title))
                {
                    return title.GetString()
                           ?? "Erro desconhecido.";
                }
            }
            catch
            {
                // Caso não seja possível interpretar o JSON,
                // utilizamos o próprio conteúdo da resposta.
            }

            return string.IsNullOrEmpty(json)
                ? "Erro desconhecido."
                : json;
        }

        /// <summary>
        /// Converte erros de conexão em mensagens mais amigáveis.
        /// </summary>
        private string CategorizeConnectionError(
            Exception ex,
            string endpoint)
        {
            System.Diagnostics.Debug.WriteLine(
                $"[HttpClientHelper] Erro em '{endpoint}': " +
                $"{ex.GetType().Name} — {ex.Message}");

            // =================================================================
            // TIMEOUT
            // =================================================================

            if (ex is TaskCanceledException or OperationCanceledException)
            {
                return
                    "⏱ A requisição excedeu o tempo limite.\n\n" +
                    "Verifique se a API do AtelieDaTransformacao " +
                    "está respondendo normalmente.";
            }

            // =================================================================
            // ERROS HTTP / CONEXÃO
            // =================================================================

            if (ex is HttpRequestException httpEx)
            {
                var msg = httpEx.Message.ToLowerInvariant();

                // -------------------------------------------------------------
                // API DESLIGADA
                // -------------------------------------------------------------

                if (msg.Contains("connection refused") ||
                    msg.Contains("actively refused") ||
                    msg.Contains("no connection could be made"))
                {
                    var apiUrl =
                        _client.BaseAddress?.ToString()
                        ?? "URL não configurada";

                    return
                        "❌ A API do AtelieDaTransformacao " +
                        "não está em execução.\n\n" +
                        $"URL configurada: {apiUrl}\n\n" +
                        "Verifique se o projeto " +
                        "AtelieDaTransformacao.API está rodando " +
                        "no Visual Studio.";
                }

                // -------------------------------------------------------------
                // SSL / CERTIFICADO
                // -------------------------------------------------------------

                if (msg.Contains("ssl") ||
                    msg.Contains("certificate") ||
                    msg.Contains("https"))
                {
                    return
                        "🔒 Erro de conexão SSL.\n\n" +
                        "Verifique o certificado HTTPS da API " +
                        "AtelieDaTransformacao.\n\n" +
                        "Durante o desenvolvimento, você também " +
                        "pode testar utilizando o perfil HTTP da API.";
                }

                // -------------------------------------------------------------
                // DNS / HOST
                // -------------------------------------------------------------

                if (msg.Contains("name or service not known") ||
                    msg.Contains("no such host") ||
                    msg.Contains("getaddrinfo"))
                {
                    return
                        "🌐 Host da API não encontrado.\n\n" +
                        $"Verifique a URL configurada: " +
                        $"{_client.BaseAddress}";
                }

                // -------------------------------------------------------------
                // ERRO HTTP GENÉRICO
                // -------------------------------------------------------------

                return
                    $"⚠ Erro de comunicação com a API:\n" +
                    $"{httpEx.Message}";
            }

            // =================================================================
            // URL INVÁLIDA
            // =================================================================

            if (ex is UriFormatException or InvalidOperationException)
            {
                return
                    "⚠ URL da API inválida.\n\n" +
                    "Verifique o AppConfig e as configurações " +
                    "de execução do projeto " +
                    "AtelieDaTransformacao.API.";
            }

            // =================================================================
            // ERRO GENÉRICO
            // =================================================================

            return
                $"⚠ Erro inesperado:\n{ex.Message}";
        }
    }
}