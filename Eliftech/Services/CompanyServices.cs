﻿using System;
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
           var Companies = context.Companies;
            //foreach (Company item in Companies)
            //    Walker(item);
            return Companies.Where(n => n.FatherCompany == null).ToList();
        }

        public Company FindCompany(int Id)
        {
            try
            {
                return context.Companies.Include(t => t.ChildrenCompanies).Where(t => t.Id == Id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Company FindCompany(string Name)
        {
            try
            {
                return context.Companies.Where(t => t.Name == Name).FirstOrDefault();

            }
            catch (Exception e)
            {
                throw e;
            }
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

        private void RemoveCompany(Company company)
        { 
            foreach (Company item in company.ChildrenCompanies.ToArray())
            {
                RemoveCompany(item);
            }
            context.Companies.Remove(company);
            context.SaveChanges();
        }
        public void Walker(Company company)
        {
            foreach (Company childCompany in company.ChildrenCompanies)
            {
                Console.WriteLine(childCompany.Name);
                Walker(childCompany);
            }
        }
    }
     
}