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
    public class LessonRepository : ILessonRepository
    {

        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Lesson lesson)
        {
            try
            {
                await _context.Lessons.AddAsync(lesson);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding lesson: {ex.Message}");
                throw; // Có thể chọn throw lại nếu muốn xử lý ở tầng cao hơn
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(id);
                if (lesson != null)
                {
                    _context.Lessons.Remove(lesson);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            try
            {
                return await _context.Lessons.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (nếu có hệ thống log như Serilog, NLog, hoặc Console)
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Lesson>(); // Trả về danh sách rỗng nếu có lỗi
            }

        }

        public async Task<Lesson> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Lessons.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }

        }

        public async Task UpdateAsync(Lesson lesson)
        {
            try
            {
                _context.Lessons.Update(lesson);
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