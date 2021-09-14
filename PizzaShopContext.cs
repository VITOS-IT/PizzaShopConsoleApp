using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PizzaShop
{
    public partial class PizzaShopContext : DbContext
    {
        public PizzaShopContext()
        {
        }

        public PizzaShopContext(DbContextOptions<PizzaShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItemDetail> OrderItemDetails { get; set; }
        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Toping> Topings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-L6HC0LG\\SQLSERVER2021V;Database=PizzaShop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OId)
                    .HasName("PK__Orders__904BC20ECDB1C052");

                entity.Property(e => e.OId).HasColumnName("o_id");

                entity.Property(e => e.Deliverharge)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("deliverharge");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Totall).HasColumnName("totall");

                entity.Property(e => e.UserId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("user_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__user_Id__3D5E1FD2");
            });

            modelBuilder.Entity<OrderItemDetail>(entity =>
            {
                entity.ToTable("Order_Item_Details");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemNumber).HasColumnName("item_number");

                entity.Property(e => e.ToppingNumber).HasColumnName("topping_number");

                entity.HasOne(d => d.ItemNumberNavigation)
                    .WithMany(p => p.OrderItemDetails)
                    .HasForeignKey(d => d.ItemNumber)
                    .HasConstraintName("FK__Order_Ite__item___440B1D61");

                entity.HasOne(d => d.ToppingNumberNavigation)
                    .WithMany(p => p.OrderItemDetails)
                    .HasForeignKey(d => d.ToppingNumber)
                    .HasConstraintName("FK__Order_Ite__toppi__44FF419A");
            });

            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.HasKey(e => e.ItemNumber)
                    .HasName("PK__Orders_d__77D8BCA1B12B8CF7");

                entity.ToTable("Orders_details");

                entity.Property(e => e.ItemNumber).HasColumnName("item_number");

                entity.Property(e => e.OId).HasColumnName("o_id");

                entity.Property(e => e.PizzaNumber).HasColumnName("pizza_number");

                entity.HasOne(d => d.OIdNavigation)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.OId)
                    .HasConstraintName("FK__Orders_det__o_id__403A8C7D");

                entity.HasOne(d => d.PizzaNumberNavigation)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.PizzaNumber)
                    .HasConstraintName("FK__Orders_de__pizza__412EB0B6");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.HasKey(e => e.PizzaNumber)
                    .HasName("PK__Pizza__9138DDC7A94D69AE");

                entity.ToTable("Pizza");

                entity.Property(e => e.PizzaNumber).HasColumnName("pizza_number");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Prise).HasColumnName("prise");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Toping>(entity =>
            {
                entity.HasKey(e => e.ToppingNumber)
                    .HasName("PK__Topings__FF786C22EB624E5D");

                entity.Property(e => e.ToppingNumber).HasColumnName("Topping_number");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Prise).HasColumnName("prise");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserEmail)
                    .HasName("PK__Users__B0FBA2133ADC1846");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
