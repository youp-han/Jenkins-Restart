using System;
using System.Configuration;
using ServiceControl.Core;
using NLog;

namespace JenkinsServiceControl
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            ServiceControlCore jsc = new ServiceControlCore();
            FolderControlCore folderDelete = new FolderControlCore();


            folderDelete.upperDirectoryName = ConfigurationManager.AppSettings["directory_RootName"];
            jsc.serviceName = ConfigurationManager.AppSettings["service_Name"];
            jsc.timeoutMillsec = 20000;

            //jsc.serviceURL = ConfigurationManager.AppSettings["service_URL"];
            //jsc.jenkinsUserName = ConfigurationManager.AppSettings["user_Name"];
            //jsc.jenkinsPassword = ConfigurationManager.AppSettings["password"];


            string[] deleteFolderName = { "lastStable", "lastStableBuild", "lastSuccessful", "lastSuccessfulBuild" };

            try
            {
                if (jsc.GetServices())
                {
                    //1. Java CLI Safe-Shutdown - in progress
                    //jsc.SafeStopService(serviceName,  serviceURL,  jenkinsUser,  jenkinsPwd);

                    //1. Services Stop
                    jsc.ControlService(0);

                    //2. Folder search & delete
                    foreach (var folderName in deleteFolderName)
                    {
                        folderDelete.directoryName = folderName;
                        folderDelete.DeleteFolders();
                    }

                    //3. Jenkins Service Start
                    jsc.ControlService(1);
                    Console.WriteLine(" < " + jsc.serviceName + " > " + " Service Restarted.... Restart Sequence Ended");
                    Console.ReadLine();

                }
                else
                {
                    logger.Info(" < " + jsc.serviceName + " > " + " Either Service NOT Found, or Not Running");
                }



            }
            catch (Exception e)
            {
                logger.Error(" Error: " + e);
            }

            

        }
    }
}
