using Microsoft.AspNetCore.Identity;

namespace KafeinShop.Core.Model
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
