using Bakery.DataAccess.Database;
using Bakery.DataAccess.Repositories;
using Bakery.DataTransferObject.DTOs.Role;
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
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private IGenericRepository<Roles> _Role;
        private BakeryDbContext _dbContext;

        public RoleController(ILogger<RoleController> logger, IGenericRepository<Roles> Role,
            BakeryDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _Role = Role;
        }
        #region Role
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetRole>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _Role.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost()]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostRole post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<Roles>(post);
            await _Role.InsertEntity(entity);
            return Ok();
        }

        [HttpPut()]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutRole models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<Roles>(models);
            await _Role.UpdateEntity(entity);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetRole), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getRole = await (from c in _Role.Table
                                 join role in _Role.Table on c.RoleId equals role.RoleId
                                 where c.RoleId == id.Value
                                 select new GetRole
                                 {
                                     Name = c.Name,
                                     Description = c.Description
                                 }
                      ).FirstOrDefaultAsync();
            return Ok(getRole);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var entity = _Role.Table.Where(s => s.RoleId == id.Value).FirstOrDefault();
            entity!.Deleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            await _Role.GetEntities();
            return Ok();

        }
        #endregion
    }
}
