using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Constants
{
    public static class PostMessages
    {
        public static string PostsListed = "Posts successfuly listed from CacheDB.";
        public static string PostsListedError = "Error! Please try again.";

        public static string PostAdded = "Post successfuly added to DB and CacheDB.";
        public static string PostAddedError = "Error! Please try again.";

        public static string PostRemoved = "Post successfuly removed from DB and CacheDB.";
        public static string PostRemovedError = "Error! Post not found.";

        public static string PostUpdated = "Post updated from DB .";
        public static string PostUpdatedError = "Error! Post not found.";
    }
}
