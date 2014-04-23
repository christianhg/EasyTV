using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for utilityDB
/// </summary>
public static class UtilityDB
{
    static string connectionString = WebConfigurationManager.ConnectionStrings["easyTV"].ConnectionString;
    static SqlCommand sqlCommand;
    static SqlDataReader sqlDataReader;
    static SqlConnection sqlConnection;

    // Get procedures that don't take any arguments
    public static DataTable GetProcedures(string procedure)
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlCommand = new SqlCommand(procedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        return Reader(sqlConnection, sqlCommand);
    }

    // Get procedures that take one argument
    public static DataTable GetProcedures(string procedure, SqlParameter parameter)
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlCommand = new SqlCommand(procedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);

        return Reader(sqlConnection, sqlCommand);
    }

    // Get procedures that take multiple arguments
    public static DataTable GetProcedures(string procedure, SqlParameter[] parameters)
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlCommand = new SqlCommand(procedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        foreach (SqlParameter parameter in parameters)
        {
            sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        }

        return Reader(sqlConnection, sqlCommand);
    }

    // Insert procedures that take multiple arguments
    public static void InsertProcedures(string procedure, SqlParameter[] parameters)
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlCommand = new SqlCommand(procedure, sqlConnection);

        sqlCommand.CommandType = CommandType.StoredProcedure;

        foreach (SqlParameter parameter in parameters)
        {
            sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        }

        try
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
        catch (SqlException err)
        {
            throw new ApplicationException("Database error: " + err.ToString());
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    // Insert procedures that takes multiple arguments and returns a key
    public static int InsertProceduresWithOutput(string procedure, SqlParameter[] parameters, string returnParameter)
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlCommand = new SqlCommand(procedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.Parameters.Add(new SqlParameter(returnParameter, SqlDbType.Int));
        sqlCommand.Parameters[returnParameter].Direction = ParameterDirection.Output;

        foreach (SqlParameter parameter in parameters)
        {
            sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        }
        try
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                return (int)sqlCommand.Parameters[returnParameter].Value;
            }
        }
        catch (SqlException e)
        {
            throw new ApplicationException("Database error: " + e.ToString());
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    // Update procedures
    public static void UpdateProcedures(string procedure, SqlParameter[] parameters)
    {
        sqlConnection = new SqlConnection(connectionString);
        sqlCommand = new SqlCommand(procedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        foreach (SqlParameter parameter in parameters)
        {
            sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        }

        try
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
        catch (SqlException e)
        {
            throw new ApplicationException("Database error: " + e.ToString());
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    // Delete procedures
    public static void DeleteProcedures(string procedure, SqlParameter[] parameters)
    {
        sqlConnection = new SqlConnection(connectionString);

        sqlCommand = new SqlCommand(procedure, sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        foreach (SqlParameter parameter in parameters)
        {
            sqlCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        }

        try
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
        catch (SqlException e)
        {
            throw new ApplicationException("Database error: " + e.ToString());
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    // Reader
    private static DataTable Reader(SqlConnection sqlConnection, SqlCommand sqlCommand)
    {
        try
        {
            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);

                return dataTable;
            }
        }
        catch (SqlException e)
        {
            throw new ApplicationException("Database error: " + e.ToString());
        }
        finally
        {
            sqlConnection.Close();
        }
    }
}