using Bakery.DataAccess.Database;
using Bakery.DataAccess.Repositories;
using Bakery.DataTransferObject.DTOs.Users;
using Bakery.Model.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IGenericRepository<Users> _User;
        private IGenericRepository<Roles> _Role;
        private BakeryDbContext _dbContext;

        public UserController(ILogger<UserController> logger, IGenericRepository<Users> User,
            BakeryDbContext dbContext, IGenericRepository<Roles> Role)
        {
            _logger = logger;
            _dbContext = dbContext;
            _User = User;
            _Role = Role;
        }
        #region User
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetUser>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _User.Table.Where(s=>s.IsActive == true).ToList();
            return Ok(getEntities);
        }

        [HttpPost()]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostUser post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<Users>(post);
            await _User.InsertEntity(entity);
            return Ok();
        }

        [HttpPut()]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutUser models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<Users>(models);
            await _User.UpdateEntity(entity);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetUser), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getUser = await (from c in _User.Table
                                 join role in _Role.Table on c.RoleId equals role.RoleId 
                                 where c.UserId == id.Value
                                 select new GetUser
                                 {
                                     IsActive = c.IsActive,
                                     Deleted = c.Deleted,
                                     Name = c.Name,
                                     Cell = c.Cell,
                                     RoleId = c.RoleId,
                                     Email = c.Email,
                                     Password = c.Password,
                                     UserId = c.UserId,
                                     RoleName = role.Name
                                 }
                      ).FirstOrDefaultAsync();
            return Ok(getUser);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var entity = _User.Table.Where(s => s.UserId == id.Value).FirstOrDefault();
            entity!.Deleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            await _User.GetEntities();
            return Ok();

        }
        #endregion
    }
}
