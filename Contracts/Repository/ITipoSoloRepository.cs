using meuprimeirocrud_karol.DTO;
using meuprimeirocrud_karol.Entity;

namespace meuprimeirocrud_karol.Contracts.Repository
{
    public interface ITipoSoloRepository
    {
        Task<IEnumerable<TipoSoloEntity>> GetAll();
        Task<TipoSoloEntity> GetById(int Id);
        Task Insert(TipoSoloInsertDTO propriedade);
        Task Update(TipoSoloEntity propriedade);
        Task Delete(int id);
    }
}