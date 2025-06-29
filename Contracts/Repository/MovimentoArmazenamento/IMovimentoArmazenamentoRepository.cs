using meuprimeirocrud_karol.DTO.MovimentoArmazenamento;
using meuprimeirocrud_karol.Entity.MovimentoArmazenamento;

namespace meuprimeirocrud_karol.Contracts.Repository.MovimentoArmazenamento
{
    public interface IMovimentoArmazenamentoRepository
    {
        Task<IEnumerable<MovimentoArmazenamentoEntity>> GetAll();
        Task<MovimentoArmazenamentoEntity> GetById(int id);
        Task<int> Insert(MovimentoArmazenamentoInsertDTO dto);
        Task Update(int id, MovimentoArmazenamentoUpdateDTO dto);
        Task Delete(int id);
    }
}