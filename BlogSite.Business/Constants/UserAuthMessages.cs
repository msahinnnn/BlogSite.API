using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Constants
{
    public class UserAuthMessages
    {
        public static string TokenCreated = "Token created.";

        public static string UserRegistered = "User successfuly added to DB and CacheDB.";
        public static string UserAlreadyExistsError = "Error! User already exists, you can login.";
        public static string UserNotExistsError = "Error! User not exists, please register.";
        public static string UserRegisterError = "Error! Please try again.";

        public static string UserLogined = "Login successful";
        public static string UserLoginError = "Error! Your email or password is wrong.";

    }
}
