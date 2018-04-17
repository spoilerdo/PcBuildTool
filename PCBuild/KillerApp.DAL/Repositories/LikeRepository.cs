using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Interfaces;

namespace KillerApp.DAL.Repositories
{
    public class LikeRepository : ILikeContext
    {
        private readonly ILikeContext _likeContext;

        public LikeRepository(ILikeContext likeContext)
        {
            _likeContext = likeContext;
        }

        public void AddLike(string buildId, string userId) => _likeContext.AddLike(buildId, userId);

        public void AddDislike(string buildId, string userId) => _likeContext.AddDislike(buildId, userId);

        public void RemoveLike(string buildId, string userId) => _likeContext.RemoveLike(buildId, userId);

        public void RemoveDislike(string buildId, string userId) => _likeContext.RemoveDislike(buildId, userId);


        public bool CheckIfLiked(string buildId, string userId) => _likeContext.CheckIfLiked(buildId, userId);

        public bool CheckIfDisliked(string buildId, string userId) => _likeContext.CheckIfDisliked(buildId, userId);
    }
}
