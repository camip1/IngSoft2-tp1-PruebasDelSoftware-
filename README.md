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

### Punto 1 : - ¿Puedes identificar pruebas de unidad y de integración en la práctica que se realizó?
 
En la práctica se identifican pruebas de integración en los casos donde Tienda interactúa con instancias reales de Producto (Agregar, Buscar y Eliminar elementos
existentes), garantizando que ambas clases colaboren correctamente. Por otro lado, los casos donde se consulta o elimina un producto inexistente sobre una tienda
vacía actúan como pruebas unitarias, ya que evalúan únicamente la lógica de control interna de la clase Tienda  de manera aislada. 

### Punto 2: - Podría haber escrito las pruebas primero antes de modificar el código de la aplicación? ¿Cómo sería el proceso de escribir primero los tests?Describe el proceso con tus palabras.

Sí, se podrían haber escrito en primera instancia las pruebas. Esta es la base de TDD (Test-Driven Development o Desarrollo Guiado por Pruebas), donde primero se define qué se espera que haga el sistema escribiendo la prueba y luego se escribe o modifica el código de producción para resolverlo. 
El proceso para escribir primero los tests consta de los siguientes pasos (ciclo Red-Green-Refactor):
1. **Fase Roja (Red)**: Se arma el test que prueba la nueva funcionalidad (por ejemplo, que BuscarProducto lance una excepción si no encuentra el producto). Al correr dotnet test, la prueba falla o no compila porque la lógica aún no fue implementada.
2.**Fase Verde (Green)**: Se modifica el código del proyecto (Tienda.cs) escribiendo lo estrictamente necesario para que la prueba pase (por ejemplo, agregar el if con el throw new KeyNotFoundException). Se vuelve a ejecutar el test y se comprueba que pasa a verde.
3. **Refactorización (Refactor)**: Con las pruebas pasando en verde, se emprolija el código, se limpian duplicaciones y se mejoran los nombres de las variables, etc.

### Punto 3:
#### 1- En lo que va del trabajo práctico, ¿puedes identificar 'Controladores' y 'Resguardos'?
En las pruebas realizadas podemos identificar elementos que cumplen la función de controladores y resguardos para aislar el comportamiento que estamos probando.
Por ejemplo, por un lado, los tests o métodos de prueba junto con el framework xUnit actúan como controladores (o drivers), encargados de establecer las condiciones iniciales, invocar a la unidad bajo prueba y utilizar aserciones para comprobar que el resultado obtenido sea el esperado. Por otro lado, los objetos simulados creados con Moq funcionan como resguardos (o stubs), ya que reemplazan a los colaboradores reales para devolver respuestas prefijadas y controlar las interacciones con objetos dependientes. 
De esta manera, los dobles de prueba permiten aislar completamente la unidad bajo prueba y evitar que los resultados dependan del comportamiento real de sus dependencias.
#### 2- ¿Qué es un “test double”? ¿Hay otros nombres para los objetos/funciones simulados?
Un test double es un objeto que se utiliza durante una prueba para reemplazar temporalmente a un objeto real que tiene alguna dependencia con la unidad que queremos probar.
Su objetivo es aislar la unidad bajo prueba y permitirnos controlar o verificar el comportamiento de esa dependencia.
Existen diferentes tipos de test doubles, entre ellos los mocks, stubs, fakes y spies. En esta práctica utilizamos un mock para simular un Producto y verificar que Tienda llame correctamente al método ActualizarPrecio.

### Punto 4:
 #### 1-Defina usando palabras propias y según la práctica realizada qué es un fixture.
Un fixture es un conjunto predefinido de datos, objetos y configuraciones de entorno que se crea de manera controlada y repetible antes de ejecutar las pruebas. Su propósito es garantizar que cada caso de prueba comience desde una línea base o estado inicial conocido y confiable (en nuestra práctica, una instancia de Tienda precargada con productos de ejemplo), sin depender del resultado o las modificaciones de tests previos. 

#### 2-¿Qué ventajas ve en el uso de fixtures? ¿Qué enfoque de diseño de pruebas estaríamos aplicando (caja negra/blanca)?
**Eliminación de código duplicado (Principio DRY)**: Evita repetir la misma lógica de inicialización e inserción de datos en la fase Arrange de cada prueba.

