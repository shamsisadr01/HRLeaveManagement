using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        // q: یک سری داغده فیک تولید کن با hasData 
        builder.HasData(new ApplicationUser
        {
            Id = "2",
            Email = "shamsisadr01@gmail.com",
            FirstName = "aksd",
            LastName = "shdfams",
            UserName = "shamsisadr01@gmail.com",
            PasswordHash = "123456",//hasher.HashPassword(null,"123456"),
            EmailConfirmed = true,
            SecurityStamp = "123456",
            ConcurrencyStamp = "123456",
        }, new ApplicationUser
        {
            Id = "1",
            Email = "shamsisadr01@hmail.com",
            FirstName = "ak",
            LastName = "shams",
            UserName = "shamsisadr01@hmail.com",
            PasswordHash ="123456", //hasher.HashPassword(null, "12345"),
            EmailConfirmed = true,
            SecurityStamp = "123456",
            ConcurrencyStamp = "123456",
        });
    }
}