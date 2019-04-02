using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Tasks.ViewModels;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Querys.GetUser;
using Application.Users.Querys.ViewModels;
using Application.Users.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkoleTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : BaseController
    {

        /// <summary>
        /// Retrive a list of Tasks
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasks()
        {
            var s = await Mediator.Send(new GetTasksQuery());
            return Ok(s);
        }


        /// <summary>
        /// Retrive a Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A user object</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TaskViewModel>> GetTask(int id)
        {
            var s = await Mediator.Send(new GetTaskQuery(id));
            return Ok(s);
        }

        /// <summary>
        /// Create a new Task
        /// </summary>
        /// <param name="command">User object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (await Mediator.Send(command))
            {
                return Ok();
            }

            return NotFound();

        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            if (await Mediator.Send(command))
            {
                return Ok();
            }
            return NotFound();
        }
    }

}
