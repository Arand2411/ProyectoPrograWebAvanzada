namespace Proyecto_Web.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public int? Cantidad { get; set; }
        public string? Imagen { get; set; }
        public char? Estado { get; set; }
        public int? IdCategoria { get; set; }

    }
}