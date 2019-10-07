using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global
{
    public class UserInfo
    {
        public static User _user;
        private static string _password;
        private static bool _isLoggedIn;

        public static int ID { get; private set; }
        public static string UserName { get; private set; }
        public static string UserTitle { get; private set; }
        public static int UserGroup { get; private set; }
        public static Permission Permission { get; private set; }

        public static void SetPermission()
        {
            Permission = new Permission();
        }

        public static bool IsLoggedIn
        {
            get { return _isLoggedIn; }
        }

        private static void Clear()
        {
            // Emad : هذه العمليات الأولى عند أول استدعاء لهذا الكلاس
            UserName = null;
            _password = null;
            _isLoggedIn = false;
        }


        //public static bool Login(User user, string userName, string password)
        //{
        //    Clear();
        //    UserName = userName;
        //    _password = password;
        //    _user = user;
        //    _TryLogin();
        //    return _isLoggedIn;
        //}
        
    }
}