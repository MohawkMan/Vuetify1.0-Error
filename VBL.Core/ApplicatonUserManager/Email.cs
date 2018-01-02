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
    public partial class VblUserManager
    {
        public async Task<EmailDTO> AddEmailAsync(ApplicationUser user, EmailDTO dto)
        {
            var email = await _db.UserEmails
                .Where(w => w.Address == dto.Address)
                .FirstOrDefaultAsync();

            if (email != null)
            {
                if (email.UserId != user.Id)
                    throw new Exception("Email belongs to another user.");

                throw new Exception("Email already exists.");
            }

            var newEmail = _mapper.Map<UserEmail>(dto);
            user.UserEmails.Add(newEmail);
            await IdentityManager.UpdateAsync(user);
            return _mapper.Map<EmailDTO>(newEmail);
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
                .Where(w => w.Address == dto.Address && w.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (email == null)
                throw new Exception($"User with Id: {user.Id} does not have Email: {dto.Address}");

            email.IsPublic = dto.IsPublic;

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

        public async Task<int> DeleteEmailAsync(int userId, string address)
        {
            var email = await _db.UserEmails.FindAsync(address);

            if (email != null)
            {
                _db.UserEmails.Remove(email);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<string> GenerateEmailTokenAsync(ApplicationUser user, int emailId)
        {
            var email = user.UserEmails.Find(f => f.Id == emailId);
            if (email == null)
                throw new NullReferenceException($"User does not have an email with Id: {emailId}");

            return await IdentityManager.GenerateUserTokenAsync(user, IdentityManager.Options.Tokens.EmailConfirmationTokenProvider, $"EmailConfirmation:{email.Address}");
        }
        public async Task<bool> ConfirmEmailAsync(int emailId, string token)
        {
            var email = await _db.UserEmails.Include(i => i.User).FirstAsync(f => f.Id == emailId);
            var result = await IdentityManager.VerifyUserTokenAsync(email.User, IdentityManager.Options.Tokens.EmailConfirmationTokenProvider, $"EmailConfirmation:{email.Address}", token);
            if(result)
            {
                email.IsVerified = true;
                await _db.SaveChangesAsync();
            }
            return result;
        }
    }
}
