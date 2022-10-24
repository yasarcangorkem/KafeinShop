using AutoMapper;
using KafeinShop.Core.Model;
using KafeinShop.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KafeinShop.API.Controllers
{
    
    public class UsersController : CustomBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public UsersController(
            UserManager<User> userManager, 
            IMapper mapper, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        //1. Register ol 
        //2. Login ol
        //3. Üyelik sil
        //4. Üyelik güncelle

        //[HttpPost]
        //public async Task<IActionResult> Register()
        //{
        //    return await  
        //}


    }
}
