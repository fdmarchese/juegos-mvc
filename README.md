# Portal de compra de juegos en ASP.NET MVC Core 📖

## Objetivos del ejercicio 📋
Utilizar Visual Studio y crear una aplicación utilizando ASP.NET MVC Core.
La aplicación se trata de un sistema de compra de Juegos con dos roles (Cliente y Administrador).

<hr />

## Enunciado 📢
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/es/vs/).
 - Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
 - Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades.
 - Crear una carpeta Database que dentro tendrá el dbContext que será utilizado.
 - Aplicar las validaciones necesarias en los controladores
 - Crear el dbContext utilizando base de datos en memoria. Como referencia ver [este dbContext](https://github.com/fdmarchese/mvc-intro/blob/e07249bc8f092124fadd318b7b9a0c40122af446/UsandoEntityFramework/Database/UsandoEFDbContext.cs). Recordar adicionalmente que debemos configurar la base de datos a utilizar en el archivo [Startup](https://github.com/fdmarchese/mvc-intro/blob/e07249bc8f092124fadd318b7b9a0c40122af446/UsandoEntityFramework/Startup.cs#L39).
 - Crear el Scaffolding para permitir los CRUD de las entidades: Géneros, Juegos, Categorías de juego y Clientes.
 - Permitir en la creación y edición de juegos asignarle el género en lugar de tener que hacerlo en un paso separado.
 - Realizar un sistema de login con roles de usuario y Admin.
 - El administrador puede hacer todo (crear nuevos géneros, agregar nuevas juegos, agregar nuevas categorías de juego y dar de alta clientes).
 - El Usuario sólo puede buscar Juegos, Comprar, ver la lista de sus Compras
 - La búsqueda de juegos debe ser una experiencia de usuario enriquecida permitiendo realizar múltiples filtros por criterios tales como Genero, rango de precio, consola, etc.
 - Al mostrar la lista de Juegos mostrar algún distintivo de css indicado en la categoria del juego utilizando la propiedad Css de "Categoria".
 - Al mostrar la lista de Juegos mostrar el ícono asociado a la consola del juego (utilizar una clase de css obtenida de [fontawesome](https://fontawesome.com/icons?d=gallery&m=free)).
 - Al ingresar al sistema hacer que se muestre un cartel de bienvenida al usuario (sólo se mostrará al loguearse).
<hr />

## Entidades 📄

**Usuario**
```
- Id : int
- Nombre : string
- Apellido : string
- FechaAlta : DateTime
- FechaUltimaModificacion : DateTime?
- FechaUltimoAcceso : DateTime?
- Username : string
- Password : byte[]
```

**Cliente : Usuario**
```
- Id : int
- FechaDeNacimiento : DateTime
- Dni : string
- Compras : List<Compra>
```

**Administrador : Usuario**
```
- Id : int
- Legajo : Guid
```

**Genero**
```
- Id : int
- Descripcion : string
- Juegos : List<JuegoGenero>
```

**JuegoGenero**
```
- Id : int
- GeneroId : int
- Genero : Genero
- JuegoId : int
- Juego : Juego
```

**Juego**
```
- Id : int
- Titulo : string
- Stock : int
- PrecioOriginal : decimal
- AnioLanzamiento : int
- CategoriaId : int
- Categoria : Categoria
- ConsolaId : int
- Consola : Consola
- Compras : List<Compra>
- Generos : List<JuegoGenero>
```

**Consola**
```
- Id : int
- Descripcion : string
- IconoCss : string
- Juegos : List<Juego>
```

**Categoria**
```
- Id : int
- Descripcion : string
- Css : string
- PorcentajeDescuento : decimal
- Juegos : List<Juego>
```

**Compra**
```
- Id : int
- JuegoId : int
- Juego : Juego
- ClienteId : int
- Cliente : Cliente
- FechaCompra : DateTime
- PrecioOriginal : decimal
- PrecioFinal : decimal
```

<hr />

## Restricciones del modelo ⌨️

NOTA: aquí un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

**Usuario**
- Id
	- Primary Key.
- Nombre
	- Requerido.
	- Máxima longitud del campo de 100 caracteres.
	- Sólo admite letras (utilizar regular expression).
- Apellido
	- Requerido.
	- Máxima longitud del campo de 100 caracteres.
	- Sólo admite letras (utilizar regular expression).
- FechaAlta
	- Debemos cargarlo nosotros en el controller cuando se crea un nuevo Cliente o Administrador
	- Para evitar que se muestre en el scaffolding generado utilizar el data annotation [ScaffoldColumn(false)]
- FechaUltimaModificacion
	- Se debe Actualizar cada vez que se edita el usuario o al crearse.
	- Para evitar que se muestre en el scaffolding generado utilizar el data annotation [ScaffoldColumn(false)]
- FechaUltimoAcceso
	- Se debe Actualizar cada vez que el usuario accede al sistema.
	- Para evitar que se muestre en el scaffolding generado utilizar el data annotation [ScaffoldColumn(false)]
- Username
	- Requerido.
	- Máxima longitud del campo de 50 caracteres.
	- Sólo admite caracteres alfanuméricos sin tildes y "_", "-".
- Password
	- todas las validaciones para este campo las haremos en el controller.
	- validar que tenga al menos una letra minúscula, una letra mayúscula y un número.
	- validar que sea de al menos 8 caracteres.

**Cliente : Usuario**
- Id
	- Primary Key.
- Dni
	- Requerido.
	- Sólo admite el formato NN.NNN.NNN (utilizar regular expression).
- FechaDeNacimiento
	- Requerido.
	- Debe ser mayor de 12 años (esta validación la realizamos en el controller).
- Compras
	- Lista de compras realizadas por el cliente.
 
**Administrador : Usuario**
- Id
	- Primary Key.
- Legajo
	- Será autogenerado mediante la función Guid.NewGuid(). Esto nos asegura una combinación de 32 dígitos hexadecimales y 4 guiones que serán únicos.

**Genero**
- Id
	- Primary Key.
- Descripcion
	- Requerido.
	- Máxima longitud del campo de 100 caracteres.
	- Sólo admite letras (utilizar regular expression).
- Juegos
	- Lista de JuegoGenero para establecer la relación muchos a muchos entre juegos y géneros (un juego puede tener muchos géneros y un género puede estar en muchas juegos)

**JuegoGenero**
- Id
	- Primary Key.
- GeneroId
	- Foreign Key con Genero.
- Genero
	- Navigation property con Genero.
- JuegoId
	- Foreign Key con Juego.
- Juego
	- Navigation property con Juego.

**Juego**
- Id
	- Primary Key.
- Titulo
	- Requerido.
	- MaxLength 200 caracteres.
- Stock
	- Stock disponible del juego. Debe restarse uno cada vez que se realiza una compra del juego en cuestión.
- PrecioOriginal
	- Precio en dólares original del juego.
- AnioLanzamiento
	- año de lanzamiento. Debe igual o menor al año actual y mayor a 1900. Al ser dinámica la validación del año actual debe hacerse en el controlador.
- CategoriaId
	- Foreign Key con Categoria.
- Categoria
	- Navigation property con Categoria.
- ConsolaId
	- Foreign Key con Consola.
- Consola
	- Navigation property con Consola.
- Compras
	- Lista de compras realizadas de este juego.
- Generos
	- Lista de generos asociados a este juego.

**Consola**
- Id
	- Primary Key.
- Descripcion
	- Requerido.
	- MaxLength 200 caracteres.
- IconoCss
	- No Requerido.
	- MaxLength 200 caracteres.
	- Sólo admite caracteres alfanuméricos, "_" y "-".
- Juegos
	- Lista de Juegos para la consola.

**Categoria**
- Id
	- Primary Key.
- Descripcion
	- Requerido.
	- MaxLength 200 caracteres.
- Css
	- No Requerido.
	- MaxLength 200 caracteres.
	- Sólo admite caracteres alfanuméricos, "_" y "-".
- PorcentajeDescuento
	- Requerido.
	- Rango entre 0 y 1.
- Juegos
	- Lista de Juegos para la categoria.

**Compra**
- Id
	- Primary Key.
- JuegoId
	- Foreign Key con Juego.
- Juego
	- Navigation property con Juego.
- ClienteId
	- Foreign Key con Cliente.
- Cliente
	- Navigation property con Cliente.
- FechaCompra
	- Fecha en que se realiza la compra. No se le debe solicitar ni mostrar al usuario al momento de realizar la compra.
- PrecioOriginal
	- Se completa en base al valor del juego.
- PrecioFinal
	- Se completa en base al valor del juego luego de aplicar el descuento de la Categoria del juego. 
