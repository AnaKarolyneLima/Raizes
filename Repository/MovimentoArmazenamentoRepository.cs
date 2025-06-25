using Dapper;
using meuprimeirocrud_karol.Contracts.Repository;
using meuprimeirocrud_karol.DTO;
using meuprimeirocrud_karol.Entity;
using meuprimeirocrud_karol.Infrastructure;
using MySql.Data.MySqlClient;

namespace meuprimeirocrud_karol.Repository
{
    public class MovimentoArmazenamentoRepository : IMovimentoArmazenamentoRepository
    {
        public async Task<IEnumerable<MovimentoArmazenamentoEntity>> GetAll()
        {
            Connection _connection = new Connection();
            using (MySqlConnection con = _connection.GetConnection())
            {
                string sql = @$"
                    SELECT 
                        Id AS {nameof(MovimentoArmazenamentoEntity.Id)},
                        ArmazenamentoId AS {nameof(MovimentoArmazenamentoEntity.ArmazenamentoId)},
                        TipoMovimentacao AS {nameof(MovimentoArmazenamentoEntity.TipoMovimentacao)},
                        Quantidade AS {nameof(MovimentoArmazenamentoEntity.Quantidade)},
                        DataMovimentacao AS {nameof(MovimentoArmazenamentoEntity.DataMovimentacao)},
                        Observacoes AS {nameof(MovimentoArmazenamentoEntity.Observacoes)}
                    FROM MOVIMENTOARMAZENAMENTO
                ";

                IEnumerable<MovimentoArmazenamentoEntity> movimentoArmazenamentoList = await con.QueryAsync<MovimentoArmazenamentoEntity>(sql);
                return movimentoArmazenamentoList;
            }
        }

        public async Task<MovimentoArmazenamentoEntity> GetById(int id)
        {
            Connection _connection = new Connection();
            using (MySqlConnection con = _connection.GetConnection())
            {
                string sql = @$"
                    SELECT 
                        Id AS {nameof(MovimentoArmazenamentoEntity.Id)},
                        ArmazenamentoId AS {nameof(MovimentoArmazenamentoEntity.ArmazenamentoId)},
                        TipoMovimentacao AS {nameof(MovimentoArmazenamentoEntity.TipoMovimentacao)},
                        Quantidade AS {nameof(MovimentoArmazenamentoEntity.Quantidade)},
                        DataMovimentacao AS {nameof(MovimentoArmazenamentoEntity.DataMovimentacao)},
                        Observacoes AS {nameof(MovimentoArmazenamentoEntity.Observacoes)}
                    FROM MOVIMENTOARMAZENAMENTO
                    WHERE Id = @id
                ";

                MovimentoArmazenamentoEntity movimentoArmazenamento = await con.QueryFirstOrDefaultAsync<MovimentoArmazenamentoEntity>(sql, new { id });
                return movimentoArmazenamento;
            }
        }

        public async Task Insert(MovimentoArmazenamentoInsertDTO movimentacao)
        {
            Connection _connection = new Connection();
            string sql = @"
                INSERT INTO MOVIMENTOARMAZENAMENTO 
                    (ArmazenamentoId, TipoMovimentacao, Quantidade, DataMovimentacao, Observacoes)
                VALUES 
                    (@ArmazenamentoId, @TipoMovimentacao, @Quantidade, @DataMovimentacao, @Observacoes)
            ";

            await _connection.Execute(sql, movimentacao);
        }

        public async Task Update(MovimentoArmazenamentoEntity movimentacao)
        {
            Connection _connection = new Connection();
            string sql = @"
                UPDATE MOVIMENTOARMAZENAMENTO
                SET 
                    ArmazenamentoId = @ArmazenamentoId,
                    TipoMovimentacao = @TipoMovimentacao,
                    Quantidade = @Quantidade,
                    DataMovimentacao = @DataMovimentacao,
                    Observacoes = @Observacoes
                WHERE Id = @Id
            ";

            await _connection.Execute(sql, movimentacao);
        }

        public async Task Delete(int id)
        {
            Connection _connection = new Connection();
            string sql = "DELETE FROM MOVIMENTOARMAZENAMENTO WHERE Id = @id";

            await _connection.Execute(sql, new { id });
        }
    }
}