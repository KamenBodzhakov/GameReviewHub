using Microsoft.AspNetCore.Identity;

namespace GameReviewHub.Seed
{
    using static Common.ExceptionMessages;

    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "User", "Administrator" };

            foreach (var role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    IdentityResult identityRoleResult =
                        await roleManager.CreateAsync(new IdentityRole(role));

                    if (!identityRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException(
                            string.Format(RoleSeedingExceptionMessage, role));
                    }
                }
            }
        }

        public static void SeedRoles(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            SeedRolesAsync(services)
                .GetAwaiter()
                .GetResult();
        }

        public static void AssignAdmin(IApplicationBuilder app, string email)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            UserManager<IdentityUser> userManager =
                services.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser? user = userManager
                .FindByEmailAsync(email)
                .GetAwaiter()
                .GetResult();

            if (user == null)
            {
                throw new InvalidOperationException(string.Format(AdminUserNotFoundExceptionMessage, email));
            }

            bool isInRole = userManager
                .IsInRoleAsync(user, "Administrator")
                .GetAwaiter()
                .GetResult();

            if (!isInRole)
            {
                IdentityResult result = userManager
                    .AddToRoleAsync(user, "Administrator")
                    .GetAwaiter()
                    .GetResult();

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(AdminRoleAssignmentExceptionMessage, email));
                }
            }
        }
    }
}