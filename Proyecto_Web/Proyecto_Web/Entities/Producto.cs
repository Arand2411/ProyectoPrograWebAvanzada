namespace Proyecto_Web.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public string? Ruta_Imagen { get; set; }
        public string? Ruta_Video { get; set; }
        public char? Estado { get; set; }



    }
}