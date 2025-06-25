using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;

public abstract class BaseRepository
{
    private readonly string _connectionString = ConexionSQL.ConnectionString;

    protected void ExecuteNonQuery(string storedProcedure, IEnumerable<SqlParameter> parameters)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                command.ExecuteNonQuery(); // Versión síncrona
            }
        }
    }

    protected object ExecuteScalar(string storedProcedure, IEnumerable<SqlParameter> parameters)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                return command.ExecuteScalar(); 
            }
        }
    }

    protected IEnumerable<T> Query<T>(string storedProcedure, Func<SqlDataReader, T> map, IEnumerable<SqlParameter>? parameters = null)
    {
        var items = new List<T>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                using (var reader = command.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        items.Add(map(reader));
                    }
                }
            }
        }
        return items;
    }
    
    protected T? QueryFirstOrDefault<T>(string storedProcedure, Func<SqlDataReader, T> map, IEnumerable<SqlParameter>? parameters = null) where T : class
    {
        using (var connection = new SqlConnection(ConexionSQL.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return map(reader);
                    }
                }
            }
        }
        return null;
    }
}