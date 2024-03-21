using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Npgsql;
using test.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace VersionsCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VersionOldController : ControllerBase
    {

        public VersionOldController()
        {
        }


       
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger(); // Initialize NLog logger

        [HttpPost]
        public VersionAddResp Add(VersionAddReq req )
        {
            VersionAddResp resp = new();
            string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO public.versions (projectid, versionnumber) VALUES (@projectid, @versionnumber)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@projectid", req.projectId);
                        cmd.Parameters.AddWithValue("@versionnumber", req.versionNumber);
                        cmd.ExecuteNonQuery();
                        //Console.WriteLine("Data inserted successfully!");
                    }

                    string sqlGetId = "SELECT CURRVAL(pg_get_serial_sequence('public.versions', 'id'))";
                    using (var cmdGetId = new NpgsqlCommand(sqlGetId, conn))
                    {
                        //resp.idVersion = Convert.ToInt32((long)cmdGetId.ExecuteScalar());
                        resp.code = 0; //0 - Success
                        //Console.WriteLine($"Data inserted with ID: {insertedId}");
                    }
                    //info logging to know that a new version is added
                    Logger.Info("A new version is added.");
                }
            }
            catch (NpgsqlException ex)
            {
                //error logging to know there is an error
                Logger.Error(ex, "Error occurred while adding a new version.");
            }
            return resp;
        }

        

        [HttpGet]
        public List<VersionGetResp> Get()
        {
            List<VersionGetResp> versions = new List<VersionGetResp>();

            string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT id, projectid, versionnumber FROM public.versions";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VersionGetResp version = new VersionGetResp
                                {
                                  //  ID = Convert.ToInt32(reader["id"]),
                                    ProjectID = Convert.ToInt32(reader["projectid"]),
                                    VersionNumber = reader["versionnumber"].ToString()
                                };

                                versions.Add(version);
                            }
                           
                        }
                    }
                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // You might want to log this error or handle it differently
            }

            return versions;
        }


        [HttpPost]
        [ProducesResponseType(typeof(VersionUpdateResp), 200)] // Successful update
        [ProducesResponseType(typeof(VersionUpdateResp), 400)] // Bad request
        public VersionUpdateResp Update(VersionUpdateReq req)
        {
            VersionUpdateResp resp = new VersionUpdateResp();
            string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Perform the update query
                    string updateSql = "UPDATE public.versions SET projectid = @projectid, versionnumber = @versionnumber WHERE id = @id";
                    using (var cmd = new NpgsqlCommand(updateSql, conn))
        
                    {
                        cmd.Parameters.AddWithValue("@projectid", req.projectId);
                        cmd.Parameters.AddWithValue("@versionnumber", req.versionNumber);
                        cmd.Parameters.AddWithValue("@id", req.Id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            resp.code = 0; // Success
                           // resp.idVersion = req.Id;
                        }
                        else
                        {
                            resp.code = -1; // Failure, no rows affected
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Error logging
                Console.WriteLine("Error: " + ex.Message);
                resp.code = -1; // Failure
            }

            return resp;
        }

        [HttpPost]
        [ProducesResponseType(typeof(VersionUpdateResp), 200)] // Successful update
        [ProducesResponseType(typeof(VersionUpdateResp), 400)] // Bad request
        public VersionDeleteResp Delete(VersionDeleteReq req)
        {
            VersionDeleteResp resp = new VersionDeleteResp();
            string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Perform the delete query
                    string updateSql = "DELETE from public.versions WHERE id = @id";
                    using (var cmd = new NpgsqlCommand(updateSql, conn))
                    {
                       
                        cmd.Parameters.AddWithValue("@id", req.Id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            resp.code = 0; // Success
                            resp.idVersion = req.Id;
                        }
                        else
                        {
                            resp.code = -1; // Failure, no rows affected
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Error logging
                Console.WriteLine("Error: " + ex.Message);
                resp.code = -1; // Failure
            }

            return resp;
        }
















    }
}
