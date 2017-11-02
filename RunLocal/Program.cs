using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;//add
using System.ServiceModel.Description;//add
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary1;

namespace RunLocal
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost serviceHost = new ServiceHost(typeof(ServiceTest));
                //foreach (ServiceEndpoint EP in serviceHost.Description.Endpoints)
                //{
                //    EP.Behaviors.Add(new BehaviorAttributeExternal());//: Attribute, IEndpointBehavior, IOperationBehavior
                //}
                //serviceHost.Authorization.ServiceAuthorizationManager = new ExternalAuthorizationManager();//: IDispatchMessageInspector
                serviceHost.Open();

                //ServiceHost serviceHost2 = new ServiceHost(typeof(Algorithm));
                //foreach (ServiceEndpoint EP in serviceHost2.Description.Endpoints)
                //{
                //    EP.Behaviors.Add(new BehaviorAttributeExternal());//: Attribute, IEndpointBehavior, IOperationBehavior
                //}
                //serviceHost2.Authorization.ServiceAuthorizationManager = new ExternalAuthorizationManager();//: IDispatchMessageInspector
                //serviceHost2.Open();

                Console.WriteLine("The service is ready");
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                serviceHost.Close();
                //serviceHost2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
