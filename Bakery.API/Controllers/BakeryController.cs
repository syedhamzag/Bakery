using AutoMapper;
using Bakery.DataAccess.Database;
using Bakery.DataAccess.Repositories;
using Bakery.DataTransferObject.DTOs.Bakery;
using Bakery.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BakeryController : ControllerBase
    {
        private IGenericRepository<Bakerys> _bakeryRepo;
        private BakeryDbContext _dbContext;

        public BakeryController(IGenericRepository<Bakerys> bakeryRepo, BakeryDbContext dbContext)
        {
            _dbContext = dbContext;
            _bakeryRepo = bakeryRepo;
        }
        #region bakeryRepo
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetBakery>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _bakeryRepo.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost()]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostBakery post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<Bakerys>(post);
            await _bakeryRepo.InsertEntity(entity);
            return Ok();
        }

        [HttpPut()]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutBakery models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Bakerys ent = new Bakerys();
            var entity = Mapper.Map<Bakerys>(models);
            await _bakeryRepo.UpdateEntity(entity);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetBakery), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getbakeryRepo = await (from c in _bakeryRepo.Table
                                 where c.BakeryId == id.Value
                                 select new GetBakery
                                 {
                                     Deleted = c.Deleted,
                                     Name = c.Name,
                                     Description = c.Description,
                                     Address = c.Address,
                                     Contact = c.Contact,
                                     City = c.City,
                                 }
                      ).FirstOrDefaultAsync();
            return Ok(getbakeryRepo);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var entity = _bakeryRepo.Table.Where(s => s.BakeryId == id!.Value).FirstOrDefault();
            entity!.Deleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            await _bakeryRepo.GetEntities();
            return Ok();

        }
        #endregion
    }
}
