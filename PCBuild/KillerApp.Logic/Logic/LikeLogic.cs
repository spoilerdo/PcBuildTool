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

        public bool GetLikeFromUser(string buildId, string userId)
        {
            if (_context.CheckLikeStatus(buildId, userId) == "true")
                return true;

            return false;
        }

        public bool GetDislikeFromUser(string buildId, string userId)
        {
            if (_context.CheckLikeStatus(buildId, userId) == "false")
                return true;

            return false;
        }

        public void SubmitLike(string buildId, string userId)
        {
            if (_context.CheckLikeStatus(buildId, userId) == "true") //de gebruiker heeft al eens geliked
                _context.RemoveLike(buildId, userId);
            else //de gebruiker heeft nog niet geliked
            {
                if(_context.CheckLikeStatus(buildId, userId) == "false") //de gebruiker heeft wel al gedisliked
                    _context.RemoveDislike(buildId, userId);

                _context.AddLike(buildId, userId);
            }
        }

        public void SubmitDislike(string buildId, string userId)
        {
            if(_context.CheckLikeStatus(buildId, userId) == "false")
                _context.RemoveDislike(buildId, userId);
            else
            {
                if(_context.CheckLikeStatus(buildId, userId) == "true")
                    _context.RemoveLike(buildId, userId);

                _context.AddDislike(buildId, userId);
            }
        }

    }
}
