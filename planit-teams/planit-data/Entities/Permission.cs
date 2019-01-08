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
        [Required]
        public bool IsAdmin { get; set; }
        //[Required]
        public virtual Board Board { get; set; }
        //[Required]
        public virtual User User { get; set; }
    }
}
