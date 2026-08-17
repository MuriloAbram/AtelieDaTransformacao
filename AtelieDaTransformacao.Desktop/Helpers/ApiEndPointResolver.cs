using System.Text.Json;

namespace AtelieDaTransformacao.Desktop.Helpers
{
    /// <summary>
    /// Responsável por descobrir automaticamente a URL base da API.
    /// </summary>
    public static class ApiEndpointResolver
    {
        // Cache da URL encontrada
        private static string? _resolvedUrl;
        private static bool _resolved = false;

        // Nome correto do projeto da API
        private const string ApiProjectName = "AtelieDaTransformacao.API";

        // Caminho do launchSettings.json dentro da API
        private const string LaunchSettingsRelativePath =
            $"{ApiProjectName}/Properties/launchSettings.json";

        // Ordem de preferência dos perfis
        private static readonly string[] PreferredProfiles =
        [
            "http",
            "https",
            "IIS Express"
        ];

        /// <summary>
        /// Resolve a URL base da API.
        /// Primeiro tenta encontrar o launchSettings.json.
        /// Se não encontrar, usa o appsettings.json do Desktop.
        /// </summary>
        /// <returns>
        /// URL base da API ou null caso não seja encontrada.
        /// </returns>
        public static string? Resolve()
        {
            // Retorna o valor armazenado em cache
            if (_resolved)
                return _resolvedUrl;

            _resolved = true;

            // ================================================================
            // PRIORIDADE 1 - launchSettings.json
            // ================================================================

            var fromLaunchSettings = TryResolveFromLaunchSettings();

            if (fromLaunchSettings != null)
            {
                _resolvedUrl = fromLaunchSettings;

                Log($"API localizada em: {_resolvedUrl}");
                Log($"Origem: launchSettings.json do {ApiProjectName}");

                return _resolvedUrl;
            }

            // ================================================================
            // PRIORIDADE 2 - appsettings.json
            // ================================================================

            var fromAppSettings = TryResolveFromAppSettings();

            if (fromAppSettings != null)
            {
                _resolvedUrl = fromAppSettings;

                Log($"API localizada em: {_resolvedUrl}");
                Log("Origem: appsettings.json");

                return _resolvedUrl;
            }

            // ================================================================
            // PRIORIDADE 3 - não encontrada
            // ================================================================

            Log("URL da API não foi localizada.");
            Log(
                $"Verifique se {ApiProjectName}/Properties/launchSettings.json existe."
            );
            Log(
                "Ou configure ApiSettings.BaseUrl no appsettings.json do Desktop."
            );

            _resolvedUrl = null;

            return null;
        }

        /// <summary>
        /// Limpa o cache para que a URL seja localizada novamente.
        /// </summary>
        public static void Reset()
        {
            _resolved = false;
            _resolvedUrl = null;
        }

        // ====================================================================
        // LAUNCH SETTINGS
        // ====================================================================

        /// <summary>
        /// Tenta localizar o launchSettings.json da API.
        /// </summary>
        private static string? TryResolveFromLaunchSettings()
        {
            var candidates = BuildLaunchSettingsCandidatePaths();

            foreach (var candidate in candidates)
            {
                Log($"Testando: {candidate}");

                if (!File.Exists(candidate))
                    continue;

                Log($"launchSettings.json encontrado: {candidate}");

                var url = ParseLaunchSettings(candidate);

                if (url != null)
                    return url;
            }

            return null;
        }

        /// <summary>
        /// Cria possíveis caminhos onde o launchSettings.json pode estar.
        /// </summary>
        private static List<string> BuildLaunchSettingsCandidatePaths()
        {
            var paths = new List<string>();

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Executável normalmente fica em:
            //
            // AtelieDaTransformacao.Desktop
            // └── bin
            //     └── Debug
            //         └── net8.0-windows
            //
            // Subimos alguns níveis para tentar encontrar
            // a raiz da solução.

            var relativeLevels = new[]
            {
                4,
                5,
                3,
                6
            };

            foreach (var levels in relativeLevels)
            {
                var dir = GoUpDirectories(baseDir, levels);

                if (dir != null)
                {
                    paths.Add(
                        Path.Combine(
                            dir,
                            LaunchSettingsRelativePath
                        )
                    );
                }
            }

            // Tenta utilizar SolutionDir, caso esteja disponível
            var solutionDir =
                Environment.GetEnvironmentVariable("SolutionDir");

            if (!string.IsNullOrWhiteSpace(solutionDir))
            {
                paths.Add(
                    Path.Combine(
                        solutionDir,
                        LaunchSettingsRelativePath
                    )
                );
            }

            // Tenta também a partir do diretório atual
            paths.Add(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    LaunchSettingsRelativePath
                )
            );

            return paths.Distinct().ToList();
        }

        /// <summary>
        /// Lê o launchSettings.json e procura a URL da API.
        /// </summary>
        private static string? ParseLaunchSettings(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);

                using var doc = JsonDocument.Parse(json);

                var root = doc.RootElement;

