using meuprimeirocrud_karol.Contracts.Repository.TipoSolo;
using meuprimeirocrud_karol.DTO.TipoSolo;
using meuprimeirocrud_karol.Entity.TipoSolo;
using meuprimeirocrud_karol.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace meuprimeirocrud_karol.Controllers.TipoSolo
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoSoloController : ControllerBase
    {
        private readonly ITipoSoloRepository _repository;

        public TipoSoloController(ITipoSoloRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipos = await _repository.GetAll();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            var tipo = await _repository.GetById(id);
            if (tipo == null)
                throw new NotFoundException($"TipoSolo com ID {id} não encontrado.");

            return Ok(tipo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoSoloInsertDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new BadRequestException("Nome é obrigatório.");

            if (!Enum.TryParse<TexturaSolo>(dto.Textura, out _))
                throw new BadRequestException("Textura inválida. Use: Arenoso, Argiloso, Medio ou Siltoso.");

            var novoId = await _repository.Insert(dto);
            var criado = await _repository.GetById(novoId);

            return CreatedAtAction(nameof(GetById), new { id = novoId }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoSoloUpdateDTO dto)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new BadRequestException("Nome é obrigatório.");

            if (!Enum.TryParse<TexturaSolo>(dto.Textura, out _))
                throw new BadRequestException("Textura inválida. Use: Arenoso, Argiloso, Medio ou Siltoso.");

            var existing = await _repository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"TipoSolo com ID {id} não encontrado.");

            await _repository.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"TipoSolo com ID {id} não encontrado.");
            }
        }
    }
}