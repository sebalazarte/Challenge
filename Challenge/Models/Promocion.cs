using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Challenge.Models
{
    public class Promocion
    {
        public Promocion()
        {
            Activo = true;
            FechaCreacion = DateTime.Now;
        }

        public ObjectId Id { get; set; }
        public IEnumerable<string> MediosDePago { get; set; }
        public IEnumerable<string> Bancos { get; set; }
        public IEnumerable<string> CategoriasProductos { get;  set; }
        public int? MaximaCantidadDeCuotas { get; set; }
        public decimal? ValorInteresCuotas { get; set; }
        public decimal? PorcentajeDeDescuento { get; set; }
        public decimal? PorcentajeDeInteres { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
