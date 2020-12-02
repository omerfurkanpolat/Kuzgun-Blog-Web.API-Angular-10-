using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sahika.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Sahika.Models
{
    public class IdentityModels
    {
        public class ApplicationUserLogin : IdentityUserLogin<int> { }
        public class ApplicationUserClaim : IdentityUserClaim<int> { }
        public class ApplicationUserRole : IdentityUserRole<int> { }

        public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int>
        {
            public string Description { get; set; }

            public ApplicationRole() : base() { }
            public ApplicationRole(string name)
                : this()
            {
                this.Name = name;
            }

            public ApplicationRole(string name, string description)
                : this(name)
            {
                this.Description = description;
            }
        }


        public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<int>
        {

            public ApplicationUser()
            {
                Posts = new List<Post>();
                FavouritePosts = new List<FavouritePost>();
                PostComments = new List<PostComment>();
                DateRegistered = DateTime.Now;
                
              
            }

            public DateTime DateRegistered { get; set; }

            public DateTime? LastActive { get; set; }
          
            public bool IsDeleted { get; set; }
            public string ImageUrl { get; set; }
            public ICollection<Post> Posts { get; set; }
            
            public ICollection<FavouritePost> FavouritePosts { get; set; }
            public ICollection<PostComment> PostComments { get; set; }
            public async Task<ClaimsIdentity>
                GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
            {
                var userIdentity = await manager
                    .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                return userIdentity;
            }

           
        }




        public class ApplicationUserStore :
   UserStore<ApplicationUser, ApplicationRole, int,
   ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUserStore<ApplicationUser, int>, IDisposable
        {
            public ApplicationUserStore()
                : this(new SahikaContext())
            {
                base.DisposeContext = true;
            }

            public ApplicationUserStore(SahikaContext context)
                : base(context)
            {
            }
        }

        public class ApplicationRoleStore
        : RoleStore<ApplicationRole, int, ApplicationUserRole>,
        IQueryableRoleStore<ApplicationRole, int>,
        IRoleStore<ApplicationRole, int>, IDisposable
        {
            public ApplicationRoleStore()
                : base(new IdentityDbContext())
            {
                base.DisposeContext = true;
            }

            public ApplicationRoleStore(SahikaContext context)
                : base(context)
            {
            }
        }

    }
}