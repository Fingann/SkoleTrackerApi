using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.UpdateTask;
using Application.Tasks.Querys.GetTask;
using Application.Tasks.Querys.GetTasks;
using Application.Tasks.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkoleTrackerApi.Controllers
{
    
    public class TasksController : BaseController
    {

        /// <summary>
        /// Retrive a list of Tasks
        /// </summary>
        /// <returns>A list of Tasks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasks()
        {
            var s = await Mediator.Send(new GetTasksQuery());
            return Ok(s);
        }


        /// <summary>
        /// Retrive a Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Task object</returns>
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
        /// <param name="command">Task object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            var s = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = s.Id }, s);
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]  
        public async Task<IActionResult> Update([FromBody] UpdateTaskCommand command)
        {
            if (await Mediator.Send(command))
            {
                return NoContent();
            }
            return NotFound();
        }
    }

}
