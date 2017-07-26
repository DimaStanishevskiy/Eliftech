using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eliftech.Models;
using Eliftech.Services;
using Newtonsoft.Json;

namespace Eliftech.Controllers
{
    [RoutePrefix("company")]
    public class CompanyController : Controller
    {
        private CompanyServices companyServices = new CompanyServices();

        [HttpPost]
        public string FindList()
        {
            var str = JsonConvert.SerializeObject(companyServices.FindListCompanies());
            return str;
        }

        [HttpPost]
        public string Find(int id)
        {
            return JsonConvert.SerializeObject(companyServices.FindCompany(id));
        }

        [HttpPost]
        public ActionResult Create(string name, int EstimatedEarnings, int fatherCompanyId = 0)
        {
            companyServices.CreateCompany(name, EstimatedEarnings, companyServices.FindCompany(fatherCompanyId));
            return new HttpStatusCodeResult(201);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            companyServices.DeleteCompany(companyServices.FindCompany(id));
            return new HttpStatusCodeResult(204);
        }

        [HttpPost]
        public ActionResult Update(int id, string name = "", int estimatedEarnings = -1)
        {
            Company company = companyServices.FindCompany(id);
            if (company == null)
                return new HttpStatusCodeResult(406);

            if (String.Compare(name, "") != 0)
                company.Name = name;

            if (estimatedEarnings != -1)
                company.EstimatedEarnings = estimatedEarnings;

            companyServices.UpdateCompany(company);
            return new HttpStatusCodeResult(200);

        }
    }
}