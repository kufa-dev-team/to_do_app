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
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString;

        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT user_id, first_name, last_name, email, created_at FROM Users ORDER BY user_id";
            var Users = await connection.QueryAsync<UserDto>(sql);
            return Ok(Users);
        }
        // POST /api/users - Create new user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Crate PasswordHash before store it
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            await using var connection = new NpgsqlConnection(_connectionString);

            
            var sql = @"
                INSERT INTO Users (first_name, last_name, email, password_hash)
                VALUES (@FirstName, @LastName, @Email, @PasswordHash)
                RETURNING user_id, first_name, last_name, email, created_at;";

            try
            {
                var newUser = await connection.QuerySingleAsync<UserDto>(sql, new
                {
                    userDto.FirstName,
                    userDto.LastName,
                    userDto.Email,
                    PasswordHash = passwordHash
                });

                return CreatedAtAction(nameof(GetUserById), new { id = newUser.user_id }, newUser);
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                return Conflict(new { message = "User with this email already exists." });
            }
        }

        // GET /api/users/{id} Get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            //Don't return password_hash ever
            
            var sql = @"
                SELECT 
                    user_id, 
                    first_name, 
                    last_name, 
                    email, 
                    created_at 
                FROM Users 
                WHERE user_id = @Id";

            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { Id = id });

            return user == null ? NotFound() : Ok(user);
        }

        // DELETE /api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM Users WHERE user_id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
}