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
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Enrollment Enrollment)
        {
            try
            {
                await _context.Enrollments.AddAsync(Enrollment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Enrollment: {ex.Message}");
                throw; // Có thể chọn throw lại nếu muốn xử lý ở tầng cao hơn
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var Enrollment = await _context.Enrollments.FindAsync(id);
                if (Enrollment != null)
                {
                    _context.Enrollments.Remove(Enrollment);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            try
            {
                return await _context.Enrollments.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (nếu có hệ thống log như Serilog, NLog, hoặc Console)
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Enrollment>(); // Trả về danh sách rỗng nếu có lỗi
            }

        }

        public async Task<Enrollment> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Enrollments.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }

        }

        public async Task UpdateAsync(Enrollment Enrollment)
        {
            try
            {
                _context.Enrollments.Update(Enrollment);
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