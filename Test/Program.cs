using DataAccess.Data;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        // Thiết lập Dependency Injection
        var serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=LENOVO;Database=OnlineLearningPlatformDb;User=sa;Password=123456;TrustServerCertificate=True;"))
            .AddScoped<IUserRepository, UserRepository>()
            .AddLogging(static config => config.AddConsole()) // Logging ra console
            .BuildServiceProvider();

        // Lấy UserRepository từ DI
        var userRepository = serviceProvider.GetService<IUserRepository>();
        if (userRepository == null)
        {
            Console.WriteLine("UserRepository is not initialized.");
            return;
        }

        // Test các phương thức trong UserRepository
        await TestUserRepository(userRepository);
    }

    static async Task TestUserRepository(IUserRepository userRepository)
    {
        try
        {
            Console.WriteLine("=== Testing UserRepository ===");

            // Thêm người dùng mới
            Console.WriteLine("Adding new users...");
            await userRepository.AddAsync(new User { FullName = "Thành Luận", Email = "thanh0101luan@gmail.com", PasswordHash="abc", Role = new Role{Name="Admin", Description = "This is admin"}  });
            await userRepository.AddAsync(new User { FullName = "Sang Lê", Email = "sangltce140411@gmail.com", PasswordHash="abc", Role = new Role{Name="Teacher", Description = "Teacher role"}  });


            // Lấy tất cả người dùng
            Console.WriteLine("Fetching all users...");
            var users = await userRepository.GetAllAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Id}, {user.FullName}, {user.Email}, {user.Role.Name}");
            }

            // Lấy người dùng theo ID
            Console.WriteLine("Fetching user by ID...");
            var userById = await userRepository.GetByIdAsync(1);
            if (userById != null)
            {
                Console.WriteLine($"Found user: {userById.FullName}, {userById.Email}");
            }

            // Cập nhật người dùng
            Console.WriteLine("Updating user...");
            if (userById != null)
            {
                userById.FullName = "Đổng Thành Luận";
                await userRepository.UpdateAsync(userById);
            }

            // Kiểm tra cập nhật
            Console.WriteLine("Fetching updated user...");
            var updatedUser = await userRepository.GetByIdAsync(1);
            if (updatedUser != null)
            {
                Console.WriteLine($"Updated user: {updatedUser.FullName}, {updatedUser.Email}");
            }

            // Xóa người dùng
            Console.WriteLine("Deleting user...");
            await userRepository.DeleteAsync(2);

            // Kiểm tra xóa
            Console.WriteLine("Fetching all users after deletion...");
            users = await userRepository.GetAllAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Id}, {user.FullName}, {user.Email}");
            }

            Console.WriteLine("=== Testing Completed ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during testing: {ex.Message}");
        }
    }
}

