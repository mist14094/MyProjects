using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JarvisDataAccess;
using NLog;

namespace JarvisBusinessAccess
{
    public class JarvisBl
    {

        internal Logger nlog = LogManager.GetCurrentClassLogger();
        CreateReport cr = new CreateReport();

        public DataSet GetViewandProcedureName()
        {
            nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                return cr.GetViewandProcedureName();
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }

        }

        public DataSet GetSampleData(string strObjectName, string strObjectType)
        {
            nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {

                if (strObjectType == "V")
                {
                    return cr.GetSampleDataView(strObjectName);
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                nlog.Error("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }

        }

        public DataSet GetViewColumnName(string strViewName)
        {
            nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                return cr.GetViewColumnName(strViewName);
            }

            catch (Exception ex)
            {
                nlog.Error("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }

            finally
            {
                nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public void GetSampleDataView(string ViewName)
        {
            nlog.Trace("JarvisBusinessAccess:JarvisBl:GetViewandProcedureName::Entering");
            try
            { }

            catch (Exception ex)
            {
                nlog.Error("JarvisBusinessAccess:JarvisBl:GetViewandProcedureName::Error", ex);
                throw ex;
            }

            finally
            {
                nlog.Trace("JarvisBusinessAccess:JarvisBl:GetViewandProcedureName::Leaving");
            }


        }

        public DataSet GetInitialDataBaseNames()
        {
            nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                return cr.GetInitialDataBaseNames();
            }

            catch (Exception ex)
            {
                nlog.Error("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }

            finally
            {
                nlog.Trace("JarvisBusinessAccess:JarvisBl:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

    }
}
