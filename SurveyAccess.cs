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

		//-----------------------------------------
		//-------- Question Table Methods ----------
		//-----------------------------------------
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

		public bool DeleteQuestion(short QuestionID)
		{
			Common.ConnectionManagement myconnector = new Common.ConnectionManagement(_connectionstring);
			List<SqlParameter> procParams = new List<SqlParameter>();
			procParams.Add(new SqlParameter("QuestionID", QuestionID));
			Common.SQLQueryResult queryRes = myconnector.ExecuteNonQueryProcedure("srvy.DeleteQuestion", procParams, 600);
			_lastSqlException = queryRes.SqlError;
			if (queryRes.SqlError != null)
				return false;
			else
				return true;
		}

		//-----------------------------------------
		//-------- Answer Table Methods ----------
		//-----------------------------------------
		public bool InsertAnswer(short QuestionID, bool? YesNo, string Description)
		{
			Common.ConnectionManagement myconnector = new Common.ConnectionManagement(_connectionstring);
			List<SqlParameter> procParams = new List<SqlParameter>();
			procParams.Add(new SqlParameter("@QuestionID", QuestionID));
			procParams.Add(new SqlParameter("@YesNo", YesNo));
			procParams.Add(new SqlParameter("@Description", Description));
			Common.SQLQueryResult queryRes = myconnector.ExecuteNonQueryProcedure("srvy.InsertAnswer", procParams, 600);
			_lastSqlException = queryRes.SqlError;
			if (queryRes.SqlError != null)
				return false;
			else
				return true;
		}

		public bool DeleteAnswer(short AnswerID)
		{
			Common.ConnectionManagement myconnector = new Common.ConnectionManagement(_connectionstring);
			List<SqlParameter> procParams = new List<SqlParameter>();
			procParams.Add(new SqlParameter("@AnswerID", AnswerID));
			Common.SQLQueryResult queryRes = myconnector.ExecuteNonQueryProcedure("srvy.DeleteAnswer", procParams, 600);
			_lastSqlException = queryRes.SqlError;
			if (queryRes.SqlError != null)
				return false;
			else
				return true;
		}
	}
}
