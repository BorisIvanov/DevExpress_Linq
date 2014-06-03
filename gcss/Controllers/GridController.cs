using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using gcss.Models.Response;


namespace gcss.Controllers
{

	public class GridController<TListModel, TItem> : LocalizationController
		where TListModel : GridModel<TItem>
		where TItem : class
  {

		protected TListModel GetResponseModel()
		{
			var model = (TListModel)Activator.CreateInstance(typeof(TListModel));
			model.Init(GridViewExtension.GetViewModel(model.GridName));
			return model;
		}

		protected ActionResult GridCustomActionCore(TListModel model)
		{
			model.Bind();
			return PartialView("_GridPartial", model);
		}

		public ActionResult List()
		{
			return View();
		}

		[HttpPost, ValidateInput(false)]
		public ActionResult GridAdd([ModelBinder(typeof(DevExpressEditorsBinder))] TItem item)
		{
			var model = GetResponseModel();

			if (ModelState.IsValid)
			{
				try
				{
					model.Add(item);
				}
				catch (Exception e)
				{
					ViewData["EditError"] = e.Message;
					ViewData["DataItem"] = item;
				}
			}
			else
			{
				ViewData["EditError"] = "Please, correct all errors.";
				ViewData["DataItem"] = item;
			}
			return GridCustomActionCore(model);
		}
		

		[ValidateInput(false)]
		public ActionResult Grid()
		{
			return GridCustomActionCore(GetResponseModel());
		}

		protected virtual void UpdateItemModel(TItem item)
		{
			throw new NotImplementedException();
		}
		
		[HttpPost, ValidateInput(false)]
		public ActionResult GridUpd([ModelBinder(typeof(DevExpressEditorsBinder))] TItem item)
		{
			var model = GetResponseModel();
			if (ModelState.IsValid)
			{
				try
				{
					TItem dbItem = model.Get(item);
					UpdateItemModel(dbItem);
					model.Upd();
				}
				catch (Exception e)
				{
					ViewData["EditError"] = e.Message;
					ViewData["DataItem"] = item;
				}
			}
			else
			{
				ViewData["EditError"] = "Please, correct all errors.";
				ViewData["DataItem"] = item;
			}
			return GridCustomActionCore(model);
		}
	

		[HttpPost, ValidateInput(false)]
		public ActionResult GridDel(Int64 id)
		{
			var model = GetResponseModel();
			if (id >= 0)
			{
				try
				{
					model.Remove(id);
				}
				catch (Exception e)
				{
					ViewData["EditError"] = e.Message;
				}
			}
			return GridCustomActionCore(model);
		}

		
		public ActionResult GridSort(GridViewColumnState column, bool reset)
		{
			var model = GetResponseModel();
			model.GridViewModel.SortBy(column, reset);
			return GridCustomActionCore(model);
		}
		
		public ActionResult GridPaging(GridViewPagerState pager)
		{
			var model = GetResponseModel();
			model.GridViewModel.Pager.Assign(pager);
			return GridCustomActionCore(model);
		}
		
		public ActionResult GridFilter(GridViewColumnState column)
		{
			var model = GetResponseModel();
			model.FilterColumnSet(column);
			return GridCustomActionCore(model);
		}
		
		public ActionResult FilterComboBoxPartial(string fieldName)
		{
			return PartialView("../Helpers/FilterComboBoxPartial", GetResponseModel().ComboFilterList[fieldName]);
		}
		
	}

}