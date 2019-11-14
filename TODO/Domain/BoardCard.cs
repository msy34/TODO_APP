using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using TODO.Domain;
using TODO.Helpers;

namespace TODO.Domain
{
    public class BoardCard :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }

    }
}
