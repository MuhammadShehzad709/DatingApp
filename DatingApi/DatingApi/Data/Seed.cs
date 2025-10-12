using DatingApi.Data.Entities;
using DatingApi.Dtos.SeedingDto;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApi.Data
{
    public class Seed
    {
        public static async Task SeedUsers(AppDbContext context)
        {
            if (await context.Users.AnyAsync()) return;
            var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);
            if (members == null)
            {
                Console.WriteLine("NO members in seed Data");
                return;
            }
            foreach (var member in members)
            {
                using var hmac = new HMACSHA512();
                var user = new AppUser
                {
                    Id = member.Id,
                    Email = member.Email,
                    DisplayName = member.DisplayName,
                    ImageUrl = member.ImageUrl,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$sw0rd")),
                    PasswordSalt = hmac.Key,
                    member = new Member
                    {
                        Id = member.Id,
                        DisplayName = member.DisplayName,
                        Description = member.Description,
                        DateOfBirth = member.DateOfBirth,
                        ImageUrl = member.ImageUrl,
                        Gender = member.Gender,
                        City = member.City,
                        Country = member.Country,
                        LastActive = member.LastActive,
                        CreatedAt = member.Created,

                    }
                };
                user.member.photos.Add(new Photo
                {
                    Url = member.ImageUrl!,
                    MemberID = member.Id,
                });
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
