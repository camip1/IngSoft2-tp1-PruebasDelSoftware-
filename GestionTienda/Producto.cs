namespace GestionTienda;

public class Producto
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public string Categoria { get; set; }

    public Producto(string nombre, decimal precio, string categoria)
    {
        Nombre = nombre;
        Precio = precio;
        Categoria = categoria;
    }

    public virtual void ActualizarPrecio(decimal nuevoPrecio)
    {
        if (nuevoPrecio < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(nuevoPrecio), nuevoPrecio, "El precio no puede ser un valor negativo.");        
        }

        Precio = nuevoPrecio;
    } 

}
    
