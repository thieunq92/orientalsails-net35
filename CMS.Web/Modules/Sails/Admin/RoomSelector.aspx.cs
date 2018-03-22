using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Util;
using GemBox.Spreadsheet;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class RoomSelector : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = @"Room organizer";
            if (!IsPostBack)
            {
                if (Request.QueryString["bookingid"] != null)
                {
                    var booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["bookingid"]));

                    panelCruise.CssClass = "cruise" + booking.Cruise.Id;
                    Dictionary<string, int> available;
                    IList list;
                    //var map = Module.RoomGetStatus(booking.Cruise, booking.StartDate, out available, booking);
                    var map = Module.RoomGetStatus(booking.Cruise, booking.StartDate, out available, out list);
                    rptRooms.DataSource = map.Values;
                    rptRooms.DataBind();

                    //rptBookingRooms.DataSource = booking.BookingRooms;
                    rptBookingRooms.DataSource = list;
                    rptBookingRooms.DataBind();
                }
                else
                {
                    // Sắp xếp cho toàn bộ các booking trong ngày
                    DateTime date = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["date"]));
                    Cruise cruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));

                    panelCruise.CssClass = "cruise" + cruise.Id;
                    Dictionary<string, int> available;
                    IList list;
                    var map = Module.RoomGetStatus(cruise, date, out available, out list);
                    rptRooms.DataSource = map.Values;
                    rptRooms.DataBind();

                    // Lấy về toàn bộ booking rooms?
                    rptBookingRooms.DataSource = list;
                    rptBookingRooms.DataBind();

                    //btnSave.Visible = false;
                }
            }
        }

        protected void rptRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is RoomStatus)
            {
                var roomStatus = (RoomStatus)e.Item.DataItem;
                var control = (HtmlGenericControl)e.Item.FindControl("divRoom");
                string cssClass = string.Format("room room{1} {0}", roomStatus.Room.Name, roomStatus.Room.Id);
                switch (roomStatus.Status)
                {
                    case 0:
                        cssClass += " available";
                        break;
                    case -1:
                        cssClass += " occupied";
                        break;
                    case 1:
                        cssClass += " current";
                        break;
                    default:
                        break;
                }

                control.Attributes.Add("class", cssClass);
                control.InnerHtml = roomStatus.Room.Id.ToString();
                control.Attributes.Add("name", roomStatus.Room.Name);
                control.Attributes.Add("type", string.Format("{0} {1}", roomStatus.Room.RoomClass.Name, roomStatus.Room.RoomType.Name));
                if (roomStatus.BookingRooms.Count == 0)
                {
                    string info = string.Format("{0} - {1} {2}", roomStatus.Room.Name, roomStatus.Room.RoomClass.Name,
                                                roomStatus.Room.RoomType.Name);
                    control.Attributes.Add("title", info);
                }
                else
                {
                    string info = roomStatus.Room.Name + ":";
                    string bookingid = string.Empty;
                    foreach (var bookingRoom in roomStatus.BookingRooms)
                    {
                        foreach (Customer customer in bookingRoom.RealCustomers)
                        {
                            if (!string.IsNullOrEmpty(customer.Fullname))
                                info += " " + customer.Fullname;
                        }
                        bookingid = string.Format("OS{0:00000}", bookingRoom.Book.Id);
                    }

                    info += " Booking Code: " + bookingid;
                    control.Attributes.Add("title", info);
                }
            }
        }

        protected void rptBookingRooms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingRoom)
            {
                var room = (BookingRoom)e.Item.DataItem;
                ValueBinder.BindLiteral(e.Item, "litType", string.Format("{0} {1}", room.RoomClass.Name, room.RoomType.Name));
                ValueBinder.BindLiteral(e.Item, "litCustomer", room.CustomerName);
                var chkLockRoom = (CheckBox) e.Item.FindControl("chkLockRoom");

                if (room.Room != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litRoomName", room.Room.Name);
                    var txtRoomNumber = (TextBox)e.Item.FindControl("txtRoomNumber");
                    txtRoomNumber.Text = room.Room.Id.ToString();
                    chkLockRoom.Checked = room.IsLocked;

                    // nếu là sale in charge hoặc admin thì cho phép lock
                    if (UserIdentity.HasPermission(AccessLevel.Administrator) || room.Book.Agency.Sale == null || room.Book.Agency.Sale.Id == UserIdentity.Id)
                    {
                        chkLockRoom.Enabled = true;
                    }
                    else
                    {
                        chkLockRoom.Enabled = false;
                        if (room.IsLocked)
                        {
                            txtRoomNumber.ReadOnly = true;
                        }
                    }
                }

                var hplBookingId = e.Item.FindControl("hplBookingId") as HyperLink;
                if (hplBookingId != null)
                {
                    hplBookingId.Text = string.Format("[{0}]", room.Book.BookingIdOS);
                    hplBookingId.NavigateUrl = string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}",
                                                             Node.Id, Section.Id, room.Book.Id);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptBookingRooms.Items)
            {
                var hiddenId = (HiddenField)item.FindControl("hiddenId");
                var txtRoomNumber = (TextBox)item.FindControl("txtRoomNumber");
                var chkLockRoom = (CheckBox) item.FindControl("chkLockRoom");

                if (!string.IsNullOrEmpty(txtRoomNumber.Text))
                {
                    BookingRoom bkroom = Module.BookingRoomGetById(Convert.ToInt32(hiddenId.Value));
                    int roomid = Convert.ToInt32(txtRoomNumber.Text);

                    // check xem có sự thay đổi không
                    if (bkroom.Room != null && bkroom.Room.Id != roomid || bkroom.IsLocked != chkLockRoom.Checked)
                    {
                        Room room = Module.RoomGetById(Convert.ToInt32(txtRoomNumber.Text));
                        bkroom.Room = room;
                        bkroom.RoomType = room.RoomType;
                        bkroom.RoomClass = room.RoomClass;
                        bkroom.IsLocked = chkLockRoom.Checked;
                        Module.SaveOrUpdate(bkroom, UserIdentity);
                    }
                }
                else
                {
                    BookingRoom bkroom = Module.BookingRoomGetById(Convert.ToInt32(hiddenId.Value));
                    //Room room = Module.RoomGetById(Convert.ToInt32(txtRoomNumber.Text));
                    if (bkroom.Room != null)
                    {
                        bkroom.Room = null;
                        bkroom.IsLocked = false;
                        Module.SaveOrUpdate(bkroom, UserIdentity);
                    }
                }
            }

            //foreach (BookingRoom room in _booking.BookingRooms)
            //{
            //    string key = room.RoomType.Id + "#" + room.RoomClass.Id;
            //    int inc; // Biến dùng để xác định xem sẽ cộng thêm bao nhiêu vào map
            //    if (room.RoomType.IsShared)
            //    {
            //        inc = room.VirtualAdult;
            //    }
            //    else
            //    {
            //        inc = 1;
            //    }

            //    if (roomMap.ContainsKey(key))
            //    {
            //        roomMap[key] += inc;
            //    }
            //    else
            //    {
            //        // Tìm số phòng trống thực tế ngay khi add vào room map để tiết kiệm số lần gọi tới CSDL
            //        roomMap.Add(key, inc);
            //        // Vấn đề cần giải quyết là: khi di chuyển booking sang ngày hôm sau & book đó là book 3 ngày
            //        // Cần phải tăng số phòng available của ngày hôm sau lên, nhưng vẫn giữ số phòng availabe của ngày sau nữa

            //        // Phương án: loại bỏ booking hiện tại. Lưu ý: có thể booking sẽ bị auto update
            //        available.Add(key,
            //                      Module.RoomCount(room.RoomClass, room.RoomType, cruise, date,
            //                                       trip.NumberOfDay,
            //                                       _booking));
            //    }
            //}
            //Page.ClientScript.RegisterStartupScript(typeof(RoomSelector), "close", "window.opener.location = window.opener.location; close();", true);
            PageRedirect(Request.RawUrl);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["date"]));
            Cruise cruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));
            IList list = GetData(cruise, date);

            int totalRows = 0;
            foreach (Booking booking in list)
            {
                totalRows += booking.BookingRooms.Count;
            }


            // Bắt đầu thao tác export
            ExcelFile excelFile = new ExcelFile();
            excelFile.LoadXls(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/Rooming_list.xls"));
            // Mở sheet 0
            ExcelWorksheet sheet = excelFile.Worksheets[0];

            #region -- Xuất dữ liệu --

            const int firstrow = 3;
            sheet.Rows[firstrow].InsertCopy(totalRows - 1, sheet.Rows[firstrow]);
            int curr = firstrow;
            foreach (Booking booking in list)
            {
                foreach (BookingRoom room in booking.BookingRooms)
                {
                    sheet.Cells[curr, 0].Value = curr - firstrow + 1;
                    string name = string.Empty;
                    foreach (Customer customer in room.Customers)
                    {
                        if (!string.IsNullOrEmpty(customer.Fullname))
                        {
                            name += customer.Fullname + "\n";
                        }
                    }
                    if (name.Length > 0)
                    {
                        name = name.Remove(name.Length - 1);
                    }
                    sheet.Cells[curr, 1].Value = name;
                    sheet.Cells[curr, 2].Value = room.Adult + room.Child;
                    sheet.Cells[curr, 3].Value = room.RoomType.Name;
                    if (room.Room != null)
                    {
                        sheet.Cells[curr, 4].Value = room.Room.Name;
                    }
                    curr++;
                }
            }

            #endregion

            #region -- Trả dữ liệu về cho người dùng --

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition",
                                  "attachment; filename=" + string.Format("Roomlist{0:dd_MMM}.xls", date));

            MemoryStream m = new MemoryStream();

            excelFile.SaveXls(m);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();

            #endregion
        }

        protected IList GetData(Cruise cruise, DateTime date)
        {
            // Điều kiện bắt buộc: chưa xóa và có status là Approved, hoặc chưa hết hạn holding
            ICriterion criterion = Module.LockCrit();

            // Không bao gồm booking đã transfer
            criterion = Expression.And(criterion, Expression.Not(Expression.Eq("IsTransferred", true)));

            // Điều kiện về tàu            
            criterion = Expression.And(criterion, Expression.Eq("Cruise", cruise));
            criterion = Module.AddDateExpression(criterion, date);

            int count;
            var list = Module.BookingGetByCriterion(criterion, null, out count, 0, 0);

            //// Nếu cần load service và cruise hiện tại khác null
            //if (loadService)
            //{
            //    LoadService(_date);
            //}

            //plhNote.Visible = ActiveCruise != null;
            return list;
        }
    }
}