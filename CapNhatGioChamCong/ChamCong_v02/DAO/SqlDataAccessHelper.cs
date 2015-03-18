using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using ChamCong_v02.DTO;
using ChamCong_v02.BUS;
using log4net;
using log4net.Core;

namespace ChamCong_v02.DAO {
	public class SqlDataAccessHelper {
		#region ConnectionString

		public static string ConnectionString { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <exception cref="SqlException">lỗi Sql</exception>
		/// <exception cref="Exception">lỗi đọc file</exception>
		public static void ReadConnectionFile(string fileName) {
			SqlConnection sqlConnection = new SqlConnection();
			try {
				StreamReader file = File.OpenText(fileName);
				string s = file.ReadToEnd();
				string tempNewConnectionString = MyUtility.giaima(s);
				sqlConnection = new SqlConnection(tempNewConnectionString);
				sqlConnection.Open();
				ConnectionString = tempNewConnectionString;
			} catch (Exception) {
				sqlConnection.Close();
				throw;
			} finally {
				sqlConnection.Close();
			}
		}
		public static string ReadEncryptConnectionString1(string fileName) {
			string kq = string.Empty, temp = string.Empty;
			try {
				StreamReader file = File.OpenText(fileName);
				temp = file.ReadToEnd();
				kq = MyUtility.giaima(temp);
			} catch (Exception ex) {
				throw ex;
			}
			return kq;
		}
		#endregion

		#region ExecuteQuery
		/// <summary>
		/// execute store procedure co tham so truyen vao
		/// </summary>
		/// <param name="spName">ten store procedure</param>
		/// <param name="sqlParams">danh sach tham so truyen vao store procedure</param>
		/// <returns>DataTable</returns>
		public static DataTable ExecuteQuery(String spName, List<SqlParameter> sqlParams) {
			DataTable dt = new DataTable();
			try {
				SqlConnection connect = new SqlConnection(SqlDataAccessHelper.ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = spName;
					if (sqlParams != null) {
						foreach (SqlParameter param in sqlParams) {
							command.Parameters.Add(param);
						}
					}
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = command;
					adapter.Fill(dt);

				} catch (SqlException ex) {
					throw ex;
				} finally {
					connect.Close();
				}
			} catch (Exception ex) {
				throw ex;
			}
			return dt;
		}

		/// <summary>
		/// execute store procedure KO tham so truyen vao
		/// </summary>
		/// <param name="spName">ten store procedure</param>
		/// <returns>DataTable</returns>
		public static DataTable ExecuteQuery(String spName) {
			return ExecuteQuery(spName, null);
		}

		/// <summary>
		/// execute store procedure voi danh sach tham so truyen vao
		/// </summary>
		/// <param name="spName">ten store procedure</param>
		/// <param name="sqlParams">danh sach tham so truyen vao store procedure</param>
		/// <returns>DataSet</returns>
		public static DataSet ExecuteQueryReturnDataSet(String spName, List<SqlParameter> sqlParams) {
			DataSet ds = new DataSet();
			try {
				SqlConnection connect = new SqlConnection(ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = spName;
					if (sqlParams != null) {
						foreach (SqlParameter param in sqlParams) {
							command.Parameters.Add(param);
						}
					}
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = command;
					adapter.Fill(ds);

				} catch (SqlException ex) {
					throw ex;
				} finally {
					connect.Close();
				}
			} catch (Exception ex) {
				throw ex;
			}
			return ds;
		}

		/// <summary>
		/// execute store procedure ko tham so
		/// </summary>
		/// <param name="spName">ten store procedure</param>
		/// <returns>DataSet</returns>
		public static DataSet ExecuteQueryReturnDataSet(String spName) {
			return ExecuteQueryReturnDataSet(spName, null);
		}

		/// <summary>
		/// Truy van dung chuoi sql string
		/// </summary>
		/// <param name="query_string">sql string</param>
		/// <returns>DataTable</returns>
		public static DataTable ExecuteQueryString(string query_string) {
			DataTable dt = new DataTable();
			try {
				SqlConnection connect = new SqlConnection(SqlDataAccessHelper.ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.Text;
					command.CommandText = query_string;

					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = command;
					adapter.Fill(dt);
					adapter.Dispose();
				} catch (SqlException ex) {
					throw ex;
				} finally {
					connect.Close();
				}
			} catch (Exception ex) {
				throw ex;
			}
			return dt;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query_string">Query string</param>
		/// <param name="parameter">Danh sách tham số</param>
		/// <param name="valuecollection">Danh sách giá trị tương ứng tham số</param>
		/// <returns>datatable</returns>InvalidOperationException
		/// <exception cref="SqlException">lỗi SqlException</exception>
		/// <exception cref="IndexOutOfRangeException">kiểm tra lại số lượng danh sách tham số IndexOutOfRangeException</exception>
		public static DataTable ExecuteQueryString(string query_string, string[] parameter, object[] valuecollection) {
			DataTable dt = new DataTable();

			try {
				SqlConnection connect = new SqlConnection(SqlDataAccessHelper.ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.Text;
					command.CommandText = query_string;

					if (parameter != null) {
						for (int i = 0; i < parameter.Length; i++)
							command.Parameters.AddWithValue(parameter[i], valuecollection[i]);
					}

					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = command;
					adapter.Fill(dt);
				} catch (SqlException sqlEx) { throw sqlEx; } finally {
					connect.Close();
				}
			} catch (Exception ex) {
				log4net.Config.XmlConfigurator.Configure();
				ILog lg = LogManager.GetLogger("SQLDataAccessHelper");
				string temp = "ExecuteQueryString: param: query_string='" + query_string + "'";
				if (parameter != null) {
					for (int i = 0; i < parameter.Length; i++)
						temp += parameter[i] + "=" + valuecollection[i];
				}
				lg.Error(temp, ex);
				throw ex;
			}

			return dt;
		}
		#endregion

		#region ExecuteNoneQuery
		/// <summary>
		/// execute store procedure insert/update/delete
		/// </summary>
		/// <param name="spName">ten store procedure</param>
		/// <param name="sqlParams">danh sach tham so truyen vao store procedure</param>
		/// <returns>số dòng bị ảnh hưởng bởi thao tác</returns>
		public static int ExecuteNoneQuery(String spName, List<SqlParameter> sqlParams) {
			int n;
			try {
				SqlConnection connect = new SqlConnection(SqlDataAccessHelper.ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = spName;
					if (sqlParams != null) {
						foreach (SqlParameter param in sqlParams) {
							command.Parameters.Add(param);
						}
					}
					n = command.ExecuteNonQuery();
				} catch {
					return 0;
				} finally {
					connect.Close();
				}
			} catch {
				return 0;
			}

			return n;
		}

		/// <summary>
		/// execute store procedure ko truyen tham so
		/// </summary>
		/// <param name="spName">ten store procedure</param>
		/// <returns>số dòng bị ảnh hưởng bởi thao tác</returns>
		public static int ExecuteNoneQuery(String spName) {
			return ExecuteNoneQuery(spName, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="executeString"></param>
		/// <param name="parameter"></param>
		/// <param name="valuecollection"></param>
		/// <returns>int n: kết quả số lượng dòng bị ảnh hưởng bởi thao tác</returns>
		public static int ExecNoneQueryString(string executeString, string[] parameter, object[] valuecollection) {
			int n = 0;
			try {
				SqlConnection connect = new SqlConnection(SqlDataAccessHelper.ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.Text;
					command.CommandText = executeString;

					if (parameter != null) {
						for (int i = 0; i < parameter.Length; i++)
							command.Parameters.AddWithValue(parameter[i], valuecollection[i]);
					}

					n = command.ExecuteNonQuery();
				} catch (Exception exception) {
					log4net.Config.XmlConfigurator.Configure();
					ILog lg = LogManager.GetLogger("DAL");
					string temp = "start\n";
					temp += " executeString = " + executeString;
					if (parameter != null)
						for (int i = 0; i < parameter.Length; i++) {
							temp += "\n" + parameter[i] + "=" + valuecollection[i] + ";";
						}
					temp += "\n" + exception.StackTrace + "end";
					lg.Fatal(temp);
					return 0;
				} finally { connect.Close(); }
			} catch {
				return 0;
			};

			return n;
		}
		#endregion


		/// <summary>
		/// Kiểm tra có kết nối được cơ sở dữ liệu hay không
		/// </summary>
		/// <returns>true if successful</returns>
		public static bool TestConnection(string pConnectionString) {
			bool kq = false;
			SqlConnection connection = new SqlConnection(pConnectionString);
			try {
				connection.Open();
				kq = true;
			} catch (Exception) {
				//MyUtility.XuLyException(ex);
				kq = false;
			} finally {
				connection.Close();
			}
			return kq;
		}

		public static object ExecuteScalar(string spName, List<SqlParameter> sqlParams) {
			object obj = null;
			try {
				SqlConnection connect = new SqlConnection(SqlDataAccessHelper.ConnectionString);
				connect.Open();
				try {
					SqlCommand command = connect.CreateCommand();
					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = spName;
					if (sqlParams != null) {
						foreach (SqlParameter param in sqlParams) {
							command.Parameters.Add(param);
						}
					}
					obj = command.ExecuteScalar();
				} catch {
					return null;
				} finally {
					connect.Close();
				}
			} catch {
				return null;
			}
			return obj;
		}

		public static object ExecuteScalar(string spName) {
			return ExecuteScalar(spName, null);
		}

	}


}
