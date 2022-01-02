using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Models
{
    public class Banco
    {
        public ObjectId Id { get; private set; }
        public string Nombre { get; set; }
    }
}
