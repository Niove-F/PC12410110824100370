using PC1.DAW.CORE.Core.Entities;

namespace PC1.DAW.CORE.Core.Services;

public interface IOrdenServicioService
{
    Task<IEnumerable<OrdenServicio>> GetAllAsync();
    Task<OrdenServicio?> GetByIdAsync(int id);
    Task<OrdenServicio> CreateAsync(OrdenServicio ordenServicio);
    Task<OrdenServicio?> UpdateAsync(int id, OrdenServicio ordenServicio);
    Task<bool> DeleteAsync(int id);
}
