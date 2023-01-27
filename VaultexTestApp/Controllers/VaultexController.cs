using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Vaultex.DataAccess;
using VaultexTestApp.Models;
using Microsoft.EntityFrameworkCore;
using VaultexTestApp.ViewModels;
using System.IO;
using ExcelDataReader;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace VaultexTestApp.Controllers
{
    public class VaultexController : Controller
    {
        private IWebHostEnvironment Environment;
        private string excelFile;
        private DataTableCollection tableCollection;

        public readonly AppDBContext context;

        public VaultexController(AppDBContext context, IWebHostEnvironment _environment)
        {
            this.context = context;
            this.Environment = _environment;
            excelFile = _environment.WebRootPath + "\\SpreadSheet\\VaultexData.xlsx";
        }

        private void AddOrganisation(Organisations organisation)
        {
            context.Organisations.Add(organisation);
            context.SaveChanges();
        }
        //Empty existing data to prevent duplications and errors.
        private void DeleteAllTableContent(string sTableName)
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [" + sTableName + "]");
        }
        public void AddEmployee(Employees employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }
        private void readExcelSheetData()
        {
            DataTable dtOrganisations = tableCollection["Organisation"];

            if (dtOrganisations != null)
            {
                DeleteAllTableContent("Organisations");
                //Loop through all the rows in the organisation excel sheet.  Start at row 1 because 0 is the column headers
                for (int i = 1; i < dtOrganisations.Rows.Count; i++)
                {
                    Organisations organisation = new Organisations();
                    organisation.OrganisationNumber = dtOrganisations.Rows[i][1].ToString();
                    organisation.OrganisationName = dtOrganisations.Rows[i][0].ToString();
                    organisation.AddressLine1 = dtOrganisations.Rows[i][2].ToString();
                    organisation.AddressLine2 = dtOrganisations.Rows[i][3].ToString();
                    organisation.AddressLine3 = dtOrganisations.Rows[i][4].ToString();
                    organisation.AddressLine4 = dtOrganisations.Rows[i][5].ToString();
                    organisation.Town = dtOrganisations.Rows[i][6].ToString();
                    organisation.PostCode = dtOrganisations.Rows[i][7].ToString();
                    AddOrganisation(organisation);
                }
            }

            dtOrganisations.Dispose();

            DataTable dtEmployees = tableCollection["Employee"];

            if (dtEmployees != null)
            {
                DeleteAllTableContent("Employees");
                //Loop through all the rows in the employee excel sheet. Start at row 1 because 0 is the column headers
                for (int i = 1; i < dtEmployees.Rows.Count; i++)
                {
                    Employees employee = new Employees();
                    employee.OrganisationNumber = dtEmployees.Rows[i][0].ToString();
                    employee.FirstName = dtEmployees.Rows[i][1].ToString();
                    employee.LastName = dtEmployees.Rows[i][2].ToString();
                    AddEmployee(employee);
                }
            }

            dtEmployees.Dispose();
        }

        public void ImportData()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (System.IO.File.Exists(excelFile))
            {
                using (var stream = System.IO.File.Open(excelFile, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        tableCollection = reader.AsDataSet().Tables;
                        readExcelSheetData();
                    }
                }            
            }
        }
        public IActionResult Index()
        {
            ImportData();
            List<Organisations> organisations = context.Organisations.ToList();
            List<Employees> employees = context.Employees.ToList();
            var tableData = from org in organisations
                            join emp in employees on org.OrganisationNumber equals emp.OrganisationNumber into joinedData
                            from emp in joinedData.DefaultIfEmpty()
                            orderby org.OrganisationNumber
                            select new EmployeesViewModel { organisations = org, employees = emp };

            return View(tableData);
        }
    }
}
