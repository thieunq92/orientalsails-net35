using System;
using System.Collections;
using System.IO;
using System.Reflection;
using CMS.Core.Domain;
using CMS.Core.Util;
using log4net;

namespace CMS.Core.Service
{
    /// <summary>
    /// The DatabaseInstaller class is responsible for installing, upgrading and uninstalling
    /// database tables and records. 
    /// </summary>
    public class DatabaseInstaller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (DatabaseInstaller));

        private readonly Assembly _assembly;
        private readonly string _databaseScriptsDirectory;
        private readonly DatabaseType _databaseType;
        private readonly string _installRootDirectory;
        private readonly ArrayList _upgradeScriptVersions;
        private Version _currentVersionInDatabase;
        private string _installScriptFile;
        private string _uninstallScriptFile;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="installRootDirectory">The physical path of the directory where the install
        /// scripts are located. This is the root install directory without 'Database/DatabaseType'.</param>
        /// <param name="assembly">The (optional) assembly that has to be upgraded or uninstalled.</param>
        public DatabaseInstaller(string installRootDirectory, Assembly assembly)
        {
            _installRootDirectory = installRootDirectory;
            _assembly = assembly;
            _databaseType = DatabaseUtil.GetCurrentDatabaseType();
            string databaseSubDirectory = Path.Combine("Database", _databaseType.ToString().ToLower());
            _databaseScriptsDirectory = Path.Combine(installRootDirectory, databaseSubDirectory);

            _upgradeScriptVersions = new ArrayList();
            CheckDatabaseScripts();
            // Sort the versions in ascending order. This way it's easy to iterate through the scripts
            // when upgrading.
            _upgradeScriptVersions.Sort();

            if (_assembly != null)
            {
                CheckCurrentVersionInDatabase();
            }
        }

        /// <summary>
        /// The database type of the current database;
        /// </summary>
        public DatabaseType DatabaseType
        {
            get { return _databaseType; }
        }

        /// <summary>
        /// The current version of the module/assembly in the database.
        /// </summary>
        public Version CurrentVersionInDatabase
        {
            get { return _currentVersionInDatabase; }
        }

        /// <summary>
        /// The version of the assembly that is to be installed or upgraded.
        /// </summary>
        public Version NewAssemblyVersion
        {
            get { return _assembly.GetName().Version; }
        }

        /// <summary>
        /// Indicates if a module or assembly can be installed from the given location.
        /// </summary>
        public bool CanInstall
        {
            get { return CheckCanInstall(); }
        }

        /// <summary>
        /// Indicates if a module or assembly can be upgraded from the given location.
        /// </summary>
        public bool CanUpgrade
        {
            get { return CheckCanUpgrade(); }
        }

        /// <summary>
        /// Indicates if a module or assembly can be uninstalled from the given location.
        /// </summary>
        public bool CanUninstall
        {
            get { return CheckCanUninstall(); }
        }

        /// <summary>
        /// Check if the database connection is valid.
        /// </summary>
        /// <returns></returns>
        public bool TestDatabaseConnection()
        {
            return DatabaseUtil.TestDatabaseConnection();
        }

        /// <summary>
        /// Install the database part of a Cuyaghoga component.
        /// </summary>
        public void Install()
        {
            if (CanInstall)
            {
                log.Info(string.Format("Installing module with {0}", _installScriptFile));
                DatabaseUtil.ExecuteSqlScript(_installScriptFile);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Can't install assembly from: {0}",
                                                                  _installRootDirectory));
            }
        }

        /// <summary>
        /// Upgrade the database part of a Cuyahoga component to higher version.
        /// </summary>
        public void Upgrade()
        {
            if (CanUpgrade)
            {
                log.Info(string.Format("Upgrading {0}", _assembly.GetName().Name));
                // Iterate through the sorted versions that are extracted from the upgrade script names.
                foreach (Version version in _upgradeScriptVersions)
                {
                    // Only run the script if the version is higher than the current database version
                    if (version > _currentVersionInDatabase)
                    {
                        string upgradeScriptPath = Path.Combine(_databaseScriptsDirectory, version.ToString(3) + ".sql");
                        log.Info(string.Format("Running upgrade script {0}", upgradeScriptPath));
                        DatabaseUtil.ExecuteSqlScript(upgradeScriptPath);
                        _currentVersionInDatabase = version;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format("Can't upgrade assembly from: {0}",
                                                                  _installRootDirectory));
            }
        }

        /// <summary>
        /// Uninstall the database part of a Cuyaghoga component.
        /// </summary>
        public void Uninstall()
        {
            if (CanUninstall)
            {
                log.Info(string.Format("Uninstalling module with {0}", _installScriptFile));
                DatabaseUtil.ExecuteSqlScript(_uninstallScriptFile);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Can't uninstall assembly from: {0}",
                                                                  _installRootDirectory));
            }
        }

        private void CheckDatabaseScripts()
        {
            DirectoryInfo directory = new DirectoryInfo(_databaseScriptsDirectory);
            if (directory.Exists)
            {
                foreach (FileInfo file in directory.GetFiles("*.sql"))
                {
                    if (file.Name.ToLower() == "install.sql")
                    {
                        _installScriptFile = file.FullName;
                    }
                    else if (file.Name.ToLower() == "uninstall.sql")
                    {
                        _uninstallScriptFile = file.FullName;
                    }
                    else
                    {
                        // Extract the version from the script filename.
                        // NOTE: these filenames have to be in the major.minor.patch.sql format
                        string[] extractedVersion = file.Name.Split('.');
                        if (extractedVersion.Length == 4)
                        {
                            Version version = new Version(
                                Int32.Parse(extractedVersion[0]),
                                Int32.Parse(extractedVersion[1]),
                                Int32.Parse(extractedVersion[2]));
                            _upgradeScriptVersions.Add(version);
                        }
                        else
                        {
                            log.Warn(String.Format("Invalid SQL script file found in {0}: {1}",
                                                   _databaseScriptsDirectory, file.Name));
                        }
                    }
                }
            }
        }

        private void CheckCurrentVersionInDatabase()
        {
            if (_assembly != null)
            {
                _currentVersionInDatabase = DatabaseUtil.GetAssemblyVersion(_assembly.GetName().Name);
            }
        }

        private bool CheckCanInstall()
        {
            return _currentVersionInDatabase == null && _installScriptFile != null;
        }

        private bool CheckCanUpgrade()
        {
            if (_assembly != null)
            {
                if (_currentVersionInDatabase != null && _upgradeScriptVersions.Count > 0)
                {
                    // Upgrade is possible if the script with the highest version number
                    // has a number higher than the current database version AND when the
                    // assembly version number is equal or higher than the script with
                    // the highest version number.
                    Version highestScriptVersion = (Version) _upgradeScriptVersions[_upgradeScriptVersions.Count - 1];

                    if (_currentVersionInDatabase < highestScriptVersion
                        && _assembly.GetName().Version >= highestScriptVersion)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckCanUninstall()
        {
            return (_assembly != null && _uninstallScriptFile != null);
        }
    }
}