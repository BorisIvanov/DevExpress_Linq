using System.Data;
using System.Linq;

using gcss.Models.Data.Extension;


namespace gcss.Models.Data
{

	public partial class Entities
	{
		public override int SaveChanges()
		{
			foreach (var ent in ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
			{
				var attr = ent.Entity.GetType().GetCustomAttributes(typeof(SequenceAttribute), true);
				if (attr != null)
				{					
					var sequence = ((SequenceAttribute)attr[0]);					
					ent.CurrentValues[sequence.FieldName] = SequenceNextVal(sequence.SequenceName);
				}
			}
			return base.SaveChanges();
		}


		public long SequenceNextVal(string sequenceName)
		{
			return Database.SqlQuery<long>(string.Format("select {0}.nextval from dual", sequenceName)).FirstOrDefault<long>();
		}

	}
	
}