using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FiveBeachStore.Models
{
    public partial class FiveBeachStoreContext : DbContext
    {
        public FiveBeachStoreContext()
        {
        }

        public FiveBeachStoreContext(DbContextOptions<FiveBeachStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbBrand> TbBrands { get; set; } = null!;
        public virtual DbSet<TbCategory> TbCategories { get; set; } = null!;
        public virtual DbSet<TbConfig> TbConfigs { get; set; } = null!;
        public virtual DbSet<TbContact> TbContacts { get; set; } = null!;
        public virtual DbSet<TbLink> TbLinks { get; set; } = null!;
        public virtual DbSet<TbMenu> TbMenus { get; set; } = null!;
        public virtual DbSet<TbOrder> TbOrders { get; set; } = null!;
        public virtual DbSet<TbOrderDetail> TbOrderDetails { get; set; } = null!;
        public virtual DbSet<TbPost> TbPosts { get; set; } = null!;
        public virtual DbSet<TbProduct> TbProducts { get; set; } = null!;
        public virtual DbSet<TbProductImage> TbProductImages { get; set; } = null!;
        public virtual DbSet<TbProductOptionValue> TbProductOptionValues { get; set; } = null!;
        public virtual DbSet<TbProductOptiọn> TbProductOptiọns { get; set; } = null!;
        public virtual DbSet<TbProductSale> TbProductSales { get; set; } = null!;
        public virtual DbSet<TbProductStore> TbProductStores { get; set; } = null!;
        public virtual DbSet<TbRole> TbRoles { get; set; } = null!;
        public virtual DbSet<TbSlider> TbSliders { get; set; } = null!;
        public virtual DbSet<TbTopic> TbTopics { get; set; } = null!;
        public virtual DbSet<TbUser> TbUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADMIN\\SQLEXPRESS;Database=FiveBeachStore;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbBrand>(entity =>
            {
                entity.ToTable("tb_Brand");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Metadesc).HasColumnName("metadesc");

                entity.Property(e => e.Metakey).HasColumnName("metakey");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Slug)
                    .HasMaxLength(1000)
                    .HasColumnName("slug");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.ToTable("tb_Category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .HasColumnName("image");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Metadesc).HasColumnName("metadesc");

                entity.Property(e => e.Metakey).HasColumnName("metakey");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Slug)
                    .HasMaxLength(1000)
                    .HasColumnName("slug");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbConfig>(entity =>
            {
                entity.ToTable("tb_Config");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasColumnName("author");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(155)
                    .HasColumnName("facebook");

                entity.Property(e => e.Googleplus)
                    .HasMaxLength(155)
                    .HasColumnName("googleplus");

                entity.Property(e => e.Metadesc)
                    .HasMaxLength(255)
                    .HasColumnName("metadesc");

                entity.Property(e => e.Metakey)
                    .HasMaxLength(255)
                    .HasColumnName("metakey");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.SiteName)
                    .HasMaxLength(255)
                    .HasColumnName("site_name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(155)
                    .HasColumnName("twitter");

                entity.Property(e => e.Youtube)
                    .HasMaxLength(155)
                    .HasColumnName("youtube");
            });

            modelBuilder.Entity<TbContact>(entity =>
            {
                entity.ToTable("tb_Contact");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.ReplayId).HasColumnName("replay_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<TbLink>(entity =>
            {
                entity.ToTable("tb_Link");

                entity.Property(e => e.Link)
                    .HasMaxLength(1000)
                    .HasColumnName("link");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<TbMenu>(entity =>
            {
                entity.ToTable("tb_Menu");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Position)
                    .HasMaxLength(255)
                    .HasColumnName("position");

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.ToTable("tb_Order");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasColumnName("note");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<TbOrderDetail>(entity =>
            {
                entity.ToTable("tb_OrderDetail");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Qty).HasColumnName("qty");
            });

            modelBuilder.Entity<TbPost>(entity =>
            {
                entity.ToTable("tb_Post");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Detail)
                    .HasColumnType("text")
                    .HasColumnName("detail");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .HasColumnName("image");

                entity.Property(e => e.Metadesc).HasColumnName("metadesc");

                entity.Property(e => e.Metakey).HasColumnName("metakey");

                entity.Property(e => e.Slug)
                    .HasMaxLength(255)
                    .HasColumnName("slug");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbProduct>(entity =>
            {
                entity.ToTable("tb_Product");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Detail)
                    .HasColumnType("text")
                    .HasColumnName("detail");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .HasColumnName("image");

                entity.Property(e => e.Metadesc).HasColumnName("metadesc");

                entity.Property(e => e.Metakey).HasColumnName("metakey");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.PriceSale)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price_sale");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Slug)
                    .HasMaxLength(1000)
                    .HasColumnName("slug");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_tb_Product_tb_Category");
            });

            modelBuilder.Entity<TbProductImage>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_tb_ProductImage1");

                entity.ToTable("tb_ProductImage");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.Metadesc)
                    .HasMaxLength(1000)
                    .HasColumnName("metadesc");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbProductOptionValue>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tb_ProductOptionValue");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Metadesc)
                    .HasMaxLength(50)
                    .HasColumnName("metadesc");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbProductOptiọn>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tb_ProductOptiọn");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Memorysize).HasColumnName("memorysize");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbProductSale>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tb_ProductSale");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.NgayBd)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayBD");

                entity.Property(e => e.NgayKt)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayKT");

                entity.Property(e => e.PriceSale)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("priceSale");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Status).HasDefaultValueSql("((2))");
            });

            modelBuilder.Entity<TbProductStore>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tb_ProductStore");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Metadesc)
                    .HasMaxLength(500)
                    .HasColumnName("metadesc");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbRole>(entity =>
            {
                entity.ToTable("tb_Role");

                entity.Property(e => e.Metadesc).HasColumnName("metadesc");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");
            });

            modelBuilder.Entity<TbSlider>(entity =>
            {
                entity.ToTable("tb_Slider");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .HasColumnName("image");

                entity.Property(e => e.Link)
                    .HasMaxLength(1000)
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.Position)
                    .HasMaxLength(255)
                    .HasColumnName("position");

                entity.Property(e => e.SortOrder).HasColumnName("sort_order");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbTopic>(entity =>
            {
                entity.ToTable("tb_Topic");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Metadesc).HasColumnName("metadesc");

                entity.Property(e => e.Metakey).HasColumnName("metakey");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Slug)
                    .HasMaxLength(1000)
                    .HasColumnName("slug");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.ToTable("tb_User");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Roles)
                    .HasMaxLength(255)
                    .HasColumnName("roles");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
