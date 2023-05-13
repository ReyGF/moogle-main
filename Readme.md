# Moogle!

![](moogle.png)

> Proyecto de Programación I.
> Facultad de Matemática y Computación - Universidad de La Habana.
> Cursos 2021, 2022.

Moogle! es una aplicación *totalmente original* cuyo propósito es buscar inteligentemente un texto en un conjunto de documentos.

Es una aplicación web, desarrollada con tecnología .NET Core 7.0, específicamente usando Blazor como *framework* web para la interfaz gráfica, y en el lenguaje C#.
La aplicación está dividida en dos componentes fundamentales:

- `MoogleServer` es un servidor web que renderiza la interfaz gráfica y sirve los resultados.
- `MoogleEngine` es una biblioteca de clases donde está implementada la lógica del algoritmo de búsqueda.

Manual de Usuario:

1.Para ejecutar el proyecto descargue todos los archivos del repositorio y dirijase a traves de la console a la carpeta donde se encuentran.

2.Ejecute el comando make dev si se encuentra en Linux y dotnet watch run --project MoogleServer si se encuentra en Windows.

3.Luego aparecera la interfaz web y podra realizar su consulta ingresando el texto deseado en la barra de busqueda.

4.Si la palabra no aparece en ningun documento no devolvera resultados.

5.Este devolvera los 5 documentos mas semejantes a la consulta y si uno de estos documentos no contiene la palabra saldra que no ha encontrado la palabra.
