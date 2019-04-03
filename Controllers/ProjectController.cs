using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Project.Commands.CreateProject;
using Application.Project.Commands.DeleteProject;
using Application.Project.Commands.UpdateProject;
using Application.Project.Querys.GetProject;
using Application.Project.Querys.GetProjects;
using Application.Project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkoleTrackerApi.Controllers
{
    
    
    public class ProjectController : BaseController
    {
        
        /// <summary>
        /// Retrieve a list of projects
        /// </summary>
        /// <returns>All projects</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectViewModel>>> GetProjects()
        {
            var s = await Mediator.Send(new GetProjectsQuery());
            return Ok(s);
        }
        
        
        /// <summary>
        /// Retrieve a Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The requested project</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectViewModel>> GetProject(int id)
        {
            var s = await Mediator.Send(new GetProjectQuery(id));
            return Ok(s);
        }
        
        /// <summary>
        /// Create a new Project
        /// </summary>
        /// <param name="command">Project to create</param>
        /// <returns>The newly created project</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody]CreateProjectCommand command)
        {
            var s = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = s.Id }, s);
        }
        
        /// <summary>
        /// Update Project
        /// </summary>
        /// <param name="command">Project beeing updated</param>
        /// <returns>The updated project</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update( [FromBody]UpdateProjectCommand command)
        {
            if (await Mediator.Send(command))
            {
                return NoContent();
            }
            return NotFound();

        }
        
       /// <summary>
       /// Delete a Project
       /// </summary>
       /// <param name="id"></param>
       /// <returns>The deleted project</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await Mediator.Send(new DeleteProjectCommand(id)))
            {
                return NoContent();
            }
            return NotFound();

        }
        

    
    }
}