﻿using Dapper;
using Microsoft.Data.SqlClient;
using MnaejoPresupuesto.Models;

namespace MnaejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        //ACA CON ESTO EXTRAEMOS LA CONFIGURACION DEL CONNECTIONSTRING
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //ACA PARA CREAR LA INSERCION DEL TIPO DE CUENTA
        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            //ACA LO DEL DAPPER EL INT ES PORQUE VAMOS A DEVOLVER UN ENTERO tipocuenta EN ESTE CASO LO PASAMOS PORQUE EL DAPPER RECONOCE EL MODELO DEL tipocuenta Y SUS CAMPOS
            var id = await connection.QuerySingleAsync<int>
                                                    (@"INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden) 
                                                    Values (@Nombre, @UsuarioId, 0);
                                                    SELECT SCOPE_IDENTITY();", tipoCuenta);
            //ACA COLOCAMOS EL VALOR QUE ESTAMOS DEVOLVIENDO DEL ID LUEGO DE INSERTAR
            tipoCuenta.Id = id;
        }
        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>($"SELECT 1 " +
                                                                        $"FROM TiposCuentas " +
                                                                        $"WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;",
                                                                        new { nombre, usuarioId });
            return existe == 1;
        }
        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden 
                                                             FROM TiposCuentas 
                                                             WHERE UsuarioId = @UsuarioId", new { usuarioId });

        }
        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("UPDATE TiposCuentas " +
                "                          SET Nombre = @Nombre WHERE Id = @Id", tipoCuenta);
        }
        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden 
                                                                           FROM TiposCuentas 
                                                                           WHERE Id = @Id AND UsuarioId = @UsuarioId", 
                                                                           new { id, usuarioId });
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas WHERE Id = @Id", new { id });
        }




    }
}
