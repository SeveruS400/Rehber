using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }
        Task<IEnumerable<UserWithRolesDto>> GetAllUsers();
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto);
        Task<Users> GetOneUser(string userName);
        Task<IdentityResult> DeleteOneUser(string userName);
        Task Update(UserDtoForUpdate userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);
        IEnumerable<string> GetCurrentUserRoles(string userName);
        Task<IEnumerable<UserWithRolesDto>> GetAllUsersByRole();
    }
}
