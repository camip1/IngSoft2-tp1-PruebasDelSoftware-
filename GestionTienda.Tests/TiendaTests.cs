using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using GestionTienda;

namespace GestionTienda.Tests;

public class ProductoTests : IDisposable
{
    public Tienda TiendaCompartida { get; private set; }
    public Producto ProductoNotebook { get; private set; }
    public Producto ProductoMouse { get; private set; }

    // SETUP: El constructor se ejecuta antes de CADA prueba individual
    public ProductoTests()
    {
        TiendaCompartida = new Tienda();

        ProductoNotebook = new Producto("Notebook", 500000m, "Tecnología");
        ProductoMouse = new Producto("Mouse", 20000m, "Tecnología");

        // Inicializamos la tienda con los productos predefinidos
        TiendaCompartida.AgregarProducto(ProductoNotebook);
        TiendaCompartida.AgregarProducto(ProductoMouse);
    }

    // TEARDOWN: Limpieza de estado al finalizar cada prueba
    public void Dispose()
    {
        TiendaCompartida.Inventario.Clear();
    }

    // PUNTO 1
    [Fact] 
    public void AgregarProducto_ProductoSeAgregaAlInventario()
    {
        // Arrange
        Producto nuevoProducto = new Producto("Teclado", 35000m, "Tecnología");
        
        // Act
        TiendaCompartida.AgregarProducto(nuevoProducto);

        // Assert
        Assert.Contains(nuevoProducto, TiendaCompartida.Inventario);
    }

    [Fact]
    public void BuscarProducto_ProductoExiste_DevuelveProducto()
    {
        // Arrange
        // Tienda tienda = new Tienda();
        // Producto producto = new Producto("Notebook", 500000m, "Tecnología");
        // tienda.AgregarProducto(producto);

        // Act
        Producto resultado = TiendaCompartida.BuscarProducto("Notebook");

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
        // Tienda tienda = new Tienda();
        // Producto producto = new Producto("Notebook", 500000m, "Tecnología");
        // tienda.AgregarProducto(producto);

        // Act
        //bool resultado = tienda.EliminarProducto("Notebook");
        TiendaCompartida.EliminarProducto("Notebook");

        // Assert
        //Assert.True(resultado);
        Assert.DoesNotContain(ProductoNotebook, TiendaCompartida.Inventario);
    }

    //PUNTO 2
    [Fact]
    public void ActualizarPrecio_PrecioNegativo_LanzaArgumentOutOfRangeException()
    {
        // Arrange
        // var producto = new Producto("Teclado", 35000m, "Tecnología");

        // Act & Assert: Verifica que al pasar un precio negativo se lance la excepción correspondiente 
        Assert.Throws<ArgumentOutOfRangeException>(() => ProductoNotebook.ActualizarPrecio(-500m));  
    }

    [Fact]
    public void EliminarProducto_ProductoInexistente_LanzaKeyNotFoundException()
    {
        // Arrange
        // var tienda = new Tienda();
        // var producto = new Producto("Mouse", 20000m, "Tecnología");
        // tienda.AgregarProducto(producto);

        // Act & Assert: Verifica que al intentar eliminar un producto que no está en el inventario se lance KeyNotFoundException
        Assert.Throws<KeyNotFoundException>(() => TiendaCompartida.EliminarProducto("ProductoInexistente"));
    }

    [Fact]
    public void BuscarProducto_ProductoInexistente_LanzaKeyNotFoundException()
    {
        // Arrange
        // var tienda = new Tienda();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => TiendaCompartida.BuscarProducto("ImpresoraInexistente"));
    }

    //PUNTO 3
    [Fact]
    public void AplicarDescuento_CalculaCorrectamenteYActualizaPrecio()
    {
        // Arrange
        var tienda = new Tienda(); // Mocks: usamos una tienda limpia o mockeamos el producto
        var mockProducto = new Mock<Producto>(
            "Notebook",
            100000m,
            "Tecnología"
        );
        
        tienda.Inventario.Add(mockProducto.Object);
        
        // Act
        tienda.AplicarDescuento("Notebook", 20m);
        
        // Assert
        mockProducto.Verify(
            p => p.ActualizarPrecio(80000m),
            Times.Once
        );
    }
}
