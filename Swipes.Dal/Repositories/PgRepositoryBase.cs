using System.Transactions;
using Npgsql;
using Swipes.Dal.Settings;

namespace Swipes.Dal.Repositories;

public abstract class PgRepositoryBase
{
    private readonly DalOptions _dalOptions;

    protected PgRepositoryBase(DalOptions dalOptions)
    {
        _dalOptions = dalOptions;
    }

    protected const int DefaultTimeoutInSeconds = 5;
    protected async Task<NpgsqlConnection> GetConnection()
    {
        if (Transaction.Current is not null 
            && Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new TransactionAbortedException("Transaction was aborted (probably by user cancellation request)");
        }

        var connection = new NpgsqlConnection(_dalOptions.PostgresConnectionString);
        await connection.OpenAsync();
        
        connection.ReloadTypes();
        return connection;
    }
}
