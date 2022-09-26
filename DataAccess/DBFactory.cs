using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using DataTypes.ModelDataTypes;
using System.Text;

namespace DataAccess
{
  public  class DbFactory
    {
        DBConnection DAConnection = new DBConnection();        
        public string constr { get; set; }
        //private SqlConnection con;
        public DbFactory()
        {
            constr = DAConnection.Main();
        }

        #region Selecting data
        // This function is used to get List of object, passing by list object, procedure name & Parameters.
        public List<T> SelectCommand_SP<T>(List<T> ObjList, string spname, DynamicParameters parameters)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    ObjList = SqlCon.Query<T>(spname.ToString(), parameters, commandType: CommandType.StoredProcedure).ToList();
                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to get List of object, passing by list object & sql statement.
        public List<T> SelectCommand_SP<T>(List<T> ObjList, string strQ)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    ObjList = SqlCon.Query<T>(strQ, commandType: CommandType.Text).ToList();
                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to get object type, passing object, procedure name & parameters.
        public T SelectCommand_SP<T>(T ObjModel, string spname, DynamicParameters parameters)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    ObjModel = SqlCon.Query<T>(spname.ToString(), parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return ObjModel;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }           
        }

        // This function is used to get object type, passing object, procedure name & parameters.
        public T SelectCommand_SP<T>(T ObjModel, string sqlQuery)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    ObjModel = SqlCon.Query<T>(sqlQuery, commandType: CommandType.Text).FirstOrDefault();
                    return ObjModel;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }


        // This function is used to get IList type, by passing IList, producdure name & parameters.
        public IList<T> SelectCommand_SP<T>(IList<T> ObjList, string spname, DynamicParameters parameters)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    ObjList = SqlCon.Query<T>(spname.ToString(), parameters, commandType: CommandType.StoredProcedure).ToList();
                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
               
        // code for making dynamic sql query for dropdown list. And retuning List as IList<T> collection 
        public IList<T> Select_Generic_DDL<T>(IList<T> ObjList, string tableName, string dataValue, string dataMember, string whereClause, string orderByColumn, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@DataValue", dataValue, DbType.String);

                    if (!string.IsNullOrEmpty(whereClause))
                        parameters.Add("@WhereClause", whereClause, DbType.String);

                    parameters.Add("@DataMember", dataMember, DbType.String);
                    if (!string.IsNullOrEmpty(orderByColumn))
                        parameters.Add("@OrderByColumn", orderByColumn, DbType.String);

                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    ObjList = SqlCon.Query<T>("System_Generic_DDL", parameters, commandType: CommandType.StoredProcedure).ToList();

                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // code for making dynamic sql query for dropdown list. And retuning List as List<T> collection
        public List<T> Select_Generic_DDL<T>(List<T> ObjList, string tableName, string dataValue, string dataMember, string whereClause, string orderByColumn, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@DataValue", dataValue, DbType.String);

                    if (!string.IsNullOrEmpty(whereClause))
                        parameters.Add("@WhereClause", whereClause, DbType.String);

                    parameters.Add("@DataMember", dataMember, DbType.String);
                    if (!string.IsNullOrEmpty(orderByColumn))
                        parameters.Add("@OrderByColumn", orderByColumn, DbType.String);

                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    ObjList = SqlCon.Query<T>("System_Generic_DDL", parameters, commandType: CommandType.StoredProcedure).ToList();

                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // Code count number of rows exists in specified table. And returns no. of rows.
        public int Select_Generic_Count<T>(List<T> ObjList, string tableName, string columnName, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@ColumnName", columnName, DbType.String);
                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    return SqlCon.Query<int>("System_Generic_Count", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // code for making dynamic sql query for dropdown list with any other more columns. And retuning List as List<T> collection
        public List<T> Select_Generic_DDLWithColumn<T>(List<T> ObjList, string tableName, string dataValue, string dataMember, string columnsName, string orderByColumn, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@DataValue", dataValue, DbType.String);
                    parameters.Add("@DataMember", dataMember, DbType.String);
                    parameters.Add("@ColumnsName", columnsName, DbType.String);
                    parameters.Add("@OrderByColumn", orderByColumn, DbType.String);
                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    ObjList = SqlCon.Query<T>("System_Generic_DDLWithColumn", parameters, commandType: CommandType.StoredProcedure).ToList();
                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // code for making dynamic sql query with given columns name, where clause & order by. This function returns collection as IList
        public IList<T> Select_Generic_Select<T>(IList<T> ObjList, string tableName, string columnsName, string whereClause, string orderByColumn, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@ColumnsName", columnsName, DbType.String);

                    if (string.IsNullOrEmpty(whereClause))
                        parameters.Add("@WhereClause", whereClause, DbType.String);

                    if (!string.IsNullOrEmpty(orderByColumn))
                        parameters.Add("@OrderByColumn", orderByColumn, DbType.String);

                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    ObjList = SqlCon.Query<T>("System_Generic_Select", parameters, commandType: CommandType.StoredProcedure).ToList();
                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // code for making dynamic sql query with given columns name, where clause & order by. This function returns collection as List
        public List<T> Select_Generic_Select<T>(List<T> ObjList, string tableName, string columnsName, string whereClause, string orderByColumn, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@ColumnsName", columnsName, DbType.String);

                    if (!string.IsNullOrEmpty(whereClause))
                        parameters.Add("@WhereClause", whereClause, DbType.String);

                    if (!string.IsNullOrEmpty(orderByColumn))
                        parameters.Add("@OrderByColumn", orderByColumn, DbType.String);

                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    ObjList = SqlCon.Query<T>("System_Generic_Select", parameters, commandType: CommandType.StoredProcedure).ToList();
                    return ObjList;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // code for making dynamic sql query with given columns name. This function returns only single object
        public T Select_Generic_Select<T>(T ObjModel, string tableName, string columnsName, string whereClause, string orderByColumn, bool checkActive, bool active)
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName, DbType.String);
                    parameters.Add("@ColumnsName", columnsName, DbType.String);

                    if (!string.IsNullOrEmpty(whereClause))
                        parameters.Add("@WhereClause", whereClause, DbType.String);

                    if (!string.IsNullOrEmpty(orderByColumn))
                        parameters.Add("@OrderByColumn", orderByColumn, DbType.String);

                    parameters.Add("@CheckActive", checkActive, DbType.Boolean);
                    parameters.Add("@Active", active, DbType.Boolean);
                    ObjModel = SqlCon.Query<T>("System_Generic_Select", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return ObjModel;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to get list of object mapped by database table's column
        public List<T> Select_All<T>(T objModel) where T : class
        {
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    return SqlCon.GetAll<T>().ToList();
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to get first value of first row. passing by procedure name & parameter.
        public object SelectScalarValue_SP(string spname, DynamicParameters parameters)
        {
            object retval = null;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    retval = SqlCon.ExecuteScalar<object>(spname, param: parameters, commandType: CommandType.StoredProcedure);
                    return retval;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to get first value of first row. passing by sql query
        public object SelectScalarValue_SP(string strQ)
        {
            object retval = null;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();
                    retval = SqlCon.ExecuteScalar<object>(strQ, commandType: CommandType.Text);
                    return retval;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        #endregion

        #region Insertion of data

        // This function is used to insert complete model object in DB, by passing model object and return integer value
        public int InsertCommand_SP<T>(T objModel) where T : class
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    rowAffected = (int)SqlCon.Insert(objModel);
                    return rowAffected;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to insert data on DB, by passing procedure name & marameters.
        public int InsertCommand_SP(string spname, DynamicParameters parameters)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    rowAffected = SqlCon.Execute(spname.ToString(), parameters, commandType: CommandType.StoredProcedure);
                    return rowAffected;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }
      
        // This function is used to insert data in database by passing sql query.
        public Int32 InsertCommandReturnIdentity(string strQ)
        {
            Int32 retval = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();
                    var sqlComd = new SqlCommand(strQ, SqlCon);
                    retval = sqlComd.ExecuteNonQuery();

                    if (retval == 1)
                    {
                        var strQ1 = new StringBuilder();
                        strQ1.Append("Select @@IDENTITY as value");
                        sqlComd = new SqlCommand(strQ1.ToString(), SqlCon);
                        var objIdentity = sqlComd.ExecuteScalar();
                        retval = Convert.ToInt32(objIdentity);
                        return retval;
                    }
                    return retval;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        // This function is used to insert data in database by passing sql query.
        public Int32 InsertCommand(string strQ)
        {
            Int32 retval = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();
                    var sqlComd = new SqlCommand(strQ, SqlCon);
                    retval = sqlComd.ExecuteNonQuery();                    
                    return retval;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }

        #endregion

        #region  updating data in DB    

        // This function is used to update data in database by passing procedure name and parameters.
        public int UpdateCommand_SP(string spname, DynamicParameters parameters)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();
                    
                    rowAffected = SqlCon.Execute(spname.ToString(), parameters, commandType: CommandType.StoredProcedure);
                    return rowAffected;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }            
        }

        // This function is used to update data in database by passing update sql query and returning effected row.
        public int UpdateCommand_SP(string strQ)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    rowAffected = SqlCon.Execute(strQ, commandType: CommandType.Text);
                    return rowAffected;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }            
        }

        // This function is used to update data in database by passing table name, columns that have to update and where clause by which column we update data.
        public int UpdateCommandForMultiCol(string tableName,string columnsWithValues, string whereClause)
        {
            int rowAffected = 0;
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TableName", tableName);
                    parameters.Add("@Column_And_Values", columnsWithValues);
                    parameters.Add("@Condition", whereClause);
                    rowAffected = SqlCon.Execute("System_Generic_Update_MultiCol", param: parameters, commandType: CommandType.StoredProcedure);
                    return rowAffected;
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }            
        }

        // This function is used to update data in database by passing complete model object. The model should have 1 Key attribute on primary key property. 
        //This will map complete model property & table columns. If model name is not same as table name then add a attribute on class.
        public bool UpdateCommand_SP<T>(T objModel) where T:class
        {            
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    if (SqlCon.State == ConnectionState.Closed)
                        SqlCon.Open();
                    return SqlCon.Update(objModel);
                }
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }            
        }

        #endregion

        public DataTable FetchDataTable(string strQ)
        {
            DataTable retval = new DataTable();
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    SqlCommand com = new SqlCommand(strQ.ToString(), SqlCon);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(retval);
                }
                return retval;
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }
        public DataSet FetchDataSet(string strQ)
        {
            DataSet retval = new DataSet();
            try
            {
                using (SqlConnection SqlCon = new SqlConnection(constr.ToString()))
                {
                    SqlCommand com = new SqlCommand(strQ.ToString(), SqlCon);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(retval);
                }
                return retval;
            }
            catch (Exception Ex)
            {
                string EMessage = Ex.Message;
                throw;
            }
        }
    }//=== Class Ends Here
}



