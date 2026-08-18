using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services
{
    /// <summary>
    /// Serviço responsável pela comunicação com os endpoints
    /// de autenticação da API.
    /// </summary>
    public class AuthApiService
    {
        private readonly HttpClientHelper _http;

        public AuthApiService()
        {
            _http = HttpClientHelper.Instance;
        }

        /// <summary>
        /// Realiza o login através da API.
        /// Endpoint: POST /api/auth/login
        /// </summary>
        public async Task<(bool Success, UserResponseDto? User, string ErrorMessage)> LoginAsync(
            string email,
            string password)
        {
            var loginDto = new LoginRequestDto
            {
                Email = email,
                Password = password
            };

            var (success, data, error) =
                await _http.PostAsync<UserResponseDto>(
                    "/api/auth/login",
                    loginDto);

            return (success, data, error);
        }

        /// <summary>
        /// Realiza o logout através da API.
        /// Endpoint: POST /api/auth/logout
        /// </summary>
        public async Task<(bool Success, string ErrorMessage)> LogoutAsync()
        {
            var result = await _http.PostEmptyAsync("/api/auth/logout");

            _http.ClearCookies();

            return result;
        }

        /// <summary>
        /// Busca os dados do usuário atualmente autenticado.
        /// Endpoint: GET /api/auth/me
        /// </summary>
        public async Task<UserResponseDto?> GetCurrentUserAsync()
        {
            return await _http.GetAsync<UserResponseDto>("/api/auth/me");
        }

        /// <summary>
        /// Registra um novo usuário através da API.
        /// Endpoint: POST /api/auth/register
        /// </summary>
        public async Task<(bool Success, string ErrorMessage)> RegisterAsync(
            string email,
            string password,
            string confirmPassword)
        {
            var registerDto = new RegisterRequestDto
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var (success, _, error) =
                await _http.PostAsync<object>(
                    "/api/auth/register",
                    registerDto);

            return (success, error);
        }
    }
}