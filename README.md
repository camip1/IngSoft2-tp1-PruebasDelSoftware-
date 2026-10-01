# IngSoft2-tp1-PruebasDelSoftware-

## “Sistema de Gestión de Productos en una Tienda”

**Baigorria Camila Virginia, Mariano Julieta, Perez Milagros Camila**

**Lenguaje de programacion elegido: C#**
**Framework elegido: xUnit**

Las partes clave de xUnit en nuestro código

**A. Las etiquetas [Fact]**
Son atributos de C#. Le indican al ejecutor de xUnit: "Este método no es un código normal de la app, es una prueba que debes ejecutar de forma independiente".

ej: [Fact]
public void BuscarProducto_NoExistente_DevuelveNull()
{ ... }

**B. La clase Assert (Afirmaciones)**
Es el motor de verificación. Le dice a xUnit qué condición se debe cumplir para que el test se considere exitoso:

Assert.Equal(esperado, actual): Comprueba que dos valores sean idénticos (ej. que el precio sea $1200.50).

Assert.NotNull(resultado): Verifica que un objeto no sea nulo.

Assert.Null(resultado): Comprueba que un método devuelva null cuando busque algo inexistente.

Assert.True(condicion): Verifica que una respuesta booleana sea verdadera (ej. al eliminar un producto existente).

Assert.Empty(coleccion): Verifica que la lista o inventario haya quedado completamente vacío.

**Si algún Assert falla, xUnit interrumpe esa prueba, la marca en rojo en la consola y te dice exactamente en qué línea y qué valor se esperaba versus el que se obtuvo.**

La estructura AAA (Arrange - Act - Assert)

xUnit promueve escribir las pruebas siguiendo el patrón estándar del software:

1) Arrange (Preparar): Creas los objetos necesarios (ej. var tienda = new Tienda();).

2) Act (Actuar): Ejecutas el método que quieres probar (ej. tienda.AgregarProducto(p);).

3) Assert (Afirmar): Verificas que el resultado sea el esperado (ej. Assert.Single(tienda.Inventario);).


