using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Entites;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Models;
using NETCore.Encrypt.Extensions;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Controllers
{
    public class MemberController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _ımapper;



        public MemberController(DatabaseContext dbContext, IConfiguration configuration, IMapper ımapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _ımapper = ımapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MemberListPartial()
        {
            List<User> users = _dbContext.Users.ToList();
            List<UserModel> userModels = users.Select(x => _ımapper.Map<UserModel>(x)).ToList();


            return PartialView("_MemberListPartial", userModels);
        }

        public IActionResult AddNewUserPartial()
        {
            return PartialView("_AddNewUserPartial", new CreateUserModel());
        }

        [HttpPost]
        public IActionResult AddNewUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(x => x.UserName.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username database de mevcuttur");
                    return PartialView("_AddNewUserPartial", model);
                }

                User user = _ımapper.Map<User>(model);
                user.Password = DoMd5HashedString(model.Password);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                return PartialView("_AddNewUserPartial", new CreateUserModel { Done="user aded"});
            }
            return PartialView("_AddNewUserPartial", model);
        }

        public IActionResult EditUserPartial(Guid id)
        {
            User user = _dbContext.Users.Find(id);
            EditUserModel model = _ımapper.Map<EditUserModel>(user);
            model.Password = user.Password;
            model.RePassword = user.Password;

            return PartialView("_EditUserPartial", model);
        }
        [HttpPost]
        public IActionResult EditUser(Guid id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(x => x.UserName.ToLower() == model.Username.ToLower() && x.Id != id))//Benim id dışındaki username leri kontrol et
                {
                    ModelState.AddModelError(nameof(model.Username), "Username database de mevcuttur");
                    return PartialView("_EditUserPartial", model);
                }
                User user = _dbContext.Users.Find(id);
                _ımapper.Map(model, user);//modeldeki verileri alıp user daki değişkenlere atama yapar
                user.Password = DoMd5HashedString(model.Password);
                _dbContext.SaveChanges();

                return PartialView("_EditUserPartial", new EditUserModel { Done = "user updated" });
            }

            return PartialView("_EditUserPartial", model);


        }
        public IActionResult DeleteUser(Guid id)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            return MemberListPartial();
        }
        private string DoMd5HashedString(String s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:Md5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

    }
}
