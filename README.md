Instrucciones para correr la solución BancoEjercicioApi

El Api en cuestión fué desarrollado en .NET 6, utilizando Visual Studio 2022. La base de datos que se utilizó fue SQL Server Express versión 15.

1) El archivo BaseDatos.sql contiene los scripts necesarios para generar la base de datos junto a sus tablas, y stored procedures. Es necesario ejecutar el script en SQL Server, utilizando por ejemplo el cliente”Microsoft SQL Management Studio”. El ejemplo se realizó utilizando un SQL Server Express versión 15.

2) Dentro de la carpeta “BancoEjercicioApi” se encuentra la solución completa de la API. Para poder probarla será necesario descargarla completa (contiene todos las dependencias descargadas con Nuget).
Una vez descargada, abrir la solución “BancoEjercicioApi.sln” dentro de la carpeta.

3) Una vez abierta la solución, abrir el archivo “appsettings.json” que se encuentra dentro del proyecto “BancoEjercicioApi.WebApi” y modificar el connection string de la db desde la propiedad “DefaultConnection” colocando la correspondiente. En la imagen se aprecia donde hacerlo:
![image](https://user-images.githubusercontent.com/109397747/189745407-af60ffc6-d0bb-4471-a3e8-0282d4440275.png)
4) Una vez modificado el connection string, basta con iniciar la aplicación (se ejecuta localmente)

5) Al ejecutarse, se abrirá la interfaz Swagger UI para poder consumir sus métodos. Si se desea usar Postman, es necesario importar las colecciones que se encuentran en el archivo “BancoEjercicioApi.postman_collection.json”, directamente en la aplicación Postman. Al importar las colecciones, se verá algo similar a esto:

 


Crear y/o Modificar la variable del Environment llamada {{ApiURL}} por la que se visualiza al momento de ejecutar la solución:

 
![image](https://user-images.githubusercontent.com/109397747/189745534-ed40cd2d-8b1d-4cf4-bc35-7ea63de5778f.png)

