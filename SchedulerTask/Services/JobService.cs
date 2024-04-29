using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace SchedulerTask.Services
{
    public class JobService
    {
        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;
        public JobService(IConfiguration? configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task RunJobSchedulerTask()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var result = await connection.ExecuteAsync("SchedulerTask", commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async Task RunJobSchedulerTask_JumpUp()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var result = await connection.ExecuteAsync("SchedulerTask_JumpUp", commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async Task RunJobSchedulerTask_ExecutivePartnerBenefits()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var result = await connection.ExecuteAsync("SchedulerTask_ExecutivePartnerBenefits", commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
