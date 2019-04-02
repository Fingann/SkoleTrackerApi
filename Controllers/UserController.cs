﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Querys.GetUser;
using Application.Users.Querys.ViewModels;
using Application.Users.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkoleTrackerApi.Controllers
{
    
    
    public class UsersController : BaseController
    {
        
        /// <summary>
        /// Retrieve a list of users
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var s = await Mediator.Send(new GetUsersQuery());
            return Ok(s);
        }
        
        
        /// <summary>
        /// Retrieve a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A user object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser([FromQuery]int id)
        {
            var s = await Mediator.Send(new GetUserQuery(id));
            return Ok(s);
        }
        
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="command">User object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create( [FromBody]CreateUserCommand command)
        {
            if (await Mediator.Send(command))
            {
                return Ok();
            }

            return NotFound();

        }
        
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update( [FromBody]UpdateUserCommand command)
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