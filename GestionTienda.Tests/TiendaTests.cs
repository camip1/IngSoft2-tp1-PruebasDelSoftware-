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

    [Fact]
    public void BuscarProducto_ProductoExiste_DevuelveProducto()
    {
        // Arrange
        Tienda tienda = new Tienda();
        Producto producto = new Producto("Notebook", 500000, "Tecnología");
        tienda.AgregarProducto(producto);

        // Act
        Producto? resultado = tienda.BuscarProducto("Notebook");

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Notebook", resultado.Nombre);
    }

    [Fact]
    public void BuscarProducto_ProductoNoExiste_DevuelveNull()
    {
        // Arrange
        Tienda tienda = new Tienda();

        // Act
        Producto? resultado = tienda.BuscarProducto("Notebook");

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void EliminarProducto_ProductoExiste_SeEliminaDelInventario()
    {
        // Arrange
        Tienda tienda = new Tienda();
        Producto producto = new Producto("Notebook", 500000, "Tecnología");
        tienda.AgregarProducto(producto);

        // Act
        //bool resultado = tienda.EliminarProducto("Notebook");
        tienda.EliminarProducto("Notebook");

        // Assert
        //Assert.True(resultado);
        Assert.DoesNotContain(producto, tienda.Inventario);
    }

    [Fact]
    public void EliminarProducto_ProductoNoExiste_DevuelveFalse()
    {
        // Arrange
        Tienda tienda = new Tienda();
        // Producto producto = new Producto("Notebook", 500000, "Tecnología");
        // tienda.AgregarProducto(producto);

        // Act
        //bool resultado = tienda.EliminarProducto("Notebook");
        // Assert
        //Assert.False(resultado);
        Assert.Throws<KeyNotFoundException>(() => tienda.EliminarProducto("Notebook"));
    }
}
