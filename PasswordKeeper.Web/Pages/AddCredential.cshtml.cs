using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PasswordKeeper.BLL;

namespace PasswordKeeper.Web.Pages
{
    public class AddCredentialModel : PageModel
    {
        private readonly ILogger<AddCredentialModel> _logger;
        [BindProperty]
        public CredentialsDTO Cred { get; set; }


        public AddCredentialModel(ILogger<AddCredentialModel> logger, IConfiguration configuration)
        {
            _logger = logger;

        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            { 
            var aES128Algorithm = new PasswordKeeper.BLL.AES128Algorithm();
                var appConfig = AppConfig.GetInstance();
                Request x = new Request(appConfig.CredentialsFile, aES128Algorithm, appConfig.EncryptionKey);
                Credential cr = new Credential() { Description = Cred.Description, UserName = Cred.UserName, Pwd = Cred.Pwd };
                FileHandling.InsertOperation(cr, aES128Algorithm);
                AppConfig.GetInstance().RefreshCredentials();
                return RedirectToPage("ViewCredentials");
            }
            else
            {
                return Page();
            }
        }
    }
}
