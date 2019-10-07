using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Global;
using Almotkaml.ArtificialLimbs.Models;
using Almotkaml.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.ArtificialLimbs.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool NameExisted(string UserName);
       Domain.User GetByNameAndPassword(LoginModel _user);
       Permission GetUserPermissions(string _userName, string _password);
    }
}
