using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Logic.Interfaces
{
    public interface ILikeLogic
    {
        void SubmitLike(string buildId, string userId);
        void SubmitDislike(string buildId, string userId);
    }
}
