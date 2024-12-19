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
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Role Role)
        {
            try
            {
                await _context.Roles.AddAsync(Role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Role: {ex.Message}");
                throw; // Có thể chọn throw lại nếu muốn xử lý ở tầng cao hơn
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var Role = await _context.Roles.FindAsync(id);
                if (Role != null)
                {
                    _context.Roles.Remove(Role);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (nếu có hệ thống log như Serilog, NLog, hoặc Console)
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Role>(); // Trả về danh sách rỗng nếu có lỗi
            }

        }

        public async Task<Role> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Roles.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }

        }

        public async Task UpdateAsync(Role Role)
        {
            try
            {
                _context.Roles.Update(Role);
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