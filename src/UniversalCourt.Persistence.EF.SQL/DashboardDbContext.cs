using UniversalCourt.Domain;
using UniversalCourt.Domain.Models;
using UniversalCourt.Infrastructure.TenantSupport;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversalCourt.Persistence.EF.SQL;

public class DashboardDbContext : IdentityDbContext<Profile, IdentityRole, string>
{
    private readonly ITenantProvider _tenantProvider;


    //public DbSet<--> -- { get; set; } = null!;
    public DbSet<SuperSetting> SuperSettings { get; set; } = null!;
    public DbSet<EnableDevice> EnableDevices { get; set; } = null!;
    public DbSet<TemplateCategory> TemplateCategories { get; set; } = null!;
    public DbSet<TemplateType> TemplateTypes { get; set; } = null!;
    public DbSet<MailingSystem> MailingSystems { get; set; } = null!;
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<WebPage> WebPages { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogCategory> BlogCategories { get; set; }
    public DbSet<PageSection> PageSections { get; set; }
    public DbSet<PageSectionList> PageSectionLists { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<PageCategory> PageCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }
    public DbSet<ProductSample> ProductSamples { get; set; }
    public DbSet<Testimony> Testimonies { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<NewsLetter> NewsLetter { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }

    //project dbset
    public DbSet<ProjectCategory> ProjectCategories { get; set; }

    public DbSet<ProjectMain> ProjectMains { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<ProjectTeam> ProjectTeams { get; set; }
    public DbSet<ProjectPosition> ProjectPositions { get; set; }
    public DbSet<ProjectFile> ProjectFiles { get; set; }
    public DbSet<ProjectFeature> ProjectFeatures { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<GeneralFeature> GeneralFeatures { get; set; }
    public DbSet<PostModal> PostModal { get; set; }
    public DbSet<DataConfig> DataConfigs { get; set; }







    public DbSet<Message> Messages { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<ClientRequest> ClientRequest { get; set; }
    public DbSet<Report> Report { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<TrainingAttendance> TrainingAttendances { get; set; }
    public DbSet<TrainingSchedule> TrainingSchedules { get; set; }




    public DbSet<Plan> Plans { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<JobFeature> JobFeatures { get; set; }
    public DbSet<Job> Jobs { get; set; }

    public DbSet<FundSource> FundSources { get; set; }
    public DbSet<CompanyFund> CompanyFunds { get; set; }
    public DbSet<ExpensesCategory> ExpensesCategories { get; set; }
    public DbSet<CompayExpenses> CompayExpenses { get; set; }
    public DbSet<Salary> Salarys { get; set; }
    public DbSet<JobDesignation> JobDesignations { get; set; }


    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<ProposalFile> ProposalFiles { get; set; }
    public DbSet<ChatMessage> ChatMessage { get; set; }
    public DbSet<CareerFile> CareerFiles { get; set; }
    public DbSet<CareerPath> CareerPaths { get; set; }
    
    public DbSet<LocalGoverment> LocalGoverments { get; set; }
    public DbSet<State> States { get; set; }





    public DbSet<AdditionalProfile> AdditionalProfiles { get; set; }
    public DbSet<ProfileCategory> ProfileCategories { get; set; }
    public DbSet<ProfileCategoryList> ProfileCategoryLists { get; set; }
    public DbSet<ProfilePortfolio> ProfilePortfolios { get; set; } 
    public DbSet<ProfilePortfolioSetting> ProfilePortfolioSetting { get; set; }
    public DbSet<PortfolioContactUs> PortfolioContactUses { get; set; }
    public DbSet<PortfolioTemplate> PortfolioTemplates { get; set; }


    public DbSet<OfficeActivityCategory> OfficeActivityCategories { get; set; }
    public DbSet<OfficeActivityDialy> OfficeActivityDialies { get; set; }



    public DbSet<DashboardSetting> DashboardSettings { get; set; }
    public DbSet<DashboardData> DashboardDatas { get; set; }
    public DbSet<DashboardDataList> DashboardDataLists { get; set; }
    public DbSet<CompanyProgram> CompanyPrograms { get; set; }
    public DbSet<CompanyProgramCategory> CompanyProgramCategories { get; set; }
    public DbSet<ProgramUser> ProgramUsers { get; set; }
    public DbSet<UserCategory> UserCategories { get; set; }


    public DashboardDbContext(DbContextOptions options, ITenantProvider tenantProvider) : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(
            _tenantProvider.ConnectionString,
            o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, /*_tenantProvider.DbSchemaName*/null));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        //configurationBuilder.Properties<string>()
        //    .HaveMaxLength(255);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema(_tenantProvider.DbSchemaName);   // set schema

        base.OnModelCreating(modelBuilder);

        // ConfigureCustomer(modelBuilder);
    }

    //private static void ConfigureCustomer(ModelBuilder builder)
    //{
    //    builder.Entity<Customer>(b =>
    //    {
    //        var table = b.ToTable("Customers");

    //        table.Property(p => p.CustomerId).ValueGeneratedOnAdd();
    //        table.HasKey(p => p.CustomerId).HasName("PK_CustomerId");
    //    });
    //}
}
