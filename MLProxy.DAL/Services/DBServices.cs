using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MLProxy.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLProxy.DAL.Services
{
    public class DBServices
    {
        private MLProxyContext db = new();



        public List<AIModel> GetUserAllowedModels(string userId)
        {
            // Get all models that the user has access to through their roles
            var models = db.RoleModelPermissions
                .Where(rmp => rmp.Role.Users.Any(ur => ur.Id == userId))
                .Select(rmp => rmp.AIModel)
                .ToList();

            return models;
        }

        public bool IsUserAllowedToUseModel(string userId, string modelId)
        {
            // Check if the user has access to the specified model
            return db.RoleModelPermissions
                .Any(rmp => rmp.AIModelID == modelId && rmp.Role.Users.Any(ur => ur.Id == userId));
        }
    }
}
