namespace meuprimeirocrud_karol.Entity.MovimentoArmazenamento
{
    public enum TipoMovimentacao
    {
        Venda,
        Transferencia,
        Ajuste
    }

    public class MovimentoArmazenamentoEntity
    {
        public int Id { get; set; }
        public int ArmazenamentoId { get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string Observacoes { get; set; }
    }
}