using NETCore.Encrypt.Extensions;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Helpers
{
   /// <summary>
   /// İlerde farklı hash Algoritmalrı için Kullanılabilir
   /// </summary>
    public interface IHash
    {
        string DoMd5HashedString(string s);
    }

    public class Hash:IHash
    {
        private readonly IConfiguration _configuration;

        public Hash(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string IHash.DoMd5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:Md5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }
    }
}
