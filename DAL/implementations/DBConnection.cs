using Microsoft.Data.SqlClient;

namespace KafeshkaV2.DAL.implementations;

public class DBConnection
{
    public static SqlConnection CreateConnection()
    {
        return new SqlConnection("Data Source=.;Initial Catalog =Kafeshka;Integrated Security=true;Trust Server Certificate=true");
    }
}