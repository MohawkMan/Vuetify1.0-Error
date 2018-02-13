using AAU;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VBL.Core
{
    public partial class AAUManager
    {
        private readonly MembershipLookupServiceClient _aau = new MembershipLookupServiceClient();
        private readonly VblConfig _config;

        public AAUManager(IOptions<VblConfig> config)
        {
            _config = config.Value;
        }

        public async Task<bool> VerifyByLastNameAsync(string membershipID, string lastName)
        {
            return await _aau.Membership_Verify_LastName_SecuredAsync(_config.AppKeys.AAUUserId, _config.AppKeys.AAURecordSource, membershipID, lastName);
        }

        public async Task<bool> VerifyByZipAsync(string membershipID, string zip)
        {
            return await _aau.Membership_Verify_SecuredAsync(_config.AppKeys.AAUUserId, _config.AppKeys.AAURecordSource, membershipID, zip);
        }

        public async Task<bool> VerifyByDobAsync(string membershipID, string dob)
        {
            return await _aau.Membership_Verify_LastName_SecuredAsync(_config.AppKeys.AAUUserId, _config.AppKeys.AAURecordSource, membershipID, dob);
        }
    }
}
