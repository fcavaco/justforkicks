using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace kata.ms.shoppingbasket.tests.Extensions
{
    // helper for tests. helps validate router declaration in controllers
    public static class RouteAttributeExtension
    {
        public static string Value(this RouteAttribute self)
        {
            
            return self.Template;
        }

        public static string Value(this RouteAttribute self, string key, string value)
        {
            return self.Template.Replace(key,value);
        }
    }
}
