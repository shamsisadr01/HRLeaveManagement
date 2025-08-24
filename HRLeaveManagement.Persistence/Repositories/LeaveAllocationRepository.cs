using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HRDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Include(a => a.LeaveType)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(a => a.LeaveType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .Include(a => a.LeaveType)
            .Where(a => a.EmployId == userId)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetUserAllocation(int leaveTypeId, string userId)
    {
        return await _context.LeaveAllocations
            .Include(a => a.LeaveType)
            .FirstOrDefaultAsync(a => a.LeaveTypeId == leaveTypeId && a.EmployId == userId);
    }

    public async Task<bool> AllocationExists(int leaveTypeId, string userId, int period)
    {
        return await _context.LeaveAllocations
            .AnyAsync(a => a.LeaveTypeId == leaveTypeId && a.EmployId == userId && a.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }
}