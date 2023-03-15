using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPIProject.Data;
using MyAPIProject.Models;

namespace MyAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly CarsAPIDbContext dbContext;
        public CarsController(CarsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return Ok(await dbContext.cars.ToListAsync());

        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCarById([FromRoute]Guid id)
        {
            var car = await dbContext.cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarRequest addCarRequest)
        {
            var car = new Cars()
            {
                Id = Guid.NewGuid(),
                carModel = addCarRequest.carModel,
                carName = addCarRequest.carName,
                carType = addCarRequest.carType,
            };

            await dbContext.cars.AddAsync(car);
            await dbContext.SaveChangesAsync();
            return Ok(car);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCar([FromRoute] Guid id, UpdateCarRequest updateCarRequest)
        {
            var car = await dbContext.cars.FindAsync(id);

            if (car != null)
            {
                car.carName = updateCarRequest.carName;
                car.carType = updateCarRequest.carType;
                car.carModel = updateCarRequest.carModel;


                await dbContext.SaveChangesAsync();
                return Ok(car);


            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCar([FromRoute] Guid id)
        {
            var car = await dbContext.cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
            dbContext.Remove(car);
            await dbContext.SaveChangesAsync();
            return Ok(car);

        }

    }
}
