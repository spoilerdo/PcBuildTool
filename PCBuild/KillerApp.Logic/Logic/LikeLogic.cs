using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Interfaces;
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

        public void SubmitLike(string buildId, string userId)
        {
            if(_context.CheckIfLiked(buildId, userId))
                _context.RemoveLike(buildId, userId);
            else
                _context.AddLike(buildId, userId);
        }

        public void SubmitDislike(string buildId, string userId)
        {
            if(_context.CheckIfDisliked(buildId, userId))
                _context.RemoveDislike(buildId, userId);
            else
                _context.AddDislike(buildId, userId);
        }

    }
}
