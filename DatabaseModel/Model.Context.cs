﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VideoRentalEntities : DbContext
    {
        public VideoRentalEntities()
            : base("name=VideoRentalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<genre> genre { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<producer> producer { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<video> video { get; set; }
        public virtual DbSet<videogenre> videogenre { get; set; }
        public virtual DbSet<videolist> videolist { get; set; }
        public virtual DbSet<videoproducer> videoproducer { get; set; }
    }
}