using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using first_net.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_net.Controlers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryControler : ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        //GET: /api/categoris=>Read categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            // Console.WriteLine($"{searchValue}");
            // if (!string.IsNullOrEmpty(searchValue))
            // {
            //     var res = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            //     return Ok(res);
            // }

            var categoryList = categories.Select(c=>new CategoryReadDto{
                CategoryId=c.CategoryId,
                Name=c.Name,
                Description=c.Description,
                CreatedAt=c.CreatedAt
            }).ToList();

            return Ok(categoryList);
        }



        //POST: /api/categoris=>Read categories
        [HttpPost]
        public IActionResult CreateCategories([FromBody] CategoryCreateDto categoryData)
        {

            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Catefory name required and not be empty");
            }
            // Console.WriteLine($"This is my bojdy: {categoryData}");
            var category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.Now,
            };
            categories.Add(category);
            return Created($"/api/categories/{category.CategoryId}", category);

        }



        //DELETE: /api/categoris=>Read categories
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategories(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == categoryId);
            Console.WriteLine(foundCategory);
            if (foundCategory is null)
            {
                return NotFound("Category with this id no not");
            }
            categories.Remove(foundCategory);
            return NoContent();

        }


        //PUT: /api/categoris=>Read categories
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategories(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
             var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == categoryId);
    Console.WriteLine(foundCategory);
    if (foundCategory is null)
    {
        return NotFound("Category with this id not found");
    }
    if (categoryData is null)
    {
        return BadRequest("CategoryData is missing!");
    }
    if (!string.IsNullOrEmpty(categoryData.Name))
    {
        if (categoryData.Name.Length >= 2)
        {

            foundCategory.Name = categoryData.Name;
        }
        else
        {
           return BadRequest("Name must be at least two charecter");
        }
    }
    if (!string.IsNullOrEmpty(categoryData.Description))
    {
        foundCategory.Description = categoryData.Description;
    }
    return Ok(foundCategory);

        }
    }
}