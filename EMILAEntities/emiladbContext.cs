using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApi.EMILAEntities
{
    public partial class emiladbContext : DbContext
    {
        public emiladbContext()
        {
        }

        public emiladbContext(DbContextOptions<emiladbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAuditLog> TblAuditLogs { get; set; }
        public virtual DbSet<TblBorrowerMst> TblBorrowerMsts { get; set; }
        public virtual DbSet<TblBranchMaster> TblBranchMasters { get; set; }
        public virtual DbSet<TblCompanyMaster> TblCompanyMasters { get; set; }
        public virtual DbSet<TblErrorLog> TblErrorLogs { get; set; }
        public virtual DbSet<TblLoanFinance> TblLoanFinances { get; set; }
        public virtual DbSet<TblLoanFolder> TblLoanFolders { get; set; }
        public virtual DbSet<TblLoanMst> TblLoanMsts { get; set; }
        public virtual DbSet<TblLoanNeed> TblLoanNeeds { get; set; }
        public virtual DbSet<TblLoanProperty> TblLoanProperties { get; set; }
        public virtual DbSet<TblLoanStage> TblLoanStages { get; set; }
        public virtual DbSet<TblLoanSubFolder> TblLoanSubFolders { get; set; }
        public virtual DbSet<TblLoanTask> TblLoanTasks { get; set; }
        public virtual DbSet<TblLoanTeam> TblLoanTeams { get; set; }
        public virtual DbSet<TblMilestone> TblMilestones { get; set; }
        public virtual DbSet<TblNeedsDefalt> TblNeedsDefalts { get; set; }
        public virtual DbSet<TblSection> TblSections { get; set; }
        public virtual DbSet<TblStatus> TblStatuses { get; set; }
        public virtual DbSet<TblTaskGroup> TblTaskGroups { get; set; }
        public virtual DbSet<TblTasksDefalt> TblTasksDefalts { get; set; }
        public virtual DbSet<TblTenantMaster> TblTenantMasters { get; set; }
        public virtual DbSet<TblTenantMilestone> TblTenantMilestones { get; set; }
        public virtual DbSet<TblTenantNeed> TblTenantNeeds { get; set; }
        public virtual DbSet<TblTenantTask> TblTenantTasks { get; set; }
        public virtual DbSet<TblTenantUserPersona> TblTenantUserPersonas { get; set; }
        public virtual DbSet<TblUserGroup> TblUserGroups { get; set; }
        public virtual DbSet<TblUserMaster> TblUserMasters { get; set; }
        public virtual DbSet<TblUserPersona> TblUserPersonas { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=162.220.162.122;user id=u_loancentraltes;password=Pass#123;database=db_loancentraltes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAuditLog>(entity =>
            {
                entity.ToTable("tbl_audit_log");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Ip)
                    .HasMaxLength(100)
                    .HasColumnName("ip")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoginDate)
                    .HasColumnName("login_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblBorrowerMst>(entity =>
            {
                entity.HasKey(e => e.BorrowerId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_borrower_mst");

                entity.HasIndex(e => e.TenantId, "FK_tbl_borrower_mst_tbl_tenant_master");

                entity.Property(e => e.BorrowerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("borrower_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsMainBorrower)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_main_borrower")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LangInfo)
                    .HasMaxLength(100)
                    .HasColumnName("lang_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblBorrowerMsts)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_tbl_borrower_mst_tbl_tenant_master");
            });

            modelBuilder.Entity<TblBranchMaster>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_branch_master");

                entity.HasIndex(e => e.CompanyId, "FK_tbl_branch_master_tbl_company_master");

                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");

                entity.Property(e => e.BranchAddress1)
                    .HasMaxLength(150)
                    .HasColumnName("branch_address_1")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchAddress2)
                    .HasMaxLength(150)
                    .HasColumnName("branch_address_2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchCity)
                    .HasMaxLength(100)
                    .HasColumnName("branch_city")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchCountry)
                    .HasMaxLength(100)
                    .HasColumnName("branch_country")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(100)
                    .HasColumnName("branch_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchNotes)
                    .HasMaxLength(500)
                    .HasColumnName("branch_notes")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchPcEmail)
                    .HasMaxLength(100)
                    .HasColumnName("branch_pc_email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchPcName)
                    .HasMaxLength(100)
                    .HasColumnName("branch_pc_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchPcPhone)
                    .HasMaxLength(50)
                    .HasColumnName("branch_pc_phone")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchState)
                    .HasMaxLength(100)
                    .HasColumnName("branch_state")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("int(11)")
                    .HasColumnName("company_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TblBranchMasters)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_branch_master_tbl_company_master");
            });

            modelBuilder.Entity<TblCompanyMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_company_master");

                entity.HasIndex(e => e.TenantId, "FK_tbl_company_master_tbl_tenant_master");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("int(11)")
                    .HasColumnName("company_id");

                entity.Property(e => e.CompanyAddress1)
                    .HasMaxLength(150)
                    .HasColumnName("company_address 1")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyAddress2)
                    .HasMaxLength(150)
                    .HasColumnName("company_address 2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyCity)
                    .HasMaxLength(100)
                    .HasColumnName("company_city")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyCountry)
                    .HasMaxLength(100)
                    .HasColumnName("company_country")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(150)
                    .HasColumnName("company_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyNotes)
                    .HasMaxLength(500)
                    .HasColumnName("company_notes")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyPcEmail)
                    .HasMaxLength(150)
                    .HasColumnName("company_pc_email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyPcName)
                    .HasMaxLength(150)
                    .HasColumnName("company_pc_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyPcPhone)
                    .HasMaxLength(50)
                    .HasColumnName("company_pc_phone")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CompanyState)
                    .HasMaxLength(100)
                    .HasColumnName("company_state")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblCompanyMasters)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_tbl_company_master_tbl_tenant_master");
            });

            modelBuilder.Entity<TblErrorLog>(entity =>
            {
                entity.ToTable("tbl_error_log");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Desc)
                    .HasColumnType("varchar(10000)")
                    .HasColumnName("desc")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FunctonName)
                    .HasMaxLength(100)
                    .HasColumnName("functon_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblLoanFinance>(entity =>
            {
                entity.HasKey(e => e.FinanceId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_finance");

                entity.HasIndex(e => e.LoanId, "FK_tbl_loan_finance_tbl_loan_mst");

                entity.Property(e => e.FinanceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("finance_id");

                entity.Property(e => e.AdjustedMargin)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("adjusted_margin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AdjustedMarginBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("adjusted_margin_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("amount")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchComm)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("branch_comm")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchCommBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("branch_comm_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Lc)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("LC")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LcBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("LC_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Lo2Comm)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("LO2_comm")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Lo2CommBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("LO2_comm_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoComm)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("LO_comm")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoCommBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("LO_comm_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Margin)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("margin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MarginBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("margin_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SmComm)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("SM_comm")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SmCommBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("SM_comm_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalComm)
                    .HasColumnType("decimal(12,2)")
                    .HasColumnName("total_comm")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalCommBps)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("total_comm_BPS")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.TblLoanFinances)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("FK_tbl_loan_finance_tbl_loan_mst");
            });

            modelBuilder.Entity<TblLoanFolder>(entity =>
            {
                entity.HasKey(e => e.FolderId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_folder");

                entity.Property(e => e.FolderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("folder_id");

                entity.Property(e => e.FolderName)
                    .HasMaxLength(50)
                    .HasColumnName("folder_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblLoanMst>(entity =>
            {
                entity.HasKey(e => e.LoanId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_mst");

                entity.HasIndex(e => e.BranchId, "FK_tbl_loan_mst_tbl_borrower_mst");

                entity.HasIndex(e => e.CoBorrower1Id, "FK_tbl_loan_mst_tbl_borrower_mst_2");

                entity.HasIndex(e => e.CoBorrower2Id, "FK_tbl_loan_mst_tbl_borrower_mst_3");

                entity.HasIndex(e => e.CoBorrower3Id, "FK_tbl_loan_mst_tbl_borrower_mst_4");

                entity.HasIndex(e => e.CoBorrower4Id, "FK_tbl_loan_mst_tbl_borrower_mst_5");

                entity.HasIndex(e => e.BorrowerId, "FK_tbl_loan_mst_tbl_borrower_mst_main");

                entity.HasIndex(e => e.FolderId, "FK_tbl_loan_mst_tbl_loan_folder");

                entity.HasIndex(e => e.StageId, "FK_tbl_loan_mst_tbl_loan_stage");

                entity.HasIndex(e => e.SubFolderId, "FK_tbl_loan_mst_tbl_loan_sub_folder");

                entity.HasIndex(e => e.TenantId, "FK_tbl_loan_mst_tbl_tenant_master");

                entity.HasIndex(e => e.MilestoneId, "FK_tbl_loan_mst_tbl_tenant_milestone");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id");

                entity.Property(e => e.BorrowerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("borrower_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BuyerAgent)
                    .HasMaxLength(100)
                    .HasColumnName("buyer_agent")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BuyerAgentInfo)
                    .HasMaxLength(250)
                    .HasColumnName("buyer_agent_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CloseDate)
                    .HasColumnName("close_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CoBorrower1Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("co_borrower_1_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CoBorrower2Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("co_borrower_2_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CoBorrower3Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("co_borrower_3_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CoBorrower4Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("co_borrower_4_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreditRepair)
                    .HasMaxLength(250)
                    .HasColumnName("credit_repair")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(150)
                    .HasColumnName("customer_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FolderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("folder_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FollowupDate)
                    .HasColumnName("followup_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Fthb)
                    .HasColumnType("bit(1)")
                    .HasColumnName("FTHB")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LeadDate)
                    .HasColumnType("date")
                    .HasColumnName("lead_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ListingAgentInfo)
                    .HasMaxLength(250)
                    .HasColumnName("listing_agent_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanProg)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_prog")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanSource)
                    .HasMaxLength(100)
                    .HasColumnName("loan_source")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanType)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_type")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Priority)
                    .HasColumnType("bit(1)")
                    .HasColumnName("priority")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Refferral)
                    .HasMaxLength(100)
                    .HasColumnName("refferral")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("stage_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SubFolderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sub_folder_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TargetDates)
                    .HasMaxLength(250)
                    .HasColumnName("target_dates")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TitleCompanyInfo)
                    .HasMaxLength(250)
                    .HasColumnName("title_company_info")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.TblLoanMstBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_borrower_mst_main");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TblLoanMsts)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_branch_master");

                entity.HasOne(d => d.CoBorrower1)
                    .WithMany(p => p.TblLoanMstCoBorrower1s)
                    .HasForeignKey(d => d.CoBorrower1Id)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_borrower_mst_2");

                entity.HasOne(d => d.CoBorrower2)
                    .WithMany(p => p.TblLoanMstCoBorrower2s)
                    .HasForeignKey(d => d.CoBorrower2Id)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_borrower_mst_3");

                entity.HasOne(d => d.CoBorrower3)
                    .WithMany(p => p.TblLoanMstCoBorrower3s)
                    .HasForeignKey(d => d.CoBorrower3Id)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_borrower_mst_4");

                entity.HasOne(d => d.CoBorrower4)
                    .WithMany(p => p.TblLoanMstCoBorrower4s)
                    .HasForeignKey(d => d.CoBorrower4Id)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_borrower_mst_5");

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.TblLoanMsts)
                    .HasForeignKey(d => d.FolderId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_loan_folder");

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.TblLoanMsts)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_tenant_milestone");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.TblLoanMsts)
                    .HasForeignKey(d => d.StageId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_loan_stage");

                entity.HasOne(d => d.SubFolder)
                    .WithMany(p => p.TblLoanMsts)
                    .HasForeignKey(d => d.SubFolderId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_loan_sub_folder");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblLoanMsts)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_tbl_loan_mst_tbl_tenant_master");
            });

            modelBuilder.Entity<TblLoanNeed>(entity =>
            {
                entity.HasKey(e => e.LoanneedsId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_needs");

                entity.HasIndex(e => e.LoanId, "FK_tbl_loan_needs_tbl_loan_mst");

                entity.HasIndex(e => e.SectionId, "FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasIndex(e => e.StatusId, "FK_tbl_tasks_defalt_tbl_status");

                entity.HasIndex(e => e.TaskGroupId, "FK_tbl_tasks_defalt_tbl_task_groups");

                entity.HasIndex(e => e.OwnId, "FK_tbl_tasks_defalt_tbl_user_personas");

                entity.Property(e => e.LoanneedsId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loanneedsId");

                entity.Property(e => e.Coordinating)
                    .HasColumnType("bit(1)")
                    .HasColumnName("coordinating")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Customer)
                    .HasMaxLength(150)
                    .HasColumnName("customer")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DescEn)
                    .HasMaxLength(250)
                    .HasColumnName("desc_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DescSp)
                    .HasMaxLength(250)
                    .HasColumnName("desc_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NeedEn)
                    .HasMaxLength(150)
                    .HasColumnName("need_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NeedSp)
                    .HasMaxLength(150)
                    .HasColumnName("need_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OwnId)
                    .HasColumnType("int(11)")
                    .HasColumnName("own_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Sample)
                    .HasMaxLength(150)
                    .HasColumnName("sample")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SectionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SectionPos)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("task_group_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Tracking)
                    .HasColumnType("bit(1)")
                    .HasColumnName("tracking")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Video)
                    .HasMaxLength(150)
                    .HasColumnName("video")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e._3rdparty)
                    .HasColumnType("bit(1)")
                    .HasColumnName("3rdparty")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.TblLoanNeeds)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("FK_tbl_loan_needs_tbl_loan_mst");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.TblLoanNeeds)
                    .HasForeignKey(d => d.OwnId)
                    .HasConstraintName("tbl_loan_needs_ibfk_5");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TblLoanNeeds)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("tbl_loan_needs_ibfk_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblLoanNeeds)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("tbl_loan_needs_ibfk_3");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.TblLoanNeeds)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("tbl_loan_needs_ibfk_4");
            });

            modelBuilder.Entity<TblLoanProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_property");

                entity.HasIndex(e => e.LoanId, "FK_tbl_loan_property_tbl_loan_mst");

                entity.Property(e => e.PropertyId)
                    .HasColumnType("int(11)")
                    .HasColumnName("property_id");

                entity.Property(e => e.AppraisalNotes)
                    .HasMaxLength(150)
                    .HasColumnName("appraisal_notes")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisalStatus)
                    .HasMaxLength(100)
                    .HasColumnName("appraisal_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CdStatus)
                    .HasMaxLength(100)
                    .HasColumnName("cd_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ContingencyInfo)
                    .HasMaxLength(150)
                    .HasColumnName("contingency_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Createdby)
                    .HasMaxLength(100)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Createddate)
                    .HasColumnType("date")
                    .HasColumnName("createddate")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CriticalNote)
                    .HasMaxLength(150)
                    .HasColumnName("critical_note")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PropertyInfo)
                    .HasMaxLength(150)
                    .HasColumnName("property_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PropertyZip)
                    .HasMaxLength(20)
                    .HasColumnName("property_zip")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.TblLoanProperties)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("FK_tbl_loan_property_tbl_loan_mst");
            });

            modelBuilder.Entity<TblLoanStage>(entity =>
            {
                entity.HasKey(e => e.StageId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_stage");

                entity.Property(e => e.StageId)
                    .HasColumnType("int(11)")
                    .HasColumnName("stage_id");

                entity.Property(e => e.StageName)
                    .HasMaxLength(50)
                    .HasColumnName("stage_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblLoanSubFolder>(entity =>
            {
                entity.HasKey(e => e.SubFolderId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_sub_folder");

                entity.Property(e => e.SubFolderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sub_folder_id");

                entity.Property(e => e.MilestonePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SubFolderName)
                    .HasMaxLength(50)
                    .HasColumnName("sub_folder_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblLoanTask>(entity =>
            {
                entity.HasKey(e => e.LoanTaskId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_loan_tasks");

                entity.HasIndex(e => e.MilestoneId, "FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasIndex(e => e.StatusId, "FK_tbl_tasks_defalt_tbl_status");

                entity.HasIndex(e => e.TaskGroupId, "FK_tbl_tasks_defalt_tbl_task_groups");

                entity.HasIndex(e => e.LoanId, "FK_tbl_tenant_tasks_tbl_tenant_master");

                entity.HasIndex(e => e.OwnId, "FK_tbl_tenant_tasks_tbl_user_personas");

                entity.Property(e => e.LoanTaskId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_task_id");

                entity.Property(e => e.Customer)
                    .HasMaxLength(150)
                    .HasColumnName("customer")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestonePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OwnId)
                    .HasColumnType("int(11)")
                    .HasColumnName("own_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskEn)
                    .HasMaxLength(150)
                    .HasColumnName("task_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("task_group_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskSp)
                    .HasMaxLength(150)
                    .HasColumnName("task_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.TblLoanTasks)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("FK_tbl_loan_tasks_tbl_loan_mst");

                entity.HasOne(d => d.LoanNavigation)
                    .WithMany(p => p.TblLoanTasks)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("tbl_loan_tasks_ibfk_1");

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.TblLoanTasks)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("tbl_loan_tasks_ibfk_3");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.TblLoanTasks)
                    .HasForeignKey(d => d.OwnId)
                    .HasConstraintName("tbl_loan_tasks_ibfk_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblLoanTasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("tbl_loan_tasks_ibfk_4");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.TblLoanTasks)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("tbl_loan_tasks_ibfk_5");
            });

            modelBuilder.Entity<TblLoanTeam>(entity =>
            {
                entity.ToTable("tbl_loan_team");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.BmId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bm_id");

                entity.Property(e => e.LmId)
                    .HasColumnType("int(11)")
                    .HasColumnName("lm_id");

                entity.Property(e => e.LoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("lo_id");

                entity.Property(e => e.LoaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loa_id");

                entity.Property(e => e.LoanId)
                    .HasColumnType("int(11)")
                    .HasColumnName("loan_id");

                entity.Property(e => e.SmId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sm_id");
            });

            modelBuilder.Entity<TblMilestone>(entity =>
            {
                entity.HasKey(e => e.MilestoneId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_milestone");

                entity.Property(e => e.MilestoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_id");

                entity.Property(e => e.MilestoneName)
                    .HasMaxLength(50)
                    .HasColumnName("milestone_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestonePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_pos")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblNeedsDefalt>(entity =>
            {
                entity.ToTable("tbl_needs_defalt");

                entity.HasIndex(e => e.SectionId, "FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasIndex(e => e.StatusId, "FK_tbl_tasks_defalt_tbl_status");

                entity.HasIndex(e => e.TaskGroupId, "FK_tbl_tasks_defalt_tbl_task_groups");

                entity.HasIndex(e => e.OwnId, "FK_tbl_tasks_defalt_tbl_user_personas");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Coordinating)
                    .HasColumnType("bit(1)")
                    .HasColumnName("coordinating")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DescEn)
                    .HasMaxLength(250)
                    .HasColumnName("desc_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DescSp)
                    .HasMaxLength(250)
                    .HasColumnName("desc_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NeedEn)
                    .HasMaxLength(150)
                    .HasColumnName("need_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NeedSp)
                    .HasMaxLength(150)
                    .HasColumnName("need_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OwnId)
                    .HasColumnType("int(11)")
                    .HasColumnName("own_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Sample)
                    .HasMaxLength(150)
                    .HasColumnName("sample")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SectionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_id");

                entity.Property(e => e.SectionPos)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("task_group_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Tracking)
                    .HasColumnType("bit(1)")
                    .HasColumnName("tracking")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Video)
                    .HasMaxLength(150)
                    .HasColumnName("video")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e._3rdparty)
                    .HasColumnType("bit(1)")
                    .HasColumnName("3rdparty")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.TblNeedsDefalts)
                    .HasForeignKey(d => d.OwnId)
                    .HasConstraintName("tbl_needs_defalt_ibfk_4");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TblNeedsDefalts)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_needs_defalt_tbl_sections");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblNeedsDefalts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("tbl_needs_defalt_ibfk_2");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.TblNeedsDefalts)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("tbl_needs_defalt_ibfk_3");
            });

            modelBuilder.Entity<TblSection>(entity =>
            {
                entity.HasKey(e => e.SectionId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_sections");

                entity.Property(e => e.SectionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_id");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(50)
                    .HasColumnName("section_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SectionPos)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_pos")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_status");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .HasColumnName("status_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblTaskGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_task_groups");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(50)
                    .HasColumnName("group_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblTasksDefalt>(entity =>
            {
                entity.ToTable("tbl_tasks_defalt");

                entity.HasIndex(e => e.MilestoneId, "FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasIndex(e => e.StatusId, "FK_tbl_tasks_defalt_tbl_status");

                entity.HasIndex(e => e.TaskGroupId, "FK_tbl_tasks_defalt_tbl_task_groups");

                entity.HasIndex(e => e.OwnId, "FK_tbl_tasks_defalt_tbl_user_personas");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MilestoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestonePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OwnId)
                    .HasColumnType("int(11)")
                    .HasColumnName("own_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskEn)
                    .HasMaxLength(150)
                    .HasColumnName("task_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("task_group_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskSp)
                    .HasMaxLength(150)
                    .HasColumnName("task_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.TblTasksDefalts)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.TblTasksDefalts)
                    .HasForeignKey(d => d.OwnId)
                    .HasConstraintName("FK_tbl_tasks_defalt_tbl_user_personas");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTasksDefalts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_tbl_tasks_defalt_tbl_status");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.TblTasksDefalts)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("FK_tbl_tasks_defalt_tbl_task_groups");
            });

            modelBuilder.Entity<TblTenantMaster>(entity =>
            {
                entity.HasKey(e => e.TenantId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_tenant_master");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsTrial)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_trial")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantName)
                    .HasMaxLength(100)
                    .HasColumnName("tenant_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updated_date")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblTenantMilestone>(entity =>
            {
                entity.HasKey(e => e.TenantMilestoneId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_tenant_milestone");

                entity.HasIndex(e => e.TenantId, "FK_tbl_tenant_milestone_tbl_tenant_master");

                entity.Property(e => e.TenantMilestoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_milestone_id");

                entity.Property(e => e.MilestoneName)
                    .HasMaxLength(50)
                    .HasColumnName("milestone_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestonePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblTenantMilestones)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_tbl_tenant_milestone_tbl_tenant_master");
            });

            modelBuilder.Entity<TblTenantNeed>(entity =>
            {
                entity.HasKey(e => e.TenantneedId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_tenant_needs");

                entity.HasIndex(e => e.SectionId, "FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasIndex(e => e.StatusId, "FK_tbl_tasks_defalt_tbl_status");

                entity.HasIndex(e => e.TaskGroupId, "FK_tbl_tasks_defalt_tbl_task_groups");

                entity.HasIndex(e => e.OwnId, "FK_tbl_tasks_defalt_tbl_user_personas");

                entity.HasIndex(e => e.TenantId, "FK_tbl_tenant_needs_tbl_tenant_master");

                entity.Property(e => e.TenantneedId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenantneedId");

                entity.Property(e => e.Coordinating)
                    .HasColumnType("bit(1)")
                    .HasColumnName("coordinating")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Customer)
                    .HasMaxLength(150)
                    .HasColumnName("customer")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DescEn)
                    .HasMaxLength(250)
                    .HasColumnName("desc_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DescSp)
                    .HasMaxLength(250)
                    .HasColumnName("desc_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NeedEn)
                    .HasMaxLength(150)
                    .HasColumnName("need_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NeedSp)
                    .HasMaxLength(150)
                    .HasColumnName("need_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OwnId)
                    .HasColumnType("int(11)")
                    .HasColumnName("own_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Sample)
                    .HasMaxLength(150)
                    .HasColumnName("sample")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SectionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SectionPos)
                    .HasColumnType("int(11)")
                    .HasColumnName("section_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("task_group_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id");

                entity.Property(e => e.Tracking)
                    .HasColumnType("bit(1)")
                    .HasColumnName("tracking")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Video)
                    .HasMaxLength(150)
                    .HasColumnName("video")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e._3rdparty)
                    .HasColumnType("bit(1)")
                    .HasColumnName("3rdparty")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.TblTenantNeeds)
                    .HasForeignKey(d => d.OwnId)
                    .HasConstraintName("tbl_tenant_needs_ibfk_4");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.TblTenantNeeds)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("tbl_tenant_needs_ibfk_1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTenantNeeds)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("tbl_tenant_needs_ibfk_2");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.TblTenantNeeds)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("tbl_tenant_needs_ibfk_3");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblTenantNeeds)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_tenant_needs_tbl_tenant_master");
            });

            modelBuilder.Entity<TblTenantTask>(entity =>
            {
                entity.HasKey(e => e.TenantTaskId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_tenant_tasks");

                entity.HasIndex(e => e.MilestoneId, "FK_tbl_tasks_defalt_tbl_milestones");

                entity.HasIndex(e => e.StatusId, "FK_tbl_tasks_defalt_tbl_status");

                entity.HasIndex(e => e.TaskGroupId, "FK_tbl_tasks_defalt_tbl_task_groups");

                entity.HasIndex(e => e.TenantId, "FK_tbl_tenant_tasks_tbl_tenant_master");

                entity.HasIndex(e => e.OwnId, "FK_tbl_tenant_tasks_tbl_user_personas");

                entity.Property(e => e.TenantTaskId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_task_id");

                entity.Property(e => e.Customer)
                    .HasMaxLength(150)
                    .HasColumnName("customer")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MilestonePos)
                    .HasColumnType("int(11)")
                    .HasColumnName("milestone_pos")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("note")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OwnId)
                    .HasColumnType("int(11)")
                    .HasColumnName("own_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskEn)
                    .HasMaxLength(150)
                    .HasColumnName("task_en")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("task_group_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TaskSp)
                    .HasMaxLength(150)
                    .HasColumnName("task_sp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.TblTenantTasks)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("tbl_tenant_tasks_ibfk_1");

                entity.HasOne(d => d.Own)
                    .WithMany(p => p.TblTenantTasks)
                    .HasForeignKey(d => d.OwnId)
                    .HasConstraintName("FK_tbl_tenant_tasks_tbl_user_personas");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTenantTasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("tbl_tenant_tasks_ibfk_2");

                entity.HasOne(d => d.TaskGroup)
                    .WithMany(p => p.TblTenantTasks)
                    .HasForeignKey(d => d.TaskGroupId)
                    .HasConstraintName("tbl_tenant_tasks_ibfk_3");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblTenantTasks)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_tbl_tenant_tasks_tbl_tenant_master");
            });

            modelBuilder.Entity<TblTenantUserPersona>(entity =>
            {
                entity.HasKey(e => e.TenantPersonaId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_tenant_user_personas");

                entity.HasIndex(e => e.TenantId, "FK_tbl_tenant_user_personas_tbl_tenant_master");

                entity.HasIndex(e => e.GroupId, "FK_tbl_user_personas_tbl_user_groups");

                entity.Property(e => e.TenantPersonaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_persona_id");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.PersonaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("persona_name");

                entity.Property(e => e.PersonaShortname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("persona_shortname");

                entity.Property(e => e.TenantId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblTenantUserPersonas)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_tenant_user_personas_ibfk_1");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.TblTenantUserPersonas)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_tbl_tenant_user_personas_tbl_tenant_master");
            });

            modelBuilder.Entity<TblUserGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_user_groups");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(50)
                    .HasColumnName("group_name")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<TblUserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_user_master");

                entity.HasIndex(e => e.BranchId, "FK_tbl_user_master_tbl_branch_master");

                entity.HasIndex(e => e.PersonaId, "FK_tbl_user_master_tbl_tenant_user_personas");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsFirstLogin)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_first_login")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsVerified)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_verified")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PersonaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("persona_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResetToken)
                    .HasMaxLength(150)
                    .HasColumnName("reset_token")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResetTokenExpires)
                    .HasColumnType("date")
                    .HasColumnName("reset_token_expires")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .HasColumnName("user_email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserInitials)
                    .HasMaxLength(20)
                    .HasColumnName("user_initials")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(200)
                    .HasColumnName("user_password")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.VerificationToken)
                    .HasMaxLength(150)
                    .HasColumnName("verification_token")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Verified)
                    .HasColumnType("date")
                    .HasColumnName("verified")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TblUserMasters)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_tbl_user_master_tbl_branch_master");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.TblUserMasters)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK_tbl_user_master_tbl_tenant_user_personas");
            });

            modelBuilder.Entity<TblUserPersona>(entity =>
            {
                entity.HasKey(e => e.PersonaId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_user_personas");

                entity.HasIndex(e => e.GroupId, "FK_tbl_user_personas_tbl_user_groups");

                entity.Property(e => e.PersonaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("persona_id");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.PersonaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("persona_name");

                entity.Property(e => e.PersonaShortname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("persona_shortname");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblUserPersonas)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_user_personas_tbl_user_groups");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
