using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Entites;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Helpers;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Models;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Controllers
{
    
    public class UserController : Controller
    {

        private readonly DatabaseContext _dbContext;
        private readonly IMapper _ımapper;
        private readonly IHash _IHash;

        public UserController(DatabaseContext dbContext, IMapper ımapper, IHash ıHash)
        {
            _dbContext = dbContext;
            _ımapper = ımapper;
            _IHash = ıHash;
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

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if(_dbContext.Users.Any(x => x.UserName.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username),"Username database de mevcuttur");
                    return View(model);
                }

                User user = _ımapper.Map<User>(model);
                user.Password=_IHash.DoMd5HashedString(model.Password);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            
            return View(model);            
        }

        public IActionResult Edit(Guid id)
        {
            User user=_dbContext.Users.Find(id);
            EditUserModel model=_ımapper.Map<EditUserModel>(user);
            model.Password = user.Password;
            model.RePassword = user.Password;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(x => x.UserName.ToLower() == model.Username.ToLower() && x.Id!=id) )//Benim id dışındaki username leri kontrol et
                {
                    ModelState.AddModelError(nameof(model.Username), "Username database de mevcuttur");
                    return View(model);
                }
                User user = _dbContext.Users.Find(id);

                _ımapper.Map( model, user);//modeldeki verileri alıp user daki değişkenlere atama yapar
                user.Password = _IHash.DoMd5HashedString(model.Password);             
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
               
             return RedirectToAction(nameof(Index));
        }
    }
}
