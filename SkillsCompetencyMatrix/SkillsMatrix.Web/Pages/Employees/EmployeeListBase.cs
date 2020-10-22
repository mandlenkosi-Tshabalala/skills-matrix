using Microsoft.AspNetCore.Components;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.Employees
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        public IPersonExpertiseService PersonExpertiseService { get; set; }

        [Inject]
        public IPersonCompetencies PersonCompetenciesService { get; set; }

        public IEnumerable<PersonalInfo> Employees { set; get; }

        public string EmployeeName { set; get; }

        public bool SelectAll { set; get; }

        public string expertiseID { get; set; }

        public string competencyID { get; set; }

        public IEnumerable<Expertise> functionalList { set; get; }

        public IEnumerable<Competency> competencyList { set; get; }

        protected override async Task OnInitializedAsync()
        {

            Employees = await PersonService.GetAllEmployees("", 0, 0);
           // functionalList = await PersonExpertiseService.GetExpertiseCategories();
           // competencyList = await PersonCompetenciesService.GetCompetencies();

        }

        protected async Task Search()
        {
            Employees = await PersonService.GetAllEmployees(EmployeeName, Convert.ToInt32(expertiseID), Convert.ToInt32(competencyID));

            if (Employees.Count() == 0)
            {
                Employees = null;
            }

            EmployeeCheckedList.Clear();
        }

        protected void DownloadCV()
        {
        }

        public List<int> EmployeeCheckedList { get; set; } = new List<int>();
        protected void empSelect(int empID, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if(EmployeeCheckedList.Count == Employees.Count())
                {
                    SelectAll = true;
                }
                    
                if (!EmployeeCheckedList.Contains(empID))
                {
                    EmployeeCheckedList.Add(empID);
                }
            }
            else
            {
                SelectAll = false;

                if (EmployeeCheckedList.Contains(empID))
                {
                    EmployeeCheckedList.Remove(empID);
                }
            }
        }

        protected void empSelectAll(object checkedValue)
        {
            
            if ((bool)checkedValue)
            {
                foreach (var item in Employees)
                {
                    if (!EmployeeCheckedList.Contains(item.Id))
                    {
                        EmployeeCheckedList.Add(item.Id);
                    }
                }

                SelectAll = true;
            }
            else
            {
                foreach (var item in Employees)
                {
                    if (EmployeeCheckedList.Contains(item.Id))
                    {
                        EmployeeCheckedList.Remove(item.Id);
                    }
                }

                SelectAll = false;
            }

            this.StateHasChanged();
        }

        protected bool empCheckSelectStatus(int empID)
        {
            if (EmployeeCheckedList.Contains(empID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void functionalClicked(object functional)
        {
            expertiseID = functional.ToString();
        }

        protected void competencyClicked(object competency)
        {
            competencyID = competency.ToString();
        }

    }




}
