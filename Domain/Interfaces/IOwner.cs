using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOwner
    {
        Task<List<Owner>> GetAllOwnersAsync();
        Task<Owner> GetOwnerByIdAsync(int id);
        Task<Owner> GetOwnerWithLaundriesAsync(int id);
        Task<bool> AddOwnerAsync(Owner owner);
        Task<bool> UpdateOwnerAsync(Owner owner);
        Task<bool> DeleteOwnerAsync(int id);
    }
}
