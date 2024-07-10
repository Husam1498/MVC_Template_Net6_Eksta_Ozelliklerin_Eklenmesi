using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Entites;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Models;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Controllers
{
    
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _ımapper;



        public UserController(DatabaseContext dbContext, IConfiguration configuration, IMapper ımapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _ımapper = ımapper;
        }

        public IActionResult Index()
        {
         /*   //Aşagıdaki 3 kod user tablosundaki verilerin bir kaç özelliğini UserModel classına aktarmasını yapıyor
            //Böyle karmaşık bir kod yazmak yerine Automapper ile daha kolay bir şekilde yapabiliriz.
            List<User> users = _dbContext.Users.ToList();
            List<UserModel> usersModel = new List<UserModel>();
            _dbContext.Users.Select(x => new UserModel { Id = x.Id,Fullname=x.Fullname,UserName=x.UserName,CreatedAt=x.CreatedAt,Locked=x.Locked,Role=x.Role }).ToList();
         */

            List<User> users = _dbContext.Users.ToList();
            List<UserModel> userModels = users.Select(x => _ımapper.Map<UserModel>(x)).ToList();


            return View(userModels);
        }
    }
}
