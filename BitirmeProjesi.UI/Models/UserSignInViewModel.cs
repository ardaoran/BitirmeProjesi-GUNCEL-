using System.ComponentModel.DataAnnotations;

namespace BitirmeProjesi.UI.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adını giriniz")]
        public string username { get; set; }

        [Required(ErrorMessage = "Lütfen şfire giriniz")]
        public string password { get; set; }    
    }
}
