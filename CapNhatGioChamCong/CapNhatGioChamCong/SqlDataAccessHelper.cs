using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace CapNhatGioChamCong {
    public class SqlDataAccessHelper {
        #region ConnectionString

        public static string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="SqlException">lỗi Sql</exception>
        /// <exception cref="Exception">lỗi đọc file</exception>
        public static void ReadConnectionString(string fileName) {
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
                MyUtility.XuLyException(ex);
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
                } catch (SqlException sqlEx) { throw sqlEx; } catch (IndexOutOfRangeException indexOutOfRangeEx) { throw indexOutOfRangeEx; } finally {
                    connect.Close();
                }
            } catch (Exception ex) { throw ex; }

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
                }
                    //[TBD] [important] [bắt đầu khối] khi release , sử dụng khối dưới chứ ko dùng khối này
                    //catch (Exception ex) { connect.Close(); throw ex; };
                    //[TBD] [kết thúc khối]
                    // [TBD] sử dụng khối này thay cho khối trên : 
                catch { return 0; } finally { connect.Close(); }
            }
                //[TBD] [important] [bắt đầu khối] khi release , sử dụng khối dưới chứ ko dùng khối này
                catch (Exception ex) { throw ex; };
            //[TBD] [kết thúc khối]

            // [TBD] sử dụng khối này thay cho khối trên : catch { return 0; }

            return n;
        }
        #endregion


        /// <summary>
        /// Kiểm tra có kết nối được cơ sở dữ liệu hay không
        /// </summary>
        /// <returns>true if successful</returns>
        public static bool TestConnection() {
            SqlConnection connection = new SqlConnection(SqlDataAccessHelper.ConnectionString);
            try {
                connection.Open();
            } catch (Exception ex) {
                MyUtility.XuLyException(ex);
                return false;
            } finally {
                connection.Close();
            }
            return true;
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


        public static DataTable TableGioLVTheoCa;
        public static DataTable TablePhongBan;
        public static DataTable TableDSNV;

        private static List<clsShifts> _dsCa;
        public static List<clsShifts> DSCa {
            set { _dsCa = value; }
            get {
                if (_dsCa != null && _dsCa.Count != 0) return _dsCa;
                if (_dsCa == null || _dsCa.Count == 0) _dsCa = new List<clsShifts>();
                if (TableGioLVTheoCa == null || TableGioLVTheoCa.Rows.Count == 0) TableGioLVTheoCa = ThamSo.GetDataShifts();

                for (int i = 0; i < TableGioLVTheoCa.Rows.Count - 1; i++) {
                    int tempShiftID = (int)TableGioLVTheoCa.Rows[i]["ShiftID"];
                    string tempShiftCode = TableGioLVTheoCa.Rows[i]["ShiftCode"].ToString();

                    TimeSpan tempOnDuty;
                    TimeSpan.TryParse(TableGioLVTheoCa.Rows[i]["Onduty"].ToString(), out tempOnDuty);
                    TimeSpan tempOnTimeIn = tempOnDuty.Subtract(new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["OnTimeIn"], 0));
                    TimeSpan tempCutIn = tempOnDuty.Add(new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["CutIn"], 0));

                    int tempDayCount = (int)TableGioLVTheoCa.Rows[i]["DayCount"];
                    int tempShowPosition = (int)TableGioLVTheoCa.Rows[i]["ShowPosition"];
                    TimeSpan tempOffDuty;
                    TimeSpan.TryParse(TableGioLVTheoCa.Rows[i]["Offduty"].ToString(), out tempOffDuty);
					// nếu add thêm 1 day ở đây thì bên kia phải trừ lại 1 day, còn ko lấy thì ko trừ => bỏ dòng này
                    tempOffDuty = tempOffDuty.Add(new TimeSpan(tempDayCount, 0, 0, 0));
                    TimeSpan tempOnTimeOut = tempOffDuty.Subtract(new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["OnTimeOut"], 0));
                    TimeSpan tempCutOut = tempOffDuty.Add(new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["CutOut"], 0));

                    TimeSpan tempAfterOT = new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["AfterOT"], 0);
                    TimeSpan tempLateGrace = new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["LateGrace"], 0);
                    TimeSpan tempEarlyGrace = new TimeSpan(0, (int)TableGioLVTheoCa.Rows[i]["EarlyGrace"], 0);

                    int tempWorkingTime = int.Parse(TableGioLVTheoCa.Rows[i]["WorkingTime"].ToString());
                    float temphesopc = 0f;
                    if (tempWorkingTime > 480) temphesopc = 0.5f;
                    else if (tempDayCount > 0) temphesopc = 0.3f;
                    clsShifts tempShift = new clsShifts() {
                        ShiftID = tempShiftID, ShiftCode = tempShiftCode,
                        DayCount = tempDayCount,
                        OnDutyTS = tempOnDuty, OffDutyTS = tempOffDuty,
                        OnTimeInTS = tempOnTimeIn, CutInTS = tempCutIn, OnTimeOutTS = tempOnTimeOut, CutOutTS = tempCutOut,
                        AfterOTTS = tempAfterOT,
                        LateGraceTS = tempLateGrace, EarlyGraceTS = tempEarlyGrace,
                        Workingday = (float)TableGioLVTheoCa.Rows[i]["Workingday"],
                        ShowPosition = tempShowPosition,
                        WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0),
                        chophepvaotreTS = tempOnDuty + tempLateGrace,
                        chopheprasomTS = tempOffDuty - tempEarlyGrace,
                        batdaulamthemTS = tempOffDuty + tempAfterOT,
                        hesoPC = temphesopc
                    };
                    _dsCa.Add(tempShift);
                }
                return _dsCa;
            }
        }

    }


}
