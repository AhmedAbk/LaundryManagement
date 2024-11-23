using Domain.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAO
{
    public class MachineDAO
    {
        private readonly string _connectionString;

        public MachineDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Machine>> GetMachinesByLaundryIdAsync(int laundryId)
        {
            var machines = new List<Machine>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(
                    "SELECT * FROM Machines WHERE LaundryId = @LaundryId", connection))
                {
                    command.Parameters.AddWithValue("@LaundryId", laundryId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            machines.Add(new Machine
                            {
                                Id = reader.GetInt32("Id"),
                                Status = reader.GetString("Status"),
                                LaundryId = reader.GetInt32("LaundryId")
                            });
                        }
                    }
                }
            }
            return machines;
        }

        public async Task<bool> UpdateMachineStatusAsync(int machineId, string status)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(
                    "UPDATE Machines SET Status = @Status WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Id", machineId);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}
