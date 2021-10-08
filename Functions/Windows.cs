using System;
using System.IO;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace Automation
{
    class Windows
    {

        #region Folders

        /// <summary>
        ///     Create a new directory
        /// </summary>
        /// <param name="DirName">The name of the directory to create.</param>
        public static void CreateDir(string DirName)
        {

            // If the directory doesn't exist, create it
            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }

            // Ensure the file exits
            Check.IsTrue(Directory.Exists(DirName), "The directory was not created.");

        }


        /// <summary>
        ///     Rename an existing directory
        /// </summary>
        /// <param name="DirName">The name of the directory to rename</param>
        /// <param name="NewDirName">The new name of the directory</param>
        public static void RenameDir(string DirName, string NewDirName)
        {

            // If the directory doesn't exist, create it
            if (Directory.Exists(DirName) && !Directory.Exists(NewDirName))
            {
                Directory.Move(DirName, NewDirName);

                // Ensure the directory exits
                string Message = "The directory was not renamed.";
                Check.IsFalse(Directory.Exists(DirName), Message);
                Check.IsTrue(Directory.Exists(NewDirName), Message);
            }
            else
            {
                
                Check.Fail((!Directory.Exists(DirName) ? "The source directory did not exist. " : " ") + (Directory.Exists(NewDirName) ? "The destination directory already existed. " : " "));

            }

        }


        /// <summary>
        ///     Copies an existing directory
        /// </summary>
        /// <param name="DirName">The name of the directory to copy</param>
        /// <param name="NewDirName">The destination of the directory</param>
        public static void CopyDir(string DirName, string NewDirName)
        {

            // If the directory doesn't exist, create it
            if (Directory.Exists(DirName) && !Directory.Exists(NewDirName))
            {

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(DirName, "*",
                SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(DirName, NewDirName));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(DirName, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(DirName, NewDirName), true);
            }
            else
            {
                Console.WriteLine("Couldn't copy " + DirName + " as either the source folder did not exist or the destination folder already exists");
            }

        }


        /// <summary>
        ///     Move an existing directory
        /// </summary>
        /// <param name="DirName">The name of the directory to move</param>
        /// <param name="NewDirName">The new location of the directory</param>
        public static void MoveDir(string DirName, string NewDirName)
        {

            // If the directory doesn't exist, create it
            if (Directory.Exists(DirName) && !Directory.Exists(NewDirName))
            {
                Directory.Move(DirName, NewDirName);
            }
            else
            {

                Console.WriteLine("Couldn't create " + NewDirName + " as either the source folder did not exist or the destination folder already exists");
            }

        }


        /// <summary>
        ///     Delete an existing directory
        /// </summary>
        /// <param name="DirName">The name of the directory to delete</param>
        public static void DeleteDir(string DirName)
        {

            // If the directory exist, delete it
            if (Directory.Exists(DirName))
            {
                Directory.Delete(DirName, true);
            }
            else
            {
                Console.WriteLine("Directory " + DirName + " does not exist so nothing to delete.");
            }

        }

        #endregion

        #region Files

        /// <summary>
        ///     Create a new file
        /// </summary>
        /// <param name="FileName">The name of the file to create</param>
        public static void CreateFile(string FileName, bool DeleteExisting = true)
        {

            // Delete the existing file
            if (DeleteExisting && File.Exists(FileName))
            {
                DeleteFile(FileName);

            }

            // If the file does not exist, create it
            if (!File.Exists(FileName))
            {
                var myFile = File.Create(FileName);
                myFile.Close();

            }

            // Ensure the file exits
            Check.IsTrue(File.Exists(FileName), "The file was not created.");

        }


        /// <summary>
        ///     Rename an existing file
        /// </summary>
        /// <param name="FileName">The name of the file to rename</param>
        /// <param name="NewFileName">The new name of the file</param>
        public static void RenameFile(string FileName, string NewFileName)
        {

            // If the file exists then rename it.
            if (File.Exists(FileName) && !File.Exists(NewFileName))
            {

                File.Move(FileName, NewFileName);

                // Ensure the file exits
                string Message = "The file was not renamed.";
                Check.IsFalse(File.Exists(FileName), Message);
                Check.IsTrue(File.Exists(NewFileName), Message);

            }
            else
            {

                Check.Fail((!File.Exists(FileName) ? "The source file did not exist. " : " ") + (File.Exists(NewFileName) ? "The destination file already existed. " : " "));

            }

        }


        /// <summary>
        ///     Copy an existing file
        /// </summary>
        /// <param name="FileName">The name of the file to copy</param>
        /// <param name="SourceDir">The directory of the file to copy</param>
        /// <param name="DestDir">The destination directory of the file to copy to</param>
        public static void CopyFile(string FileName, string SourceDir, string DestDir)
        {

            string SourcePath = SourceDir;
            string TargetPath = DestDir;
            string DestFile = Path.Combine(TargetPath, FileName);


            // If the target path does not exist, create it
            if (!Directory.Exists(TargetPath))
            {
                CreateDir(TargetPath);
            }
            File.Copy(Path.Combine(SourcePath, FileName), DestFile, true);

            if (Directory.Exists(SourcePath))
            {
                string[] Files = Directory.GetFiles(SourcePath);

                // Copy the Files and overwrite destination Files if they already exist. 
                foreach (string CopyFile in Files)
                {
                    // Use static Path methods to extract only the File name from the path.
                    FileName = Path.GetFileName(CopyFile);
                    DestFile = Path.Combine(TargetPath, FileName);
                    File.Copy(CopyFile, DestFile, true);
                    Check.IsTrue(File.Exists(DestFile), "The file was not copied.");

                }
            }
            else
            {
                Check.Fail("Source path does not exist.");
            }

        }


        /// <summary>
        ///     Move an existing file
        /// </summary>
        /// <param name="FileName">The name of the file to move</param>
        /// <param name="SourceDir">The directory of the file to move</param>
        /// <param name="DestDir">The destination directory of the file to move to</param>
        public static void MoveFile(string FileName, string SourceDir, string DestDir)
        {

            string SourceFile   = Path.Combine(SourceDir, FileName);
            string DestFile     = Path.Combine(DestDir, FileName);


            // Check the directories exist.
            if (Directory.Exists(SourceDir) && Directory.Exists(DestDir))
            {

                File.Move(SourceFile, DestFile);

                // Ensure the file exits
                Check.IsFalse(File.Exists(SourceFile), "The source file still exists.");
                Check.IsTrue(File.Exists(DestFile), "The file was not moved to the destination.");

            } else
            {
                
                Check.Fail((!Directory.Exists(SourceDir) ? "The source directory did not exist." : " ") + (!Directory.Exists(DestDir) ? " The desination directory did not exist." : ""));

            }

        }


        /// <summary>
        ///     Delete an existing file
        /// </summary>
        /// <param name="FileName">The name of the file to delete</param>
        public static void DeleteFile(string FileName)
        {
            try
            {
                // If the file exists then delete it.
                if (File.Exists(FileName))
                {

                    File.Delete(FileName);
                    Check.IsFalse(File.Exists(FileName), "The File was not deleted.");

                }

            }
            catch (Exception error)
            {

                Check.Fail("Something went wrong. Error: " + error.ToString());

            }

        }


        /// <summary>
        ///     Write to an existing file
        /// </summary>
        /// <param name="Text">The text to write to the file</param>
        /// <param name="FileName">The name of the file to write to</param>
        /// <param name="Create">Optional flag to create the file if it doesn't already exist</param>
        public static void WriteToFile(string Text, string FileName, bool Create = true)
        {

            // If the file does not exist and we we need to create it, then create it
            if (!File.Exists(FileName) && Create)
            {

                CreateFile(FileName);

            }

            // Write the text to the file
            Check.IsTrue(File.Exists(FileName));
            File.AppendAllText(FileName, Text + Environment.NewLine);

        }


        /// <summary>
        ///     Waits for a file to exist
        /// </summary>
        /// <param name="FileName">The name of the file to wait for</param>
        /// <param name="Timeout">Optional flag to specify the timeout period</param>
        public static void WaitForFileToExist(string FileName, int Timeout = 0)
        {

            int counter = 0;
            Timeout = (Timeout == 0 ? Config.current.defaults.timeout : Timeout);


            // While the file does not exist...
            while (!File.Exists(FileName))
            {

                // Wait for 1 second and increment the counter
                Utils.Wait(1);
                counter++;

                // If we've hit our timeout limit, break out the loop
                if (counter == Timeout)
                {

                    Check.Fail("File did not exist after the timeout.");
                    break;

                }

            }

        }


        /// <summary>
        ///     Reads the contents of a file to a string
        /// </summary>
        /// <param name="FileName">The name of the file to read in</param>
        public static string ReadFile(string FileName)
        {

            string text = null; 


            // If the file exists, read the contents. Otherwise log the error.
            if (File.Exists(FileName))
            {

                StreamReader streamReader = new StreamReader(FileName, Encoding.UTF8);
                text = streamReader.ReadToEnd();
                streamReader.Close();

            }
            else
            {

                Check.Fail("The file did not exist.");

            }

            // Return the file contents
            return text;

        }


        /// <summary>
        ///     Clears the contents of the file
        /// </summary>
        /// <param name="FileName">The name of the directory to clear.</param>
        public static void ClearFile(string FileName)
        {

            // If the file exists, read the contents. Otherwise log the error.
            if (File.Exists(FileName))
            {

                TextWriter streamWriter = new StreamWriter(FileName);
                streamWriter.WriteLine();
                streamWriter.Close();

            }
            else
            {

                Check.Fail("The file did not exist.");

            }

        }


        /// <summary>
        ///     Sets the given File to Read-Only
        /// </summary>
        /// <param name="FileName">The name of the file to change the permissions.</param>
        public static void MakeFileReadOnly(string FileName)
        {

            // If the File exists, make it Read-Only. Otherwise log the error.
            if (File.Exists(FileName))
            {

                FileInfo fileInfo = new FileInfo(FileName);

                // If the File is not currently set to Read-Only, set it to Read-Only now.
                if (!fileInfo.IsReadOnly)
                {

                    fileInfo.IsReadOnly = true;

                }

                // Ensure the File is Read-Only
                Check.IsTrue(fileInfo.IsReadOnly);

            }
            else
            {

                Check.Fail("The file '" + FileName + "' did not exist.");

            }

        }


        /// <summary>
        ///     Sets the given File to Read-Write
        /// </summary>
        /// <param name="FileName">The name of the file to change the permissions.</param>
        public static void MakeFileReadWrite(string FileName)
        {

            // If the File exists, make it Read-Write. Otherwise log the error.
            if (File.Exists(FileName))
            {

                FileInfo fileInfo = new FileInfo(FileName);

                // If the File is currently set to Read-Only, set it to Read-Write now.
                if (fileInfo.IsReadOnly)
                {

                    fileInfo.IsReadOnly = false;

                }

                // Ensure the File is Read-Write
                Check.IsFalse(fileInfo.IsReadOnly);

            }
            else
            {

                Check.Fail("The file '" + FileName + "' did not exist.");

            }

        }

        #endregion

        #region Processes

        public static bool IsProcessRunning(string Lookup)
        {

            // Get the process by name
            Process[] Running = Process.GetProcessesByName(Lookup);

            // Return true if it was found, false if not
            return Running.Length != 0;

        }


        public static void KillAllProcesses(string Lookup)
        {

            int counter = 0;


            // Kill all of the processes
            foreach (var Process in Process.GetProcessesByName(Lookup))
            { 

                Process.Kill();

            }

            // Wait while the processes are being shutdown
            while (IsProcessRunning(Lookup))
            {

                // Break out of the loop if the timeout has been reached.
                if (counter == Config.current.defaults.timeout)
                { 
                    break;

                }

                // Wait and increment the counter
                Utils.Wait(1);
                counter++;

            }

        }


        public static void StartProcess(string Application, string Name, string Args = null)
        {

            int counter = 0;


            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = Application;
            startInfo.Arguments = Args;
            process.StartInfo = startInfo;
            process.Start();
            var Response = process.StandardOutput.ReadToEnd();
            process.WaitForExit();


            // Wait while the process is not running
            while (!IsProcessRunning(Name))
            {

                // Break out of the loop if the timeout has been reached.
                if (counter == Config.current.defaults.timeout)
                {
                    break;

                }

                // Wait and increment the counter
                Utils.Wait(1);
                counter++;

            }

        }

        #endregion


        /// <summary>
        ///     Gets the current logged in user's name. It will tidy it up if Tidy = true
        /// </summary>
        /// <param name="Tidy">Optional flag to specify if the username needs to be tidied up and remove the machine name</param>
        /// <returns>The username as a string</returns>
        public static string GetMyUsername(bool Tidy = true)
        {

            string Username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace(Environment.MachineName + @"\\", ""); ;


            // If we need to tidy up the username, remove the machine name and return it back. Otherwise just return the username.
            return (Tidy ? Username.Substring(Username.IndexOf(@"\") + 1, Username.Length - Username.IndexOf(@"\") - 1) : Username);

        }


        /// <summary>
        ///     Gets the given System Variable
        /// </summary>
        /// <param name="Variable">The system variable to get.
        ///     For Example: "windir"
        /// </param>
        /// <returns>The specified system variable. For Example: "C:\Windows"</returns>
        public static string GetSystemVariable(string Variable)
        {

            string Value = null;


            // Try and get the variable. If we can't get it, never mind
            try
            {

                Value = Environment.GetEnvironmentVariable(Variable).ToString();

            }
            catch (Exception error)
            {

                Check.Fail("Couldn't grab the variable \"" + Variable + "\"\nError: " + error.ToString());

            }

            // Return the variable
            return Value;

        }


        public static void FTP(string Dir, string Files = "*.*")
        {

            string FTPURL = "";
            string Username = "";
            string Password = "";
            //string ServerDir    = null;


            // Try uploading the files.
            try
            {

                // Get a list of Files in the directory 
                string[] files = Directory.GetFiles(Dir, Files);

                // If their are files to upload
                if (files != null)
                {

                    // For each file in the directory
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(file);
                        string fileName = fi.Name;
                        string fileurl = Dir + @"/" + fileName;
                        string ftpFile = FTPURL + @"/" + fileName;
                        FtpWebRequest myRequest = (FtpWebRequest)FtpWebRequest.Create(ftpFile);
                        myRequest.Credentials = new NetworkCredential(Username, Password);
                        myRequest.Method = WebRequestMethods.Ftp.UploadFile;
                        myRequest.Timeout = Convert.ToInt32(Config.current.defaults.timeout);
                        myRequest.UseBinary = true;
                        myRequest.KeepAlive = true;
                        myRequest.ContentLength = fi.Length;
                        byte[] buffer = new byte[4097];
                        int bytes = 0;
                        int total_bytes = (int)fi.Length;
                        FileStream fs = fi.OpenRead();
                        Stream rs = myRequest.GetRequestStream();

                        while (total_bytes > 0)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            rs.Write(buffer, 0, bytes);
                            total_bytes = total_bytes - bytes;
                        }

                        fs.Close();
                        rs.Close();

                        FtpWebResponse uploadResponse = (FtpWebResponse)myRequest.GetResponse();
                        uploadResponse.Close();
                    }

                }

            }

            // Catch any errors
            catch (Exception Error)
            {

                Console.WriteLine("Couldn't upload the files.\nError: " + Error.ToString());
            }

        }

    }

}