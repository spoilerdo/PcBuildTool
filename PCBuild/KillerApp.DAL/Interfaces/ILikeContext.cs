﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.DAL.Interfaces
{
    public interface ILikeContext
    {
        void AddLike(string buildId, string userId);
        void AddDislike(string buildId, string userId);
        void RemoveLike(string buildId, string userId);
        void RemoveDislike(string buildId, string userId);

        bool CheckIfLiked(string buildId, string userId);
        bool CheckIfDisliked(string buildId, string userId);

    }
}
