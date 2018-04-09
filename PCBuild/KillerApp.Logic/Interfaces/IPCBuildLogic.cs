using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.Domain;

namespace KillerApp.Logic.Interfaces
{
    public interface IPcBuildService
    {
        IEnumerable<PcPart> GetPartsByType(Build build, string latestType);
        IEnumerable<PcPart> GetSelectedParts(int buildiD);
        IEnumerable<string> GetAllTypes();
        IEnumerable<Result> GetPrices(IEnumerable<PcPart> pcParts, IEnumerable<Website> websites);
        IEnumerable<Website> GetWebsites();
        IEnumerable<Propertie> GetProperties();

        void SetBuild(int id);
        Build AddPcPart(Build build, PcPart pcPart);
        void AddPcPartToBuild(PcPart pcPart, int buildId);
        void AddPcPart(PcPart pcPart);
    }
}
