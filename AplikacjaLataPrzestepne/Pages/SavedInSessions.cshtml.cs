using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using AplikacjaLataPrzestepne.Forms;

namespace AplikacjaLataPrzestepne.Pages
{
    public class SavedInSessionModel : PageModel
    {

        public Session FizzBuzzSession { get; set; } = new Session();

        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            return Page();
        }
    }
}

