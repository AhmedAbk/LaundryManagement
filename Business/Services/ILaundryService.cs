using Business.DTOs;
using Infrastructure.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ILaundryService
    {
        Task<List<OwnerDTO>> GetAllOwnersAsync();
        Task<List<LaundryDTO>> GetLaundriesByOwnerIdAsync(int ownerId);
        Task<List<MachineDTO>> GetMachinesByLaundryIdAsync(int laundryId);
        Task<bool> UpdateMachineStatusAsync(int machineId, string status);
    }

    public class LaundryService : ILaundryService
    {
        private readonly OwnerDAO _ownerDAO;
        private readonly LaundryDAO _laundryDAO;
        private readonly MachineDAO _machineDAO;

        public LaundryService(string connectionString)
        {
            _ownerDAO = new OwnerDAO(connectionString);
            _laundryDAO = new LaundryDAO(connectionString);
            _machineDAO = new MachineDAO(connectionString);
        }

        public async Task<List<OwnerDTO>> GetAllOwnersAsync()
        {
            var owners = await _ownerDAO.GetAllOwnersAsync();
            return owners.Select(o => new OwnerDTO
            {
                Id = o.Id,
                Name = o.Name
            }).ToList();
        }

        public async Task<List<LaundryDTO>> GetLaundriesByOwnerIdAsync(int ownerId)
        {
            var laundries = await _laundryDAO.GetLaundriesByOwnerIdAsync(ownerId);
            return laundries.Select(l => new LaundryDTO
            {
                Id = l.Id,
                Name = l.Name,
                OwnerId = l.OwnerId
            }).ToList();
        }

        public async Task<List<MachineDTO>> GetMachinesByLaundryIdAsync(int laundryId)
        {
            var machines = await _machineDAO.GetMachinesByLaundryIdAsync(laundryId);
            return machines.Select(m => new MachineDTO
            {
                Id = m.Id,
                Status = m.Status,
                LaundryId = m.LaundryId
            }).ToList();
        }

        public async Task<bool> UpdateMachineStatusAsync(int machineId, string status)
        {
            return await _machineDAO.UpdateMachineStatusAsync(machineId, status);
        }
    }
}
