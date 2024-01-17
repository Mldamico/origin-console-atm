## Origin Challenge Console

Aplicacion de consola ATM.

Base de datos: SQL Server 2022.
Cadena de conexion en Utils/Configuration.cs.

Cadena de conexion utilizando sql server en un contenedor levantandolo de la siguiente manera:

```
docker run -d --name sql_server_demo -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=reallyStrongPwd123' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
```

Al iniciar la aplicacion se creara la base de datos y agregara datos de prueba en la aplicacion.

Tarjeta de credito de prueba: 4111111111111111
Pin de prueba: 1234

Posee 4 entidades, cuenta, tarjeta, operacion y tipo de operacion. 

Al ingresar 4 veces de manera incorrecta el pin se bloqueara la cuenta, si ingresa correctamente luego de haber ingresado mal el pin se actualizara nuevamente la base de datos para volver a los intentos restantes
por defecto.

Al haber ingresado tiene las opciones para realizar las operaciones, tanto el balance como el retiro genera una entrada en la tabla de operaciones, la cual se puede visualizar en reporte.


DER:

![DER Origin](https://raw.githubusercontent.com/Mldamico/origin-console-atm/main/OriginConsole/Images/der.png)


Diagrama de flujo:

![Diagrama de flujo](https://raw.githubusercontent.com/Mldamico/origin-console-atm/main/OriginConsole/Images/diagramaflujo.png)
