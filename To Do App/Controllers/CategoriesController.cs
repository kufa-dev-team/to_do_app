using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using To_Do_App.DTOs;
using To_Do_App.Models;
using Dapper;

namespace To_Do_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //
        private readonly string _connectionString;

        public CategoriesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // POST /api/categories - Create new Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
                INSERT INTO Categories (user_id, name, color)
                VALUES (@UserId, @Name, @Color)
                RETURNING *;";

            var newCategory = await connection.QuerySingleAsync<Category>(sql, categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.category_id }, newCategory);
        }

        // GET /api/categories - Get all categore for a spicefic user by id
        [HttpGet]
        public async Task<IActionResult> GetCategoriesForUser([FromQuery] int userId)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Categories WHERE user_id = @UserId ORDER BY name";
            var categories = await connection.QueryAsync<Category>(sql, new { UserId = userId });
            return Ok(categories);
        }

        // GET /api/categories/{id} - Get category by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Categories WHERE category_id = @Id";
            var category = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
            return category == null ? NotFound() : Ok(category);
        }

        // PUT /api/categories/{id} - Updata category
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
                UPDATE Categories
                SET name = @Name, color = @Color, updated_at = NOW()
                WHERE category_id = @Id
                RETURNING *;";

            var updatedCategory = await connection.QuerySingleOrDefaultAsync<Category>(sql, new
            {
                categoryDto.Name,
                categoryDto.Color,
                Id = id
            });

            return updatedCategory == null ? NotFound() : Ok(updatedCategory);
        }

        // DELETE /api/categories/{id} - Delete category
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM Categories WHERE category_id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
}