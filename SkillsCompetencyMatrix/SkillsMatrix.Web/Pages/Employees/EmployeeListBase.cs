using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Extensions;
using Skclusive.Blazor.Dashboard.App.View.Common;
using Skclusive.Blazor.Dashboard.App.View.Services;
using SkillsMatrix.Models;
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

        public IEnumerable<PersonalInfo> Employees { set; get; }

        public string EmployeeNameSearch { set; get; }

        public bool SelectAll { set; get; }

        protected override async Task OnInitializedAsync()
        {

            Employees = await PersonService.GetAllEmployees();
        }

        protected void HandleValidSubmit()
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

    }




}
