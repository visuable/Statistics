using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Statistics.Entities;
using Statistics.Units;

namespace Statistics.Controllers
{
    /// <summary>
    /// Исключительно для тестирования
    /// </summary>
    [ApiController]
    [Route("test")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TestController : ControllerBase
    {
        private readonly StatisticsContext _context;

        public TestController(StatisticsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <returns>Модель идентификатора пользователя</returns>
        [HttpPost("users")]
        public async Task<IActionResult> CreateNewUser()
        {
            var user = new User.Builder()
                .HasContact("88005553535", "example@mail.ru")
                .HasIdentity()
                .Build();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new UserCreatedModel() { UserId = user.Id });
        }

        /// <summary>
        /// Достает идентификаторы всех пользователей
        /// </summary>
        /// <returns>Модель идентификаторов пользователей</returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(new UsersModel()
            {
                Ids = await _context.Users.AsNoTracking().Select(user => user.Id).ToListAsync()
            });
        }

        public class UserCreatedModel
        {
            public Guid UserId { get; set; }
        }

        public class UsersModel
        {
            public ICollection<Guid> Ids { get; set; }
        }
    }
}
