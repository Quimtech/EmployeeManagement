# Guía para Iniciar el Proyecto

Este documento proporciona instrucciones para clonar y configurar el proyecto en tu entorno de desarrollo local usando Visual Studio.

## 1. Clonar el Proyecto

1. **Clona el repositorio del proyecto** en tu máquina local. Abre Visual Studio y usa la opción **"Clone a repository"**. El repositorio se clonará por defecto en `C:\Users\myUser\source\repos`.

    ```bash
    git clone <URL_DEL_REPOSITORIO>
    ```

## 2. Configurar el Archivo de Configuración

1. **Abre el proyecto en Visual Studio**.

2. **Abre el archivo `web.config`** que se encuentra en el directorio raíz del proyecto.

3. **Encuentra y modifica la sección `connectionStrings`** en el archivo `web.config` para que coincida con la configuración de tu servidor SQL. Actualiza la línea correspondiente con la información de tu servidor SQL:

    ```xml
    <connectionStrings>
        <add name="EmployeeDBContext" connectionString="Data Source=nombre del servidor sql server;Initial Catalog=EmployeeDB;Persist Security Info=true;User Id=xxx;Password=xxxx;" providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```

    - `Data Source`: Nombre del servidor SQL Server.
    - `User Id`: ID de SQL Server (por defecto es `sa`).
    - `Password`: Contraseña de SQL Server.

## 3. Configurar la Base de Datos

1. Ve a **"Herramientas"** en el menú superior de Visual Studio.

2. Selecciona **"Administrador de paquetes NuGet"** y luego **"Consola de administrador de paquetes"**.

3. Ejecuta los siguientes comandos en la consola para habilitar migraciones, agregar una migración inicial y actualizar la base de datos:


    ```powershell
    Install-Package EntityFramework
    ```
    
    ```powershell
    Enable-Migrations
    ```

    ```powershell
    Add-Migration InitialCreate
    ```

    ```powershell
    Update-Database -Verbose
    ```

## 4. Ejecutar la Aplicación

1. Navega a la carpeta `Views\Employee`.

2. Abre el archivo `Index.cshtml`.

3. Pulsa **F5** o haz clic en el botón **"IIS Express"** en la parte superior de Visual Studio para ejecutar la aplicación.

¡Eso es todo! Ahora deberías tener el proyecto configurado y corriendo en tu entorno local. Si encuentras algún problema o tienes preguntas adicionales, no dudes en consultar la documentación del proyecto o contactar al equipo de soporte.

