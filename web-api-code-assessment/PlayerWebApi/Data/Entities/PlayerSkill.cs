// /////////////////////////////////////////////////////////////////////////////
// PLEASE DO NOT RENAME OR REMOVE ANY OF THE CODE BELOW. 
// YOU CAN ADD YOUR CODE TO THIS FILE TO EXTEND THE FEATURES TO USE THEM IN YOUR WORK.
// /////////////////////////////////////////////////////////////////////////////
using System.ComponentModel.DataAnnotations;

namespace PlayerWebApi.Data.Entities;

public class PlayerSkill
{
    public int Id { get; set; }

    [Validate("PlayerSkill", AllowableStringValues = new[] { "defense", "attack", "speed", "strength", "stamina" })]
    public string Skill { get; set; }

    public int Value { get; set; }

    public int PlayerId { get; set; }
}
