using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILaundry
    {
        Task<List<Laundry>> GetAllLaundriesAsync();
        Task<Laundry> GetLaundryByIdAsync(int id);
        Task<List<Laundry>> GetLaundriesByOwnerIdAsync(int ownerId);
        Task<Laundry> GetLaundryWithMachinesAsync(int id);
        Task<bool> AddLaundryAsync(Laundry laundry);
        Task<bool> UpdateLaundryAsync(Laundry laundry);
        Task<bool> DeleteLaundryAsync(int id);
    }
}
