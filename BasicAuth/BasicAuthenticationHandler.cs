using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace publishing_api.BasicAuth
{
    /// <summary>
    /// Реализация простой аутентификации
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// Наименование заголовка авторизации
        /// </summary>
        private readonly string AuthorizationHeaderName = "Authorization";

        /// <summary>
        /// Наименование схемы аутетификации
        /// </summary>
        private readonly string BasicSchemeName = "Basic";

        /// <summary>
        /// Логин. Берем из файла конфигурации проекта
        /// </summary>
        private readonly string _userName;

        /// <summary>
        /// Пароль. Берем из файла конфигурации проекта
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _userName = configuration.GetSection("ps_credentials:user_name").Value;
            _password = configuration.GetSection("ps_credentials:password").Value;
        }

        /// <summary>
        /// Обработчик простой аутентификации
        /// </summary>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthorizationHeaderName))
            {
                // если это не домашняя страница, пишем ошибку
                if (!"/".Equals(Request.Path.Value, StringComparison.OrdinalIgnoreCase) && !"/Home".Equals(Request.Path.Value, StringComparison.OrdinalIgnoreCase))
                {
                    Logger.LogError("Authorization header not in request.");
                }

                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers[AuthorizationHeaderName], out AuthenticationHeaderValue headerValue))
            {
                Logger.LogError("Invalid Authorization header.");

                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!BasicSchemeName.Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                Logger.LogError("Not Basic authentication header.");

                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var userAndPassword = Encoding.ASCII.GetString(Convert.FromBase64String(headerValue.Parameter)).Split(':');

            if (userAndPassword.Length != 2)
            {
                Logger.LogError("Invalid Basic authentication header.");

                return Task.FromResult(AuthenticateResult.Fail("Invalid Basic authentication header."));
            }

            if (!ValidateCredentials(userAndPassword[0], userAndPassword[1]))
            {
                Logger.LogError("Invalid Username or Password.");

                return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password."));
            }

            var claims = new[] { new Claim(ClaimTypes.Name, _userName) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        /// <summary>
        /// Проверка логина и пароля
        /// </summary>
        private bool ValidateCredentials(string userName, string password)
        {
            if (userName == _userName && password == _password)
            {
                return true;
            }

            return false;
        }
    }
}
