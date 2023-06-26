using Moq;
using ProductCrud.Application.Contracts.Identity;
using ProductCrud.Application.Contracts.Persistence;
using ProductCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.UnitTests.Mocks
{
    public class MockUserService
    {
        public static Mock<IUserService> GetUserService()
        {
            var mockService = new Mock<IUserService>();

            mockService.Setup(u => u.GetCurrentUserName()).ReturnsAsync("test");

            return mockService;
        }
    }
}
