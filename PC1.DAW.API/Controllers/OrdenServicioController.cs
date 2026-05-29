using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PC1.DAW.CORE.Core.Entities;
using PC1.DAW.CORE.Core.Services;

namespace PC1.DAW.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenServicioController : ControllerBase
{
    private readonly IOrdenServicioService _service;
    private readonly ILogger<OrdenServicioController> _logger;

    public OrdenServicioController(IOrdenServicioService service, ILogger<OrdenServicioController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las órdenes de servicio
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrdenServicio>>> GetAll()
    {
        try
        {
            var ordenes = await _service.GetAllAsync();
            return Ok(ordenes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todas las órdenes de servicio");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene una orden de servicio por su ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrdenServicio>> GetById(int id)
    {
        try
        {
            var orden = await _service.GetByIdAsync(id);
            if (orden == null)
                return NotFound(new { message = "Orden de servicio no encontrada" });

            return Ok(orden);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la orden de servicio con ID {Id}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea una nueva orden de servicio
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OrdenServicio>> Create([FromBody] OrdenServicio ordenServicio)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaOrden = await _service.CreateAsync(ordenServicio);
            return CreatedAtAction(nameof(GetById), new { id = nuevaOrden.ID_OS }, nuevaOrden);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Datos nulos al crear orden de servicio");
            return BadRequest(new { message = "Los datos de la orden de servicio no pueden ser nulos" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear orden de servicio");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza una orden de servicio existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<OrdenServicio>> Update(int id, [FromBody] OrdenServicio ordenServicio)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ordenActualizada = await _service.UpdateAsync(id, ordenServicio);
            if (ordenActualizada == null)
                return NotFound(new { message = "Orden de servicio no encontrada" });

            return Ok(ordenActualizada);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Datos nulos al actualizar orden de servicio");
            return BadRequest(new { message = "Los datos de la orden de servicio no pueden ser nulos" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar orden de servicio con ID {Id}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina una orden de servicio
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var resultado = await _service.DeleteAsync(id);
            if (!resultado)
                return NotFound(new { message = "Orden de servicio no encontrada" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar orden de servicio con ID {Id}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
