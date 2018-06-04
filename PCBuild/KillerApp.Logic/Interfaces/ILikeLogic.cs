using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.Domain;

namespace KillerApp.Logic.Interfaces
{
    public interface ILikeLogic
    {
        bool GetLikeFromUser(string buildId, string userId);
        bool GetDislikeFromUser(string buildId, string userId);

        void SubmitLike(PcBuild build, string userId);
        void SubmitDislike(PcBuild build, string userId);
    }
}
