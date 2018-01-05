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
        public async Task<List<PhoneDTO>> GetUserPhoneListAsync(ApplicationUser user)
        {
            user = await EnsureUserPhones(user);

            return _mapper.Map<List<PhoneDTO>>(user.UserPhones);
        }
        public async Task<List<PhoneDTO>> GetUserPhoneListAsync(int userId)
        {
            var applicationUser = await IdentityManager.FindByIdAsync(userId.ToString());
            return await GetUserPhoneListAsync(applicationUser);
        }
        public async Task<List<PhoneDTO>> GetUserPhoneListAsync(ClaimsPrincipal user)
        {
            var applicationUser = await IdentityManager.GetUserAsync(user);
            return await GetUserPhoneListAsync(applicationUser);
        }

        public async Task<PhoneDTO> AddPhoneAsync(ApplicationUser user, PhoneDTO dto)
        {
            if(dto.Id != 0)
                throw new Exception("Can not add an existing phone");

            user = await EnsureUserPhones(user);

            if(user.UserPhones.Any(a => a.Number == dto.Number))
                throw new Exception("Phone already exists.");

            if(dto.IsPrimary)
            {
                foreach(var p in user.UserPhones.Where(w => w.IsPrimary))
                {
                    p.IsPrimary = false;
                }
            }

            var newPhone = _mapper.Map<UserPhone>(dto);
            newPhone.Number = dto.Number;
            user.UserPhones.Add(newPhone);

            await IdentityManager.UpdateAsync(user);
            return _mapper.Map<PhoneDTO>(newPhone);
        }
        public async Task<PhoneDTO> AddPhoneAsync(int userId, PhoneDTO dto)
        {
            var applicationUser = await IdentityManager.FindByIdAsync(userId.ToString());
            return await AddPhoneAsync(applicationUser, dto);
        }
        public async Task<PhoneDTO> AddPhoneAsync(ClaimsPrincipal user, PhoneDTO dto)
        {
            var applicationUser = await IdentityManager.GetUserAsync(user);
            return await AddPhoneAsync(applicationUser, dto);
        }

        public async Task<PhoneDTO> UpdatePhoneAsync(ApplicationUser user, PhoneDTO dto)
        {
            user = await EnsureUserPhones(user);

            var phone = user.UserPhones.Where(w => w.Id == dto.Id).FirstOrDefault();

            if (phone == null)
                throw new Exception($"User with Id: {user.Id} does not have Phone: {dto.Number}");

            _mapper.Map(dto, phone);

            if(dto.IsPrimary)
            {
                foreach (var p in user.UserPhones.Where(w => w.IsPrimary && w.Id != dto.Id))
                {
                    p.IsPrimary = false;
                }
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<PhoneDTO>(phone);
        }
        public async Task<PhoneDTO> UpdatePhoneAsync(int userId, PhoneDTO dto)
        {
            var applicationUser = await IdentityManager.FindByIdAsync(userId.ToString());
            return await UpdatePhoneAsync(applicationUser, dto);
        }
        public async Task<PhoneDTO> UpdatePhoneAsync(ClaimsPrincipal user, PhoneDTO dto)
        {
            var applicationUser = await IdentityManager.GetUserAsync(user);
            return await UpdatePhoneAsync(applicationUser, dto);
        }

        public async Task<int> DeletePhoneAsync(int userId, int userPhoneId)
        {
            var phone = await _db.UserPhones.FirstOrDefaultAsync(f => f.Id == userPhoneId && f.UserId == userId);
            if (phone != null)
            {
                _db.UserPhones.Remove(phone);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        private async Task<ApplicationUser> EnsureUserPhones(ApplicationUser user)
        {
            if (!_db.Entry(user).Collection(u => u.UserPhones).IsLoaded)
                await _db.Entry(user).Collection(u => u.UserPhones).LoadAsync();
            return user;
        }
    }
}
