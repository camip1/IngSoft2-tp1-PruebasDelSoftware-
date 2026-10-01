using Xunit;
using GestionTienda;

namespace GestionTienda.Tests;

public class ProductoTests
{
    [Fact] 
    public void AgregarProducto_ProductoSeAgregaAlInventario()
    {
        // Arrange
        Tienda tienda = new Tienda();
        Producto producto = new Producto("Notebook", 500000, "Tecnologia");

        // Act
        tienda.AgregarProducto(producto);

        // Assert
        Assert.Contains(producto, tienda.Inventario);
    }

    
}
