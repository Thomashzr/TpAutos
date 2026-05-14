# TpAuto - Sistema de Gestión de Alquiler de Vehículos

TpAuto es una aplicación de consola robusta desarrollada en **C#** y **.NET** diseñada para la administración eficiente de una flota de vehículos de alquiler. El sistema permite gestionar clientes, autos y reservas, asegurando la integridad de los datos mediante una base de datos local **SQLite**.

## 🚀 Características Principales

### 📋 Gestión de Entidades (CRUD)
* **Clientes:** Registro, consulta, modificación y eliminación de clientes con validaciones de DNI y teléfono.
* **Vehículos:** Administración del inventario de autos, incluyendo marca, modelo, patente y precio por día.
* **Reservas:** Gestión del ciclo de vida de los alquileres (Crear, Modificar, Cancelar, Eliminar).

### 🧠 Lógica de Negocio Avanzada
* **Cálculo Automático de Costos:** El sistema calcula el `PrecioTotal` basándose en la diferencia de días entre la fecha de retiro y devolución, multiplicado por la tarifa diaria del vehículo seleccionado.
* **Validación de Disponibilidad:** Al realizar una reserva, el sistema filtra automáticamente los vehículos que ya tienen compromisos en ese rango de fechas para evitar colisiones.
* **Validación de Fechas:** No se permiten reservas con fechas de inicio anteriores a la fecha actual.

### 📊 Reportes y Exportación
* Generación de rankings de clientes frecuentes.
* Reporte de los vehículos más utilizados.
* Opción de exportar informes a archivos de texto (`.txt`).

## 🏗️ Arquitectura del Proyecto

El código sigue una estructura de capas para facilitar el mantenimiento:

- **`TpAuto.Modelos`**: Contiene las entidades principales (`Cliente`, `Vehiculo`, `Reserva`) con su lógica interna de cálculo.
- **`TpAuto.Controladores`**: Clases prefijadas con `n` (`nCliente`, `nVehiculo`, `nReserva`) que manejan el flujo de la aplicación y las reglas de negocio.
- **`TpAuto.Persistencia`**: Clases prefijadas con `p` que gestionan exclusivamente las operaciones SQL hacia `DbAutos.db`.
- **`Herramientas`**: Utilidades para visualización (`Tabla.cs`) y menús interactivos (`MenuSeleccion.cs`).

## 🛠️ Tecnologías Utilizadas

- **Lenguaje:** C#
- **Framework:** .NET
- **Base de Datos:** SQLite (vía `Microsoft.Data.Sqlite`)
- **Interfaz:** Consola interactiva con soporte para navegación mediante teclado.

## ⚙️ Configuración e Instalación

1.  Asegúrate de tener instalado el SDK de .NET.
2.  Clona este repositorio o descarga los archivos.
3.  Asegúrate de que el archivo `DbAutos.db` esté en la carpeta raíz de ejecución o que la cadena de conexión en `Conexion.cs` sea la correcta.
4.  Instala la dependencia de SQLite:
    ```bash
    dotnet add package Microsoft.Data.Sqlite
    ```
5.  Ejecuta la aplicación:
    ```bash
    dotnet run
    ```
