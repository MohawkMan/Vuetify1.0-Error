using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VBL.Core
{
    public partial class EmailManager
    {
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly VblUserManager _userManager;
        private readonly HttpClient _client = new HttpClient();

        public EmailManager(VBLDbContext db, ILogger<EmailManager> logger, IOptions<VblConfig> config, VblUserManager userManager)
        {
            _db = db;
            _logger = logger;
            _config = config.Value;
            _userManager = userManager;
        }
        private async Task<object> MapSparkPostTemplate(SparkPostEmailTemplate template, string templateFor)
        {
            if (template == null)
            {
                template = await _db.SparkPostEmailTemplates
                    .Where(w => w.For == templateFor)
                    .OrderByDescending(o => o.IsCurrent)
                    .ThenByDescending(t => t.DtModified)
                    .FirstOrDefaultAsync();
            }
            return new
            {
                template_id = template.TemplateId,
                use_draft_template = template.IsDraft
            };
        }

        public async Task<HttpResponseMessage> SparkPostSend(object contentObj)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_config.AppKeys.SparkPost);

            var content = JsonConvert.SerializeObject(contentObj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), DateFormatString = "yyyy-MM-ddTHH:mm:ssZ" });
            return await _client.PostAsync(_config.SparkPost.SendingUrl, new StringContent(content, Encoding.UTF8, "application/json"));
        }
    }
}
