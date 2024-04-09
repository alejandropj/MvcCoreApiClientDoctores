using ApiCrudCoreDoctores.Models;
using Microsoft.AspNetCore.Mvc;
using MvcCoreApiClientDoctores.Services;

namespace MvcCoreApiClientDoctores.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceApiDoctores service;
        public DoctoresController(ServiceApiDoctores service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> Details(int id)
        {
            Doctor doc = await this.service.FindDoctorAsync(id);
            return View(doc);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doc)
        {
            await this.service.InsertDoctorAsync(doc.IdDoctor, doc.Apellido, 
                doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Doctor doc = await this.service.FindDoctorAsync(id);
            return View(doc);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Doctor doc)
        {
            await this.service.UpdateDoctorAsync(doc.IdDoctor, doc.Apellido,
                doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }
    }
}
