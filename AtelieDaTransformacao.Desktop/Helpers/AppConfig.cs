using System.Text.Json;

namespace AtelieDaTransformacao.Desktop.Helpers
{
    /// <summary>
    /// Responsável por carregar as configurações do Desktop.
    /// </summary>
    public static class AppConfig
    {
        private static JsonDocument? _config;

        /// <summary>
        /// URL base da API.
        ///
        /// A URL é resolvida pelo ApiEndpointResolver seguindo a ordem:
        ///
        /// 1. launchSettings.json do AtelieDaTransformacao.API
        /// 2. appsettings.json → ApiSettings.BaseUrl
        /// 3. String vazia caso nenhuma URL seja encontrada.
        /// </summary>
        public static string ApiBaseUrl =>
            ApiEndpointResolver.Resolve() ?? string.Empty;

        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public static string AppName =>
            GetNestedValue("AppSettings", "AppName")
            ?? "Atelie da Transformacao Desktop";

        /// <summary>
        /// Versão da aplicação.
        /// </summary>
        public static string Version =>
            GetNestedValue("AppSettings", "Version")
            ?? "1.0.0";

        /// <summary>
        /// Tempo limite das requisições HTTP, em segundos.
        /// </summary>
        public static int Timeout
        {
            get
            {
                var raw = GetNestedValue("AppSettings", "Timeout");

                return int.TryParse(raw, out var timeout)
                    ? timeout
                    : 30;
            }
        }

        /// <summary>
        /// Carrega o arquivo appsettings.json.
        /// </summary>
        private static JsonDocument GetConfig()
        {
            if (_config != null)
                return _config;

            try
            {
                var path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "appsettings.json");

                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);

                    // Remove comentários para permitir
                    // a leitura do JSON pelo JsonDocument.
                    json = RemoveJsonComments(json);

                    _config = JsonDocument.Parse(json);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[AppConfig] Erro ao ler appsettings.json: {ex.Message}");
            }

            return _config ?? JsonDocument.Parse("{}");
        }

        /// <summary>
        /// Obtém um valor dentro de uma seção do appsettings.json.
        /// </summary>
        private static string? GetNestedValue(
            string section,
            string key)
        {
            try
            {
                var config = GetConfig();

                if (config.RootElement.TryGetProperty(
                    section,
                    out var sectionElement))
                {
                    if (sectionElement.TryGetProperty(
                        key,
                        out var value))
                    {
                        return value.GetString()
                               ?? value.ToString();
                    }
                }
            }
            catch
            {
                // Retorna null caso a configuração não seja encontrada.
            }

            return null;
        }

        /// <summary>
        /// Remove comentários de linha do appsettings.json.
        /// </summary>
        private static string RemoveJsonComments(string json)
        {
            var lines = json.Split('\n');
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var line in lines)
            {
                var trimmed = line.TrimStart();

                // Ignora linhas que são somente comentários.
                if (trimmed.StartsWith("//"))
                    continue;

                // Remove comentários que aparecem depois de uma configuração.
                var commentIndex = line.IndexOf(
                    "//",
                    StringComparison.Ordinal);

                stringBuilder.AppendLine(
                    commentIndex > 0
                        ? line[..commentIndex]
                        : line);
            }

            return stringBuilder.ToString();
        }
    }
}