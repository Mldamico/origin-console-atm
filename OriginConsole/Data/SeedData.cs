using System.Data.SqlClient;
using Dapper;

namespace OriginConsole.Data;

public class SeedData
{
    

    public async Task CreateDB()
    { 
        var connectionString = "server=localhost; user id=sa; password=reallyStrongPwd123; Encrypt=false;"; 
        using var connection = new SqlConnection(connectionString);
      
      await connection.ExecuteAsync(@"
        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'origin') CREATE DATABASE [origin];
      ");
    }
    
    public async Task Initialize()
    {
        var connectionString = "server=localhost; database=origin; user id=sa; password=reallyStrongPwd123; Encrypt=false;"; 
        using var connection = new SqlConnection(connectionString);
        
     
      
        await connection.ExecuteAsync(@"if not exists (select * from sysobjects where name='tipo_operacion' and xtype='U')
            CREATE TABLE 
            tipo_operacion (
            id INT IDENTITY (1, 1) NOT NULL,
            detalle NVARCHAR (50) NOT NULL,
            CONSTRAINT PK_tipo_operacion PRIMARY KEY CLUSTERED (id ASC)
        );");
        
        await connection.ExecuteAsync(@"if not exists (select * from sysobjects where name='cuenta' and xtype='U')
            CREATE TABLE cuenta (
            id  INT IDENTITY (1, 1) NOT NULL,
            pin NVARCHAR (10) NOT NULL,
            CONSTRAINT PK_Cuenta PRIMARY KEY CLUSTERED (id ASC)
        );");

        await connection.ExecuteAsync(@"if not exists (select * from sysobjects where name='tarjeta' and xtype='U')
            CREATE TABLE tarjeta (
               id                 INT           IDENTITY (1, 1) NOT NULL,
               numero            NVARCHAR (16) NOT NULL,
               bloqueada          BIT           CONSTRAINT DEFAULT_tarjeta_bloqueada DEFAULT 0 NOT NULL,
               intentos_restantes INT           CONSTRAINT DEFAULT_tarjeta_intentos_restantes DEFAULT 4 NOT NULL,
               cuenta_id          INT           NOT NULL,
               fecha_vencimiento  DATETIME      NOT NULL,
               saldo DECIMAL (18, 2) NOT NULL,
               CONSTRAINT PK_tarjeta PRIMARY KEY CLUSTERED (id ASC),
               CONSTRAINT FK_tarjeta_cuenta FOREIGN KEY (cuenta_id) REFERENCES cuenta (id)
        );");


        await connection.ExecuteAsync(@"if not exists (select * from sysobjects where name='operacion' and xtype='U')
            CREATE TABLE operacion (
               id         INT             IDENTITY (1, 1) NOT NULL,
               fecha       DATETIME        NOT NULL,
               monto        DECIMAL (18, 2) NULL,
               tipo_operacion_id INT             NOT NULL,
               tarjeta_id   INT             NOT NULL,
               saldo_previo DECIMAL(18, 2) DEFAULT 0 NOT NULL
               CONSTRAINT PK_operacion PRIMARY KEY CLUSTERED (id ASC),
               CONSTRAINT FK_operacion_tarjeta FOREIGN KEY (tarjeta_id) REFERENCES tarjeta (id),
               CONSTRAINT FK_operacion_tipo_operacion FOREIGN KEY (tipo_operacion_id) REFERENCES tipo_operacion (id)
        );");

        

    }

    public async Task Seed()
    {
        var connectionString = "server=localhost; database=origin; user id=sa; password=reallyStrongPwd123; Encrypt=false;";
        using var connection = new SqlConnection(connectionString);
        var tipoOperacionData = await connection.QueryFirstOrDefaultAsync<int>(@"select * from tipo_operacion");
        
        if (tipoOperacionData == 0)
        {
             await connection.ExecuteAsync(@"INSERT INTO tipo_operacion (detalle) values(@detalle);", new {detalle = "Balance"});
             await connection.ExecuteAsync(@"INSERT INTO tipo_operacion (detalle) values(@detalle);", new {detalle = "Retiro"});    
        }
        
        var cuentasData = await connection.QueryFirstOrDefaultAsync<int>(@"select * from cuenta");
        
        if (cuentasData == 0)
        {
            await connection.ExecuteAsync(@"INSERT INTO cuenta (pin) values(@pin);", new {pin = "1234"});
            await connection.ExecuteAsync(@"INSERT INTO cuenta (pin) values(@pin);", new {pin = "9876"});    
        }
        
        var tarjetasData = await connection.QueryFirstOrDefaultAsync<int>(@"select * from tarjeta");
        
        if (tarjetasData == 0)
        {
            await connection.ExecuteAsync(@"INSERT INTO tarjeta (cuenta_id, numero, fecha_vencimiento, saldo) values(@cuenta_id, @numero, @fecha_vencimiento, @saldo);", new {cuenta_id = 1, numero="4111111111111111", fecha_vencimiento= DateTime.Now.AddYears(1), saldo=5000.00});
            await connection.ExecuteAsync(@"INSERT INTO tarjeta (cuenta_id, numero, fecha_vencimiento, saldo) values(@cuenta_id, @numero, @fecha_vencimiento, @saldo);", new {cuenta_id = 2, numero="4111111111111111", fecha_vencimiento= DateTime.Now.AddYears(1), saldo=5000.00});    
        }
        
        var operacionesData = await connection.QueryFirstOrDefaultAsync<int>(@"select * from operacion");
        if (operacionesData == 0)
        {
            await connection.ExecuteAsync(@"INSERT INTO operacion (fecha, monto, tipo_operacion_id, tarjeta_id, saldo_previo) values(@fecha, @monto, @tipo_operacion_id, @tarjeta_id, @saldo_previo);", new {fecha = DateTime.Today, monto=100.00, tipo_operacion_id= 2, tarjeta_id=1, saldo_previo=5100.00});
            await connection.ExecuteAsync(@"INSERT INTO operacion (fecha, tipo_operacion_id, tarjeta_id, saldo_previo) values(@fecha, @tipo_operacion_id, @tarjeta_id, @saldo_previo);", new {fecha = DateTime.Today, tipo_operacion_id= 1, tarjeta_id=1, saldo_previo= 5000});
        }
    }
}