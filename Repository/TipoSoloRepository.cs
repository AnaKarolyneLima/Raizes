
using Dapper;
using meuprimeirocrud_karol.Contracts.Repository;
using meuprimeirocrud_karol.DTO;
using meuprimeirocrud_karol.Entity;
using meuprimeirocrud_karol.Infrastructure;
using MySql.Data.MySqlClient;

namespace meuprimeirocrud_karol.Repository
{
    public class TipoSoloRepository : ITipoSoloRepository
    {
        public async Task<IEnumerable<TipoSoloEntity>> GetAll()
        {
            Connection _connection = new Connection();
            using (MySqlConnection con = _connection.GetConnection())
            {
                string sql = @$"
                    SELECT Id AS {nameof(TipoSoloEntity.Id)},
                           Nome AS {nameof(TipoSoloEntity.Nome)},
                           Textura AS {nameof(TipoSoloEntity.Textura)},
                           Descricao AS {nameof(TipoSoloEntity.Descricao)}
                    FROM TipoSolo
                ";

                IEnumerable<TipoSoloEntity> tipoSololist = await con.QueryAsync<TipoSoloEntity>(sql);

                return tipoSololist;
            }
        }

        public async Task<TipoSoloEntity> GetById(int id)
        {
            Connection _connection = new Connection();
            using (MySqlConnection con = _connection.GetConnection())
            {
                string sql = @$"
                    SELECT Id AS {nameof(TipoSoloEntity.Id)},
                           Nome AS {nameof(TipoSoloEntity.Nome)},
                           Textura AS {nameof(TipoSoloEntity.Textura)},
                           Descricao AS {nameof(TipoSoloEntity.Descricao)}
                    FROM TipoSolo
                    WHERE ID = @id
                ";

                TipoSoloEntity tipoSolo = await con.QueryFirstAsync<TipoSoloEntity>(sql, new { id });
                return tipoSolo;
            }

        }

        public async Task Insert(TipoSoloInsertDTO tipoSolo)
        {
            Connection _connection = new Connection();
            string sql = @"
                INSERT INTO TIPOSOLO (NOME,TEXTURA,DESCRICAO,)
                            VALUE(@Nome,@Textura,@Descricao)
            ";

            await _connection.Execute(sql, tipoSolo);
        }

        public async Task Update(TipoSoloEntity cidade)
        {
            Connection _connection = new Connection();
            string sql = @"
                UPDATE TIPOSOLO 
                SET 
                    Nome = @Nome,
                    Descricao = @Descricao,
                    Textura = @Textura
                WHERE ID = @Id;
            ";

            await _connection.Execute(sql, cidade);
        }

        public async Task Delete(int id)
        {
            Connection _connection = new Connection();
            string sql = "DELETE FROM TIPOSOLO WHERE ID = @id";

            await _connection.Execute(sql, new { id });
        }
    }
}