﻿using BidService.API.BidService.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace RoomService.Infrastructure.Data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<BidModel> NotifyBidders { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public AppDbContext()
        {

        }
        
    }
}