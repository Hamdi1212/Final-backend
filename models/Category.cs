using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.models
{

	public class Category
	{
		public virtual Guid id { get; set; }
		public virtual string code { get; set; }
		public virtual string description { get; set; }

		public virtual DateTime Date { get; set; }

}



}
