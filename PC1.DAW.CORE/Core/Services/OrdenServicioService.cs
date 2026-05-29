using PC1.DAW.CORE.Core.Entities;
using PC1.DAW.CORE.Core.Interfaces;

namespace PC1.DAW.CORE.Core.Services;

public class OrdenServicioService : IOrdenServicioService
{
    private readonly IOrdenServicioRepository _repository;

    public OrdenServicioService(IOrdenServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrdenServicio>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<OrdenServicio?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<OrdenServicio> CreateAsync(OrdenServicio ordenServicio)
    {
        if (ordenServicio == null)
            throw new ArgumentNullException(nameof(ordenServicio));

        return await _repository.CreateAsync(ordenServicio);
    }

    public async Task<OrdenServicio?> UpdateAsync(int id, OrdenServicio ordenServicio)
    {
        if (ordenServicio == null)
            throw new ArgumentNullException(nameof(ordenServicio));

        return await _repository.UpdateAsync(id, ordenServicio);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
