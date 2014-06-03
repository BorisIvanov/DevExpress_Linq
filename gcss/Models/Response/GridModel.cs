using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Web.Mvc;

using gcss.Models.Data;
using gcss.Models.Grid;
using LinqExtension;



namespace gcss.Models.Response
{
	public class GridModel<TEntity> where TEntity : class
	{

		public GridViewModel GridViewModel = new GridViewModel();

		public virtual string GridName 
		{ 
			get { throw new NotImplementedException(); } 
			set { throw new NotImplementedException(); }
		}
		protected virtual object CallbackRouteValues
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public ActionList Action { get; set; }
		public Dictionary<string, ComboFilterField<TEntity>> ComboFilterList = new Dictionary<string, ComboFilterField<TEntity>>();

		protected readonly Entities Entities = new Entities();

		public virtual DbSet<TEntity> Entity 
		{
			get { throw new NotImplementedException(); }
		}
		
		
		public virtual void Init(GridViewModel gridViewModel = null)
		{
			bool isNoInit = gridViewModel == null;
			GridViewModel = isNoInit ? new GridViewModel { KeyFieldName = "ID" } : gridViewModel;
		}
		
		protected void ColumnAdd(string fieldName, bool isNoInit, bool isFilter = false)
		{
			if (isNoInit)
				GridViewModel.Columns.Add(fieldName);
			if (isFilter)
			{
				ComboFilterList.Add(fieldName, 
					new ComboFilterField<TEntity>
					{
						DataSource = Entity,
						ClientName = string.Format("cbFilter_{0}", fieldName),
						FieldName = fieldName,
						GridName = GridName,
						CallbackRouteValues = CallbackRouteValues
					}
				);
			}
		}

		public void FilterColumnSet(GridViewColumnState columnState)
		{
			GridViewModel.Columns[columnState.FieldName].Assign(columnState);
			FilterColumnInit(columnState);
		}

		protected void FilterListInit()
		{
			foreach (GridViewColumnState columnItem in GridViewModel.Columns)
			{
				FilterColumnInit(columnItem);
			}
		}

		private void FilterColumnInit(GridViewColumnState columnItem)
		{
			if (!ComboFilterList.ContainsKey(columnItem.FieldName))
				return;

			var op = CriteriaOperator.Parse(columnItem.FilterExpression, null) as FunctionOperator;
			if (null == op)
			{
				ComboFilterList[columnItem.FieldName].FilterValue = "";
				return;
			}

			var val = op.Operands[1].ToString();
			val = val.Substring(1, val.Length - 2);
			ComboFilterList[columnItem.FieldName].FilterValue = val;
		}

		public void Bind()
		{
			GridViewModel.ProcessCustomBinding(
				GetDataRowCount,
				GetData
				);
		}
		

		public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
		{
			e.DataRowCount = Entity.Where(ComboFilterList).Count();
		}

		public void GetData(GridViewCustomBindingGetDataArgs e)
		{
			e.Data = Entity.AsQueryable()
				.Where(ComboFilterList)
				.OrderBy(GridViewModel)
				.Skip(e.StartDataRowIndex)
				.Take(e.DataRowCount)
				.ToList();
		}

		public void Add(TEntity item)
		{
			Entity.Add(item);
			Entities.SaveChanges();
		}

		public TEntity Get(TEntity item)
		{
			return Entity.FirstOrDefaultById(item);
		}

		public void Upd()
		{
			Entities.SaveChanges();
		}

		public void Remove(Int64 id)
		{
			TEntity item = Entity.FirstOrDefaultById(id);
			if (item != null)
				Entity.Remove(item);
			Entities.SaveChanges();
		}

		public TEntity DefaultItem()
		{
			return (TEntity)Activator.CreateInstance(typeof(TEntity));
		}
	}

}