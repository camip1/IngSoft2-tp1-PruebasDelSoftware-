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
        if (porcentaje < 0 || porcentaje > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(porcentaje), "El porcentaje de descuento debe estar entre 0 y 100.");
        }
        Producto producto = BuscarProducto(nombre);

        decimal nuevoPrecio = producto.Precio * (1 - porcentaje / 100);
        
        producto.ActualizarPrecio(nuevoPrecio);
    }

    public decimal calcular_total_carrito(List<string> carrito)
    {
        decimal total = 0m;
        foreach(var nombre in carrito)
        {
            // Si un producto no existe se lanza KeyNotFoundException
            Producto producto = BuscarProducto(nombre);
            total += producto.Precio;
        }
        return total;
    }
}
