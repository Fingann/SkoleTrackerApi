using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Sessions.Commands.CreateSession;
using Application.Sessions.Commands.DeleteSession;
using Application.Sessions.Commands.UpdateSession;
using Application.Sessions.Querys.GetSession;
using Application.Sessions.Querys.GetSessions;
using Application.Sessions.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkoleTrackerApi.Controllers
{
    public class SessionsController : BaseController
    {
        /// <summary>
        /// Retrieve a list of Sessions
        /// </summary>
        /// <returns>All Sessions</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SessionViewModel>>> GetProjects()
        {
            var s = await Mediator.Send(new GetSessionsQuery());
            return Ok(s);
        }


        /// <summary>
        /// Retrieve a Session
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The requested Session</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SessionViewModel>> GetProject(int id)
        {
            var s = await Mediator.Send(new GetSessionQuery(id));
            return Ok(s);
        }

        /// <summary>
        /// Create a new Session
        /// </summary>
        /// <param name="command">Session to create</param>
        /// <returns>The newly created Session</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody]CreateSessionCommand command)
        {
            var s = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = s.Id }, s);
        }

        /// <summary>
        /// Update Session
        /// </summary>
        /// <param name="command">Session beeing updated</param>
        /// <returns>The updated Session</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody]UpdateSessionCommand command)
        {
            if (await Mediator.Send(command))
            {
                return NoContent();
            }
            return NotFound();

        }

        /// <summary>
        /// Delete a Session
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted Session</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await Mediator.Send(new DeleteSessionCommand(id)))
            {
                return NoContent();
            }
            return NotFound();

        }
    }
}
