using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eliftech.Models;
using System.Data.Entity;

namespace Eliftech.Services
{
    public class CompanyServices
    {
        private DataContext context;

        public CompanyServices()
        {
            context = new DataContext();
        }

        public List<Company> FindListCompanies()
        {
            //сначала получаем все компании
            var Companies = context.Companies;
            //потом получаем только корневые, остальные будут вложенные
            List<Company> RootCompanies = Companies.Where(n => n.FatherCompany == null).ToList();
            return RootCompanies;
        }

        public Company FindCompany(int Id)
        {
            return context.Companies.Include(t => t.ChildrenCompanies).Where(t => t.Id == Id).FirstOrDefault();
        }

        public Company FindCompany(string Name)
        {
            return context.Companies.Where(t => t.Name == Name).FirstOrDefault();
        }

        public bool CreateCompany(string Name, int EstimatedEarnings, Company FatherCompany = null)
        {
            Company newCompany = new Company(Name, EstimatedEarnings, FatherCompany);
            context.Companies.Add(newCompany);
            int result = context.SaveChanges();
            return result > 0;
        }

        public bool UpdateCompany(Company company)
        {
            return context.SaveChanges() > 0;
        }

        public bool DeleteCompany(Company company)
        {
            RemoveCompany(company);
            return context.SaveChanges() > 0;
        }

        public bool DeleteCompany(int Id)
        {
            Company companyForRemoving = context.Companies.Where(t => t.Id == Id).First();
            RemoveCompany(companyForRemoving);
            return context.SaveChanges() > 0;
        }


        //Костыль: так как запрещенно каскадное удаление при ссылки таблицы на себя: рекурсивный вызов удаления дочерних компаний
        private void RemoveCompany(Company company)
        { 
            foreach (Company item in company.ChildrenCompanies.ToArray())
            {
                RemoveCompany(item);
            }
            context.Companies.Remove(company);
            context.SaveChanges();
        }
    }
     
}