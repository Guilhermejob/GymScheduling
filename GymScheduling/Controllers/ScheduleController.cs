using GymScheduling.Application.Services;
using GymScheduling.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymScheduling.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{

    private readonly SchedulingService _scheduleService;
    public ScheduleController(SchedulingService scheduleservice) => _scheduleService = scheduleservice;


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScheduleDto dto)
    {
        var result = await _scheduleService.ScheduleStudentAsync(dto.StudentId, dto.ClassSessionId);
        if (result.IsSuccess) return Ok(new { Message = "Agendamento realizado com sucesso" });
        return BadRequest(new { Error = result.ErrorMessage });
    }
}
