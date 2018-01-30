using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobPosition
{
	public List<Item> Items { get; set; }

	public class Item
	{
		public int Id { get; set; }
		public int ParentId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
	}
}