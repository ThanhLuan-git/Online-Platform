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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User User)
        {
            try
            {
                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding User: {ex.Message}");
                throw; // Có thể chọn throw lại nếu muốn xử lý ở tầng cao hơn
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var User = await _context.Users.FindAsync(id);
                if (User != null)
                {
                    _context.Users.Remove(User);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (nếu có hệ thống log như Serilog, NLog, hoặc Console)
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<User>(); // Trả về danh sách rỗng nếu có lỗi
            }

        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }

        }

        public async Task UpdateAsync(User User)
        {
            try
            {
                _context.Users.Update(User);
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