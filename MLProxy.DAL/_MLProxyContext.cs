using CMouss.IdentityFramework;
using Microsoft.EntityFrameworkCore;
using MLProxy.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLProxy.DAL
{
    public class MLProxyContext:IDFDBContext
    {

        public DbSet<AIModel> AIModels { get; set; }
        public DbSet<RoleModelPermission> RoleModelPermissions { get; set; }






        public void InsertDemoData()
        {
            Role role1 = new Role() {Id = Helpers.GenerateId() , Title = "Role1" };
            this.Roles.Add(role1);

            AIModel aIModel1 = new AIModel { ID = Helpers.GenerateId(), Title = "qwen3:8b" };
            this.AIModels.Add(aIModel1);

            RoleModelPermission roleModelPermission1 = new() { ID = Helpers.GenerateId(),  RoleID = role1.Id, AIModelID = aIModel1.ID };
            this.RoleModelPermissions.Add(roleModelPermission1);

            string user1ID = IDFManager.userService.Create("user1", "P@ssw0rd", "User1", "user1@mail.com");
            IDFManager.userService.GrantRole(user1ID,role1.Id);



            this.SaveChanges();
        }
    }
}
