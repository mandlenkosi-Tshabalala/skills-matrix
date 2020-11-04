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
        public IExpertiseService ExpertiseService { get; set; }

        [Inject]
        public ICompetenciesCategoryService CompetenciesCategoryService { get; set; }

        [Inject]
        public ICompetenciesService CompetenciesService { get; set; }

        public IEnumerable<PersonalInfo> Employees { set; get; }

        public string EmployeeName { set; get; }

        public string Skills { set; get; }

        public string QualificationLevel { set; get; }

        public string Country { set; get; }

        public bool SelectAll { set; get; }

        public string expertiseID { get; set; }

        public string competencyCategoryID { get; set; }

        public string competencyID { get; set; }

        public IEnumerable<Expertise> functionalList { get; set; }

        public IEnumerable<CompetencyCategory> competencyCategoryList { get; set; }

        protected List<Competency> competencies { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {

            Employees = await PersonService.GetAllEmployees("", 0, 0,"","","");
            functionalList = await ExpertiseService.GetAll();
            competencyCategoryList = await CompetenciesCategoryService.GetCompetencies();

        }

        protected async Task Search()
        {
            Employees = await PersonService.GetAllEmployees(EmployeeName, Convert.ToInt32(expertiseID), Convert.ToInt32(competencyCategoryID), Skills, QualificationLevel,Country);

            if (Employees.Count() == 0)
            {
                Employees = null;
            }

            EmployeeCheckedList.Clear();
        }

        protected void DownloadCV()
        {
        }

        protected void ViewCV(int ViewUserID)
        {

            NavigationManager.NavigateTo($"/viewCV/" + ViewUserID.ToString());
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

        protected async void competencyCategoryClicked(object competencyCategory)
        {
            competencyCategoryID = competencyCategory.ToString();
            competencies = await CompetenciesService.GetAll(Convert.ToInt32(competencyCategoryID));
            this.StateHasChanged();
        }

    }




}
