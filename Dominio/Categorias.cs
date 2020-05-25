namespace Dominio
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int ParentId { get; set; }
        public bool Estado { get; set; }
    }
}
