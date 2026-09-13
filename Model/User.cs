using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyFlow.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public DateTime UserCreatedAt { get; set; }
    }
}