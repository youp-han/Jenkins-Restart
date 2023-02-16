using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using NLog;

namespace JenkinsServiceControl.Core
{
    public class ServiceControlCore
    {
        public string serviceName { get; set; }
        public int timeoutMillsec { get; set; }
        public string serviceURL { get; set; }
        public string jenkinsUserName { get; set; }
        public string  jenkinsPassword { get; set; }


        private static Logger logger = LogManager.GetCurrentClassLogger();


        //Services 에서 돌고 있는 리스트 출력
        public bool GetServices()
        {
            ServiceController[] services =  ServiceController.GetServices();
            var checkServiceName = services.FirstOrDefault(s => s.ServiceName == serviceName);
            bool result = false;

            if (checkServiceName != null && checkServiceName.Status.Equals(ServiceControllerStatus.Running))
            {
                logger.Info(" < " + serviceName + " > " + " Service Found, ReStart Sequence Started");
                Console.WriteLine(" < " + serviceName + " > " + " Service Found, ReStart Sequence Started");
                result = true;
            }
            else
            {
                logger.Info(" < " + serviceName + " > " + " Service NOT Found");
                Console.WriteLine(" < " + serviceName + " > " + " Service NOT Found");
            }

            return result;
        }

        
        public void ControlService(int flag)
        {
            ServiceController service = new ServiceController(serviceName);
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMillsec);

            try
            {

                switch (flag)
                {
                    case 0: //stop
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                        if (!CheckServiceStopped())
                        {
                            logger.Info(" Service Stopped Successfully.");
                            Console.WriteLine("  Service Stopped Successfully.");

                        }
                        break;

                    case 1: //start
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                        if (CheckServiceStarted())
                        {
                            logger.Info(" Service Started Successfully.");
                            Console.WriteLine("  Service Started Successfully.");
                        }
                        break;
                }
                
            }
            catch (Exception e)
            {
                logger.Error(" ControlService Error: " + flag + ", " + e);
                throw;
            }
            finally
            {
                service.Dispose();
            }

        }


        #region  SafeStopService Method : in Progress
        //public void SafeStopService()
        //{
        //    ExecuteCommandInCMD ecc = new ExecuteCommandInCMD();

        //    try
        //    {
        //        ecc.runCommand(serviceURL, jenkinsUserName, jenkinsPassword);

        //        if (!CheckServiceStopped())
        //        {
        //            Console.WriteLine(" Service Stopped Successfully.");
        //            logger.Info(" Service Stopped Successfully.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(" StopService Error: " + e);
        //        throw;
        //    }

        //}

        #endregion



        //Safe Stop 은 돌고 있는 job 이 끝나면 서비스가 내려가기 때문에
        //지속적으로 멈추는 시점을 확인 해야 한다.
        bool CheckServiceStopped()
        {
            if (GetServices())
            {
                logger.Info(" Service is Stopping.......................................");
                Console.WriteLine(" Service is Stopping.......................................");

                Thread.Sleep(timeoutMillsec/10);
                CheckServiceStopped();
            }

            return false;
        }

        bool CheckServiceStarted()
        {
            if (!GetServices())
            {
                logger.Info(" Service is Starting.......................................");
                Console.WriteLine(" Service  is Starting.......................................");
                Thread.Sleep(timeoutMillsec / 10);
                CheckServiceStarted();
            }

            return true;
        }
    }
}
