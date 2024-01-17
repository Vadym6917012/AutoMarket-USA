﻿using Application.DTOs.Account;

namespace Application.AccountMediatoR.Queries
{
    public class RefreshTokenResult
    {
        public ApplicationUserDTO ApplicationUserDTO { get; set; }
        public string RefreshedToken { get; set; }
        public DateTimeOffset? DateExpiresUtc { get; set; }
    }
}
