using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        #region Atributos/Propriedades
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Construtor
        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole>  roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Métodos
        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("admin@localhost.com").Result is null)
            {
                ApplicationUser admin = new ApplicationUser();

                string adminEmail = "admin@localhost.com";

                admin.UserName = adminEmail;
                admin.Email = adminEmail;
                admin.NormalizedUserName = adminEmail.ToUpper();
                admin.NormalizedEmail = adminEmail.ToUpper();
                admin.EmailConfirmed = true;
                admin.LockoutEnabled = false;
                admin.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(admin, "cleanArch@2022").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(admin, "Admin").Wait();
            }

            if (_userManager.FindByEmailAsync("user@localhost.com").Result is null)
            {
                ApplicationUser user = new ApplicationUser();

                string userEmail = "user@localhost.com";

                user.UserName = userEmail;
                user.Email = userEmail;
                user.NormalizedUserName = userEmail.ToUpper();
                user.NormalizedEmail = userEmail.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "cleanArch@2022").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            { 
                IdentityRole roleUser = new IdentityRole();
                roleUser.Name = "User";
                roleUser.NormalizedName = roleUser.Name.ToUpper();
                IdentityResult roleResult = _roleManager.CreateAsync(roleUser).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = "Admin";
                roleAdmin.NormalizedName = roleAdmin.Name.ToUpper();
                IdentityResult roleResult = _roleManager.CreateAsync(roleAdmin).Result;
            }
        }
        #endregion
    }
}
