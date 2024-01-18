﻿namespace Domain.Constants
{
    public static class SD
    {
        //Roles
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string MemberRole = "Member";

        public const string AdminUserName = "admin@automarket.com";
        public const string SuperAdminChangeNotAllowed = "Зміна облікового запису Суперадміна заборонена!";
        public const int MaximumLoginAttempts = 3;
    }
}
