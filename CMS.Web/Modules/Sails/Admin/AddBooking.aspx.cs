using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Util;
using NHibernate.Criterion;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AddBooking : SailsAdminBase
    {

        /// <summary>
        /// Ngày khởi hành của booking lấy từ dữ liệu vào
        /// </summary>
        private DateTime? _date;

        /// <summary>
        /// Đẳng cấp phòng hiện tại (dùng trong vòng lặp lấy tất cả các phòng của tàu)
        /// </summary>
        private RoomClass _roomClass;

        /// <summary>
        /// Hành trình sẽ book
        /// </summary>
        private SailsTrip _trip;

        /// <summary>
        /// Danh sách phòng đặt của booking
        /// </summary>
        private IList _bookingRooms;

        /// <summary>
        /// Danh sách chính sách giá áp dụng cho đại lý hiện tại
        /// </summary>
        private IList _policies;

        /// <summary>
        /// Tàu sẽ book
        /// </summary>
        private Cruise _cruise;

        private readonly ILog _logger = LogManager.GetLogger(typeof(AddBooking));

        /// <summary>
        /// Lấy hành trình từ hộp chọn của người dùng
        /// </summary>
        protected SailsTrip Trip
        {
            get
            {
                if (_trip == null)
                {
                    _trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
                }
                return _trip;
            }
        }

        /// <summary>
        /// Lấy ngày khởi hành từ hộp thoại của người dùng
        /// </summary>
        protected DateTime? Date
        {
            get
            {
                if (_date == null)
                {
                    try
                    {
                        _date = DateTime.ParseExact(Request.Form.Get(txtDate.UniqueID), "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return null;
                    }
                }
                return _date;
            }
        }

        private DateTime? _endDate;

        /// <summary>
        /// Lấy ngày khởi hành từ hộp thoại của người dùng
        /// </summary>
        protected DateTime? EndDate
        {
            get
            {
                if (_endDate == null)
                {
                    try
                    {
                        _endDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return null;
                    }
                }
                return _endDate;
            }
        }

        /// <summary>
        /// Lấy tàu từ hộp chọn của người dùng
        /// </summary>
        protected Cruise ActiveCruise
        {
            get
            {
                if (_cruise == null)
                {
                    _cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                }
                return _cruise;
            }
        }

        /// <summary>
        /// Tất cả các hành trình
        /// </summary>
        private IList _trips;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.textAddBooking;
            plhTrip.Visible = TripBased;
            plhEndDate.Visible = !plhTrip.Visible;
            // Lấy tất cả các hành trình để lọc ra các hành trình có nhiều option, phục vụ cho việc ẩn/hiện hộp chọn option
            _trips = Module.TripGetAll(true);
            string visibleIds = string.Empty;
            foreach (SailsTrip trip in _trips)
            {
                if (trip.NumberOfOptions == 2)
                {
                    visibleIds += "#" + trip.Id + "#";
                }
            }
            ddlTrips.Attributes.Add("onChange",
                string.Format("ddltype_changed('{0}','{1}','{2}')", ddlTrips.ClientID, ddlOptions.ClientID, visibleIds));
            if (!IsPostBack)
            {
                BindTrips();
                BindCruises();
                if (DetailService)
                {
                    BindServices();
                }
                else
                {
                    rptExtraServices.Visible = false;
                }
                lbtCheckAvaiable_Click(null, null);
                if (Master != null) Master.FindControl("divMessage").Visible = false;
                plhCruiseName.Visible = false;
            }
            else
            {
                txtDate.Text = Request.Form.Get(txtDate.UniqueID);
                lbtCheckAvaiable_Click(null, null);
            }
        }

        protected void rptClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is RoomClass)
            {
                Repeater rptTypes = (Repeater)e.Item.FindControl("rptTypes");
                _roomClass = e.Item.DataItem as RoomClass;
                // Sử dụng biến toàn cục _roomClass để check với tất cả các room type
                rptTypes.DataSource = AllRoomTypes;
                rptTypes.DataBind();
            }
        }

        protected void rptTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is RoomTypex)
            {
                RoomTypex type = (RoomTypex)e.Item.DataItem;
                Label labelName = e.Item.FindControl("labelName") as Label;
                if (labelName != null)
                {
                    labelName.Text = string.Format("{0} {1}", _roomClass.Name, type.Name);
                }

                #region -- thông tin về khách và phòng trống --

                DropDownList ddlAdults = (DropDownList)e.Item.FindControl("ddlAdults");
                DropDownList ddlChild = (DropDownList)e.Item.FindControl("ddlChild");
                DropDownList ddlBaby = (DropDownList)e.Item.FindControl("ddlBaby");
                if (Date != null)
                {
                    ddlAdults.Items.Clear();
                    ddlChild.Items.Clear();
                    ddlBaby.Items.Clear();

                    int roomCount;

                    Locked locked = Module.LockedCheckByDate(ActiveCruise, _date.Value,
                        _date.Value.AddDays(Trip.NumberOfDay - 1));
                    if (locked != null)
                    {
                        if (!string.IsNullOrEmpty(locked.Description))
                        {
                            ShowError(
                                string.Format(
                                    "Hành trình này đã bị khóa với lý do: {0}, vẫn có thể add booking như buộc phải chuyển sang tàu khác",
                                    locked.Description));
                        }
                        else
                        {
                            ShowError(
                                string.Format(
                                    "Hành trình này đã bị khóa, vẫn có thể add booking như buộc phải chuyển sang tàu khác"));
                        }
                        // Khi ấy thì lấy về số phòng, trong đó bỏ qua lock
                        roomCount = Module.RoomCount(_roomClass, type, ActiveCruise, _date.Value, Trip.NumberOfDay, true,
                            Trip.HalfDay);
                    }
                    else
                    {
                        roomCount = Module.RoomCount(_roomClass, type, ActiveCruise, _date.Value, Trip.NumberOfDay,
                            Trip.HalfDay);
                    }

                    if (roomCount < 0)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                    int maxAdults;

                    // Nếu là phòng ở ghép thì số người = giá trị lấy về, số phòng = giá trị lấy về chia cho sức chứa
                    if (type.IsShared)
                    {
                        maxAdults = roomCount;
                        roomCount = roomCount / type.Capacity;
                    }
                    else
                    {
                        // Nếu không số người = giá gị lấy về nhân với sức chứa
                        maxAdults = roomCount * type.Capacity;
                    }

                    // Nếu là phòng ở ghép thì cho chọn theo số người
                    if (type.IsShared)
                    {
                        // Từ 0 đến số người max
                        for (int i = 0; i <= maxAdults; i++)
                        {
                            ddlAdults.Items.Add(new ListItem(string.Format(Resources.formatPersonItem, i), i.ToString()));
                        }
                    }
                    else
                    {
                        // Nếu không phải phòng ở ghép thì lấy theo số phòng
                        for (int i = 0; i <= roomCount; i++)
                        {
                            ddlAdults.Items.Add(new ListItem(string.Format(Resources.formatRoomItem, i), i.ToString()));
                        }
                    }

                    for (int i = 0; i <= roomCount; i++)
                    {
                        ddlBaby.Items.Add(new ListItem(string.Format(Resources.formatBabyItem, i), i.ToString()));
                        ddlChild.Items.Add(new ListItem(string.Format(Resources.formatChildItem, i), i.ToString()));
                    }

                }

                #endregion

                #region -- Giá thủ công --

                // Nếu hệ thống được cấu hình để nhập giá thủ công cho từng loại phòng ngay khi add booking thì hiển thị khu vực này
                PlaceHolder plhCustomPrice = e.Item.FindControl("plhCustomPrice") as PlaceHolder;
                if (plhCustomPrice != null)
                {
                    if (CustomPriceAddBooking)
                    {
                        plhCustomPrice.Visible = true;
                        TextBox txtPrice = e.Item.FindControl("txtPrice") as TextBox;
                        if (txtPrice != null)
                        {
                            double price;
                            try
                            {
                                price = BookingRoom.Calculate(_roomClass, type, type.Capacity, 0, false, _trip,
                                    ActiveCruise,
                                    TripOption.Option1, Date.Value, Module, _policies,
                                    ChildPrice, AgencySupplement, agencySelector.SelectedAgency);
                            }
                            catch (PriceException ex)
                            {
                                ShowError(ex.Message);
                                price = 0;
                            }
                            txtPrice.Text = price.ToString("0.##");
                        }
                    }
                    else
                    {
                        plhCustomPrice.Visible = false;
                    }
                }

                #endregion
            }
        }

        protected void rptExtraServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is ExtraOption)
            {
                CheckBox chkService = (CheckBox)e.Item.FindControl("chkService");
                chkService.Checked = ((ExtraOption)e.Item.DataItem).IsIncluded;
                // Mặc định là chọn dịch vụ nếu đã được bao gồm trong giá phòng
            }
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                var cruise = (Cruise)e.Item.DataItem;

                if (!cruise.Trips.Contains(Trip))
                {
                    e.Item.Visible = false;
                    return;
                }

                int total = 0;
                string detail = string.Empty;
                for (int i = 0; i < AllRoomClasses.Count; i++)
                {
                    var rclass = AllRoomClasses[i] as RoomClass;
                    for (int j = 0; j < AllRoomTypes.Count; j++)
                    {
                        var rtype = AllRoomTypes[j] as RoomTypex;
                        int avail;
                        if (TripBased)
                        {
                            avail = Module.RoomCount(rclass, rtype, cruise, Date.Value, Trip.NumberOfDay, Trip.HalfDay);
                        }
                        else
                        {
                            avail = Module.RoomCount(rclass, rtype, cruise, Date.Value, Trip.NumberOfDay, Trip.HalfDay);
                        }
                        if (avail > 0)
                        {
                            total += avail;
                            detail += string.Format("{0} {2} {1} ", avail, rtype.Name, rclass.Name);
                        }
                    }
                }

                var lbtCruiseName = (LinkButton)e.Item.FindControl("lbtCruiseName");
                if (lbtCruiseName != null)
                {
                    lbtCruiseName.Text = cruise.Name;
                    lbtCruiseName.CommandArgument = cruise.Id.ToString();
                    lbtCruiseName.Attributes.Add("totalAvaiable", total.ToString());
                }

                if (ViewState["cruiseId"] != null)
                {
                    var cruiseIdViewState = (int) ViewState["cruiseId"];
                    if (cruise.Id == cruiseIdViewState)
                        chkCharter.Visible = CheckCruiseForCharter(cruise, total);
                }

                var litRoomCount = e.Item.FindControl("litRoomCount") as Literal;
                if (litRoomCount != null)
                {
                    litRoomCount.Text = total.ToString();
                }

                var litRoomDetail = e.Item.FindControl("litRoomDetail") as Literal;
                if (litRoomDetail != null)
                {
                    litRoomDetail.Text = detail;
                }

                var trCruise = e.Item.FindControl("trCruise") as HtmlTableRow;
                if (trCruise != null)
                {
                    if (total > 0)
                    {
                        trCruise.Attributes.CssStyle["background-color"] = SailsModule.GOOD;
                    }
                    else
                    {
                        trCruise.Attributes.CssStyle["background-color"] = SailsModule.IMPORTANT;
                    }
                }
            }
        }

        protected void rptPendings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Booking)
            {
                var booking = (Booking)e.Item.DataItem;
                var hplCode = e.Item.FindControl("hplCode") as HyperLink;
                if (hplCode != null)
                {
                    hplCode.Text = string.Format(BookingFormat, booking.Id);
                    hplCode.NavigateUrl = string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}",
                        Node.Id, Section.Id, booking.Id);
                }

                var hplAgency = e.Item.FindControl("hplAgency") as HyperLink;
                if (hplAgency != null)
                {
                    hplAgency.Text = booking.Agency.Name;
                    hplAgency.NavigateUrl = string.Format("AgencyEdit.aspx?NodeId={0}&SectionId={1}&AgencyId={2}",
                        Node.Id, Section.Id, booking.Agency.Id);
                }

                ValueBinder.BindLiteral(e.Item, "litRooms", booking.RoomName);
                ValueBinder.BindLiteral(e.Item, "litTrip", booking.Trip.Name);
                //ValueBinder.BindLiteral(e.Item, "litPartner", booking.Agency.Name);
                if (booking.Deadline.HasValue)
                    ValueBinder.BindLiteral(e.Item, "litPending", booking.Deadline.Value.ToString("HH:mm dd/MM/yyyy"));

                var lblCreatedBy = e.Item.FindControl("lblCreatedBy") as Label;
                if (lblCreatedBy != null)
                {
                    lblCreatedBy.Text = booking.CreatedBy.FullName;
                    ValueBinder.BindLiteral(e.Item, "litCreatedBy", booking.CreatedBy.FullName);
                    ValueBinder.BindLiteral(e.Item, "litCreatorPhone", booking.CreatedBy.Website);
                    ValueBinder.BindLiteral(e.Item, "litCreatorEmail", booking.CreatedBy.Email);
                }
                if (booking.Agency.Sale != null)
                {
                    ValueBinder.BindLabel(e.Item, "lblSaleInCharge", booking.Agency.Sale.FullName);
                    ValueBinder.BindLiteral(e.Item, "litSale", booking.Agency.Sale.FullName);
                    ValueBinder.BindLiteral(e.Item, "litSalePhone", booking.Agency.Sale.Website);
                    ValueBinder.BindLiteral(e.Item, "litSaleEmail", booking.Agency.Sale.Email);

                }
            }
        }

        protected void lbtCruiseName_Click(object sender, EventArgs e)
        {
            LinkButton lbtCruiseName = (LinkButton)sender;
            _cruise = Module.CruiseGetById(Convert.ToInt32(lbtCruiseName.CommandArgument));
            rptClass.DataSource = AllRoomClasses;
            rptClass.DataBind();
            litCurrentCruise.Text = _cruise.Name;
            ddlCruises.SelectedValue = _cruise.Id.ToString();
            plhCruiseName.Visible = true;

            var total = Convert.ToInt32(lbtCruiseName.Attributes["totalAvaiable"]);
            //Kiểm tra tàu nếu còn trống tất cả các phòng thì hiện nút charter
            //còn nếu tàu đã có booking thì ẩn nút charter 
            chkCharter.Visible = CheckCruiseForCharter(_cruise, total);
            //---------------------------------------------------
            ViewState["cruiseId"] = _cruise.Id;
        }

        protected void lbtCheckAvaiable_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Date.HasValue)
                {
                    ShowError(Resources.errorDateRequired);
                    return;
                }

                //if (!ActiveCruise.Trips.Contains(Trip))
                //{
                //    ShowError(string.Format("{0} doesn't run {1} trip", ActiveCruise.Name, Trip.Name));
                //    return;
                //}

                // Nếu sử dụng custom price, hiển thị giá mặc định theo agency
                if (CustomPriceAddBooking)
                {
                    // Lấy về agency và thông tin về chính sách giá cả
                    if (agencySelector.SelectedAgency != null && agencySelector.SelectedAgency.Role != null)
                    {
                        _policies = Module.AgencyPolicyGetByRole(agencySelector.SelectedAgency.Role);
                    }
                    else
                    {
                        _policies = Module.AgencyPolicyGetByRole(Module.RoleGetById(4));
                    }
                }

                //rptClass.DataSource = AllRoomClasses;
                //rptClass.DataBind();

                rptCruises.DataSource = Module.CruiseGetAll();
                rptCruises.DataBind();

                // lấy về danh sách booking pending chạy ngày này
                ICriterion criterion = Expression.And(Expression.Eq("Status", StatusType.Pending),
                    Expression.Ge("Deadline", DateTime.Now)); // pending và chưa hết hạn

                criterion = Expression.And(criterion, Expression.Eq("Deleted", false));
                criterion = Module.AddDateExpression(criterion, Date.Value);

                var list = Module.BookingGetByCriterion(criterion, null, 0, 0);
                //------------------------------------------------
                if (list.Count > 0)
                {
                    plhPending.Visible = true;
                    rptPendings.DataSource = list;
                    rptPendings.DataBind();
                }
                else
                {
                    plhPending.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                _logger.Error(ex.Message, ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckAvailable())
            {
                return;
            }

            // Kiểm tra thêm về phòng và thông tin đối tác
            if (!AllowNoAgency && agencySelector.SelectedAgency == null)
            {
                ShowError("Chưa có thông tin đối tác");
                return;
            }

            // Kiểm tra số phòng đã chọn, nếu chưa chọn phòng nào thì thông báo yêu cầu
            if (_bookingRooms.Count <= 0 && !chkCharter.Checked)
            {
                ShowError("Hãy chọn ít nhất một phòng");
                return;
            }

            //2. Lưu thông tin phòng như thế nào
            // Dùng vòng lặp lưu thông tin đơn thuần, không có giá trị đi kèm nào cả

            // Phải lưu thông tin booking trước

            #region -- Booking --

            Booking booking = new Booking();
            if (agencySelector.SelectedAgency != null)
            {
                booking.Agency = agencySelector.SelectedAgency;
            }
            booking.CreatedBy = Page.User.Identity as User;
            booking.CreatedDate = DateTime.Now;
            booking.ModifiedDate = DateTime.Now;
            booking.Partner = null;
            booking.Sale = booking.CreatedBy;
            booking.StartDate = Date.Value;
            
            if (ApprovedDefault)
            {
                booking.Status = StatusType.Approved;
            }
            else
            {
                booking.Status = StatusType.Pending;
            }

            booking.Status = StatusType.Pending;

            if (TripBased)
            {
                booking.Trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
            }
            else
            {
                booking.Trip = null;
            }
            var cruise = null as Cruise;
            if (ViewState["cruiseId"] != null)
            {
                var cruiseIdViewState = (int)ViewState["cruiseId"];
                cruise = Module.GetObject<Cruise>(cruiseIdViewState);          
            }   
            booking.Cruise = cruise;
            booking.IsCharter = chkCharter.Checked;
            booking.Total = 0;

            // Xác định custom booking id
            if (UseCustomBookingId)
            {
                int maxId = Module.BookingGenerateCustomId(booking.StartDate);
                booking.Id = maxId;
            }

            //if (booking.Cruise.Name.Contains("Scorpio") && !booking.Cruise.Name.Contains("1"))
            //{
            //    booking.IsCharter = true;
            //}
            Module.Save(booking, UserIdentity);
            #endregion

            // Sau đó mới có thể lưu thông tin phòng
            // Đối với phòng có baby và child theo phân bố từ trước sẽ thêm child, baby

            #region -- Booking Room --
            foreach (BookingRoom room in _bookingRooms)
            {
                room.Book = booking;
                Module.Save(room);
               
                // Ngoài ra còn phải lưu thông tin khách hàng

                #region -- Thông tin khách hàng, suy ra từ thông tin phòng --

                for (int ii = 1; ii <= room.Booked; ii++)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Adult;
                    if (CustomerPrice)
                    {
                        customer.Total = room.Total / 2;
                    }
                    Module.Save(customer);
                }

                if (room.HasBaby)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Baby;
                    Module.Save(customer);
                }

                if (room.HasChild)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Children;
                    Module.Save(customer);
                }

                #endregion
            }

            #endregion

            #region -- Thông tin dịch vụ đi kèm --

            foreach (RepeaterItem serviceItem in rptExtraServices.Items)
            {
                //HiddenField hiddenValue = (HiddenField)serviceItem.FindControl("hiddenValue");
                HiddenField hiddenId = (HiddenField)serviceItem.FindControl("hiddenId");
                CheckBox chkService = (CheckBox)serviceItem.FindControl("chkService");
                if (chkService.Checked)
                {
                    ExtraOption service = Module.ExtraOptionGetById(Convert.ToInt32(hiddenId.Value));
                    BookingExtra extra = new BookingExtra();
                    extra.Booking = booking;
                    extra.ExtraOption = service;
                    Module.Save(extra);
                }
            }

            #endregion

            #region -- Track thêm mới --

            BookingTrack track = new BookingTrack();
            track.Booking = booking;
            track.ModifiedDate = DateTime.Now;
            track.User = UserIdentity;
            Module.SaveOrUpdate(track);

            BookingChanged change = new BookingChanged();
            change.Action = BookingAction.Created;
            change.Parameter = string.Format("{0}", booking.Total);
            change.Track = track;
            Module.SaveOrUpdate(change);

            #endregion

            if (chkCharter.Checked)
            {
                var locked = new Locked();
                locked.Charter = booking;
                locked.Cruise = booking.Cruise;
                locked.Description = "Booking charter";
                locked.Start = booking.StartDate;
                locked.End = booking.EndDate;
                Module.SaveOrUpdate(locked);
            }

            //var smtpClient = new SmtpClient("mail.orientalsails.com", 26)
            //{
            //    Credentials = new NetworkCredential("mo@orientalsails.com", "EGGaXBwuEWa+")

            //};

            //var message = new MailMessage
            //{
            //    From = new MailAddress("mo@orientalsails.com"),
            //    IsBodyHtml = true,
            //    BodyEncoding = Encoding.UTF8,
            //    Body = "alooo",
            //    Subject = "CutOff Date Booking Reminder"
            //};

            //message.To.Add(new MailAddress("it2@atravelmate.com"));
            //if (booking.BookingSale != null)
            //{
            //    if (booking.BookingSale.Sale != null)
            //    {
            //        message.To.Add(new MailAddress(booking.BookingSale.Sale.Email));
            //    }
            //}
            //smtpClient.Send(message);

            PageRedirect(string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}&Notify=0", Node.Id,
                Section.Id, booking.Id));
        }

        /// <summary>
        ///     Kiểm tra số phòng của tàu với số phòng còn trống của tàu.
        /// </summary>
        /// <param name="cruise"></param>
        /// <param name="roomAvaiable"></param>
        /// <returns>True tàu còn trống tất cả các phòng False tàu đã có booking</returns>
        public bool CheckCruiseForCharter(Cruise cruise, int roomAvaiable)
        {
            if (cruise != null)
            {
                int roomOfCruise =
                    Module.CountObjet<Room>(Expression.And(Expression.Eq("Cruise", cruise),
                        Expression.Eq("Deleted", false)));
                return roomOfCruise == roomAvaiable;
            }
            else
            {
                throw new Exception("cruise = null");
            }
        }

        /// <summary>
        /// Load dữ liệu hành trình vào hộp chọn
        /// </summary>
        public void BindTrips()
        {
            ddlTrips.DataSource = _trips; // Danh sách trip luôn được get về trước khi gọi tới hàm bind trips
            ddlTrips.DataTextField = "Name";
            ddlTrips.DataValueField = "Id";
            ddlTrips.DataBind();
        }

        /// <summary>
        /// Load dữ liệu hành tàu vào hộp chọn
        /// </summary>
        public void BindCruises()
        {
            ddlCruises.DataSource = Module.CruiseGetAll();
            ddlCruises.DataTextField = "Name";
            ddlCruises.DataValueField = "Id";
            ddlCruises.DataBind();

            if (ddlCruises.Items.Count == 1)
            {
                ddlCruises.Visible = false;
            }
        }

        /// <summary>
        /// Load các dịch vụ gia tăng vào danh sách hộp đánh dấu
        /// </summary>
        public void BindServices()
        {
            rptExtraServices.DataSource = Module.ExtraOptionGetBooking();
            rptExtraServices.DataBind();
        }

        /// <summary>
        /// Kiểm tra xem có còn đủ phòng trống hay không, đồng thời tạo ra dữ liệu booking room
        /// </summary>
        /// <returns></returns>
        public bool CheckAvailable()
        {
            _bookingRooms = new ArrayList();
            foreach (RepeaterItem classItem in rptClass.Items)
            {
                var hiddenId = (HiddenField)classItem.FindControl("hiddenId");
                var rclass = Module.RoomClassGetById(Convert.ToInt32(hiddenId.Value));
                var rptTypes = (Repeater)classItem.FindControl("rptTypes");
                foreach (RepeaterItem typeItem in rptTypes.Items)
                {
                    if (!typeItem.Visible)
                        continue; // Bỏ qua nếu là đối tượng ẩn (ẩn là do không tồn tại loại phòng này)
                    HiddenField hiddenTypeId = (HiddenField)typeItem.FindControl("hiddenId");
                    RoomTypex rtype = Module.RoomTypexGetById(Convert.ToInt32(hiddenTypeId.Value));
                    DropDownList ddlAdults = (DropDownList)typeItem.FindControl("ddlAdults");
                    DropDownList ddlChild = (DropDownList)typeItem.FindControl("ddlChild");
                    DropDownList ddlBaby = (DropDownList)typeItem.FindControl("ddlBaby");
                    double unitprice = 0;
                    if (CustomPriceAddBooking)
                    {
                        TextBox txtPrice = (TextBox)typeItem.FindControl("txtPrice");
                        unitprice = Convert.ToDouble(txtPrice.Text);
                    }

                    // Tìm số phòng available
                    int roomCount;
                    Locked locked = null;
                    var cruise = null as Cruise;
                    if (ViewState["cruiseId"] != null)
                    {
                        var cruiseIdViewState = (int)ViewState["cruiseId"];
                        cruise = Module.GetObject<Cruise>(cruiseIdViewState);
                        locked = Module.LockedCheckByDate(cruise, _date.Value,
                        _date.Value.AddDays(Trip.NumberOfDay - 1));
                    }       
                    if (locked != null)
                    {
                        if (!string.IsNullOrEmpty(locked.Description))
                        {
                            ShowError(
                                string.Format(
                                    "Hành trình này đã bị khóa với lý do: {0}, vẫn có thể add booking như buộc phải chuyển sang tàu khác",
                                    locked.Description));
                        }
                        // Khi ấy thì lấy về số phòng, trong đó bỏ qua lock
                        roomCount = Module.RoomCount(rclass, rtype, cruise, _date.Value, Trip.NumberOfDay, true,
                            Trip.HalfDay);
                    }
                    else
                    {
                        roomCount = Module.RoomCount(rclass, rtype, cruise, _date.Value, Trip.NumberOfDay,
                            Trip.HalfDay);
                    }

                    if (roomCount < 0)
                    {
                        continue;
                    }

                    // Lấy về số phòng (đối với phòng thường) và số người đối với phòng ở ghép
                    int adult = Convert.ToInt32(ddlAdults.SelectedValue);
                    int child = Convert.ToInt32(ddlChild.SelectedValue);
                    int baby = Convert.ToInt32(ddlBaby.SelectedValue);
                    int room;

                    // Nếu là phòng ở ghép thì tính theo đơn vị số người chứ không phải số phòng
                    if (rtype.IsShared)
                    {
                        room = adult / rtype.Capacity;
                    }
                    else
                    {
                        room = adult;
                    }

                    if (child > room || baby > room)
                    {
                        ShowError(Resources.errorOneChildOneBaby);
                        return false;
                    }

                    if (adult > roomCount)
                    {
                        ShowError(Resources.errorNotEnoughAvailable);
                        return false;
                    }

                    // Nếu đủ số phòng trống thì mới thực hiện việc tạo dữ liệu phòng đã book

                    bool isShared = false;
                    if (rtype.IsShared && adult % 2 == 1)
                    {
                        room += 1;
                        isShared = true;
                    }

                    // Với mỗi loại room, căn cứ theo số phòng xác định phòng có child, baby và phòng share
                    for (int ii = 1; ii <= room; ii++)
                    {
                        var booking = new BookingRoom();
                        booking.BookingType = BookingType.Double;
                        booking.RoomType = rtype;
                        booking.RoomClass = rclass;

                        // Nếu là phòng cuối thì cho phòng này là phòng shared
                        if (ii == room && isShared)
                        {
                            booking.Booked = 1;
                        }
                        else
                        {
                            booking.Booked = 2;
                        }

                        // Các phòng đầu sẽ ghép child/baby vào (nếu có) vì có thể đảm bảo rằng không phải ở ghép
                        if (child > 0)
                        {
                            booking.HasChild = true;
                            child--;
                        }

                        if (baby > 0)
                        {
                            booking.HasBaby = true;
                            baby--;
                        }

                        if (CustomPriceAddBooking)
                        {
                            booking.Total = unitprice;
                        }

                        _bookingRooms.Add(booking);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check input khi save va hien thi loi
        /// </summary>
        /// <returns></returns>
        public bool CheckInputAndShowError()
        {
            string error = "";
            bool haveError = false;
            if (ddlTrips.SelectedIndex == -1)
            {
                error += "Chưa chọn trip <br/>";
                haveError = true;
            }
            if (String.IsNullOrEmpty(txtDate.Text))
            {
                error += "Chưa chọn start date <br/>";
                haveError = true;
            }

            if (agencySelector.SelectedAgency == null)
            {
                error += "Chưa chọn agency <br/>";
                haveError = true;
            }
            if (haveError)
                ShowError(error);

            return haveError;

        }
    }
}