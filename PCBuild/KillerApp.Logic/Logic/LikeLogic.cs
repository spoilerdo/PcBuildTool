using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;
using KillerApp.Logic.Interfaces;

namespace KillerApp.Logic.Logic
{
    public class LikeLogic : ILikeLogic
    {
        private readonly ILikeContext _context;

        public LikeLogic(ILikeContext context)
        {
            _context = context;
        }

        #region SelectMethods

        public bool GetLikeFromUser(string buildId, string userId) =>
            _context.CheckLikeStatus(buildId, userId) == true;

        public bool GetDislikeFromUser(string buildId, string userId) => 
            _context.CheckLikeStatus(buildId, userId) == false;

        #endregion

        #region InsertMethods

        public void SubmitLike(PcBuild build, string userId)
        {
            bool? likeStatus = _context.CheckLikeStatus(build.ID, userId);

            if (likeStatus == true)
                _context.RemoveLike(build, userId);
            else
            {
                if(likeStatus == false)
                    _context.RemoveDislike(build, userId);

                _context.AddLike(build, userId);
            }
        }

        public void SubmitDislike(PcBuild build, string userId)
        {
            bool? likeStatus = _context.CheckLikeStatus(build.ID, userId);

            if (likeStatus == false)
                _context.RemoveDislike(build, userId);
            else
            {
                if(likeStatus == true)
                    _context.RemoveLike(build, userId);

                _context.AddDislike(build, userId);
            }
        }

        #endregion
    }
}
