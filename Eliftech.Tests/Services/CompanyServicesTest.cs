using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eliftech.Services;
using System.Collections.Generic;
using Eliftech.Models;
using System.Linq;

namespace Eliftech.Tests.Services
{
    [TestClass]
    public class CompanyServicesTest
    {
        CompanyServices services;
        DataContext context;
        [TestInitialize]
        public void Initialize()
        {
            services = new CompanyServices();
            context = new DataContext();
            List<Company> list = services.FindListCompanies();
            foreach (Company item in list)
            {
                services.DeleteCompany(item.Id);
            }
        }
        [TestMethod]
        public void ServiceCreateCompany()
        {
            bool result = services.CreateCompany("Father", 100);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ServiceCreateChildrenCompany()
        {
            bool result = services.CreateCompany("Children", 100, services.FindCompany("Father"));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ServiceDeleteCompany()
        {
            services.CreateCompany("CompanyForDelete", 100);
            bool result = services.DeleteCompany(services.FindCompany("CompanyForDelete"));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ServiceFindListCompany()
        {
            Assert.IsNotNull(services.FindListCompanies());
        }

        [TestMethod]
        public void ServiceSearchRepeats()
        {
            List<Company> list = services.FindListCompanies();
            bool result = true;
            foreach(Company item in list)
            {
                if(item.FatherCompany != null)
                {
                    result = false;
                    break;
                }
            }
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ServiceCascadeDelete()
        {
            services.CreateCompany("com1", 100);
            services.CreateCompany("com2", 120, services.FindCompany("com1"));
            Company com1 = services.FindCompany("com1");
            services.DeleteCompany(com1);
            Company com2;
            try
            {
                com2 = services.FindCompany("com2");

            }
            catch
            {
                com2 = null;
            }
            Assert.IsNull(com2);
        }

        [TestMethod]
        public void ServiceCheckLoadSecondShildren()
        {
            services.CreateCompany("com1", 100);
            services.CreateCompany("com2", 120, services.FindCompany("com1"));
            services.CreateCompany("com3", 140, services.FindCompany("com2"));
            bool result = false;
            Company com3 = services.FindCompany("com3");
            if (com3.FatherCompany != null & com3.FatherCompany.FatherCompany != null)
                result = true;

            services.DeleteCompany(services.FindCompany("com1"));
            Assert.IsTrue(result);
            
        }
    }
}
