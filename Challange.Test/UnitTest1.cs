using Challenge.Controllers;
using Challenge.Models;
using Challenge.Repositories;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challange.Test
{
    public class Tests
    {

        PromocionController promocionController = new PromocionController();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BancosCargados()
        {
            IBancoCollection dbBancos = new BancoCollection();
            var bancos = await dbBancos.ObtenerTodos();
            Assert.IsTrue(bancos.Count > 0, "No fueron cargados los bancos");

            var bancosPermitidos = bancos.Select(i => i.Nombre).ToList();
            Assert.Contains("Macro", bancosPermitidos);
            Assert.Contains("Ciudad", bancosPermitidos);
        }

        [Test]
        public async Task MediosPagoCargados()
        {
            IMedioPagoCollection dbMedios = new MedioPagoCollection();
            var lista = await dbMedios.ObtenerTodos();
            Assert.IsTrue(lista.Count > 0, "No fueron cargados los medios de pago");

            var posibles = lista.Select(i => i.Nombre).ToList();
            Assert.Contains("TARJETA_CREDITO", posibles);
            Assert.Contains("TARJETA_DEBITO", posibles);
            Assert.Contains("EFECTIVO", posibles);
            Assert.Contains("GIFT_CARD", posibles);
        }
        
        [Test]
        public async Task CategoriasCargadas()
        {
            ICategoriaCollection dbCategorias = new CategoriaCollection();
            var lista = await dbCategorias.ObtenerTodos();
            Assert.IsTrue(lista.Count > 0, "No fueron cargados las categorias");

            var posibles = lista.Select(i => i.Nombre).ToList();
            Assert.Contains("Hogar", posibles);
            Assert.Contains("Jardin", posibles);
        }

        [Test]
        public async Task CrearPromocion()
        {
            var prom = new Promocion
            {
                Bancos = new List<string> { "Macro", "Ciudad" },
                MediosDePago = new List<string> { "TARJETA_CREDITO", "TARJETA_DEBITO" },
                CategoriasProductos = new List<string>(),
                FechaInicio = new System.DateTime(2020, 1, 1),
                FechaFin = new System.DateTime(2020, 1, 10),
                MaximaCantidadDeCuotas = 12
            };

            var result = await promocionController.Crear(prom);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            
            Assert.AreEqual(((OkObjectResult)result).StatusCode, 200);

            var ok = (OkObjectResult)result;
            var id = ok.Value.ToString();

            var vigentesResponse = (OkObjectResult) await promocionController.ObtenerVigentes(new System.DateTime(2020, 1, 5));
            var lista = (List<Promocion>)vigentesResponse.Value;

            Assert.IsTrue(lista.Count > 0, "Error al buscar la promocion vigente para la fecha");

           var eliminarResponse = (OkObjectResult)await promocionController.Eliminar(id);
            Assert.Equals(eliminarResponse.Value, "Eliminado");
        }
    }
}