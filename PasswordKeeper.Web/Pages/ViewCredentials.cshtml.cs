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
    public class ViewCredentialsModel : PageModel
    {
        private readonly ILogger<ViewCredentialsModel> _logger;
        public List<Credential> Credentials { get; set; }

        [BindProperty]
        public String Filter { get; set; }

        public ViewCredentialsModel(ILogger<ViewCredentialsModel> logger, IConfiguration configuration)
        {
            _logger = logger;

        }

        public void OnGet()
        {
            Credentials = AppConfig.GetInstance().GetAllCredentials();
        }


        public void OnPost()
        {
            var allCredentials = AppConfig.GetInstance().GetAllCredentials();
            if (string.IsNullOrEmpty(Filter))
                Credentials = allCredentials;
            else
                Credentials = allCredentials.Where(credential => credential.Description.ToLower().Contains(Filter.ToLower())).ToList<Credential>();
        }

        //public void OnPostFilter()
        //{
        //    var allCredentials = AppConfig.GetInstance().GetAllCredentials();
        //    if (string.IsNullOrEmpty(Filter))
        //        Credentials = allCredentials;
        //    else
        //        Credentials = allCredentials.Where(credential => credential.Description.ToLower().Contains(Filter.ToLower())).ToList<Credential>();
        //}
    }
}
