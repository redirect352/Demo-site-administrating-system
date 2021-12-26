using System.Collections.Generic;

namespace Task4.Models
{
    public class UsersTableViewModel
    {
        
        public IEnumerable<User> Users { get; set; }
        public IList<Status> Statuses { get; set; }
        public bool[] UserChecked { get; set; }
       
    }
}
