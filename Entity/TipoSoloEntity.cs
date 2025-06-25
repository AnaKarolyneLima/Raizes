namespace meuprimeirocrud_karol.Entity
{
    public enum TexturaSolo
    {
        Arenoso,
        Argiloso,
        Medio,
        Siltoso
    }

    public class TipoSoloEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TexturaSolo Textura { get; set; }
    }
}