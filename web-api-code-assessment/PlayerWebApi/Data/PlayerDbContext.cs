// /////////////////////////////////////////////////////////////////////////////
// PLEASE DO NOT RENAME OR REMOVE ANY OF THE CODE BELOW. 
// YOU CAN ADD YOUR CODE TO THIS FILE TO EXTEND THE FEATURES TO USE THEM IN YOUR WORK.
// /////////////////////////////////////////////////////////////////////////////
using Microsoft.EntityFrameworkCore;
using PlayerWebApi.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PlayerWebApi.Data;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext()
        : base() { }

    public PlayerDbContext(DbContextOptions<PlayerDbContext> options) 
        : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerSkill> PlayerSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>(config => config.HasKey(ps => ps.Id));

        modelBuilder.Entity<PlayerSkill>(config =>
        {
            config.HasKey(ps => ps.Id);
            config.HasOne<Player>().WithMany(p => p.PlayerSkills).HasForeignKey(ps => ps.PlayerId);
        });
    }
}

public class Validate : ValidationAttribute
{
    public string[] AllowableStringValues { get; set; }
    public string Property { get; set; }

    public Validate(string property) 
    {
        Property = property;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (Property == "Player")
        {
            ErrorMessage = $"Invalid value for position: {value}";
            if (AllowableStringValues.Contains(value.ToString()))
                return ValidationResult.Success;
        }

        else if (Property == "PlayerSkill")
        {
            var skillValue = validationContext.ObjectInstance.GetType().GetProperty("Value").GetValue(validationContext.ObjectInstance);
            var skill = validationContext.ObjectInstance.GetType().GetProperty("Skill").GetValue(validationContext.ObjectInstance);

            ErrorMessage = $"Invalid value for player skill '{skill}' : {skillValue}";
            if (AllowableStringValues.Contains(value.ToString()) && (skillValue is >= 1 and <= 99))
                return ValidationResult.Success;
        }

       
        return new ValidationResult(ErrorMessage);
    }
}