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
    public class LaundryDAO
    {
        private readonly string _connectionString;

        public LaundryDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Laundry>> GetLaundriesByOwnerIdAsync(int ownerId)
        {
            var laundries = new List<Laundry>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(
                    "SELECT * FROM Laundries WHERE OwnerId = @OwnerId", connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", ownerId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            laundries.Add(new Laundry
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                OwnerId = reader.GetInt32("OwnerId")
                            });
                        }
                    }
                }
            }
            return laundries;
        }

        public async Task<Laundry> GetLaundryWithMachinesAsync(int id)
        {
            Laundry laundry = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(
                    "SELECT l.*, m.Id as MachineId, m.Status " +
                    "FROM Laundries l " +
                    "LEFT JOIN Machines m ON l.Id = m.LaundryId " +
                    "WHERE l.Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (laundry == null)
                            {
                                laundry = new Laundry
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    OwnerId = reader.GetInt32("OwnerId")
                                };
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("MachineId")))
                            {
                                laundry.Machines.Add(new Machine
                                {
                                    Id = reader.GetInt32("MachineId"),
                                    Status = reader.GetString("Status"),
                                    LaundryId = id
                                });
                            }
                        }
                    }
                }
            }
            return laundry;
        }
    }
}