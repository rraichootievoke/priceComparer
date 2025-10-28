using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

public class OracleDynamicParameters : SqlMapper.IDynamicParameters
{
    private readonly DynamicParameters dynamicParameters = new DynamicParameters();
    private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();
    private Dictionary<string, ParamInfo> parameters = new Dictionary<string, ParamInfo>();
    private List<object> templates;

    private class ParamInfo
    {
        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        private string m_Name;

        public object Value
        {
            get
            {
                return m_Value;
            }

            set
            {
                m_Value = value;
            }
        }

        private object m_Value;

        public ParameterDirection ParameterDirection
        {
            get
            {
                return m_ParameterDirection;
            }

            set
            {
                m_ParameterDirection = value;
            }
        }

        private ParameterDirection m_ParameterDirection;

        public OracleDbType? DbType
        {
            get
            {
                return m_DbType;
            }

            set
            {
                m_DbType = value;
            }
        }

        private OracleDbType? m_DbType;

        public int? Size
        {
            get
            {
                return m_Size;
            }

            set
            {
                m_Size = value;
            }
        }

        private int? m_Size;

        public IDbDataParameter AttachedParam
        {
            get
            {
                return m_AttachedParam;
            }

            set
            {
                m_AttachedParam = value;
            }
        }

        private IDbDataParameter m_AttachedParam;
    }

    public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, object value = null, System.Nullable<int> size = null)
    {
        OracleParameter oracleParameter;
        if (size.HasValue)
            oracleParameter = new OracleParameter(name, oracleDbType, size.Value, value, direction);
        else
            oracleParameter = new OracleParameter(name, oracleDbType, value, direction);

        oracleParameters.Add(oracleParameter);
    }

    public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
    {
        var oracleParameter = new OracleParameter(name, oracleDbType, direction);
        oracleParameters.Add(oracleParameter);
    }
    public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
    {
        ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);
        OracleCommand oracleCommand = command as OracleCommand;
        if (oracleCommand is object)
        {
            oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
        }
    }

    /// <summary>
    ///   ''' Get the value of a parameter
    ///   ''' </summary>
    ///   ''' <typeparam name="T"></typeparam>
    ///   ''' <param name="name"></param>
    ///   ''' <returns>The value, note DBNull.Value is not returned, instead the value is returned as null</returns>
    public T Get<T>(string name)
    {
        var val = (from n in oracleParameters
                   where n.ParameterName == name
                   select n).FirstOrDefault().Value;
        if (val == null)
        {
            if (null != null)
                throw new ApplicationException("Attempting to cast a DBNull to a non nullable type!");
            return default(T);
        }
        return (T)val;
    }

    private static string Clean(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            switch (name[0])
            {
                case '@':
                case ':':
                case '?':
                    {
                        return name.Substring(1);
                    }
            }
        }
        return name;
    }
}
