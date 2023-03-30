using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Constants
{
    public static class CommentMessages
    {
        public static string CommentsListed = "Comment successfuly listed from CacheDB.";
        public static string CommentsListedError = "Error! Please try again.";

        public static string CommentAdded = "Comment successfuly added to DB and CacheDB.";
        public static string CommentAddedError = "Error! Please try again.";
        public static string CommentAddedCacheError = "Error! Comment succesfuly added to DB but couldn' t added to CacheDB";

        public static string CommentRemoved = "Comment successfuly removed from DB and CacheDB.";
        public static string CommentRemovedError = "Error! Comment not found.";
        public static string CommentRemovedCacheError = "Error! Comment not found.";

        public static string CommentUpdated = "Comment updated from DB .";
        public static string CommentUpdatedError = "Error! Comment not found.";
    }
}
