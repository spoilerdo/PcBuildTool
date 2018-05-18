using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.Domain;

namespace KillerApp.DAL.Interfaces
{
    public interface ILikeContext
    {
        bool? CheckLikeStatus(string buildId, string userId);

        void AddLike(PcBuild build, string userId);
        void AddDislike(PcBuild build, string userId);
        void RemoveLike(PcBuild build, string userId);
        void RemoveDislike(PcBuild build, string userId);
    }
}
