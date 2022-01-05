/*
Teniendo en cuenta la librería ICache, que fue escrita e implementada por otro equipo y utiliza una cache del tipo Key Value,
tomar la clase CajaRepository y modificar los métodos AddAsync, GetAllAsync, GetAllBySucursalAsync y GetOneAsync para que utilicen cache.

Datos:
    * Existen en la empresa 20 sucursales
    * Como mucho hay 100 cajas en la base

Restricción:    
	* Solo es posible utilizar 1 key (IMPORTANTE)
	
Aclaración:
	* No realizar una implementación de ICache, otro equipo la esta brindando
*/

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Threading.Tasks;

public interface ICache
{
    Task AddAsync<T>(string key, T obj, int? durationInMinutes);
    Task<T> GetOrDefaultAsync<T>(string key);
    Task RemoveAsync(string key);
}

public class Caja
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public int SucursalId { get; set; }
}

public class CajaRepository
{
    private readonly DataContext _db;
    private ICache _cache;
    const string CacheKey = "_lista";

    public CajaRepository(DataContext db, ICache cache)
    {
        _db = db ?? throw new ArgumentNullException(nameof(DataContext));
        _cache = cache;
    }

    public async Task AddAsync(Caja caja)
    {
        //este codigo si iria, solo lo comento para que compile

        //await _db.Cajas.AddAsync(caja);
        //await _db.SaveChangesAsync();

        await _cache.RemoveAsync(CacheKey);
        var lista = await this.GetAllAsync();
        await _cache.AddAsync(CacheKey, lista, 120);
    }

    public async Task<IEnumerable<Caja>> GetAllAsync()
    {
        //return await _db.Cajas
        //    .ToListAsync()
        var lista = await _cache.GetOrDefaultAsync<IEnumerable<Caja>>(CacheKey);
        return lista;
    }

    public async Task<IEnumerable<Caja>> GetAllBySucursalAsync(int sucursalId)
    {
        //return await _db.Cajas
        //    .Where(x => x.SucursalId == sucursalId)
        //    .ToListAsync()

        var lista = await _cache.GetOrDefaultAsync<IEnumerable<Caja>>(CacheKey);
        return lista.Where(x => x.SucursalId == sucursalId).ToList();
    }

    public async Task<Caja> GetOneAsync(Guid id)
    {
        //return await _db.Cajas
        //    .FirstOrDefaultAsync(x => x.Id == id);
        var lista = await _cache.GetOrDefaultAsync<IEnumerable<Caja>>(CacheKey);
        return lista.FirstOrDefault(x => x.Id == id);
    }
}