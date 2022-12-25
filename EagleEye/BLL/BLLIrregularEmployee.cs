using System;
using System.Collections.Generic;
using System.Linq;
using EagleEye.DAL.Partial;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL;
using EagleEye.Common;

namespace EagleEye.BLL
{
    public class BLLIrregularEmployee
    {
        DALIrregularEmployee objDAL = new DALIrregularEmployee();

        public List<IrregularEmployee_P> GetAllEmployees()
        {

            List<IrregularEmployee_P> list = new List<IrregularEmployee_P>();
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

        public bool DeleteEmployee(int Code)
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

        public IrregularEmployee_P GetIrrEmployee(string Code)
        {
            IrregularEmployee_P emp = new IrregularEmployee_P();
            try
            {
                emp = objDAL.GetIrrEmployee(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;
        }
        public bool AddUpdateIrrEmployee(string Msg, int Code)
        {
            bool flag = false;
            IrregularEmployee_P emp = new IrregularEmployee_P();
            try
            {
                emp.Msg = Msg;
                emp.Code = Code;
                flag = objDAL.AddUpdateIrrEmployee(emp);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public List<IrregularEmployee_P> GetDataTable(JqueryDatatableParam param, out int TotalRecords)
        {
            List<IrregularEmployee_P> list = new List<IrregularEmployee_P>();
            TotalRecords = 0;

            try
            {
                list = objDAL.GetDataTable(param, out TotalRecords);

            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return list;
        }

        public IrregularEmployee_P GetIrrEmployeeByEmpID(string emp_id)
        {
           
            IrregularEmployee_P emp = new IrregularEmployee_P();
            try
            {

                emp = objDAL.GetIrrEmployeeByEmpID(emp_id);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return emp;
        }
    }

}