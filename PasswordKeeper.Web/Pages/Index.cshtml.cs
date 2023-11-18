using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasswordKeeper.BLL;

namespace PasswordKeeper.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            AppConfig appConfig = AppConfig.GetInstance();
            configuration.GetSection("PasswordkeeperConfig").Bind(appConfig);
            var credentials_encryption = new CredentialsEncryption(new AES128Algorithm());
            appConfig.EncryptionKey = credentials_encryption.DecryptKey(appConfig.EncryptionKey);
            appConfig.AuthenticationKey = credentials_encryption.DecryptKey(appConfig.AuthenticationKey);
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetRefresh()
        {
           AppConfig.GetInstance().RefreshCredentials();
            return Page();
        }

    }
}
