﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class User
	{
		public int ID { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Role { get; set; }
	}
}
