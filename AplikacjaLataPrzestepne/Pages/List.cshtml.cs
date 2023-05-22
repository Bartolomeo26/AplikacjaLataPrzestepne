using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using ListLeapYears;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AplikacjaLataPrzestepne.Pages
{
    public class ListModel : PageModel
    {
        public IEnumerable<RokPrzestepny> LeapYearList;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly Wyszukiwania _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ListModel(ILogger<IndexModel> logger, Wyszukiwania context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            Configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public RokPrzestepny object_toSearch { get; set; } = new RokPrzestepny();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<RokPrzestepny> LeapYears_ { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                object_toSearch.user_id = user_id.Value;
                
            }
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_asc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            IQueryable<RokPrzestepny> studentsIQ = from s in _context.LeapData
                                                   select s;
            var pageSize = Configuration.GetValue("PageSize", 20);
            LeapYears_ = await PaginatedList<RokPrzestepny>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public IActionResult OnPost(int id_User)
        {    
            object_toSearch = _context.LeapData.Find(id_User);
            
            object_toSearch.Id = id_User;
            _context.LeapData.Remove(object_toSearch);
            _context.SaveChanges();
            
            return RedirectToAction("Async");
        }
    }
}