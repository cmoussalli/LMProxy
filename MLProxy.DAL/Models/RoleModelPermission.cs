using CMouss.IdentityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLProxy.DAL.Models
{
    public class RoleModelPermission
    {
        public string ID { get; set; }
        public string RoleID { get; set; }
        public string AIModelID { get; set; }


        public virtual Role Role { get; set; }
        public virtual AIModel AIModel { get; set; }
    }
}
