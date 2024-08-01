namespace Proyecto_API.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public string? Ruta_Imagen { get; set; }
        public char? Estado { get; set; }
        public int? IdCategoria { get; set; }


        public class ProductoRespuesta
        {



            public int Codigo { get; set; }
            public string? Mensaje { get; set; }
            public object? Contenido { get; set; }

        }
    }
}