using meuprimeirocrud_karol.DTO;
using meuprimeirocrud_karol.Entity;

namespace meuprimeirocrud_karol.Contracts.Repository
{
    public interface IMovimentoArmazenamentoRepository
    {
        Task<IEnumerable<MovimentoArmazenamentoEntity>> GetAll();
        Task<MovimentoArmazenamentoEntity> GetById(int id);
        Task Insert(MovimentoArmazenamentoInsertDTO movimentacao);
        Task Update(MovimentoArmazenamentoEntity movimentacao);
        Task Delete(int id);
    }
}