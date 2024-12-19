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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Review Review)
        {
            try
            {
                await _context.Reviews.AddAsync(Review);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Review: {ex.Message}");
                throw; // Có thể chọn throw lại nếu muốn xử lý ở tầng cao hơn
            }

        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var Review = await _context.Reviews.FindAsync(id);
                if (Review != null)
                {
                    _context.Reviews.Remove(Review);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            try
            {
                return await _context.Reviews.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (nếu có hệ thống log như Serilog, NLog, hoặc Console)
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Review>(); // Trả về danh sách rỗng nếu có lỗi
            }

        }

        public async Task<Review> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Reviews.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with ID {id}: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }

        }

        public async Task UpdateAsync(Review Review)
        {
            try
            {
                _context.Reviews.Update(Review);
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