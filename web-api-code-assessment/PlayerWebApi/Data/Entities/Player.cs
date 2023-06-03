// /////////////////////////////////////////////////////////////////////////////
// PLEASE DO NOT RENAME OR REMOVE ANY OF THE CODE BELOW. 
// YOU CAN ADD YOUR CODE TO THIS FILE TO EXTEND THE FEATURES TO USE THEM IN YOUR WORK.
// /////////////////////////////////////////////////////////////////////////////
using System.ComponentModel.DataAnnotations;

namespace PlayerWebApi.Data.Entities;

public class Player
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cannot be null or empty")]
    public string Name { get; set; }

    [Validate("Player", AllowableStringValues = new[] { "defender", "midfielder", "forward"})]
    public string Position { get; set; }

    public IEnumerable<PlayerSkill> PlayerSkills { get; set; }
}
