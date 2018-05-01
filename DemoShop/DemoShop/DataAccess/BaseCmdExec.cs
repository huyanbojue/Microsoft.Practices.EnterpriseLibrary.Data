using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DemoShop.DataAccess
{
    public class BaseCmdExec
    {
        Database _db;

        #region constructions

        public BaseCmdExec(string strConn)
        {
            _db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(strConn);
        }

        #endregion constructions

        #region lib

        protected T XMLToObject<T>(IDataRecord dataRecord, string strMappedColumn) where T : new()
        {
            T ret = new T();

            try
            {
                var isDBNull = dataRecord.IsDBNull(dataRecord.GetOrdinal(strMappedColumn));

                if (!isDBNull)
                {
                    string xmlString = dataRecord.GetString(dataRecord.GetOrdinal(strMappedColumn));
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    StringReader rdr = new StringReader(xmlString);
                    ret = (T)serializer.Deserialize(rdr);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        protected string ObjectToXML<T>(List<T> obj) where T : new()
        {
            string xml;

            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(List<T>));

                XmlDocument doc = new XmlDocument();

                System.IO.StringWriter sww = new System.IO.StringWriter();
                XmlWriter writer = XmlWriter.Create(sww);
                xsSubmit.Serialize(writer, obj);
                xml = sww.ToString(); // Your xml
            }
            catch (Exception ex)
            {
                throw;
            }

            return xml;
        }

        protected IRowMapper<TReturn> GenerateRowMapper<TReturn, TChild1>
                    (string strMappedColumn1, PropertyInfo childType1)
            where TReturn : new()
            where TChild1 : new()
        {
            try
            {
                return MapBuilder<TReturn>.MapAllProperties()
                    .Map(childType1)
                    .WithFunc(n => XMLToObject<TChild1>(n, strMappedColumn1))
                    .Build();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected IRowMapper<TReturn> GenerateRowMapper<TReturn, TChild1, TChild2>
                    (string strMappedColumn1, PropertyInfo childType1,
                    string strMappedColumn2, PropertyInfo childType2)
            where TReturn : new()
            where TChild1 : new()
            where TChild2 : new()
        {
            try
            {
                return MapBuilder<TReturn>.MapAllProperties()
                    .Map(childType1)
                    .WithFunc(n => XMLToObject<TChild1>(n, strMappedColumn1))
                    .Map(childType2)
                    .WithFunc(n => XMLToObject<TChild2>(n, strMappedColumn2))
                    .Build();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion lib

        #region public methods

        public IEnumerable<T> ExecStoredProc<T>(string storeName, object[] paramObj)
            where T : new()
        {
            return _db.ExecuteSprocAccessor(storeName, MapBuilder<T>.MapAllProperties().Build(), paramObj);
        }

        public IEnumerable<T> ExecStoredProc<T>(string storeName, object[] paramObj, IRowMapper<T> rowMapper)
            where T : new()
        {
            return _db.ExecuteSprocAccessor(storeName, rowMapper, paramObj);
        }

        public IEnumerable<T> ExecStoredProc<T>(string storeName, object[] paramObj, IResultSetMapper<T> resultMapper)
            where T : new()
        {
            return _db.ExecuteSprocAccessor(storeName, resultMapper, paramObj);
        }

        public T ExecStoredScalar<T>(string storeName, object[] paramObj)
            where T : new()
        {
            return (T)_db.ExecuteScalar(storeName, paramObj);
        }

        public void ExecStoredNonQuery<T>(string storeName, object[] paramObj)
            where T : new()
        {
            _db.ExecuteNonQuery(storeName, paramObj);
        }

        public PropertyInfo GetPropertyInfo<T>(Expression<Func<T, object>> lambda)
        {
            var member = lambda.Body as MemberExpression;
            return member.Member as PropertyInfo;
        }

        #endregion public methods
    }
}
