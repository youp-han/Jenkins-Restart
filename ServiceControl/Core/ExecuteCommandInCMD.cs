using System;
using System.Diagnostics;
using NLog;
using static System.Environment;

namespace ServiceControl.Core
{
    public class ExecuteCommandInCMD
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void runCommand(string serviceURL, string jenkinsUser, string jenkinsPwd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;            
            startInfo.CreateNoWindow = true;                                       
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;                                
            startInfo.RedirectStandardInput = true;                                
            startInfo.RedirectStandardError = true;                                   
            startInfo.FileName = @"cmd.exe";

            //명령어 설정
            //현재 폴더패스 에서 cmd (jenkins-cli.jar파일 위치) 폴더로 이동 후 명령어 진행
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string moveToCMD = @"cd " + currentDirectory + @"cmd";
            string shutdownCmd= @"java -jar jenkins-cli.jar -s " + serviceURL + " safe-shutdown --username " + jenkinsUser + " --password "+ jenkinsPwd;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;

            try
            {
                process.Start();

                Console.WriteLine(moveToCMD);
                logger.Info(moveToCMD);
                process.StandardInput.Write(moveToCMD + NewLine);

                Console.WriteLine(shutdownCmd);
                logger.Info(shutdownCmd);
                process.StandardInput.Write(shutdownCmd + NewLine);


            }
            catch (Exception e)
            {
                logger.Error(" StopService Error: " + e);

                throw;
            }
            finally
            {
                process.StandardInput.Close();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
            }
            
        }
    }
}
