using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL.DTO;
using EagleEye.Common;

namespace EagleEye.BLL
{
    public class BLLEmployee
    {
        DALEmployee objDAL = new DALEmployee();

        public List<Employee_P> GetAllEmployees()
        {

            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetAllEmployees();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public List<EmployeeDTO> GetAllEmployeeDTO(JqueryDatatableParam param, string locationFilter, string statusFilter, out int totalRecords)
        {
            totalRecords = 0;
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            try
            {
                list = objDAL.GetAllEmployeeDTO(param, locationFilter, statusFilter, out totalRecords);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public Employee_P GetEmployeeByCode(int Code)
        {

            Employee_P emp = new Employee_P();
            try
            {
                emp = objDAL.GetEmployeeByCode(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;
        }

        public int RestoreUsersList()
        {
            int count = 0;
            try
            {
                var empCount = objDAL.RestoreUsersList();
                count = empCount;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return count;
        }
        public List<Employee_P> GetEmployeeByCodes(string[] codes)
        {

            List<Employee_P> emp = new List<Employee_P>();
            try
            {
                emp = objDAL.GetEmployeeByCodes(codes);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;

        }
        public int GetEmployeeCount()
        {

            int Count = 0;
            try
            {
                Count = objDAL.GetEmployeeCount();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return Count;

        }
        public List<Employee_P> GetAllUserIds(string devID)
        {
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetAllUserIds(devID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }
        public List<Employee_P> GetEmployeeList(string[] list, string deviceID)
        {
            List<Employee_P> lists = new List<Employee_P>();
            try
            {
                lists = objDAL.GetEmployeeList(list, deviceID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return lists;

        }
        public List<Employee_P> GetAllDeletedUsers()
        {

            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetAllDeletedUsers();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public Employee_P GetEmployeeByID(string Id)
        {

            Employee_P emp = new Employee_P();
            try
            {
                emp = objDAL.GetEmployeeByID(Id);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;

        }

        public Employee_P GetEmployeeByID(string Id, string empName)
        {

            Employee_P emp = new Employee_P();
            try
            {
                emp = objDAL.GetEmployeeByID(Id, empName);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;

        }

        public Employee_P GetEmployeeByIDIsDeleted(string Id)
        {

            Employee_P emp = new Employee_P();
            try
            {
                emp = objDAL.GetEmployeeByID(Id);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;

        }

        public List<Employee_P> GetEmployeeBydevID(string devID)
        {
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetEmployeeBydevID(devID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public List<Employee_P> GetExistingEmployee(string user_id, string Card_No, string Password)
        {

            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetExistingEmployee(user_id, Card_No, Password);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public bool CheckRealEmployeeExistInDB(string CODE, string Device_ID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckRealEmployeeExistInDB(CODE, Device_ID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool checkEmployeeIsNotDeleted(string empID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.checkEmployeeIsNotDeleted(empID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool AddUpdateEmployee(Employee_P employee)
        {

            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }
        public bool UploadEmployee(Employee_P employee, Employee_P encemployee)
        {

            bool flag = false;
            try
            {
                flag = objDAL.UploadEmployee(employee, encemployee);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }

        public bool TransferByMachine(Employee_P employee)
        {

            bool flag = false;
            try
            {
                flag = objDAL.TransferByMachine(employee);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }





        public bool DeleteEmployee(string Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteEmployee(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool RestoreUserFromDB(string Code, int action)
        {
            bool flag = false;
            try
            {
                flag = objDAL.RestoreUserFromDB(Code, action);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool RestoreUserFromDB(string Code, string empName, int action)
        {
            bool flag = false;
            try
            {
                flag = objDAL.RestoreUserFromDB(Code, empName, action);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteEmployeeByDevice_ID(string deviceID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteEmployeeByDevice_ID(deviceID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return flag;
        }
        public bool DeleteEmployeeByCode(string deviceID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteEmployeeByCode(deviceID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return flag;
        }
        public bool IsDeleteEmployee(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.IsDeleteEmployee(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }


        public bool CheckEmployeeExistInDB(string CODE)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckEmployeeExistInDB(CODE);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckEmployeeExistInDB(string CODE, out string Employee_Name)
        {
            bool flag = false;
            Employee_Name = "";
            try
            {
                flag = objDAL.CheckEmployeeExistInDB(CODE, out Employee_Name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckPasswordExistInDB(string PWD, string CODE)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckPasswordExistInDB(PWD, CODE);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckCardExistInDB(string CardNo, string CODE)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckCardExistInDB(CardNo, CODE);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckCardExistInDB(string CardNo, out Employee_P employee)
        {
            bool flag = false;
            employee = new Employee_P();
            try
            {
                flag = objDAL.CheckCardExistInDB(CardNo, out employee);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckUserExistInDB(string CODE, out Employee_P employee)
        {
            bool flag = false;
            employee = new Employee_P();
            try
            {
                flag = objDAL.CheckUserExistInDB(CODE, out employee);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckPwdExistInDB(string Pwd, out Employee_P employee)
        {
            bool flag = false;
            employee = new Employee_P();
            try
            {
                flag = objDAL.CheckPwdExistInDB(Pwd, out employee);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public Employee_P GetIrregularEmployeeByCode(int Code)
        {

            Employee_P emp = new Employee_P();
            try
            {
                emp = objDAL.GetIrregularEmployeeByCode(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;

        }

        //Using in Restore method in UserController
        public bool CheckCardExistInDB(string emp_ID, out int existingEmpID)
        {
            bool flag = false;
            existingEmpID = 0;
            try
            {
                flag = objDAL.CheckCardExistInDB(emp_ID, out existingEmpID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }



        //Report
        public List<Employee_P> GetEmployeesWithAttCount(string dt)
        {

            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetEmployeesWithAttCount(dt);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }


        public List<Employee_P> GetEmployeesWParam(JqueryDatatableParam param, string devices, out int TotalRecords, out int FilteredRecords)
        {
            TotalRecords = 0;
            FilteredRecords = 0;
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = objDAL.GetEmployeesWParam(param, devices, out TotalRecords, out FilteredRecords);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }


        public List<string> GetRecordsByFilter(string loc, string status)
        {


            List<string> Codes = new List<string>();
            try
            {
                Codes = objDAL.GetRecordsByFilter(loc, status);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return Codes;


        }
    }
}