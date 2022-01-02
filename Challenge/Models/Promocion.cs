using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Challenge.Models
{
    public class Promocion
    {
        public ObjectId Id { get; private set; }
        public IEnumerable<string> MediosPagos { get; set; }
        public IEnumerable<string> Bancos { get; set; }
        public IEnumerable<string> CategiriasProductos { get; private set; }
        public int? MaximaCantidadCuotas { get; private set; }
        public decimal? ValorInteresCuotas { get; private set; }
        public decimal? PorcentajeDeDescuentos { get; private set; }
        public decimal? PorcentajeDeInteres { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; private set; }
    }
}
