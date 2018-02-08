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
        HttpClient _client = new HttpClient();

        public AAUManager()
        {

        }

        public async Task SoapRequest()
        {
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                <soapenv:Header/>
                <soapenv:Body>
                    <tem:Membership_Verify_LastName_Secured>
                        <tem:uUserID>2894774d-6d07-e811-966d-a4badb131cf0</tem:uUserID>
                        <tem:iRecordSource>56985473</tem:iRecordSource>
                        <tem:MembershipID>54T57FA8</tem:MembershipID>
                        <tem:LastName>Oliverson</tem:LastName>
                    </tem:Membership_Verify_LastName_Secured>
                </soapenv:Body>
            </soapenv:Envelope>";

            _client.DefaultRequestHeaders.Clear();
           // _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
           // _client.DefaultRequestHeaders.Add("SOAPAction", "Membership_Verify_LastName_Secured");

            var response = await _client.PostAsync("https://websrv1.aausports.org/Membership/svcMembershipLookup.svc/Membership/svcMembershipLookup.svc", new StringContent(body, Encoding.UTF8, "text/xml"));
        }
    }
}
