using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiPairScalp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPairScalp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PairsController : ControllerBase
    {
        //https://api.chillacoin.ru/pairs

        [HttpGet]
        public async Task <IActionResult> Get(IDbConnection db)
        {
            var sql = "SELECT * FROM pairs ORDER BY pp ASC"; // Убедитесь, что название таблицы и поля соответствуют вашей БД

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

        
        [HttpPost]
        public async Task<IActionResult> Create(IDbConnection db, [FromBody] Pairs pair)
        {
            var sql1 = $"SELECT * FROM pairs WHERE symbol1 = @symbol1 AND symbol2 = @symbol2 AND per = @per";

            var result1 = await db.QueryAsync<Pairs>(sql1, pair);

            if (result1.Any())
            {
                var sql2 = """
                UPDATE pairs
                SET kf1 = @kf1, kf2 = @kf2, pp = @pp, pv = @pv, updata = @updata
                WHERE symbol1 = @symbol1 AND symbol2 = @symbol2 AND per = @per
                """;
               
                var result2 = await db.ExecuteAsync(sql2, pair);
               
                if (result2 > 0)
                {
                    return Ok(new { Message = "pair updated successfully" });
                }
                return NotFound(new { Message = "pair not found" });

            }
            else
            {
                var sql3 = """
                INSERT INTO pairs (symbol1, symbol2, per, kf1, kf2, pp, pv, updata)
                VALUES (@symbol1, @symbol2, @per, @kf1, @kf2, @pp, @pv, @updata)
                """;

                var result3 = await db.ExecuteAsync(sql3, pair);

                if (result3 > 0)
                {
                    return Ok(new { Message = "pair create successfully" });
                }
                return NotFound(new { Message = "pair not found" });
            }

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(IDbConnection db, [FromBody] Pairs pair)
        {
            var sql1 = $"SELECT * FROM pairs WHERE symbol1 = @symbol1 AND symbol2 = @symbol2 AND per = @per";

            var result1 = await db.QueryAsync<Pairs>(sql1, pair);

            if (result1.Any())
            {
                var sql2 = $"delete from pairs WHERE symbol1 = @symbol1 AND symbol2 = @symbol2 AND per = @per";

                var result2 = await db.ExecuteAsync(sql2, pair);

                if (result2 > 0)
                {
                    return Ok(new { Message = "pair delete successfully" });
                }
                return NotFound(new { Message = "pair not found" });

            }

            return NotFound(new { Message = "pair not found" });
        }
    }
}
