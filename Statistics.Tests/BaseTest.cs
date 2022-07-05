using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Statistics.Tests
{
    public class BaseTest
    {
        public static IServiceProvider ServiceProvider { get; set; }

        protected static IServiceCollection Services { get; set; }

        static BaseTest()
        {
            Services = new ServiceCollection();
        }
    }
}
