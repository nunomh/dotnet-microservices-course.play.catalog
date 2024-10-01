using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [ApiController] // this attribute includes features that improves API experience, such as automatic model validation and error handling (400 bad request)
    [Route("items")]  // https://localhost:5551/items - this attribute specifies the base route for all actions in this controller
    public class ItemsController : ControllerBase // ControllerBase is a base class for all controllers in ASP.NET Core (properties and methods for handling HTTP requests)
    {
        // private static readonly List<ItemDto> items = new()
        // {
        //     new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
        // };
        private readonly ItemsRepository itemsRepository = new();

        [HttpGet]
        // public IEnumerable<ItemDto> Get() // simplest way, without database connection
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync())
                        .Select(item => item.AsDto());
            return items;
        }

        // GET /items/12345
        [HttpGet("{id}")]
        // public ItemDto GetById(Guid id)  // this is the old way of doing it, this only excepts ItemDto or null as a return
        // to return a NotFound() the ActionResult<ItemDto> is needed
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            // var item = items.Where(item => item.Id == id).SingleOrDefault();
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto) // ActionResult allows to return different types
        {
            //var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            //items.Add(item);

            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        // PUT /items/12345
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto) // IActionResult - don't need a specific return type
        {
            // var existingItem = items.Where(item => item.Id == id).SingleOrDefault();

            // if (existingItem == null)
            // {
            //     return NotFound();
            // }

            // var updatedItem = existingItem with
            // {
            //     Name = updateItemDto.Name,
            //     Description = updateItemDto.Description,
            //     Price = updateItemDto.Price
            // };

            // var index = items.FindIndex(existingItem => existingItem.Id == id);
            // items[index] = updatedItem;

            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        // DELETE /items/12345
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // var index = items.FindIndex(existingItem => existingItem.Id == id);

            // if (index < 0)
            // {
            //     return NotFound();
            // }

            // items.RemoveAt(index);

            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(existingItem.Id);

            return NoContent();
        }
    }
}