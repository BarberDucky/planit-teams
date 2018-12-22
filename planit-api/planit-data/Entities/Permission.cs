using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Board Board { get; set; }

        public virtual User User { get; set; }
    }
}
