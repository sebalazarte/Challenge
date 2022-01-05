/*
A partir de las clases CajaRepository y SucursalRepository, crear la clase BaseRepository<T> 
que unifique los métodos GetAllAsync y GetOneAsync
Crear un abstract BaseEntity que defina la property Id y luego modificar las entities Caja y Sucursal para que hereden de BaseEntity 
Aclaración: Se deben respetar la interfaces. 
*/

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }

    public class Caja: BaseEntity
    {
        public int SucursalId { get; }
        public string Descripcion { get; }
        public int TipoCajaId { get; }

        public Caja(int sucursalId, string descripcion, int tipoCajaId)
        {
            SucursalId = sucursalId;   
            Descripcion = descripcion;
            TipoCajaId = tipoCajaId;
        }
    }

    public class Sucursal: BaseEntity
    {
        public string Direccion { get; }
        public string Telefono { get; }

        public Sucursal(string direccion, string telefono)
        {
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}

namespace Infrastructure.Data.Repositories
{
	public interface ICajaRepository 
	{
		Task<IEnumerable<Caja>> GetAllAsync();
		Task<Caja> GetOneAsync(Guid id);
	}
	
	public interface ISucursalRepository
	{
		Task<IEnumerable<Sucursal>> GetAllAsync();
		Task<Sucursal> GetOneAsync(int id);
	}

    //podria crear una interfaz generica
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(int id);
    }

    //tambien podria crear una clase abstracta base generica, en mis proyectos personales uso la interfaz
    public abstract class BaseRepository<T>
    {
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract Task<Caja> GetOneAsync(Guid id);
    }

    //public class CajaRepository
    //{
    //    private readonly DataContext _db;

    //    public CajaRepository(DataContext db)
    //        => _db = db;

    //    public async Task<IEnumerable<Caja>> GetAllAsync()
    //        => await _db.Cajas.ToListAsync();

    //    public async Task<Caja> GetOneAsync(Guid id)
    //        => await _db.Cajas.FirstOrDefaultAsync(x => x.Id == id);
    //}

    //public class SucursalRepository
    //{
    //    private readonly DataContext _db;

    //    public CajaRepository(DataContext db)
    //        => _db = db;

    //    public async Task<IEnumerable<Sucursal>> GetAllAsync()
    //        => await _db.Sucursales.ToListAsync();

    //    public async Task<Sucursal> GetOneAsync(int id)
    //        => await _db.Sucursales.FirstOrDefaultAsync(x => x.Id == id);
    //}

    


}