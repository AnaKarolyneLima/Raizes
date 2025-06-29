using meuprimeirocrud_karol.DTO.TipoSolo;
using meuprimeirocrud_karol.Entity.TipoSolo;

namespace meuprimeirocrud_karol.Contracts.Repository.TipoSolo
{
    public interface ITipoSoloRepository
    {
        Task<IEnumerable<TipoSoloEntity>> GetAll();
        Task<TipoSoloEntity> GetById(int id);
        Task<int> Insert(TipoSoloInsertDTO dto);
        Task Update(int id, TipoSoloUpdateDTO dto);
        Task Delete(int id);
    }
}