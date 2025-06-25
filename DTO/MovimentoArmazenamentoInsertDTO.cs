namespace meuprimeirocrud_karol.DTO
{
    public class MovimentoArmazenamentoInsertDTO
    {
        public int Id { get; set; }
        public int ArmazenamentoId { get; set; }
        public string TipoMovimentacao { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string Observacoes { get; set; }
    }
}