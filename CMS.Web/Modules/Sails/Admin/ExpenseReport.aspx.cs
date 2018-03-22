using System;
using System.Collections;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ExpenseReport : ExpenseEdit
    {
        //protected override void rptBookingList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.DataItem is SailExpense)
        //    {
        //        SailExpense expense = (SailExpense)e.Item.DataItem;
        //        int count;
        //        IList bookings =
        //            Module.BookingGetByCriterion(
        //                Expression.And(Expression.Eq(Booking.STARTDATE, expense.Date.Date),
        //                               Expression.Eq(Booking.STATUS, StatusType.Approved)), null, out count, 0, 0);

        //        int adult = 0;
        //        int child = 0;
        //        int baby = 0;
        //        foreach (Booking booking in bookings)
        //        {
        //            adult += booking.Adult;
        //            child += booking.Child;
        //            baby += booking.Baby;
        //        }
        //        _pax += adult + child;

        //        HiddenField hiddenAdult = (HiddenField)e.Item.FindControl("hiddenAdult");
        //        HiddenField hiddenChild = (HiddenField)e.Item.FindControl("hiddenChild");
        //        HiddenField hiddenBaby = (HiddenField)e.Item.FindControl("hiddenBaby");
        //        hiddenAdult.Value = adult.ToString();
        //        hiddenChild.Value = child.ToString();
        //        hiddenBaby.Value = baby.ToString();

        //        GetCurrentTable(expense.Date);

        //        if (_cruiseTable == null)
        //        {
        //            throw new Exception("Hai phong cruise price table is out of valid");
        //        }
        //        if (expense.Calculate(_table, _cruiseTable, adult, child, baby))
        //        {
        //            Module.SaveOrUpdate(expense);
        //        }

        //        #region -- Show info --

        //        Literal litDate = (Literal) e.Item.FindControl("litDate");
        //        litDate.Text = expense.Date.ToString("dd/MM/yyyy");

        //        Literal litPax = (Literal) e.Item.FindControl("litPax");
        //        litPax.Text = adult.ToString();

        //        Literal litTransfer = (Literal)e.Item.FindControl("litTransfer");
        //        litTransfer.Text = String.Format("{0:0.#}", expense.Transfer);

        //        Literal litTicket = (Literal)e.Item.FindControl("litTicket");
        //        litTicket.Text = String.Format("{0:0.#}", expense.Ticket);

        //        Literal litGuide = (Literal)e.Item.FindControl("litGuide");
        //        litGuide.Text = String.Format("{0:0.#}", expense.Guide);

        //        Literal litMeal = (Literal)e.Item.FindControl("litMeal");
        //        litMeal.Text = String.Format("{0:0.#}", expense.Meal);

        //        Literal litKayaing = (Literal)e.Item.FindControl("litKayaing");
        //        litKayaing.Text = String.Format("{0:0.#}", expense.Kayaing);

        //        Literal litServiceSup = (Literal)e.Item.FindControl("litServiceSup");
        //        litServiceSup.Text = String.Format("{0:0.#}", expense.Service);

        //        Literal litCruise = (Literal)e.Item.FindControl("litCruise");
        //        litCruise.Text = String.Format("{0:0.#}", expense.Cruise);

        //        Literal litTotal = (Literal) e.Item.FindControl("litTotal");
        //        litTotal.Text = String.Format("{0:#,0.#}", expense.TotalCost);

        //        #endregion

        //        #region -- Sum --

        //        _transfer += expense.Transfer;
        //        _ticket += expense.Ticket;
        //        _guide += expense.Guide;
        //        _meal += expense.Meal;
        //        _kayaing += expense.Kayaing;
        //        _service += expense.Service;
        //        _cruise += expense.Cruise;

        //        #endregion

        //        return;
        //    }
        //    if (e.Item.ItemType == ListItemType.Footer)
        //    {
        //        SetFooterValues(e.Item);
        //    }
        //}
    }
}