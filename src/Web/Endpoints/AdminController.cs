using Application.AdminMediatoR.Commands;
using Application.AdminMediatoR.Queries;
using Application.DTOs.Admin;
using Ardalis.GuardClauses;
using Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-members")]
        public async Task<IResult> GetMembers()
        {
            var members = await _mediator.Send(new GetAllUsers());

            return TypedResults.Ok(members);
        }

        [HttpGet("get-member/{id}")]
        public async Task<IResult> GetMember(string id)
        {
            var member = await _mediator.Send(new GetUserById { Id = id });

            if (member == null)
            {
                return Results.NotFound();
            }

            return TypedResults.Ok(member);
        }

        [HttpPut("approve-advertisement")]
        public async Task<IResult> UpdateCar(int carId, bool isApproved)
        {
            var success = await _mediator.Send(new ApproveAdvertisement { Id = carId, IsApproved = isApproved });

            if (!success)
            {
                return Results.NotFound();
            }

            return Results.Ok(new JsonResult(new { title = "Оголошення підтвердженно успішно", message = "Оголошення було підтвердженно успішно", id = carId }));
        }

        [HttpPost("add-edit-member")]
        public async Task<IActionResult> AddEditMember(MemberAddEditDto model)
        {
            try
            {
                var command = new AddEditUser { model = model };
                var result = await _mediator.Send(command);

                if (string.IsNullOrEmpty(result.model.Id))
                {
                    return Ok(new JsonResult(new { title = "Користувач доданий", message = $"{model.UserName} створений" }));
                }
                else
                {
                    return Ok(new JsonResult(new { title = "Користувач відредагований", message = $"{model.UserName} оновлений" }));
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("delete-member/{id}")]
        public async Task<IResult> DeleteMember(string id)
        {
            try
            {
                await _mediator.Send(new DeleteUser { Id = id });
            }
            catch (NotFound ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.BadRequest("Internal Server Error");
            }
            return Results.NoContent();
        }

        [HttpGet("get-application-roles")]
        public async Task<IResult> GetApplicationRoles()
        {
            var query = await _mediator.Send(new GetApplicationRoles());
            return TypedResults.Ok(query);
        }
    }
}
