using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsApp.Web.Models;

namespace NewsApp.Web.Core.Identity
{
    public class UserService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext dataContextFactory)
        {
            _userManager = new UserManager(dataContextFactory);
            _roleManager = new RoleManager(dataContextFactory);
            _context = dataContextFactory;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        //public async Task<User> GetUserByEmail(string email)
        //{
        //    return await _userManager.FindByEmailAsync(email);
        //}

        public async Task<ApplicationUser> GetUserByEmailOrPhone(string emailOrPhone)
        {
            if (emailOrPhone.Contains("@"))
            {
                return await _userManager.FindByEmailAsync(emailOrPhone);
            }
            else
            {
                return await _userManager.Users
                    .FirstOrDefaultAsync(i => i.Email == emailOrPhone);

            }
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> GetUsers(string search = "", string role = null)
        {
            return await GetUsersQuery(search, role).ToListAsync();
        }

        public T GetUsersByQuery<T>(string search, string role, Func<IQueryable<ApplicationUser>, T> processQueryable)
        {
            var users = GetUsersQuery(search, role);
            return processQueryable(users);
        }

        private IQueryable<ApplicationUser> GetUsersQuery(string search, string role)
        {
            var users = _userManager.Users;

            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users.Where(i =>
                    i.FirstName.Contains(search) ||
                    i.LastName.Contains(search) ||
                    i.UserName.Contains(search) ||
                    i.Email.Contains(search));
            }

            if (!string.IsNullOrEmpty(role))
            {
                var identityRole = _roleManager.FindByName(role);
                if (identityRole == null)
                {
                    throw new ArgumentException("Cannot find role '" + role + "'.");
                }

                users = users.Where(i => i.Roles.Select(r => r.RoleId).Contains(identityRole.Id));
            }

            return users.OrderByDescending(i => i.LastName)
                .ThenBy(i => i.FirstName);
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            if (!(await ValidateUserIdentity(user.Email, user.Id)))
            {
                return new IdentityResult("Email or phone number already exist in database");
            }

            using (var dc = _context)
            {
                dc.SetModified(user);
                await dc.SaveChangesAsync();
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> ChangePassword(IIdentity identity, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(identity.GetUserId(), oldPassword, newPassword);
        }

        public string HashPassword(string password)
        {
            return _userManager.PasswordHasher.HashPassword(password);
        }

        public async Task<IReadOnlyCollection<IdentityRole>> GetAllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IReadOnlyCollection<string>> GetRolesForUser(string userId)
        {
            return (await _userManager.GetRolesAsync(userId)).ToList();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersInRole(string role)
        {
            var userRole = await _roleManager.FindByNameAsync(role);
            var userIds = userRole.Users.Select(i => i.UserId).ToList();
            var users = _userManager.Users.Where(i => userIds.Contains(i.Id));
            return await users.ToListAsync();
        }

        public async Task<bool> UserIsInRole(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user.Id, role);
        }

        public async Task AddUserToRole(string userId, string roleName)
        {
            await _userManager.AddToRoleAsync(userId, roleName);
        }

        public async Task RemoveUserFromRole(string userId, string roleName)
        {
            await _userManager.RemoveFromRoleAsync(userId, roleName);
        }

        public async Task<bool> ValidateUserIdentity(string email, string phoneNumber, string userId = null)
        {
            var validEmail = true;
            var users = _userManager.Users;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                users = users.Where(i => i.Id != userId);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                var user = await users.FirstOrDefaultAsync(i => i.Email == email);
                validEmail = user == null;
            }

            return validEmail;

        }
    }
}