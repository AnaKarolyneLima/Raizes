using Dapper;
using meuprimeirocrud_karol.Contracts.Repository.TipoSolo;
using meuprimeirocrud_karol.DTO.TipoSolo;
using meuprimeirocrud_karol.Entity.TipoSolo;
using meuprimeirocrud_karol.Infrastructure;

namespace meuprimeirocrud_karol.Repository.TipoSolo
{
    public class TipoSoloRepository : ITipoSoloRepository
    {
        public async Task<IEnumerable<TipoSoloEntity>> GetAll()
        {
            using var con = new Connection().GetConnection();

            string sql = @"
                SELECT Id AS Id,
                       Nome AS Nome,
                       Textura AS Textura,
                       Descricao AS Descricao
                FROM TipoSolo";

            return await con.QueryAsync<TipoSoloEntity>(sql);
        }

        public async Task<TipoSoloEntity> GetById(int id)
        {
            using var con = new Connection().GetConnection();

            string sql = @"
                SELECT Id AS Id,
                       Nome AS Nome,
                       Textura AS Textura,
                       Descricao AS Descricao
                FROM TipoSolo
                WHERE Id = @id";

            return await con.QueryFirstOrDefaultAsync<TipoSoloEntity>(sql, new { id });
        }

        public async Task<int> Insert(TipoSoloInsertDTO dto)
        {
            string sql = @"
                INSERT INTO TipoSolo (Nome, Descricao, Textura)
                VALUES (@Nome, @Descricao, @Textura);
                SELECT LAST_INSERT_ID();";

            using var con = new Connection().GetConnection();

            return await con.ExecuteScalarAsync<int>(sql, new
            {
                dto.Nome,
                dto.Descricao,
                dto.Textura
            });
        }

        public async Task Update(int id, TipoSoloUpdateDTO dto)
        {
            string sql = @"
                UPDATE TipoSolo
                SET Nome = @Nome,
                    Descricao = @Descricao,
                    Textura = @Textura
                WHERE Id = @Id";

            using var con = new Connection().GetConnection();

            await con.ExecuteAsync(sql, new
            {
                Id = id,
                dto.Nome,
                dto.Descricao,
                dto.Textura
            });
        }

        public async Task Delete(int id)
        {
            using var con = new Connection().GetConnection();

            string sql = "DELETE FROM TipoSolo WHERE Id = @id";

            await con.ExecuteAsync(sql, new { id });
        }
    }
}