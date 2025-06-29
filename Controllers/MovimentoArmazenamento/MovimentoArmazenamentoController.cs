using Dapper;
using meuprimeirocrud_karol.Contracts.Repository.MovimentoArmazenamento;
using meuprimeirocrud_karol.DTO.MovimentoArmazenamento;
using meuprimeirocrud_karol.Entity.MovimentoArmazenamento;
using meuprimeirocrud_karol.Exceptions;
using meuprimeirocrud_karol.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace meuprimeirocrud_karol.Controllers.MovimentoArmazenamento
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentoArmazenamentoController : ControllerBase
    {
        private readonly IMovimentoArmazenamentoRepository _repository;

        public MovimentoArmazenamentoController(IMovimentoArmazenamentoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            var item = await _repository.GetById(id);

            if (item == null)
                throw new NotFoundException($"Movimento com ID {id} não encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovimentoArmazenamentoInsertDTO dto)
        {
            if (dto.ArmazenamentoId <= 0)
                throw new BadRequestException("ArmazenamentoId inválido.");

            if (!Enum.IsDefined(typeof(TipoMovimentacao), dto.TipoMovimentacao))
                throw new BadRequestException("TipoMovimentacao inválido. Use: Venda, Transferência ou Ajuste.");

            if (dto.Quantidade <= 0)
                throw new BadRequestException("Quantidade deve ser maior que zero.");

            if (dto.DataMovimentacao == default)
                throw new BadRequestException("DataMovimentacao é obrigatória.");

            using var con = new Connection().GetConnection();
            var sql = "SELECT COUNT(1) FROM ArmazenamentoColheita WHERE Id = @Id";
            var exists = await con.ExecuteScalarAsync<int>(sql, new { Id = dto.ArmazenamentoId });

            if (exists == 0)
                throw new BadRequestException($"O ArmazenamentoId {dto.ArmazenamentoId} não existe.");

            var novoId = await _repository.Insert(dto);
            var criado = await _repository.GetById(novoId);

            return CreatedAtAction(nameof(GetById), new { id = novoId }, criado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovimentoArmazenamentoUpdateDTO dto)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            if (!Enum.IsDefined(typeof(TipoMovimentacao), dto.TipoMovimentacao))
                throw new BadRequestException("TipoMovimentacao inválido. Use: Venda, Transferência ou Ajuste.");

            if (dto.Quantidade <= 0)
                throw new BadRequestException("Quantidade deve ser maior que zero.");

            if (dto.DataMovimentacao == default)
                throw new BadRequestException("DataMovimentacao é obrigatória.");

            var existing = await _repository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"Movimento com ID {id} não encontrado.");

            await _repository.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ID inválido.");

            var existing = await _repository.GetById(id);
            if (existing == null)
                throw new NotFoundException($"Movimento com ID {id} não encontrado.");

            await _repository.Delete(id);
            return NoContent();
        }
    }
}