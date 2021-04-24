using FixedModules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Data
{
	[Table(name: "RoleMenuPermission")]
	public class RoleMenuPermission
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		public string RoleId { get; set; }

		public Guid NavigationMenuId { get; set; }

		public NavigationMenu NavigationMenu { get; set; }
		[ForeignKey("RolesStatus")]
		public int Status { get; set; }
		public RolesStatus RolesStatus { get; set; }
	}
}
