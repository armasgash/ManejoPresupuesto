using Dapper;
using Microsoft.Data.SqlClient;
using MnaejoPresupuesto.Models;

namespace MnaejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
    }
    public class RepositorioCuentas : IRepositorioCuentas
    {
        private readonly string connectionString;
        public RepositorioCuentas(IConfiguration configuration) 
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Cuentas (Nombre, TipoCuentaId, Descripcion, Balance)
                                                         VALUES (@Nombre, @TipoCuentaId, @Descripcion, @Balance);
                                                         SELECT SCOPE_IDENTITY();", cuenta);
            cuenta.Id = id;
        }
        //OJO ACA ABAJO. CUANDO SE CREA UN CAMPO NUEVO POR NECESIDAD PARA GUARDAR EL RESULTADO EN CUENTA QUE ES EL MAPEO, SE AGREGA EN EL MODELO DE CUENTA
        //NO OLVIDAR CONTROL . PARA EL PULL ARRIBA SOBRE CREAR EN ESTE CASO
        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Cuenta>("SELECT Cuentas.Id, Cuentas.Nombre, Balance, tc.Nombre AS TipoCuenta" +
                "                                       FROM Cuentas " +
                "                                       INNER JOIN TiposCuentas tc" +
                "                                       ON tc.Id = Cuentas.TipoCuentaId" +
                "                                       WHERE tc.UsuarioId = @UsuarioId" +
                "                                       ORDER BY tc.Orden", new {usuarioId});
        }

    }
}
