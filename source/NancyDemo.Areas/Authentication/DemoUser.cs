using System.Collections.Generic;
using Nancy.Security;

namespace NancyDemo.Areas.Authentication
{
    public class DemoUser : IUserIdentity
    {
        public string UserName { get; private set; }
        public IEnumerable<string> Claims { get; private set; }

        public DemoUser(string userName, IEnumerable<string> claims)
        {
            UserName = userName;
            Claims = claims;
        }
    }
}