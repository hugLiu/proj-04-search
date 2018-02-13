using System.Data.Entity;
using Jurassic.So.Domain.Models;

namespace Jurassic.So.EntityFramework
{
    public partial class SooilDbContext : DbContext
    {
        public SooilDbContext()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Base_Article> Base_Article { get; set; }
        public virtual DbSet<Base_ArticleExt> Base_ArticleExt { get; set; }
        public virtual DbSet<Base_ArticleRelation> Base_ArticleRelation { get; set; }
        public virtual DbSet<Base_ArticleText> Base_ArticleText { get; set; }
        public virtual DbSet<Base_Catalog> Base_Catalog { get; set; }
        public virtual DbSet<Base_CatalogExt> Base_CatalogExt { get; set; }
        public virtual DbSet<Dep_Department> Dep_Department { get; set; }
        public virtual DbSet<Dep_DepHistory> Dep_DepHistory { get; set; }
        public virtual DbSet<Dep_DepPost> Dep_DepPost { get; set; }
        public virtual DbSet<Dep_DepUser> Dep_DepUser { get; set; }
        public virtual DbSet<Dep_Post> Dep_Post { get; set; }
        public virtual DbSet<Dep_PostHistory> Dep_PostHistory { get; set; }
        public virtual DbSet<Dep_PostUser> Dep_PostUser { get; set; }
        public virtual DbSet<Dep_UserHistory> Dep_UserHistory { get; set; }
        public virtual DbSet<Flow_Direct> Flow_Direct { get; set; }
        public virtual DbSet<Flow_Graph> Flow_Graph { get; set; }
        public virtual DbSet<Flow_Instance> Flow_Instance { get; set; }
        public virtual DbSet<Flow_Run> Flow_Run { get; set; }
        public virtual DbSet<Flow_RunUser> Flow_RunUser { get; set; }
        public virtual DbSet<Flow_Step> Flow_Step { get; set; }
        public virtual DbSet<Rec_BORelationPara> Rec_BORelationPara { get; set; }
        public virtual DbSet<Rec_BOSimilarConditions> Rec_BOSimilarConditions { get; set; }
        public virtual DbSet<Rec_BOSimilarPara> Rec_BOSimilarPara { get; set; }
        public virtual DbSet<Rec_BOSpatialPara> Rec_BOSpatialPara { get; set; }
        public virtual DbSet<Rec_SenceBORelation> Rec_SenceBORelation { get; set; }
        public virtual DbSet<Rec_SenceBOSimilar> Rec_SenceBOSimilar { get; set; }
        public virtual DbSet<Rec_SenceBOSpatial> Rec_SenceBOSpatial { get; set; }
        public virtual DbSet<Rec_SenceInfo> Rec_SenceInfo { get; set; }
        public virtual DbSet<Sooil_History_Data> Sooil_History_Data { get; set; }
        public virtual DbSet<Sooil_ISSUser_Infor> Sooil_ISSUser_Infor { get; set; }
        public virtual DbSet<Sooil_Range_BaseSetting> Sooil_Range_BaseSetting { get; set; }
        public virtual DbSet<Sooil_Range_BO> Sooil_Range_BO { get; set; }
        public virtual DbSet<Sooil_Range_BP> Sooil_Range_BP { get; set; }
        public virtual DbSet<Sooil_Range_Source> Sooil_Range_Source { get; set; }
        public virtual DbSet<Sooil_Search_History> Sooil_Search_History { get; set; }
        public virtual DbSet<Sooil_User_Education> Sooil_User_Education { get; set; }
        public virtual DbSet<Sooil_User_Info> Sooil_User_Info { get; set; }
        public virtual DbSet<Sooil_User_Setting> Sooil_User_Setting { get; set; }
        public virtual DbSet<Sys_BasicSettings> Sys_BasicSettings { get; set; }
        public virtual DbSet<Sys_Collection_Group> Sys_Collection_Group { get; set; }
        public virtual DbSet<Sys_Comment> Sys_Comment { get; set; }
        public virtual DbSet<Sys_Comment_Judge> Sys_Comment_Judge { get; set; }
        public virtual DbSet<Sys_Data_Collection> Sys_Data_Collection { get; set; }
        public virtual DbSet<Sys_Data_Rating> Sys_Data_Rating { get; set; }
        public virtual DbSet<Sys_Keyword_Collection> Sys_Keyword_Collection { get; set; }
        public virtual DbSet<Sys_Log> Sys_Log { get; set; }
        public virtual DbSet<Sys_Note> Sys_Note { get; set; }
        public virtual DbSet<Sys_Note_Tag> Sys_Note_Tag { get; set; }
        public virtual DbSet<Sys_Person_Info> Sys_Person_Info { get; set; }
        public virtual DbSet<Sys_User_DataOperate_Log> Sys_User_DataOperate_Log { get; set; }
        public virtual DbSet<Sys_User_Knowledge> Sys_User_Knowledge { get; set; }
        public virtual DbSet<Sys_User_Search_Log> Sys_User_Search_Log { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
        public virtual DbSet<Sooil_User_WorkInfo> Sooil_User_WorkInfo { get; set; }

        public virtual DbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Base_Article>()
                .Property(e => e.UrlTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Article>()
                .HasMany(e => e.Base_ArticleExt)
                .WithRequired(e => e.Base_Article)
                .HasForeignKey(e => e.ArticleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Base_Article>()
                .HasMany(e => e.Base_ArticleRelation)
                .WithRequired(e => e.Base_Article)
                .HasForeignKey(e => e.SourceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Base_Article>()
                .HasMany(e => e.Base_ArticleRelation1)
                .WithRequired(e => e.Base_Article1)
                .HasForeignKey(e => e.TargetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Base_Article>()
                .HasOptional(e => e.Base_ArticleText)
                .WithRequired(e => e.Base_Article);

            modelBuilder.Entity<Base_Catalog>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Catalog>()
                .Property(e => e.IconLocation)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Catalog>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<Base_Catalog>()
                .HasMany(e => e.Base_Article)
                .WithRequired(e => e.Base_Catalog)
                .HasForeignKey(e => e.CatalogId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Base_Catalog>()
                .HasMany(e => e.Base_Catalog1)
                .WithOptional(e => e.Base_Catalog2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Base_Catalog>()
                .HasMany(e => e.Base_CatalogExt)
                .WithRequired(e => e.Base_Catalog)
                .HasForeignKey(e => e.CatalogId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Base_CatalogExt>()
                .HasMany(e => e.Base_ArticleExt)
                .WithRequired(e => e.Base_CatalogExt)
                .HasForeignKey(e => e.CatlogExtId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dep_Department>()
                .HasMany(e => e.Dep_DepPost)
                .WithRequired(e => e.Dep_Department)
                .HasForeignKey(e => e.DepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dep_Department>()
                .HasMany(e => e.Dep_DepUser)
                .WithRequired(e => e.Dep_Department)
                .HasForeignKey(e => e.DepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dep_Department>()
                .HasMany(e => e.Dep_DepHistory)
                .WithRequired(e => e.Dep_Department)
                .HasForeignKey(e => e.DepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dep_DepPost>()
                .HasMany(e => e.Dep_PostUser)
                .WithRequired(e => e.Dep_DepPost)
                .HasForeignKey(e => e.DepPostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dep_DepUser>()
                .HasMany(e => e.Dep_PostUser)
                .WithRequired(e => e.Dep_DepUser)
                .HasForeignKey(e => e.DepUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dep_Post>()
                .HasMany(e => e.Dep_DepPost)
                .WithRequired(e => e.Dep_Post)
                .HasForeignKey(e => e.PostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Graph>()
                .HasMany(e => e.Flow_Instance)
                .WithRequired(e => e.Flow_Graph)
                .HasForeignKey(e => e.GraphId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Instance>()
                .HasMany(e => e.Flow_Run)
                .WithRequired(e => e.Flow_Instance)
                .HasForeignKey(e => e.InstanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Instance>()
                .HasMany(e => e.Flow_Run1)
                .WithRequired(e => e.Flow_Instance1)
                .HasForeignKey(e => e.InstanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Run>()
                .HasMany(e => e.Flow_RunUser)
                .WithRequired(e => e.Flow_Run)
                .HasForeignKey(e => e.RunId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Step>()
                .Property(e => e.UserFilter)
                .IsUnicode(false);

            modelBuilder.Entity<Flow_Step>()
                .HasMany(e => e.Flow_Direct)
                .WithRequired(e => e.Flow_Step)
                .HasForeignKey(e => e.StepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Step>()
                .HasMany(e => e.Flow_Direct1)
                .WithRequired(e => e.Flow_Step1)
                .HasForeignKey(e => e.NextStepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Step>()
                .HasMany(e => e.Flow_Graph)
                .WithRequired(e => e.Flow_Step)
                .HasForeignKey(e => e.StartStepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flow_Step>()
                .HasMany(e => e.Flow_Run)
                .WithRequired(e => e.Flow_Step)
                .HasForeignKey(e => e.StepId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rec_BORelationPara>()
                .HasMany(e => e.Rec_SenceBORelation)
                .WithRequired(e => e.Rec_BORelationPara)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rec_BOSimilarPara>()
                .HasOptional(e => e.Rec_BOSimilarConditions)
                .WithRequired(e => e.Rec_BOSimilarPara);

            modelBuilder.Entity<Rec_BOSimilarPara>()
                .HasMany(e => e.Rec_SenceBOSimilar)
                .WithRequired(e => e.Rec_BOSimilarPara)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rec_BOSpatialPara>()
                .HasMany(e => e.Rec_SenceBOSpatial)
                .WithRequired(e => e.Rec_BOSpatialPara)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rec_SenceInfo>()
                .HasMany(e => e.Rec_SenceBORelation)
                .WithRequired(e => e.Rec_SenceInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rec_SenceInfo>()
                .HasMany(e => e.Rec_SenceBOSimilar)
                .WithRequired(e => e.Rec_SenceInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rec_SenceInfo>()
                .HasMany(e => e.Rec_SenceBOSpatial)
                .WithRequired(e => e.Rec_SenceInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sooil_Range_BP>()
                .Property(e => e.CCCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasOptional(e => e.Sooil_Range_BaseSetting)
                .WithRequired(e => e.Sooil_User_Info);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasMany(e => e.Sooil_Range_BO)
                .WithRequired(e => e.Sooil_User_Info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasMany(e => e.Sooil_Range_BP)
                .WithRequired(e => e.Sooil_User_Info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasMany(e => e.Sooil_Range_Source)
                .WithRequired(e => e.Sooil_User_Info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasMany(e => e.Sooil_User_Education)
                .WithRequired(e => e.Sooil_User_Info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasOptional(e => e.Sooil_User_Setting)
                .WithRequired(e => e.Sooil_User_Info);

            modelBuilder.Entity<Sooil_User_Info>()
                .HasMany(e => e.Sooil_User_WorkInfo)
                .WithRequired(e => e.Sooil_User_Info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sys_Comment>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.ClientIP)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.LogType)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.Request)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.Browser)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.BrowserVersion)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.Platform)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Note>()
                .Property(e => e.noteContent)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User_Search_Log>()
                .HasMany(e => e.Sys_User_DataOperate_Log)
                .WithOptional(e => e.Sys_User_Search_Log)
                .HasForeignKey(e => e.parentLogId);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Base_Article)
                .WithRequired(e => e.UserProfile)
                .HasForeignKey(e => e.EditorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Dep_DepUser)
                .WithRequired(e => e.UserProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Dep_PostUser)
                .WithRequired(e => e.UserProfile)
                .HasForeignKey(e => e.OperationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasOptional(e => e.Sooil_ISSUser_Infor)
                .WithRequired(e => e.UserProfile);


        }
    }
}
