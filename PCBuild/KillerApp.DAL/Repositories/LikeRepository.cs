using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Repositories
{
    public class LikeRepository : ILikeContext
    {
        private readonly ILikeContext _likeContext;

        public LikeRepository(ILikeContext likeContext)
        {
            _likeContext = likeContext;
        }

        #region SelectMethods

        public bool? CheckLikeStatus(string buildId, string userId) => _likeContext.CheckLikeStatus(buildId, userId);

        #endregion

        #region InsertMethods

        public void AddLike(PcBuild build, string userId) => _likeContext.AddLike(build, userId);

        public void AddDislike(PcBuild build, string userId) => _likeContext.AddDislike(build, userId);

        public void RemoveLike(PcBuild build, string userId) => _likeContext.RemoveLike(build, userId);

        public void RemoveDislike(PcBuild build, string userId) => _likeContext.RemoveDislike(build, userId);

        #endregion
    }
}
