﻿using System;
using System.Collections.Generic;
using System.Text;
using KillerApp.DAL.Contexts.LikeContext;
using KillerApp.DAL.Repositories;
using KillerApp.Logic.Interfaces;
using KillerApp.Logic.Logic;
using Microsoft.Extensions.Configuration;

namespace KillerApp.Factory
{
    public class LikeFactory
    {
        public static ILikeLogic CreateLogic(IConfiguration config)
        {
            return new LikeLogic(new LikeRepository(new LikeSqlContext(config)));
        }

        public static ILikeLogic CreateTestLogic()
        {
            return new LikeLogic(new LikeRepository(new LikeMemoryContext()));
        }
    }
}
