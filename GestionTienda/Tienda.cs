namespace GestionTienda;

public class Tienda
{
    public List<Producto> Inventario { get; set; }
    public Tienda()
    {
        Inventario = new List<Producto>();
    }

    public void AgregarProducto(Producto producto)
    {
        Inventario.Add(producto);
    }

    public Producto BuscarProducto(string nombre)
    {
        var producto = Inventario.FirstOrDefault(p => p.Nombre == nombre);
        if(producto == null)
        {
            throw new KeyNotFoundException($"El producto '{nombre}' no se encuentra en el inventario.");
        }
        return producto;
    }

    public void EliminarProducto(string nombre)
    {
        Producto producto = BuscarProducto(nombre); //Al utilizar el metodo BuscarProducto, si no encuentra el producto se lanza la excepción
        Inventario.Remove(producto);
    }

    public void AplicarDescuento(string nombre, decimal porcentaje)
    {
        Producto producto = BuscarProducto(nombre);

        decimal nuevoPrecio = producto.Precio * (1 - porcentaje / 100);
        
        producto.ActualizarPrecio(nuevoPrecio);
    }
}
