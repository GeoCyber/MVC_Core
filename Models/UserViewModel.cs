
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
	public class UserViewModel
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public bool IsInvited { get; set; }
		public bool EmailConfirmed { get; set; }
		public RoleViewModel[] Roles { get; set; }
	}
}
