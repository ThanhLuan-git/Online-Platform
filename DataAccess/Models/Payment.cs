using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }

    }
}