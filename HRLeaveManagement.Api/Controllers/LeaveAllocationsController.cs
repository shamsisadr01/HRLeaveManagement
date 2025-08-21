using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocationDetails;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HRLeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LeaveAllocationDto>>> GetLeaveAllocations(
            bool isLoggerInUnser = false)
        {
            var query = new GetAllLeaveAllocationQuery();
            var leaveAllocations = await _mediator.Send(query);
            return Ok(leaveAllocations);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> GetLeaveAllocation(int id)
        {
            var query = new GetAllLeaveAllocationDetailQuery(id);
            var leaveAllocation = await _mediator.Send(query);
            return Ok(leaveAllocation);

        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveTypeDetailDto>> Post(CreateLeaveAllocationCommand leaveAllocation)
        {
            var respone = await _mediator.Send(leaveAllocation);
            return CreatedAtAction(nameof(GetLeaveAllocation), new { id = respone });
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
        {
            await _mediator.Send(leaveAllocation);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
