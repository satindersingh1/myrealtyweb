using MyRealtyWeb.Data.Common;
using MyRealtyWeb.Data.DAL;
using MyRealtyWeb.Data.DataModel;
using MyRealtyWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRealtyWeb.Data.Access.Website
{
    public class RequestFromServices
    {

        #region private varaiable
        DalFunctions objDal;
        CommonFunctions objCom;
        MyRealtyWeb.Data.Common.ClsConnection objCon;
        ClsConState.clsConnectionState objState;
        #endregion

        public RequestFromModel SaveRequest(RequestFromModel RFM, string mode)
        {
            objDal = new DalFunctions();
            objCon = new MyRealtyWeb.Data.Common.ClsConnection();
            objCom = new CommonFunctions();
            SqlParameter[] sqlParams = {
                          new SqlParameter("@Id",SqlDbType.Int){Value = 1},
              new SqlParameter("@FName",SqlDbType.NVarChar){Value = RFM.FName},
              new SqlParameter("@LName",SqlDbType.NVarChar){Value = RFM.LName},
              new SqlParameter("@EmailAddress",SqlDbType.NVarChar){Value = RFM.EmailAddress},
              new SqlParameter("@IsActive",SqlDbType.Bit){Value = false},
              new SqlParameter("@IsDelete",SqlDbType.Bit){Value = false},
              new SqlParameter("@UpdDt",SqlDbType.DateTime){Value = DateTime.Now},
              new SqlParameter("@UpdBy",SqlDbType.NVarChar){Value = ""},
              new SqlParameter("@Mode",SqlDbType.NVarChar){Value = mode},               
           };
            CommandType type = CommandType.StoredProcedure;
            SqlConnection con = objCon.myCon();
            objState = new ClsConState.clsConnectionState(con);
            RequestFromModel objModel = new RequestFromModel();
            DataTable dt = new DataTable();
            using (con)
            {
                dt = objDal.ExecuteReader(con, "Website_InsertRequest", type, sqlParams);
            }
            if (dt.Rows.Count > 0)
            {
                objModel.FName = Convert.ToString(objCom.HandleNull(dt.Rows[0]["FName"]));
                objModel.LName = Convert.ToString(objCom.HandleNull(dt.Rows[0]["LName"]));
                objModel.EmailAddress = Convert.ToString(objCom.HandleNull(dt.Rows[0]["EmailAddress"]));
            }
            return objModel;
        }
    }
}
