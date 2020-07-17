using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SurveyAccess
    {
        public string _connectionstring;
        public string _connectionstringName = "SurveyManagement";
        public SqlException _lastSqlException;

        public SurveyAccess()
        {
            _connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings[_connectionstringName].ToString();
        }

		public bool InsertQuestion(string Description, bool AllowYesNo, bool AllowShortAnswer)
		{
			Common.ConnectionManagement myconnector = new Common.ConnectionManagement(_connectionstring);
			List<SqlParameter> procParams = new List<SqlParameter>();
			procParams.Add(new SqlParameter("@Description", Description));
			procParams.Add(new SqlParameter("@AllowYesNo", AllowYesNo));
			procParams.Add(new SqlParameter("@AllowShortAnswer", AllowShortAnswer));
			Common.SQLQueryResult queryRes = myconnector.ExecuteNonQueryProcedure("srvy.InsertQuestion", procParams, 600);
			_lastSqlException = queryRes.SqlError;
			if (queryRes.SqlError != null)
				return false;
			else
				return true;

		}
	}
}
