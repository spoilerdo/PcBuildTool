using KillerApp.DAL.Contexts.PcBuildContext;
using KillerApp.DAL.Repositories;
using KillerApp.Logic.Interfaces;
using KillerApp.Logic.Logic;
using Microsoft.Extensions.Configuration;

namespace KillerApp.Factory
{
    public class PcBuildFactory
    {
        public static IPcBuildLogic CreateLogic(IConfiguration config)
        {
            return new PcBuildLogic(new PcBuildRepository(new PcBuildSqlContext(config)));
        }

        public static IPcBuildLogic CreateTestLogic()
        {
            return new PcBuildLogic(new PcBuildRepository(new PcBuildMemoryContext()));
        }
    }
}