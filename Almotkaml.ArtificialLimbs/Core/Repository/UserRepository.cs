using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;



using System.Web.Script.Serialization;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class UserRepository : Repository<Domain.User>, IUserRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;
        private readonly UnitOfWork _unitOfWork;

        public UserRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
            _unitOfWork = new UnitOfWork(_Context);
        }

        public bool NameExisted(string _userName)
        {
            return _context.Users.Any(d => d.UserName.ToString() == _userName);
        }

        public Domain.User GetByNameAndPassword(LoginModel _user)
        {
            string _pas = Hash.MD5(_user.Password);
            var user = _Context.Users
                        .Include(u => u.UserGroup)
                        .Where(u => u.UserName == _user.UserName && u.Password == _pas)
                        .FirstOrDefault();
            if (user != null)
            {
                HttpContext.Current.Session["UserID"] = user.UserId;
                HttpContext.Current.Session["UserTitle"] = user.Title;
            }
            return user;
        }

        public Permission GetUserPermissions(string _userName, string _password)
        {
            string _pas = Hash.MD5(_password);
            var user = _Context.Users
                        .Include(u => u.UserGroup)
                        .Where(u => u.UserName == _userName && u.Password == _pas)
                        .FirstOrDefault();

            JavaScriptSerializer java = new JavaScriptSerializer();
            return java.Deserialize<Permission>(user.UserGroup._Permissions);
            // return user;
        }

    }
}