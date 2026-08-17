using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Desktop.Helpers
{
    public sealed class SessionManager
    {
        private static readonly Lazy<SessionManager> _instance =
            new(() => new SessionManager());

        public static SessionManager Instance => _instance.Value;

        private SessionManager()
        {
        }

        /// <summary>
        /// Dados do usuário atualmente autenticado.
        /// É null quando nenhum usuário está logado.
        /// </summary>
        public UserResponseDto? CurrentUser { get; private set; }

        /// <summary>
        /// Indica se existe um usuário autenticado.
        /// </summary>
        public bool IsAuthenticated => CurrentUser != null;

        /// <summary>
        /// Indica se o usuário autenticado é administrador.
        /// </summary>
        public bool IsAdmin =>
            CurrentUser?.Roles?.Contains("Admin") ?? false;

        /// <summary>
        /// Define o usuário autenticado na sessão.
        /// </summary>
        public void SetUser(UserResponseDto user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// Limpa os dados da sessão.
        /// </summary>
        public void Clear()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Retorna o e-mail do usuário atual.
        /// </summary>
        public string GetEmail()
        {
            return CurrentUser?.Email ?? string.Empty;
        }

        /// <summary>
        /// Retorna o nome de exibição baseado no e-mail.
        /// </summary>
        public string GetDisplayName()
        {
            var email = GetEmail();

            if (string.IsNullOrEmpty(email))
                return "Usuário";

            var at = email.IndexOf("@");

            return at > 0
                ? email[..at]
                : email;
        }
    }
}