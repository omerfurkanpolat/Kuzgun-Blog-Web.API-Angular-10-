namespace Sahika.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNewDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        SubCategoryId = c.Int(),
                        SubCategory_SubCategoryId = c.Int(),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_SubCategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.SubCategory_SubCategoryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.FavouritePosts",
                c => new
                    {
                        FavouritePostId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(),
                        UserId = c.Int(nullable: false),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.FavouritePostId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => new { t.PostId, t.UserId }, unique: true)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRegistered = c.DateTime(nullable: false),
                        LastActive = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        PostCommentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(),
                        Comment = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.PostCommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => new { t.PostId, t.UserId }, unique: true)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PostStats",
                c => new
                    {
                        PostStatId = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        Claps = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.PostStatId)
                .ForeignKey("dbo.Posts", t => t.PostStatId)
                .Index(t => t.PostStatId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(nullable: false),
                        DateCreated = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        MessageHeader = c.String(nullable: false),
                        MessageBody = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Posts", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Posts", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Posts", "SubCategory_SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.PostStats", "PostStatId", "dbo.Posts");
            DropForeignKey("dbo.PostComments", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.FavouritePosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostComments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavouritePosts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavouritePosts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.PostStats", new[] { "PostStatId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.PostComments", new[] { "Post_PostId" });
            DropIndex("dbo.PostComments", new[] { "PostId", "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FavouritePosts", new[] { "Post_PostId" });
            DropIndex("dbo.FavouritePosts", new[] { "PostId", "UserId" });
            DropIndex("dbo.Posts", new[] { "Category_CategoryId" });
            DropIndex("dbo.Posts", new[] { "SubCategory_SubCategoryId" });
            DropIndex("dbo.Posts", new[] { "SubCategoryId" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.SubCategories");
            DropTable("dbo.PostStats");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PostComments");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FavouritePosts");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
