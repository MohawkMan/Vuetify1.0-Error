using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using VBL.Data.Mapping;

namespace VBL.Core
{
    public partial class ApplicationUserManager
    {
        public async Task<EmailDTO> AddEmailAsync(ApplicationUser user, EmailDTO dto)
        {
            var email = await _db.UserEmails
                .Where(w => w.EmailId == dto.Address)
                .FirstOrDefaultAsync();

            if (email != null)
            {
                if (email.UserId != user.Id)
                    throw new Exception("Email belongs to another user.");

                throw new Exception("Email already exists.");
            }

            var result = new UserEmail
            {
                Email = new Email
                {
                    Address = dto.Address,
                    IsPublic = dto.Public
                }
            };
            user.UserEmails.Add(result);
            await IdentityManager.UpdateAsync(user);
            return _mapper.Map<EmailDTO>(result);
        }
        public async Task<EmailDTO> AddEmailAsync(int userId, EmailDTO dto)
        {
            var applicationUser = await IdentityManager.FindByIdAsync(userId.ToString());
            return await AddEmailAsync(applicationUser, dto);
        }
        public async Task<EmailDTO> AddEmailAsync(ClaimsPrincipal user, EmailDTO dto)
        {
            var applicationUser = await IdentityManager.GetUserAsync(user);
            return await AddEmailAsync(applicationUser, dto);
        }

        public async Task<EmailDTO> UpdateEmailAsync(ApplicationUser user, EmailDTO dto)
        {
            var email = await _db.UserEmails
                .Include(i => i.Email)
                .Where(w => w.EmailId == dto.Address && w.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (email == null)
                throw new Exception($"User with Id: {user.Id} does not have Email: {dto.Address}");

            email.Email.IsPublic = dto.Public;

            await _db.SaveChangesAsync();
            return _mapper.Map<EmailDTO>(email);
        }
        public async Task<EmailDTO> UpdateEmailAsync(int userId, EmailDTO dto)
        {
            var applicationUser = await IdentityManager.FindByIdAsync(userId.ToString());
            return await UpdateEmailAsync(applicationUser, dto);
        }
        public async Task<EmailDTO> UpdateEmailAsync(ClaimsPrincipal user, EmailDTO dto)
        {
            var applicationUser = await IdentityManager.GetUserAsync(user);
            return await UpdateEmailAsync(applicationUser, dto);
        }

        public async Task<int> DeleteEmailAsync(int userId, string number)
        {
            var email = await _db.Emails.FindAsync(number);

            if (email != null)
            {
                _db.Emails.Remove(email);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
    }
}
