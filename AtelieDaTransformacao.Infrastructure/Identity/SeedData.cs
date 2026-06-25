using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace AtelieDaTransformacao.Infrastructure.Identity
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, string adminEmail, string adminPassword)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // 1. Garante que a regra 'Admin' existe no banco de dados
            string roleName = "Admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // 2. Procura se o usuário administrador já existe
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                // Criar o usuário caso não exista
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);

                if (createResult.Succeeded)
                {
                    // 3. Vincula o usuário recém-criado à Role Admin
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
            else
            {
                // Se o usuário já existia mas não era Admin, adiciona a permissão
                if (!await userManager.IsInRoleAsync(adminUser, roleName))
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }
    }
}