using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AtelieDaTransformacao.Infrastructure.Identity
{
    public static class SeedData //•	Declara uma classe estática SeedData usada para criar dados iniciais (seed) de Identity.
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider) //•	Método público assíncrono que inicializa os dados; recebe IServiceProvider para obter serviços necessários.
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>(); //•	Resolve o UserManager<IdentityUser> do DI. Lança se o serviço não estiver registrado — necessário para operar no user store.

            string email = "admin@ateliedatransformacao.com";//•	Define a string do e-mail que será usado como identificador do usuário seed.

            var user = await userManager.FindByEmailAsync(email);//•	Procura no banco/armazenamento um usuário com esse e-mail; retorna null se não existir.

            if (user == null) //•	Verifica se o usuário não existe; em caso positivo, executa a criação do usuário administrador.
            {
                user = new IdentityUser //•	Cria uma nova instância de IdentityUser para representar o usuário a ser criado.
                {
                    UserName = email,       //------
                    Email = email,          //•	Inicializa UserName e Email com o mesmo e-mail e marca EmailConfirmed = true (pulando verificação por e‑mail).
                    EmailConfirmed = true   //------
                };

                await userManager.CreateAsync(          //•	Persiste o usuário com a senha "Admin@123" no store usando o UserManager (assíncrono). Observação: senha em claro só aceitável para dev/local.
                    user,
                    "Admin@123"
                );

                //admin@ateliedatransformacao.com
                //Senha: Admin@123
            }
        }
    }
}