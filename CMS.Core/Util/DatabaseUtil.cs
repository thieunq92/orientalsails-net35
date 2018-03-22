using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

using NHibernate;
using NHibernate.Dialect;
using log4net;

using CMS.Core.Domain;
using CMS.Core.Service;

namespace CMS.Core.Util
{
	/// <summary>
	/// The DataBaseUtil class contains helper methods for misc. database related tasks.
	/// </summary>
	public class DatabaseUtil
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseUtil));

		private DatabaseUtil()
		{
		}

		/// <summary>
		/// Get the current database type.
		/// </summary>
		/// <returns></returns>
		public static DatabaseType GetCurrentDatabaseType()
		{
            var nhSessionFactory = GetNHibernateSessionFactory() as NHibernate.Impl.SessionFactoryImpl;
			if (nhSessionFactory.Dialect is MsSql2000Dialect)
			{
				return DatabaseType.MsSql2000;
			}
			else if (nhSessionFactory.Dialect is PostgreSQLDialect)
			{
				return DatabaseType.PostgreSQL;
			}
			else if (nhSessionFactory.Dialect is MySQLDialect)
			{
				return DatabaseType.MySQL;
			}
			throw new Exception("Unknown database type configured.");
		}

		/// <summary>
		/// Check if the database connection string in the web.config (NHibernate configuration) is valid.
		/// </summary>
		/// <returns></returns>
		public static bool TestDatabaseConnection()
		{
            var sf = GetNHibernateSessionFactory() as NHibernate.Impl.SessionFactoryImpl;
			try
			{
				IDbConnection con = sf.ConnectionProvider.GetConnection();
				con.Close();
				return true;
			}
			catch (Exception ex)
			{
				log.Error("Unable to connect to database.", ex);
				return false;
			}
		}

		/// <summary>
		/// Execute a given SQL script file.
		/// </summary>
		/// <param name="scriptFilePath"></param>
		public static void ExecuteSqlScript(string scriptFilePath)
		{
			log.Info(string.Format("Executing script: {0}", scriptFilePath));
			string delimiter = GetDelimiter();
			StreamReader scriptFileStreamReader = new StreamReader(scriptFilePath);
			string completeScript = scriptFileStreamReader.ReadToEnd();

			var nhSessionFactory = GetNHibernateSessionFactory() as NHibernate.Impl.SessionFactoryImpl;
			IDbConnection connection = nhSessionFactory.ConnectionProvider.GetConnection();
			IDbTransaction transaction = connection.BeginTransaction();
			try
			{
				IDbCommand cmd = connection.CreateCommand();
				cmd.Transaction = transaction;
				string splitRegex = string.Format(@"{0}\s*\n", delimiter);
				string[] sqlCommands = Regex.Split(completeScript, splitRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
				// Strip the delimiter from the last command. It might not be caught by the regex.
				sqlCommands[sqlCommands.Length - 1] = 
					Regex.Replace(sqlCommands[sqlCommands.Length - 1], delimiter, String.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

				foreach (string sqlCommand in sqlCommands)
				{
					if (sqlCommand.Trim().Length > 0)
					{
						log.Info(string.Format("Executing the follwing command: {0}", sqlCommand));
						cmd.CommandText = sqlCommand;
						cmd.ExecuteNonQuery();
					}
				}

				log.Info(string.Format("Committing transaction for script: {0}", scriptFilePath));
				transaction.Commit();
			}
			catch (Exception ex)
			{
				log.Warn(string.Format("Rolling back transaction for script: {0}", scriptFilePath));
				log.Error(string.Format("An error occured while executing the following script: {0}", scriptFilePath), ex);
				transaction.Rollback();
				throw new Exception(string.Format("An error occured while executing the following script: {0}", scriptFilePath), ex);
			}
			finally
			{
				connection.Close();
				scriptFileStreamReader.Close();
			}
		}

		/// <summary>
		/// Get the version of the given assembly from the database.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static Version GetAssemblyVersion(string assembly)
		{
			Version version = null;

			var nhSessionFactory = GetNHibernateSessionFactory() as NHibernate.Impl.SessionFactoryImpl;
			IDbConnection connection = nhSessionFactory.ConnectionProvider.GetConnection();
			// TODO: create proper NHibernate mapping for version :).
			string sql = String.Format("SELECT major, minor, patch FROM bitportal_version WHERE assembly = '{0}'", assembly);
			log.Info(string.Format("Version query: {0}", sql));
			IDbCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;

			try
			{
				IDataReader dr = cmd.ExecuteReader();
				if (dr.Read())
				{
					version = new Version(Convert.ToInt32(dr["major"]), Convert.ToInt32(dr["minor"]), Convert.ToInt32(dr["patch"]));
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				log.Error(String.Format("An error occured while retrieving the version for {0}.", assembly), ex);
			}
			finally
			{
				connection.Close();
			}
			return version;
		}

		private static ISessionFactory GetNHibernateSessionFactory()
		{
			SessionFactory sf = SessionFactory.GetInstance();
			return sf.GetNHibernateFactory();
		}

		private static string GetDelimiter()
		{
			switch (GetCurrentDatabaseType())
			{
				case DatabaseType.MsSql2000:
					return "^go";
				case DatabaseType.PostgreSQL:
				case DatabaseType.MySQL:
					return ";";
				default:
					throw new Exception("Unknown database type.");
			}
		}
	}
}
