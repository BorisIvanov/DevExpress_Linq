using System.Linq;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.Mvc;
using LinqExtension;


namespace gcss.Models.Grid
{

	public class ComboFilterField<T>
	{
		public IQueryable<T> DataSource { get; set; }
		
		public string FieldName { get; set; }
		
		public string FilterValue { get; set; }

		public string GridName { get; set; }

		public string ClientName { get; set; }

		public object CallbackRouteValues { get; set; }

		public ComboFilterField()
		{
			GetRange = GetRangeDelegate;
			GetId = GetIdDelegate;
		}

		public ItemsRequestedByFilterConditionMethod GetRange { get; set; }
		public ItemRequestedByValueMethod GetId { get; set; }

		private object GetRangeDelegate(ListEditItemsRequestedByFilterConditionEventArgs args)
		{
			if (string.IsNullOrEmpty(args.Filter))
				return null;
			FilterValue = args.Filter;
			var skip = args.BeginIndex;
			var take = args.EndIndex - args.BeginIndex + 1;
			return DataSource.Where(this).OrderBy(FieldName).Skip(skip).Take(take).ToList();
		}

		private object GetIdDelegate(ListEditItemRequestedByValueEventArgs args)
		{
			return null;
		}

	}

	

}