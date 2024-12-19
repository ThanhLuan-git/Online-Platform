using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Duration { get; set; }  // Thời gian bài học (tính bằng phút)

        public Course Course { get; set; }

    }
}