using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails();

    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);

    Task<LeaveAllocation> GetUserAllocation(int leaveTypeId, string userId);

    Task<bool> AllocationExists(int leaveTypeId, string userId, int period);

    Task AddAllocations(List<LeaveAllocation> allocations);
}