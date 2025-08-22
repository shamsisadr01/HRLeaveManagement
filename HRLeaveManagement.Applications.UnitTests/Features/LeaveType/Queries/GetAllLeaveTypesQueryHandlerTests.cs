using System.Collections;
using AutoMapper;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.MappingProfiles;
using HRLeaveManagement.Applications.UnitTests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace HRLeaveManagement.Applications.UnitTests.Features.LeaveType.Queries;

public class GetAllLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllLeaveTypesQueryHandler>> _logger;

    public GetAllLeaveTypesQueryHandlerTests()
    {
        _leaveTypeRepositoryMock = MockLeaveTypeRepository.GetLeaveTypesMockLeaveTypeRepository();

        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
 
        });

        var mapperConfig = new MapperConfiguration(
            cfg => cfg.AddProfile<LeaveTypeProfile>(), loggerFactory
        );

        IMapper mapper = mapperConfig.CreateMapper();

        _mapper = mapperConfig.CreateMapper();

        _logger = new Mock<IAppLogger<GetAllLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task Handle_ShouldReturnAllLeaveTypes()
    {
        // Arrange
        var handler = new GetAllLeaveTypesQueryHandler(_leaveTypeRepositoryMock.Object, _mapper, _logger.Object);
        var query = new GetAllLeaveTypesQuery();
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
       // Assert.NotNull(result);
       // Assert.Equal(2, result.Count());
       // Assert.Contains(result, lt => lt.Name == "Annual Leave");
       // Assert.Contains(result, lt => lt.Name == "Sick Leave");
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count().ShouldBe(2);
    }
}