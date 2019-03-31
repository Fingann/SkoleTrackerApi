﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Querys.GetUser;
using Application.Users.Querys.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkoleTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var s = await Mediator.Send(new GetUsersQuery());
            return Ok(s);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUser(int id)
        {
            var s = await Mediator.Send(new GetUserQuery(id));
            return Ok(s);
        }
        // PUT api/Users/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post( [FromBody]CreateUserCommand command)
        {
            if (await Mediator.Send(command))
            {
                return Ok();
            }

            return NotFound();

        }
        
//    // PUT api/Users/5
//    [HttpPut("{id}")]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesDefaultResponseType]
//    public async Task<IActionResult> Update(string id, [FromBody]ChangePasswordCommand command)
//    {
//    if (await Mediator.Send(command))
//    {
//    return Ok();
//    }
//    
//    return NotFound();
//    
//    }

    // PUT api/Users/5
//    [HttpPost("{id}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesDefaultResponseType]
//    public async Task<IActionResult> Post(string id, [FromBody]CreateUserLoginCommand command)
//    {
//    if (await Mediator.Send(command))
//    {
//    return Ok();
//    }
//    
//    return NotFound();
//    
//    }
    
    }
}