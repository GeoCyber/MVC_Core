using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FixedModules.Models
{
	public abstract class BaseEntities
	{
		[Key]
		public int Id { get; set; }

		[DisplayName("Creation Time")]
		public DateTime CreationTimeStamp { get; set; }

		[DisplayName("Modification Time")]
		public DateTime? ModificationTimeStamp { get; set; }

		[DisplayName("Created By")]
		public string CreatedBy { get; set; }

		[DisplayName("Modified By")]
		public string ModifiedBy { get; set; }
	}
}
