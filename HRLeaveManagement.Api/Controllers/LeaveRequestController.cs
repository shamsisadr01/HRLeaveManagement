using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> Get(bool isLoggeInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
            
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailDto>> Get(int id)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestDetailQuery(id));

            return Ok(leaveRequests);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(CreateLeaveRequestCommand leaveType)
        {
            var respone = await _mediator.Send(leaveType);
            return CreatedAtAction(nameof(Get), new { id = respone });
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(UpdateLeaveRequestCommandHandler leaveType)
        {
            await _mediator.Send(leaveType);
            return NoContent();
        }

        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(CancelLeaveRequestCommand leaveType)
        {
            await _mediator.Send(leaveType);
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(ChangeLeaveRequestApprovalCommand leaveType)
        {
            await _mediator.Send(leaveType);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
