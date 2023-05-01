using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Constants
{
    public static class RedisMessages
    {
        public static string ItemsListed = "Items successfuly listed from CacheDB.";
        public static string ItemsListedError = "Error! please try again";

        public static string ItemAdded = "Items successfuly added to CacheDB.";
        public static string ItemAddedError = "Error! please try again.";

        public static string ItemDeleted = "Items successfuly deleted from CacheDB.";
        public static string ItemDeletedError = "Error! Item not found, please try again.";
    }
}
