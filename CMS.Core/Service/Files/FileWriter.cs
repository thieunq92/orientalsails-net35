using System;
using System.Collections;
using System.IO;
using Castle.Services.Transaction;

namespace CMS.Core.Service.Files
{
    /// <summary>
    /// The FileWriter class perform file actions in a transactional context.
    /// </summary>
    public class FileWriter : IResource
    {
        private readonly IList _createdFiles = new ArrayList();
        private readonly IList _deletedFiles = new ArrayList();
        private readonly string _tempDir = Environment.GetEnvironmentVariable("TEMP");

        /// <summary>
        /// Contructor.
        /// </summary>
        public FileWriter(string tempDir)
        {
            if (! String.IsNullOrEmpty(tempDir))
            {
                _tempDir = tempDir;
            }
        }

        #region IResource Members

        public void Start()
        {
            // Nothing
        }

        public void Rollback()
        {
            // Remove temp file.
            foreach (string[] newFileLocations in _createdFiles)
            {
                // index 0 is permanent location, index 1 = temp location
                File.Delete(newFileLocations[1]);
            }
        }

        public void Commit()
        {
            // Move temp files to permanent location.
            foreach (string[] newFileLocations in _createdFiles)
            {
                // index 0 is permanent location, index 1 = temp location
                File.Move(newFileLocations[1], newFileLocations[0]);
            }

            // Delete scheduled deletions
            foreach (string fileToBeDeleted in _deletedFiles)
            {
                File.Delete(fileToBeDeleted);
            }
        }

        #endregion

        /// <summary>
        /// Create a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="inputStream"></param>
        public void CreateFromStream(string filePath, Stream inputStream)
        {
            if (File.Exists(filePath))
            {
                throw new ArgumentException("The file already exists", filePath);
            }
            string directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName))
            {
                throw new DirectoryNotFoundException(
                    String.Format("The physical upload directory {0} for the file does not exist", directoryName));
            }
            string tempFilePath = Path.Combine(_tempDir, Path.GetFileName(filePath));

            FileStream fs = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write);
            StreamUtil.Copy(inputStream, fs);
            fs.Flush();
            fs.Close();

            _createdFiles.Add(new string[2] {filePath, tempFilePath});
        }

        /// <summary>
        /// Mark a file for deletion by adding it to the list of file that are going to be deleted.
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file was not found so it could not be deleted.", filePath);
            }
            _deletedFiles.Add(filePath);
        }
    }
}