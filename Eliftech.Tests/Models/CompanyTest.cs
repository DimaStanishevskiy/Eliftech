using System;
using Eliftech.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Eliftech.Tests.Models
{
    [TestClass]
    public class CompanyTest
    {
        private Company father;
        private Company children1Generation;
        private Company children2Generation;

        [TestInitialize]
        public void SetupContext()
        {
            father = new Company("father", 100);
            children1Generation = new Company("children1", 80, father);
            children2Generation = new Company("children2", 120, children1Generation);
        }

        [TestMethod]
        public void CreateCompany()
        {
            Assert.AreEqual(children2Generation.EstimatedEarnings, children2Generation.FullEstimatedEarnings);
        }

        [TestMethod]
        public void ChangeEstimatedEarnings()
        {
            children2Generation.EstimatedEarnings = 40;
            Assert.AreEqual(children2Generation.FullEstimatedEarnings, 40);
        }
        [TestMethod]
        public void FirstGenerationSum()
        {
            int firstGenerationSum = children1Generation.EstimatedEarnings + children2Generation.EstimatedEarnings;
            Assert.AreEqual(children1Generation.FullEstimatedEarnings, firstGenerationSum);
        }
        [TestMethod]
        public void SecondGenerationSum()
        {
            int secondGenerationSum 
                = father.EstimatedEarnings
                + children1Generation.EstimatedEarnings
                + children2Generation.EstimatedEarnings;
            Assert.AreEqual(father.FullEstimatedEarnings, secondGenerationSum);
        }
        [TestMethod]
        public void DeleteFather()
        {
            children1Generation.FatherCompany = null;
            Assert.AreEqual(father.FullEstimatedEarnings, 100);
        }
        [TestMethod]
        public void ChangeFather()
        {
            int oldSum = father.FullEstimatedEarnings;
            children2Generation.FatherCompany = father;
            Assert.AreEqual(oldSum, father.FullEstimatedEarnings);
        }
    }
}
