using _10_Mvc_Core_SqlOracle_Hospitales.Models;
using _10_Mvc_Core_SqlOracle_Hospitales.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _10_Mvc_Core_SqlOracle_Hospitales.Controllers
{
    public class HospitalController : Controller
    {

        IRepositoryHospitales irepo;

        public HospitalController(IRepositoryHospitales hospital)
        {
            this.irepo = hospital;
        }

        public IActionResult Index()
        {
            List<Hospital> hospitales = this.irepo.GetHospitales();
            return View(hospitales);
        }

        public IActionResult Details(int hospitalCod)
        {
            Hospital hospital = this.irepo.FindHospital(hospitalCod);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string nombre, string direccion, string telefono, int numCama)
        {
            this.irepo.CreateHospital(nombre, direccion, telefono, numCama);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int hospitalCod)
        {
            this.irepo.DeleteHospital(hospitalCod);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int hospitalCod)
        {
            Hospital hospital = this.irepo.FindHospital(hospitalCod);
            return View(hospital);
        }

        [HttpPost]
        public IActionResult Update(int hospitalCod, string nombre, string direccion, string telefono, int numCama)
        {
            this.irepo.UpdateHospital(hospitalCod, nombre, direccion, telefono, numCama);
            return RedirectToAction("Index");
        }
    }
}
