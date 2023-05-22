using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace AplikacjaLataPrzestepne.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public RokPrzestepny FizzBuzz
        {
            get; set;
        } = new RokPrzestepny();

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Wyszukiwania _context;

        public IndexModel(ILogger<IndexModel> logger, Wyszukiwania context, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
        }


        
        public void OnGet()
        {

            
            
        }

        public IActionResult OnPost()
        {

            FizzBuzz.Data = DateTime.Now;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            { var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                FizzBuzz.user_id = user_id.Value;
                FizzBuzz.login = _contextAccessor.HttpContext.User.Identity.Name;
            }


            string rok_przestepny;


            if ((FizzBuzz.Rok % 4 == 0 && FizzBuzz.Rok % 100 != 0) || FizzBuzz.Rok % 400 == 0)
            {
                rok_przestepny = "To był rok przestępny."; FizzBuzz.czy_przestepny = "rok przestępny";
            }
            else
            {
                rok_przestepny = "To nie był rok przestępny."; FizzBuzz.czy_przestepny = "rok nieprzestępny";
            }

            string result = FizzBuzz.Imie + " urodził się w " + FizzBuzz.Rok + " roku. " + rok_przestepny;
            string result1 = FizzBuzz.Rok + " rok. " + rok_przestepny;
            if (!String.IsNullOrEmpty(FizzBuzz.Imie) && !String.IsNullOrEmpty(FizzBuzz.Rok.ToString()))
            {
                ViewData["message"] = result;
                _context.LeapData.Add(FizzBuzz);
                _context.SaveChanges();

            }
            else if (!String.IsNullOrEmpty(FizzBuzz.Rok.ToString()))
            {
                ViewData["message"] = result1;
                _context.LeapData.Add(FizzBuzz);
                _context.SaveChanges();
            }
            

            return Page();


        }
    }
}