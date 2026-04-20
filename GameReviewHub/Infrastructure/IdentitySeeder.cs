using Microsoft.AspNetCore.Identity;

namespace GameReviewHub.Seed {
    using static Common.ExceptionMessages;

    public static class IdentitySeeder {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider) {
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "User", "Administrator" };

            foreach (string role in roles) {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists) {
                    IdentityResult identityRoleResult =
                        await roleManager.CreateAsync(new IdentityRole(role));

                    if (!identityRoleResult.Succeeded) {
                        throw new InvalidOperationException(
                            string.Format(RoleSeedingExceptionMessage, role));
                    }
                }
            }
        }

        public static async Task SeedAdminAsync(IServiceProvider serviceProvider, string email, string password) {
            UserManager<IdentityUser> userManager =
                serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser? user = await userManager.FindByEmailAsync(email);

            if (user == null) {
                user = new IdentityUser {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult createResult = await userManager.CreateAsync(user, password);

                if (!createResult.Succeeded) {
                    string errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create seeded admin user: {errors}");
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(user, "Administrator");

            if (!isInRole) {
                IdentityResult result = await userManager.AddToRoleAsync(user, "Administrator");

                if (!result.Succeeded) {
                    string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to assign Administrator role to '{email}': {errors}");
                }
            }
        }

        public static async Task SeedIdentityAsync(IApplicationBuilder app, IConfiguration configuration) {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            string? adminEmail = configuration["AdminSettings:Email"];
            string? adminPassword = configuration["AdminSettings:Password"];

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword)) {
                throw new InvalidOperationException("AdminSettings are missing or incomplete.");
            }

            await SeedRolesAsync(services);
            await SeedAdminAsync(services, adminEmail, adminPassword);
            await SeedRegularUserAsync(services, "user1@gamereviewhub.com", "User123!");
            await SeedRegularUserAsync(services, "user2@gamereviewhub.com", "User234!");
            await SeedRegularUserAsync(services, "user3@gamereviewhub.com", "User345!");
        }

        public static async Task SeedRegularUserAsync(IServiceProvider serviceProvider, string email, string password) {
            UserManager<IdentityUser> userManager =
                serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser? user = await userManager.FindByEmailAsync(email);

            if (user == null) {
                user = new IdentityUser {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult createResult = await userManager.CreateAsync(user, password);

                if (!createResult.Succeeded) {
                    string errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create seeded user: {errors}");
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(user, "User");

            if (!isInRole) {
                IdentityResult roleResult = await userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded) {
                    string errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to assign User role: {errors}");
                }
            }
        }
    }
}