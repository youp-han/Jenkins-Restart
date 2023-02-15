﻿using System;
using System.Configuration;
using JenkinsServiceControl.Core;
using NLog;

namespace JenkinsServiceControl
{
    class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            JenkinsControlCore jsc = new JenkinsControlCore();
            FolderDelete folderDelete = new FolderDelete();


            folderDelete.upperDirectoryName = ConfigurationSettings.AppSettings["directory_RootName"];
            jsc.serviceName = ConfigurationSettings.AppSettings["service_Name"];
            jsc.timeoutMillsec = 20000;

            //jsc.serviceURL = ConfigurationSettings.AppSettings["service_URL"];
            //jsc.jenkinsUserName = ConfigurationSettings.AppSettings["user_Name"];
            //jsc.jenkinsPassword = ConfigurationSettings.AppSettings["password"];


            string[] deleteFolderName = { "lastStable", "lastStableBuild", "lastSuccessful", "lastSuccessfulBuild" };

            try
            {
                if (jsc.GetServices())
                {
                    //1. Java CLI Safe-Shutdown
                    //jsc.SafeStopService(serviceName,  serviceURL,  jenkinsUser,  jenkinsPwd);

                    //1. Services Stop
                    jsc.ControlService(0);

                    //2. Folder search & delete
                    foreach (var folderName in deleteFolderName)
                    {
                        folderDelete.directoryName = folderName;
                        folderDelete.DeleteFolders();
                    }

                    //4. Jenkins Service Start
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