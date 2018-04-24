using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Logic.Interfaces
{
    public interface ILikeLogic
    {
        bool GetLikeFromUser(string buildId, string userId);
        bool GetDislikeFromUser(string buildId, string userId);

        void SubmitLike(string buildId, string userId);
        void SubmitDislike(string buildId, string userId);
    }
}
