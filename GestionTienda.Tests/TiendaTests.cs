using Xunit;
using Moq;
using GestionTienda;

namespace GestionTienda.Tests;

public class ProductoTests
{
    // PUNTO 1
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


    // NOTA: Test en desuso. 
    // Al refactorizar BuscarProducto para que lance KeyNotFoundException al no encontrar un producto, 
    // el retorno ya no devuelve null, por lo que este escenario es cubierto por BuscarProducto_ProductoNoExiste_LanzaKeyNotFoundException.

    // [Fact]
    // public void BuscarProducto_ProductoNoExiste_DevuelveNull()
    // {
    //     // Arrange
    //     Tienda tienda = new Tienda();

    //     // Act
    //     Producto? resultado = tienda.BuscarProducto("Notebook");

    //     // Assert
    //     Assert.Null(resultado);
    // }

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
        //Assert.False(resultado);  Se modificó el método de tipo bool a void por el uso de excepciones
        Assert.Throws<KeyNotFoundException>(() => tienda.EliminarProducto("Notebook"));
    }

    //PUNTO 2
    [Fact]
    public void ActualizarPrecio_PrecioNegativo_LanzaArgumentOutOfRangeException()
    {
        // Arrange
        var producto = new Producto("Teclado", 35000, "Tecnología");

        // Act & Assert: Verifica que al pasar un precio negativo se lance la excepción correspondiente 
        Assert.Throws<ArgumentOutOfRangeException>(() => producto.ActualizarPrecio(-500));  
    }

    [Fact]
    public void EliminarProducto_ProductoInexistente_LanzaKeyNotFoundException()
    {
        // Arrange
        var tienda = new Tienda();
        var producto = new Producto("Mouse", 20000, "Tecnología");
        tienda.AgregarProducto(producto);

        // Act & Assert: Verifica que al intentar eliminar un producto que no está en el inventario se lance KeyNotFoundException
        Assert.Throws<KeyNotFoundException>(() => tienda.EliminarProducto("ProductoInexistente"));
    }

    [Fact]
    public void BuscarProducto_ProductoInexistente_LanzaKeyNotFoundException()
    {
        // Arrange
        var tienda = new Tienda();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => tienda.BuscarProducto("Impresora"));
    }

    //PUNTO 3
    [Fact]
    public void AplicarDescuento_CalculaCorrectamenteYActualizaPrecio()
    {
        // Arrange
        var tienda = new Tienda();
        
        var mockProducto = new Mock<Producto>(
            "Notebook",
            100000m,
            "Tecnología"
        );
        
        tienda.Inventario.Add(mockProducto.Object);
        
        // Act
        tienda.AplicarDescuento("Notebook", 20);
        
        // Assert
        mockProducto.Verify(
            p => p.ActualizarPrecio(80000m),
            Times.Once
        );
    }
}
