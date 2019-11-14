using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Domain;
using TODO.FRM;

namespace TODO.Service
{
    public class UserService
    {
        public void InsertUser(User user)
        {
            new RepositoryBase<User>().Add(user);
        }
    }
}
