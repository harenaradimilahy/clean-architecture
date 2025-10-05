using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Data;
using Application.Users.Register;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using SharedKernel;
using Xunit;

namespace ApplicationTests.Users;

public class RegisterUserTests
{

    [Fact]
    public async Task AddUser_Should_Add_To_DbSet_And_SaveChanges()
    {

        var mockSet = new Mock<DbSet<User>>();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            PasswordHash = "hashed_pwd"
        };


        mockContext.Object.Users.Add(user);
        await mockContext.Object.SaveChangesAsync(new CancellationToken());


        mockSet.Verify(m => m.Add(It.Is<User>(u =>
            u.Email == "test@example.com" &&
            u.FirstName == "John" &&
            u.LastName == "Doe")), Times.Once);

        mockContext.Verify(c => c.SaveChangesAsync(new CancellationToken()), Times.Once);
    }

    
    [Fact]
    public async Task SaveChanges_Should_Throw_Exception_When_Database_Error()
    {

        var mockSet = new Mock<DbSet<User>>();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        mockContext
            .Setup(c => c.SaveChangesAsync(new CancellationToken()))
            .ThrowsAsync(new Exception("Database failure"));

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "fail@example.com",
            FirstName = "Fail",
            LastName = "User",
            PasswordHash = "hashed_pwd"
        };


        mockContext.Object.Users.Add(user);
        await Assert.ThrowsAsync<Exception>(() =>
            mockContext.Object.SaveChangesAsync(new CancellationToken()));
    }

    [Fact]
    public async Task AddUser_Should_Add_Correct_Id()
    {

        var mockSet = new Mock<DbSet<User>>();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var newId = Guid.NewGuid();
        var user = new User
        {
            Id = newId,
            Email = "john@example.com",
            FirstName = "John",
            LastName = "Smith",
            PasswordHash = "hash"
        };


        mockContext.Object.Users.Add(user);
        await mockContext.Object.SaveChangesAsync(new CancellationToken());


        mockSet.Verify(m => m.Add(It.Is<User>(u => u.Id == newId)), Times.Once);
        mockContext.Verify(c => c.SaveChangesAsync(new CancellationToken()), Times.Once);
    }

    [Fact]
    public async Task AddUser_Should_Add_Multiple_Users()
    {

        var mockSet = new Mock<DbSet<User>>();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var user1 = new User { Id = Guid.NewGuid(), Email = "user1@test.com" };
        var user2 = new User { Id = Guid.NewGuid(), Email = "user2@test.com" };


        mockContext.Object.Users.Add(user1);
        mockContext.Object.Users.Add(user2);
        await mockContext.Object.SaveChangesAsync(new CancellationToken());


        mockSet.Verify(m => m.Add(It.Is<User>(u => u.Email == "user1@test.com")), Times.Once);
        mockSet.Verify(m => m.Add(It.Is<User>(u => u.Email == "user2@test.com")), Times.Once);
        mockContext.Verify(c => c.SaveChangesAsync(new CancellationToken()), Times.Once);
    }
}
