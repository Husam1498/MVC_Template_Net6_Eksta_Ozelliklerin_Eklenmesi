using AutoMapper;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Entites;
using MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Models;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User,UserModel>().ReverseMap();//User classını UserModel Classına çevirmeyi öğren,ReverseMap metoduyla da Tersini Deöğren diyoruz
            CreateMap<User, CreateUserModel>().ReverseMap();
        }
    }
}
