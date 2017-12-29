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
        public async Task<PhoneDTO> AddPhoneAsync(ApplicationUser user, PhoneDTO dto)
        {
            var phone = await _db.UserPhones
                .Where(w => w.PhoneId == dto.Number)
                .FirstOrDefaultAsync();

            if (phone != null)
            {
                if (phone.UserId != user.Id)
                    throw new Exception("Phone belongs to another user.");

                throw new Exception("Phone already exists.");
            }

            var result = new UserPhone
            {
                Phone = new Phone
                {
                    Number = dto.Number,
                    IsSMS = dto.SMS
                },
                IsPublic = dto.Public
            };
            user.UserPhones.Add(result);
            await IdentityManager.UpdateAsync(user);
            return _mapper.Map<PhoneDTO>(result);
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
            var phone = await _db.UserPhones
                .Include(i => i.Phone)
                .Where(w => w.PhoneId == dto.Number && w.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (phone == null)
                throw new Exception($"User with Id: {user.Id} does not have Phone: {dto.Number}");

            phone.IsPublic = dto.Public;
            phone.Phone.IsSMS = dto.SMS;

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

        public async Task<int> DeletePhoneAsync(int userId, string number)
        {
            //var userPhone = await _db.UserPhones.FindAsync(number, userId);
            var phone = await _db.Phones.FindAsync(number);
            //if (userPhone != null)
            //{
            //    _db.UserPhones.Remove(userPhone);
            //    return await _db.SaveChangesAsync();
            //}
            if (phone != null)
            {
                _db.Phones.Remove(phone);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
    }
}
