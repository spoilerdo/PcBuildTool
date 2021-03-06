﻿using KillerApp.DAL.Contexts.AccountContext;
using KillerApp.DAL.Repositories;
using KillerApp.Logic.Interfaces;
using KillerApp.Logic.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace KillerApp.Factory
{
    public class AccountFactory
    {
        public static IAccountLogic CreateLogic(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            return new AccountLogic(new AccountRepository(new AccountSqlContext(config)), httpContextAccessor);
        }

        public static IAccountLogic CreateTestLogic()
        {
            return new AccountLogic(new AccountRepository(new AccountMemoryContext()), null);
        }
    }
}