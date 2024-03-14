using Microsoft.AspNetCore.Mvc;
using Npgsql;
using test.Models;

namespace VersionsCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        public VersionController()
        {
        }

        [HttpPost]
        public VersionAddResp Add(VersionAddReq req)
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

                    string sqlGetId = "SELECT CURRVAL(pg_get_serial_sequence('public.versions', 'idversion'))";
                    using (var cmdGetId = new NpgsqlCommand(sqlGetId, conn))
                    {
                        resp.idVersion = Convert.ToInt32((long)cmdGetId.ExecuteScalar());
                        resp.code = 0; //0 - Success
                        //Console.WriteLine($"Data inserted with ID: {insertedId}");
                    }
                    //info logging to know that a new version is added
                }
            }
            catch (NpgsqlException ex)
            {
                //error logging to know there is an error
                Console.WriteLine("Error: " + ex.Message);
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
                                    ID = Convert.ToInt32(reader["id"]),
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











    }
}
