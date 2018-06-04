using System;
using System.Collections.Generic;
using System.Text;

namespace KillerApp.Domain
{
    public class LDBuilds
    {
        public string UserId { get; set; }
        public string BuildId { get; set; }
        public bool Liked { get; set; }

        public LDBuilds(string userId, string buildId, bool liked)
        {
            UserId = userId;
            BuildId = buildId;
            Liked = liked;
        }
    }
}
