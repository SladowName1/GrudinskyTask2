//using CustomIdentityApp.Models;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EFDataApp.Models
//{
//    public class ApplicationContext : IdentityDbContext<User>
//    {
//        public ApplicationContext(DbContextOptions<ApplicationContext> options)
//            : base(options)
//        {
//            Database.EnsureCreated();
//        }
//    }
//}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentityApp.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
