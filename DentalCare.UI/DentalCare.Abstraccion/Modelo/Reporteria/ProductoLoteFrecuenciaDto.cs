namespace DentalCare.Abstraccion.Modelo.Reporteria
{
    public class ProductoLoteFrecuenciaDto
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Lote { get; set; }
        public int CantidadTotal { get; set; }
    }
}
