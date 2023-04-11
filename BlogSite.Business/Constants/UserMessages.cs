﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Constants
{
    public static class UserMessages
    {
        public static string UsersListed = "Users successfuly listed from DB.";
        public static string UsersListedError = "Error! Please try again.(UsersListedError)";

        public static string UserAdded = "User successfuly added to DB and CacheDB.";
        public static string UserAldreadyExistsError = "Error! User already exists.";
        public static string UserAddedError = "Error! Please try again.(UserAddedError)";

        public static string UserRemoved = "User successfuly removed from DB and CacheDB.";
        public static string UserRemovedError = "Error! User not found.(UserRemovedError)";

        public static string UserUpdated = "User updated from DB .";
        public static string UserUpdatedError = "Error! User not found.(UserUpdatedError)";

    }
}
