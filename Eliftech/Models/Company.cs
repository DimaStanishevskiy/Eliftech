using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Eliftech.Models
{
    [Table("Companies")]
    public class Company
    {
        public int Id { set; get; }

        [Required]
        public string Name { set; get; }
        [NotMapped]
        private int estimatedEarnings = 0;
        [Required]
        public int EstimatedEarnings
        {
            set
            {
                ChangeEstimatedEarnings(this.estimatedEarnings, value);
                estimatedEarnings = value;
            }
            get
            {
                return estimatedEarnings;
            }
        }

        [NotMapped]
        public int FullEstimatedEarnings { private set; get; } = 0;

        [NotMapped]
        private Company fatherCompany = null;
        [JsonIgnore]
        public Company FatherCompany {
            set
            {
                if (fatherCompany != null)
                    fatherCompany.ChangeEstimatedEarnings(FullEstimatedEarnings, 0);
                fatherCompany = value;
                if (fatherCompany != null)
                    fatherCompany.ChangeEstimatedEarnings(0, FullEstimatedEarnings);
            }
            get
            {
                return fatherCompany;
            }
        }

        public virtual List<Company> ChildrenCompanies { set; get; }

        
        private void ChangeEstimatedEarnings(int oldValue, int newValue)
        {
            this.FullEstimatedEarnings += newValue - oldValue;
            if (FatherCompany != null)
                FatherCompany.ChangeEstimatedEarnings(oldValue, newValue);
        }

        public Company()
        { }

        public Company(string Name, int EstimatedEarnings)
        {
            this.Name = Name;
            this.FullEstimatedEarnings = 0;
            this.EstimatedEarnings = EstimatedEarnings;
        }

        public Company(string Name, int EstimatedEarning, Company FatherCompany) : this(Name, EstimatedEarning)
        {
            this.FatherCompany = FatherCompany;
        }
    }
}