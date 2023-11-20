using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static UniversalCourt.Domain.Enum.Enum;

namespace UniversalCourt.Domain.Models
{

    public class Profile : IdentityUser
    {
        [Display(Name = "Unique ID")]
        public string? UniqueId { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

         [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [Display(Name = "Gender")]
        public GenderStatus Gender { get; set; }

         [Display(Name = "Address")]
        public string? Address { get; set; }

         [Display(Name = "Permanent Home Address")]
        public string? PermanentHomeAddress { get; set; }

         [Display(Name = "Permanent LGA")]
        public string? PermanentLga { get; set; }

        [Display(Name = "Permanent State")]
        public string? PermanentState { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "State")]
        public string? State { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }


        [Display(Name = "CV")]
        public string? CVFilePathUrl { get; set; }
        public string? CVFilePathKey { get; set; }

        [Display(Name = "Passport")]
        public string? PassportFilePathUrl { get; set; }
        public string? PassportFilePathKey { get; set; }



        [Display(Name = "Nationality")]
        public string? Nationality { get; set; }

        [Display(Name = "Marital Status")]
        public string? MaritalStatus { get; set; }

        [Display(Name = "Emergency Contact Name")]
        public string? EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact Number")]
        public string? EmergencyContactNumber { get; set; }

        [Display(Name = "Emergency Contact Relationship")]
        public string? EmergencyContactRelationship { get; set; }

         public string GetFullNameWithTitle()
        {
             string fullName = string.Empty;

            if (!string.IsNullOrEmpty(Title))
            {
                fullName += Title;
            }

            

            if (!string.IsNullOrEmpty(FirstName))
            {
                fullName += FirstName;
            }

            if (!string.IsNullOrEmpty(MiddleName))
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    fullName += " ";
                }
                fullName += MiddleName;
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    fullName += " ";
                }
                fullName += LastName;
            }

            return fullName;
        }


        public string GetFullName()
        {
            string fullName = string.Empty;

            if (!string.IsNullOrEmpty(FirstName))
            {
                fullName += FirstName;
            }

            if (!string.IsNullOrEmpty(MiddleName))
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    fullName += " ";
                }
                fullName += MiddleName;
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    fullName += " ";
                }
                fullName += LastName;
            }

            return fullName;
        }
        [Display(Name = "FullName")]
        public string FullnameX
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }
        public long? JobDesignationId { get; set; }
        public JobDesignation JobDesignation { get; set; }

        public string? RefCode { get; set; }


        public string? Biography { get; set; }

        [Display(Name = "Alternative Phone Number")]
        public string? AltPhoneNumber { get; set; }
        [Display(Name = "Office Email")]
        public string? OfficeEmail { get; set; }
        [Display(Name = "Website Address")]
        public string? WebsiteAddress { get; set; }
         

        [Display(Name = "Date Of Birth Status")]
        public DOBStatus DateOfBirthStatus { get; set; }


         [Display(Name = "Contact Mail Email")]
        public string? ContactMailEmail { get; set; }
        public bool PortfolioProfile { get; set; }


        public long? UserCategoryId { get; set; }
        public UserCategory UserCategory { get; set; }
    }

}
