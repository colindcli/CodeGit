using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ethnic
{
	public List<Item> Items { get; set; }

	public class Item
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}
}
