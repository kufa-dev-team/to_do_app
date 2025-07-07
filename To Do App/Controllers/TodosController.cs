using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using To_Do_App.DTOs;
using To_Do_App.Models;
using Npgsql;

namespace To_Do_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly string _connectionString;

        public TodosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // POST /api/todos - Create new Todo
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto todoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todoToCreate = new
            {
                todoDto.UserId,
                todoDto.CategoryId,
                todoDto.Title,
                todoDto.Description,
                Completed = false, // Always false on creation
                todoDto.Priority,
                todoDto.DueDate
            };

            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
                INSERT INTO Todos (user_id, category_id, title, description, completed, priority, due_date)
                VALUES (@UserId, @CategoryId, @Title, @Description, @Completed, @Priority, @DueDate)
                RETURNING *;";
            var newTodo = await connection.QuerySingleAsync<Todo>(sql, todoToCreate);
            return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.todo_id }, newTodo);
        }

        // GET /api/todos - Get all Todos with Filter by completed or priority or categoryId
        [HttpGet]
        public async Task<IActionResult> GetTodos(
            [FromQuery] int userId,
            [FromQuery] bool? completed,
            [FromQuery] int? priority,
            [FromQuery] int? categoryId)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate("SELECT * FROM Todos /**where**/ ORDER BY created_at DESC");

            sqlBuilder.Where("user_id = @UserId", new { UserId = userId });

            if (completed.HasValue)
                sqlBuilder.Where("completed = @Completed", new { Completed = completed.Value });

            if (priority.HasValue)
                sqlBuilder.Where("priority = @Priority", new { Priority = priority.Value });

            if (categoryId.HasValue)
                sqlBuilder.Where("category_id = @CategoryId", new { CategoryId = categoryId.Value });

            var todos = await connection.QueryAsync<Todo>(template.RawSql, template.Parameters);
            return Ok(todos);
        }

        // GET /api/todos/{id} - Get Todo by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Todos WHERE todo_id = @Id";
            var todo = await connection.QuerySingleOrDefaultAsync<Todo>(sql, new { Id = id });
            return todo == null ? NotFound() : Ok(todo);
        }

        // PUT /api/todos/{id} - Updata Todo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto todoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await using var connection = new NpgsqlConnection(_connectionString);
            // Get Todo to chech does it found or not
            var existingTodo = await connection.QuerySingleOrDefaultAsync<Todo>("SELECT * FROM Todos WHERE todo_id = @Id", new { Id = id });
            if (existingTodo == null) return NotFound();

            // Else update Todo
            // The property names now match the database columns
            existingTodo.category_id = todoDto.CategoryId;
            existingTodo.title = todoDto.Title;
            existingTodo.description = todoDto.Description;
            existingTodo.completed = todoDto.Completed;
            existingTodo.priority = todoDto.Priority;
            existingTodo.due_date = todoDto.DueDate;
            existingTodo.updated_at = DateTime.UtcNow;
            if (todoDto.Completed && existingTodo.completion_timestamp == null)
            {
                existingTodo.completion_timestamp = DateTime.UtcNow;
            }
            else if (!todoDto.Completed)
            {
                existingTodo.completion_timestamp = null;
            }

            // The parameter names in the SQL now match the model property names for clarity
            var sql = @"
                UPDATE Todos
                SET category_id = @category_id, title = @title, description = @description,
                    completed = @completed, priority = @priority, due_date = @due_date,
                    updated_at = @updated_at, completion_timestamp = @completion_timestamp
                WHERE todo_id = @todo_id";

            // Dapper will map the properties of the existingTodo object to the parameters
            await connection.ExecuteAsync(sql, existingTodo);
            return NoContent();
        }

        // DELETE /api/todos/{id} - Delete Todo 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM Todos WHERE todo_id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows == 0 ? NotFound() : NoContent();
        }
    }
}