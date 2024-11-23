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
    public class OwnerDAO
    {
        private readonly string _connectionString;

        public OwnerDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Owner>> GetAllOwnersAsync()
        {
            var owners = new List<Owner>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM Owners", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            owners.Add(new Owner
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name")
                            });
                        }
                    }
                }
            }
            return owners;
        }

        public async Task<Owner> GetOwnerWithLaundriesAsync(int id)
        {
            Owner owner = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(
                    "SELECT o.*, l.Id as LaundryId, l.Name as LaundryName " +
                    "FROM Owners o " +
                    "LEFT JOIN Laundries l ON o.Id = l.OwnerId " +
                    "WHERE o.Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (owner == null)
                            {
                                owner = new Owner
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name")
                                };
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("LaundryId")))
                            {
                                owner.Laundries.Add(new Laundry
                                {
                                    Id = reader.GetInt32("LaundryId"),
                                    Name = reader.GetString("LaundryName"),
                                    OwnerId = id
                                });
                            }
                        }
                    }
                }
            }
            return owner;
        }
    }
}
