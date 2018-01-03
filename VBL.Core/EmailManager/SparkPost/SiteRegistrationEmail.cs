using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VBL.Data;

namespace VBL.Core
{
    public partial class EmailManager
    {
        [Queue("email")]
        public async Task SendSiteRegistrationEmailAsync(int userEmailId)
        {
            var email = await _db.UserEmails.Include(i => i.User).FirstAsync(f => f.Id == userEmailId);

            var sparkPostContent = new
            {
                substitution_data = MapSiteRegistrationData(),
                recipients = await MapSiteRegistrationRecipientAsync(email),
                content = await MapSparkPostTemplate(null, "NewUser")
            };
            var result = await SparkPostSend(sparkPostContent);
        }

        private object MapSiteRegistrationData()
        {
            return new
            {
                FeedbackLink = _config.Links.Feedback
            };
        }
        public async Task<List<object>> MapSiteRegistrationRecipientAsync(UserEmail email)
        {
            var token = await _userManager.GenerateEmailTokenAsync(email.User, email.Id);
            var confirmLink = $"{_config.Links.EmailConfirmBase}/{email.Id}/{token}";
            return new List<object>
            {
                new
                {
                    address = email.Address,
                    substitution_data = new
                    {
                        FirstName = email.User.FirstName,
                        FirstNameUpper = email.User.FirstName.ToUpper(),
                        ConfirmLink = confirmLink
                    }
                }
            };
        }
    }
}
