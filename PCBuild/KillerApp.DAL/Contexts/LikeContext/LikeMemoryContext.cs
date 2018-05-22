using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Interfaces;
using KillerApp.Domain;

namespace KillerApp.DAL.Contexts.LikeContext
{
    public class LikeMemoryContext : ILikeContext
    {
        private readonly List<LDBuilds> _context = new List<LDBuilds>();

        public PcBuild PcBuild { get; set; }

        public bool? CheckLikeStatus(string buildId, string userId)
        {
            try
            {
                return _context.Find(x => x.BuildId == buildId && x.UserId == userId).Liked;
            }
            catch
            {
                return null;
            }
        }

        public void AddLike(PcBuild build, string userId)
        {
            build.Likes += 1;
            PcBuild = build;

            _context.Add(new LDBuilds(userId, build.ID, true));
        }

        public void AddDislike(PcBuild build, string userId)
        {
            build.Dislikes += 1;
            PcBuild = build;

            _context.Add(new LDBuilds(userId, build.ID, false));
        }

        public void RemoveLike(PcBuild build, string userId)
        {
            build.Likes -= 1;
            PcBuild = build;

            _context.Clear();
        }

        public void RemoveDislike(PcBuild build, string userId)
        {
            build.Dislikes -= 1;
            PcBuild = build;

            _context.Clear();
        }
    }
}