**Mantenibilidad**: Si los constructores o propiedades de Producto o Tienda cambian en el futuro, solo se modifica la inicialización en el fixture en lugar de actualizar cada test individual.

**Aislamiento e independencia entre pruebas**: Al reinicializarse el fixture antes de cada ejecución, se garantiza que los cambios que un test haga en el inventario no afecten ni contaminen a las demás pruebas.

**Claridad y legibilidad**: Los métodos de prueba quedan mucho más limpios y enfocados en lo que realmente evalúan (Act y Assert).
Estaremos aplicando principalmente un enfoque de caja negra (específicamente pruebas funcionales de comportamiento). Diseñamos las pruebas y preparamos el fixture basándonos en la interfaz pública y las especificaciones de los métodos (AgregarProducto, BuscarProducto, etc.) evaluando qué entra y qué sale, sin que el test dependa de los detalles de implementación interna o de cómo están programados los bucles y estructuras por dentro.

**Variante**: Estamos aplicando principalmente un enfoque de Caja Blanca (o estructura interna). Al ser las pruebas unitarias diseñadas por nosotros mismos sobre el código fuente de producción, conocemos en detalle la estructura interna de las clases Tienda y Producto, cómo se gestiona la lista en memoria y qué excepciones específicas (KeyNotFoundException, ArgumentOutOfRangeException) se disparan según el flujo lógico de la implementación. 

#### 3-Explique los conceptos de Setup y Teardown en testing.
**Setup (Preparación):** Es la etapa que se ejecuta antes de cada prueba para preparar el escenario necesario (instanciar clases, poblar colecciones o crear conexiones). En xUnit, el Setup se implementa de manera idiomática a través del constructor de la clase de pruebas, el cual se invoca de nuevo para cada método [Fact].

**Teardown (Limpieza):** Es la etapa que se ejecuta después de cada prueba para liberar recursos, vaciar colecciones o restaurar el estado original del sistema, previniendo efectos secundarios entre pruebas o fugas de memoria. En **xUnit**, el Teardown se logra implementando la interfaz IDisposable y codificando la lógica de limpieza dentro del método Dispose().

### Punto 5:
#### 1-¿Realizó una prueba de cobertura completa? ¿Qué tipo de cobertura utilizó?
No se realizó una cobertura completa del código, ya que no se midió formalmente el porcentaje de cobertura mediante una herramienta específica (como Coverlet), pero a nivel de diseño de pruebas se alcanzó una cobertura de sentencias (o instrucciones) y una cobertura de decisión (o ramas) sobre los métodos principales del sistema. 
Por ejemplo, en BuscarProducto se evaluó tanto el escenario en que el producto existe como el caso en que no existe. De esta manera, la condición producto == null se ejecutó tanto en su rama verdadera (lanzando KeyNotFoundException) como en su rama falsa (retornando el producto). 
También se probaron ambos casos de ActualizarPrecio: un precio negativo, que provoca una excepción, y un precio válido, que permite actualizar el precio.
Sin embargo, no se puede afirmar que exista una cobertura completa de condición múltiple o de todos los caminos posibles del sistema, ya que no se evaluaron exhaustivamente todas las combinaciones de decisiones compuestas o bordes de la lógica. 
#### 2-¿Puede describir una situación de desarrollo para este caso en donde se plantee pruebas de integración ascendente? Describa la situación.
Una situación de integración ascendente para este sistema sería comenzar probando primero las funcionalidades de menor nivel y luego integrarlas progresivamente.
Por ejemplo, primero se pueden probar las operaciones de la clase Producto, como ActualizarPrecio. Luego se integra Producto con Tienda para comprobar que Tienda pueda buscar productos y aplicar descuentos correctamente. Finalmente se incorporaría el cálculo del carrito para verificar que las diferentes funcionalidades trabajen juntas y permitan obtener el total correcto.
De esta manera, las pruebas avanzan desde los componentes más pequeños hacia funcionalidades de mayor nivel, hasta llegar al flujo completo del sistema.