                // Procura a seção profiles
                if (!root.TryGetProperty("profiles", out var profiles))
                {
                    Log(
                        "launchSettings.json não possui a seção 'profiles'."
                    );

                    return null;
                }

                // Procura os perfis na ordem definida
                foreach (var profileName in PreferredProfiles)
                {
                    if (!profiles.TryGetProperty(
                            profileName,
                            out var profile))
                    {
                        continue;
                    }

                    if (!profile.TryGetProperty(
                            "applicationUrl",
                            out var urlProperty))
                    {
                        continue;
                    }

                    var applicationUrl =
                        urlProperty.GetString();

                    if (string.IsNullOrWhiteSpace(applicationUrl))
                        continue;

                    var url =
                        ExtractBestUrl(
                            applicationUrl,
                            profileName
                        );

                    if (url != null)
                    {
                        Log(
                            $"Perfil '{profileName}' encontrado."
                        );

                        Log(
                            $"applicationUrl: {applicationUrl}"
                        );

                        Log(
                            $"URL selecionada: {url}"
                        );

                        return url;
                    }
                }

                Log(
                    "Nenhum perfil válido com applicationUrl foi encontrado."
                );

                return null;
            }
            catch (JsonException ex)
            {
                Log(
                    $"Erro ao interpretar launchSettings.json: {ex.Message}"
                );

                return null;
            }
            catch (Exception ex)
            {
                Log(
                    $"Erro ao ler launchSettings.json: {ex.Message}"
                );

                return null;
            }
        }

        /// <summary>
        /// Escolhe a melhor URL quando applicationUrl possui mais de uma.
        /// </summary>
        private static string? ExtractBestUrl(
            string applicationUrl,
            string profileName)
        {
            var urls = applicationUrl
                .Split(
                    ';',
                    StringSplitOptions.RemoveEmptyEntries
                )
                .Select(u => u.Trim())
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .ToList();

            if (urls.Count == 0)
                return null;

            // Perfil HTTP: prioriza HTTP
            if (profileName.Equals(
                    "http",
                    StringComparison.OrdinalIgnoreCase))
            {
                var httpUrl = urls.FirstOrDefault(
                    u => u.StartsWith(
                        "http://",
                        StringComparison.OrdinalIgnoreCase
                    )
                );

                return httpUrl ?? urls[0];
            }

            // Perfil HTTPS: prioriza HTTPS
            var httpsUrl = urls.FirstOrDefault(
                u => u.StartsWith(
                    "https://",
                    StringComparison.OrdinalIgnoreCase
                )
            );

            return httpsUrl ?? urls[0];
        }

        // ====================================================================
        // APPSETTINGS
        // ====================================================================

        /// <summary>
        /// Tenta encontrar a URL no appsettings.json do Desktop.
        /// </summary>
        private static string? TryResolveFromAppSettings()
        {
            try
            {
                var path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "appsettings.json"
                );

                if (!File.Exists(path))
                {
                    Log(
                        "appsettings.json não encontrado."
                    );

                    return null;
                }

                var json = File.ReadAllText(path);

                using var doc = JsonDocument.Parse(json);

                var root = doc.RootElement;

                // ============================================================
                // Formato atual
                // ============================================================

                if (root.TryGetProperty(
                        "ApiSettings",
                        out var apiSettings))
                {
                    if (apiSettings.TryGetProperty(
                            "BaseUrl",
                            out var baseUrl))
                    {
                        var url = baseUrl.GetString();

                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            Log(
                                $"ApiSettings.BaseUrl encontrado: {url}"
                            );

                            return url;
                        }
                    }
                }

                // ============================================================
                // Formato antigo - mantido por compatibilidade
                // ============================================================

                if (root.TryGetProperty(
                        "ApiBaseUrl",
                        out var legacyUrl))
                {
                    var url = legacyUrl.GetString();

                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        Log(
                            $"ApiBaseUrl encontrado: {url}"
                        );

                        return url;
                    }
                }

                Log(
                    "appsettings.json não possui ApiSettings.BaseUrl."
                );

                return null;
            }
            catch (JsonException ex)
            {
                Log(
                    $"Erro ao interpretar appsettings.json: {ex.Message}"
                );

                return null;
            }
            catch (Exception ex)
            {
                Log(
                    $"Erro ao ler appsettings.json: {ex.Message}"
                );

                return null;
            }
        }

        // ====================================================================
        // UTILITÁRIOS
        // ====================================================================

        /// <summary>
        /// Sobe determinada quantidade de diretórios.
        /// </summary>
        private static string? GoUpDirectories(
            string path,
            int levels)
        {
            var dir = new DirectoryInfo(path);

            for (int i = 0; i < levels; i++)
            {
                dir = dir.Parent;

                if (dir == null)
                    return null;
            }

            return dir.FullName;
        }

        /// <summary>
        /// Escreve mensagens de diagnóstico no Output/Console.
        /// </summary>
        private static void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(
                $"[ApiEndpointResolver] {message}"
            );

            Console.WriteLine(
                $"[ApiEndpointResolver] {message}"
            );
        }
    }
}