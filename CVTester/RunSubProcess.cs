using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CVTester
{
    class RunSubProcess
    {
        public static ExperimentResult run(string fileName)
        {
            string currentDir = Directory.GetCurrentDirectory();

            string mainFileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string logFileName = String.Format("{0}\\results\\{1}.log", currentDir, mainFileName);
            string resultPicFileName = String.Format("{0}\\results\\{1}.png", currentDir, mainFileName);
            ProcessStartInfo processInfo = new ProcessStartInfo("tester.exe", String.Format("{0} {1} {2}", fileName, logFileName, resultPicFileName));
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;

            Process process = Process.Start(processInfo);
            process.WaitForExit();

            int exitCode = process.ExitCode;
            try
            {
                if (exitCode == 0)
                {
                    ExperimentResult result = new ExperimentResult();
                    result.textFileName = logFileName;
                    result.pictureFileName = resultPicFileName;
                    return result;
                }
                else
                {
                    throw new Exception("no result");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                process.Close();
            }
        }
    }
}
