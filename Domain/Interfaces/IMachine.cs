using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMachine
    {
        Task<List<Machine>> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(int id);
        Task<List<Machine>> GetMachinesByLaundryIdAsync(int laundryId);
        Task<Machine> GetMachineWithCyclesAsync(int id);
        Task<bool> AddMachineAsync(Machine machine);
        Task<bool> UpdateMachineAsync(Machine machine);
        Task<bool> DeleteMachineAsync(int id);
    }
}
