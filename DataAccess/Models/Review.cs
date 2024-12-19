using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }  // Đánh giá (1-5)
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Course Course { get; set; }
        public User User { get; set; }

    }
}