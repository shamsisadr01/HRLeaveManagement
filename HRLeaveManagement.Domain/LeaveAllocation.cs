﻿using System.ComponentModel.DataAnnotations.Schema;
using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{

    public int NumberOfDays { get; set; }

    public int Period { get; set; }

    public int LeaveTypeId { get; set; }

    public LeaveType? LeaveType { get; set; }
}