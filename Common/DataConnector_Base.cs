using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
	public class ConnectionManagement
	{
		private SqlConnection _connection;
		private string _connectionstring;

		public ConnectionManagement(string sConnectionString)
		{
			_connectionstring = sConnectionString;

		}

		public SQLQueryResult ExecuteNonQueryProcedure(string storedprocedureName, List<SqlParameter> paramList, int timeout_overwrite = 0)
		{
			SQLQueryResult res = new SQLQueryResult();
			_connection = new SqlConnection(_connectionstring);

			try
			{
				SqlCommand objCmd = new SqlCommand();
				objCmd.Connection = _connection;

				objCmd.CommandType = CommandType.StoredProcedure;
				objCmd.CommandText = storedprocedureName;

				if (timeout_overwrite > 0)
					objCmd.CommandTimeout = timeout_overwrite;

				SqlDataAdapter objAdapter = new SqlDataAdapter();
				objAdapter.SelectCommand = objCmd;

				//Parameter List
				foreach (SqlParameter param in paramList)
				{
					try
					{
						if (param.Value != null && param.Value.ToString() != string.Empty)
							objCmd.Parameters.Add(param);
						else
							objCmd.Parameters.Add(new SqlParameter(param.ParameterName, DBNull.Value));
					}
					catch (Exception ex)
					{
					}
				}

				_connection.Open();
				objCmd.ExecuteNonQuery();

				//Checking for output variables
				foreach (SqlParameter param in paramList)
				{
					try
					{
						if (param.Direction == ParameterDirection.Output)
						{
							string paramKey = param.ParameterName.ToString();
							string paramValue = param.Value.ToString();
							res.OutputParams.Add(paramKey, paramValue);
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
			catch (SqlException ex)
			{
				res.SqlError = ex;
			}

			try
			{
				_connection.Close();
			}
			catch (Exception ex)
			{
			}

			return res;
		}

		public SQLQueryResult ExecuteQueryProcedure(string storedprocedureName, List<SqlParameter> paramList, int timeout_overwrite = 0)
		{
			SQLQueryResult res = new SQLQueryResult();
			_connection = new SqlConnection(_connectionstring);

			try
			{
				SqlCommand objCmd = new SqlCommand();
				objCmd.Connection = _connection;

				objCmd.CommandType = CommandType.StoredProcedure;
				objCmd.CommandText = storedprocedureName;

				if (timeout_overwrite > 0)
					objCmd.CommandTimeout = timeout_overwrite;

				SqlDataAdapter objAdapter = new SqlDataAdapter();
				objAdapter.SelectCommand = objCmd;

				//Parameter List
				foreach (SqlParameter param in paramList)
				{
					try
					{
						if (param.Value != null && param.Value.ToString() != string.Empty)
							objCmd.Parameters.Add(param);
						else
							objCmd.Parameters.Add(new SqlParameter(param.ParameterName, DBNull.Value));
					}
					catch (Exception ex)
					{
					}
				}

				objAdapter.Fill(res.QueryOutput, "OutputDataSet");

				//Checking for output variables
				foreach (SqlParameter param in paramList)
				{
					try
					{
						if (param.Direction == ParameterDirection.Output)
						{
							string paramKey = param.ParameterName.ToString();
							string paramValue = param.Value.ToString();
							res.OutputParams.Add(paramKey, paramValue);
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
			catch (SqlException ex)
			{
				res.SqlError = ex;
			}

			return res;
		}

	}

	public class SQLQueryResult
	{
		public SqlException SqlError;
		public DataSet QueryOutput;
		public Dictionary<string, string> OutputParams;
		public SqlDataReader DataReader;

		public SQLQueryResult()
		{
			SqlError = null;
			QueryOutput = new DataSet();
			OutputParams = new Dictionary<string, string>();
			DataReader = null;
		}
	}
}