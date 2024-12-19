using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Course course)
        {
            try
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Course: {ex.Message}");
                throw; // Có thể chọn throw lại nếu muốn xử lý ở tầng cao hơn
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course != null)
                {
                    _context.Courses.Remove(course);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            try
            {
                return await _context.Courses.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (nếu có hệ thống log như Serilog, NLog, hoặc Console)
                Console.WriteLine($"Error fetching courses: {ex.Message}");
                return new List<Course>(); // Trả về danh sách rỗng nếu có lỗi
            }

        }

        public async Task<Course> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Courses.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }

        }

        public async Task UpdateAsync(Course course)
        {
            try
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                throw;
            }

        }
    }
}