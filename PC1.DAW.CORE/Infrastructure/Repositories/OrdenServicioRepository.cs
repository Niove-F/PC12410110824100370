using Microsoft.EntityFrameworkCore;
using PC1.DAW.CORE.Core.Entities;
using PC1.DAW.CORE.Core.Interfaces;
using PC1.DAW.CORE.Infrastructure.Data;

namespace PC1.DAW.CORE.Infrastructure.Repositories;

public class OrdenServicioRepository : IOrdenServicioRepository
{
    private readonly TallerDbContext _context;

    public OrdenServicioRepository(TallerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrdenServicio>> GetAllAsync()
    {
        return await _context.OrdenServicio
            .Include(o => o.Vehiculo)
            .Include(o => o.TipoServicio)
            .ToListAsync();
    }

    public async Task<OrdenServicio?> GetByIdAsync(int id)
    {
        return await _context.OrdenServicio
            .Include(o => o.Vehiculo)
            .Include(o => o.TipoServicio)
            .FirstOrDefaultAsync(o => o.ID_OS == id);
    }

    public async Task<OrdenServicio> CreateAsync(OrdenServicio ordenServicio)
    {
        _context.OrdenServicio.Add(ordenServicio);
        await _context.SaveChangesAsync();
        return ordenServicio;
    }

    public async Task<OrdenServicio?> UpdateAsync(int id, OrdenServicio ordenServicio)
    {
        var existing = await _context.OrdenServicio.FirstOrDefaultAsync(o => o.ID_OS == id);
        if (existing == null)
            return null;

        existing.FechaIngreso = ordenServicio.FechaIngreso;
        existing.DescripcionProblema = ordenServicio.DescripcionProblema;
        existing.CostoEstimado = ordenServicio.CostoEstimado;
        existing.Estado = ordenServicio.Estado;
        existing.ID_V = ordenServicio.ID_V;
        existing.ID_TS = ordenServicio.ID_TS;

        _context.OrdenServicio.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ordenServicio = await _context.OrdenServicio.FirstOrDefaultAsync(o => o.ID_OS == id);
        if (ordenServicio == null)
            return false;

        _context.OrdenServicio.Remove(ordenServicio);
        await _context.SaveChangesAsync();
        return true;
    }
}
