﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Football_EF_Data_Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FootballContext : DbContext
    {
        public FootballContext()
            : base("name=FootballContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<InternationalMatch> InternationalMatches { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<TeamMatch> TeamMatches { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
    }
}
