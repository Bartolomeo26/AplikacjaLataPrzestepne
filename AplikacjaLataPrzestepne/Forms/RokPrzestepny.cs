using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplikacjaLataPrzestepne.Forms
{
    
    public class RokPrzestepny
    {
        [Key]
        [DisplayName("Rok")]
        [Required, Range(1899, 2022, ErrorMessage = "Oczekiwana wartość {0} z zakresu {1} i {2}.")]
        
        public int Rok { get; set; }
        [DisplayName("Imie użytkownika")]
        
        public string? Imie { get; set; }

        
        public string? czy_przestepny { get; set; }
    }
}
