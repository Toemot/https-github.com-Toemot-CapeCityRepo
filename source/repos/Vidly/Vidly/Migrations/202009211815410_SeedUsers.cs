namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'34b39dae-ac85-43d5-9cbc-01838f8be25a', N'admin@vidly.com', 0, N'ABpG/QzJP3Qeu0Koi6q9XKyHLq6TTET1Bw4klXiWArkZRKJ9VbbIfn80AWQOJod9Cw==', N'816c4bf8-3eb7-4b05-9866-14dcbfb8e18d', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'54ca9643-c856-46e0-9991-d7f0426d4290', N'guest@vidly.com', 0, N'AMnVCQIXN7xWs6vhyILVkr8bzNrnUpndquM/DeShqp8tMDerzhEAgntMM/QkGhPwsw==', N'765aeac1-6204-4faa-be1c-f0b00c09138e', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a80a4e51-8a50-4684-927f-f593ec061714', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'34b39dae-ac85-43d5-9cbc-01838f8be25a', N'a80a4e51-8a50-4684-927f-f593ec061714')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
