using Microsoft.Practices.Unity.Mvc;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Controllers;

namespace MvcAuth
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetUpContainer();
       
        }
        private static void SetUpContainer()
        {
            //var container = new UnityContainer();
            //container.RegisterType<ITwitterClient, TwitterClient>();

            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ITwitterClient, TwitterClient>();
            container.RegisterType<AccountController>(new InjectionConstructor() );
               // container.RegisterType<AccountController>(new InjectionConstructor());

            return container;
        }
    }
}