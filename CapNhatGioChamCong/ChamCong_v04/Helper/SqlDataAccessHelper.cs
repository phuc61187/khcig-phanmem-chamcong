using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.Helper {
	public class SqlDataAccessHelper {
		#region ConnectionString

		public static string ConnectionString;

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
		/// <param name="CanLog">Cho phép ghi log hay ko</param>
		/// <returns>datatable</returns>InvalidOperationException
		/// <exception cref="SqlException">lỗi SqlException</exception>
		/// <exception cref="IndexOutOfRangeException">kiểm tra lại số lượng danh sách tham số IndexOutOfRangeException</exception>
		public static DataTable ExecuteQueryString(string query_string, string[] parameter, object[] valuecollection, bool CanLog = true) {
			DataTable dt = new DataTable();
			SqlConnection 	connect = new SqlConnection(ConnectionString);
			try {
				connect.Open();
				SqlCommand command = connect.CreateCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = query_string;

				if (parameter != null) {
					for (int i = 0; i < parameter.Length; i++)
						command.Parameters.AddWithValue(parameter[i], valuecollection[i]);
				}

				SqlDataAdapter adapter = new SqlDataAdapter { SelectCommand = command };
				adapter.Fill(dt);

			} catch (Exception ex) {
				log4net.Config.XmlConfigurator.Configure();
				ILog lg = LogManager.GetLogger("SQLDataAccessHelper");
				string chuoi = string.Empty;
				//Debug.Assert(parameter != null, "parameter != null");
				if (parameter != null)
				{
					for (int i = 0; i < parameter.Length; i++)
					chuoi += parameter[i] + "=" + valuecollection[i];
				}
				if (CanLog)
					lg.Error(string.Format("ExecuteQueryString=[{0}]; chuoi=[{1}]\n", query_string, chuoi), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			} finally {
				connect.Close();
			}

			return dt;
		}
		#endregion

		#region ExecuteNoneQuery


		/// <summary>
		/// 
		/// </summary>
		/// <param name="executeString"></param>
		/// <param name="parameter"></param>
		/// <param name="valuecollection"></param>
		/// <param name="CanLog">Cho phép ghi log hay ko</param>
		/// <returns>int n: kết quả số lượng dòng bị ảnh hưởng bởi thao tác</returns>
		public static int ExecNoneQueryString(string executeString, string[] parameter, object[] valuecollection, bool CanLog = true) {
			int n = 0;
			SqlConnection connect = new SqlConnection(ConnectionString);
			try {
				connect.Open();
				SqlCommand command = connect.CreateCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = executeString;

				if (parameter != null) {
					for (int i = 0; i < parameter.Length; i++)
						command.Parameters.AddWithValue(parameter[i], valuecollection[i]);
				}

				n = command.ExecuteNonQuery();
			} catch (Exception ex) {
				log4net.Config.XmlConfigurator.Configure();
				ILog lg = LogManager.GetLogger("SQLDataAccessHelper");

				string chuoi = string.Empty;
				for (int i = 0; i < parameter.Length; i++)
					chuoi += parameter[i] + "=" + valuecollection[i];
				if (CanLog)
					lg.Error(string.Format("ExecuteQueryString=[{0}]; chuoi=[{1}]\n", executeString, chuoi), ex);

				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
				return 0;
			} finally { connect.Close(); }

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
			} catch (Exception exception) {
				log4net.Config.XmlConfigurator.Configure();
				ILog lg = LogManager.GetLogger("DAL");
				lg.Error("TestConnectionFail\n", exception);
				kq = false;
			} finally {
				connection.Close();
			}
			return kq;
		}





		#region ExecuteQuery
		public static DataTable ExecSPQuery(String spName, params SqlParameter[] sqlParams) {
			DataTable dt = new DataTable();
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


		#endregion

		#region ExecuteNoneQuery
		public static int ExecSPNoneQuery(String spName, params SqlParameter[] sqlParams) {
			int n;
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
		public static int ExecNoneSPQuery(String spName) {
			return ExecSPNoneQuery(spName, null);
		}
		#endregion

	}


}
