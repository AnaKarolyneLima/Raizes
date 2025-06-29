using Dapper;
using meuprimeirocrud_karol.Contracts.Repository.MovimentoArmazenamento;
using meuprimeirocrud_karol.DTO.MovimentoArmazenamento;
using meuprimeirocrud_karol.Entity.MovimentoArmazenamento;
using meuprimeirocrud_karol.Infrastructure;

namespace meuprimeirocrud_karol.Repository.MovimentoArmazenamento
{
    public class MovimentoArmazenamentoRepository : IMovimentoArmazenamentoRepository
    {
        public async Task<IEnumerable<MovimentoArmazenamentoEntity>> GetAll()
        {
            using var con = new Connection().GetConnection();

            string sql = @"
                SELECT Id, ArmazenamentoId, TipoMovimentacao, Quantidade, DataMovimentacao, Observacoes
                FROM MovimentoArmazenamento
            ";

            return await con.QueryAsync<MovimentoArmazenamentoEntity>(sql);
        }

        public async Task<MovimentoArmazenamentoEntity> GetById(int id)
        {
            using var con = new Connection().GetConnection();

            string sql = @"
                SELECT Id, ArmazenamentoId, TipoMovimentacao, Quantidade, DataMovimentacao, Observacoes
                FROM MovimentoArmazenamento
                WHERE Id = @Id
            ";

            return await con.QueryFirstOrDefaultAsync<MovimentoArmazenamentoEntity>(sql, new { Id = id });
        }

        public async Task<int> Insert(MovimentoArmazenamentoInsertDTO dto)
        {
            using var con = new Connection().GetConnection();

            string sql = @"
                INSERT INTO MovimentoArmazenamento 
                    (ArmazenamentoId, TipoMovimentacao, Quantidade, DataMovimentacao, Observacoes)
                VALUES 
                    (@ArmazenamentoId, @TipoMovimentacao, @Quantidade, @DataMovimentacao, @Observacoes);
                SELECT LAST_INSERT_ID();
            ";

            return await con.ExecuteScalarAsync<int>(sql, dto);
        }

        public async Task Update(int id, MovimentoArmazenamentoUpdateDTO dto)
        {
            using var con = new Connection().GetConnection();

            string sql = @"
                UPDATE MovimentoArmazenamento
                SET TipoMovimentacao = @TipoMovimentacao,
                    Quantidade = @Quantidade,
                    DataMovimentacao = @DataMovimentacao,
                    Observacoes = @Observacoes
                WHERE Id = @Id
            ";

            await con.ExecuteAsync(sql, new
            {
                Id = id,
                dto.TipoMovimentacao,
                dto.Quantidade,
                dto.DataMovimentacao,
                dto.Observacoes
            });
        }

        public async Task Delete(int id)
        {
            using var con = new Connection().GetConnection();

            string sql = "DELETE FROM MovimentoArmazenamento WHERE Id = @Id";
            await con.ExecuteAsync(sql, new { Id = id });
        }
    }
}