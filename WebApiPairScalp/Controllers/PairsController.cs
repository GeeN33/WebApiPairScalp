using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiPairScalp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPairScalp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairsController : ControllerBase
    {

        // GET: api/<PairsController>
        [HttpGet]
        public async Task <IActionResult> Get(IDbConnection db)
        {
            var sql = "SELECT * FROM pairs"; // Убедитесь, что название таблицы и поля соответствуют вашей БД

            try
            {
                var result = await db.QueryAsync(sql); // Запрос к БД с использованием Dapper
              
                if (result.Any())
                {
                    return Ok(result); // Возвращаем результат, если записи найдены
                }
                return NotFound("Записи не найдены."); // Возвращаем NotFound, если записи не найдены
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}"); // Возвращаем ошибку сервера в случае исключения
            }
        }

        // POST api/<PairsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

   
        // DELETE api/<PairsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
