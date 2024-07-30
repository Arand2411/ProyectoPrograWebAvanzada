namespace Proyecto_API.Entities
{
    public class Categoria
    {
        public int? IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? RutaImagen { get; set; }
        public char? Estado { get; set; }

    }
    public class CategoriaRespuesta
    {



        public int Codigo { get; set; }
        public string? Mensaje { get; set; }
        public object? Contenido { get; set; }

    }
}
