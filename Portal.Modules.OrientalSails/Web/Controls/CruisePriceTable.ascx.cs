using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public partial class CruisePriceTable : UserControl
    {
        private SailsModule _module;
        public SailsModule Module
        {
            get
            {
                if (_module ==null)
                {
                    if (Page is SailsAdminBasePage)
                    {
                        _module = ((SailsAdminBasePage) Page).Module;
                    }
                }
                return _module;
            }
        }
        public SailsPriceTable SailsPriceTable;
        public SailsPriceTable SailsPriceTableTemplate;
        public Cruise ActiveCruise;
        public IList AllRoomClasses;
        public IList AllRoomTypes;

        public SailsTrip Trip;
        public TripOption Option;

        private bool _hasRoom = false;
        private bool _isValid;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Bind()
        {
            //litTripName.Text = Trip.Name;

            AllRoomTypes = Module.RoomTypeGetByCruise(ActiveCruise);

            rptRoomClass.DataSource = AllRoomClasses;
            rptRoomClass.DataBind();

            //rptRoomTypeHeader.DataSource = AllRoomTypes;
            //rptRoomTypeHeader.DataBind();
        }

        protected void rptRoomClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            {
                RoomClass roomClass = (RoomClass)e.Item.DataItem;

                #region -- Header --

                //// Đối với header, thêm danh sách roomType thông thường
                //using (Repeater rpt = e.Item.FindControl("rptRoomTypeHeader") as Repeater)
                //{
                //    if (rpt != null)
                //    {
                //        rpt.DataSource = Module.RoomTypexGetAll();
                //        rpt.DataBind();
                //    }
                //}

                #endregion

                #region -- Item --

                #region RoomClass Id

                using (Label labelRoomClassId = e.Item.FindControl("labelRoomClassId") as Label)
                {
                    if (labelRoomClassId != null)
                    {
                        labelRoomClassId.Text = roomClass.Id.ToString();
                    }
                }

                #endregion

                ValueBinder.BindLabel(e.Item, "lblTrip", Trip.TripCode);
                if (Trip.NumberOfOptions > 1)
                {
                    ValueBinder.BindLiteral(e.Item, "litOption", Option);
                }
                ValueBinder.BindLiteral(e.Item, "litName", roomClass.Name);

                //Đối với từng dòng
                using (Repeater rpt = e.Item.FindControl("rptRoomTypeCell") as Repeater)
                {
                    if (rpt != null)
                    {
                        // Gán sự kiện ItemDataBound (vì control trong Repeater không tự nhận hàm này)
                        rpt.ItemDataBound += RptRoomTypeItemDataBound;

                        IList roomTypeList = AllRoomTypes;

                        _hasRoom = false;

                        rpt.DataSource = roomTypeList;
                        rpt.DataBind();

                        e.Item.Visible = _hasRoom;
                    }
                }

                #endregion
            }
        }

        protected void RptRoomTypeItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RoomTypex rtype = e.Item.DataItem as RoomTypex;
            RoomClass father = (RoomClass)(((RepeaterItem)e.Item.Parent.Parent).DataItem);
            TextBox txtSingle = (e.Item.Parent.Parent).FindControl("txtSingle") as TextBox;
            TextBox txtSingleVND = (e.Item.Parent.Parent).FindControl("txtSingleVND") as TextBox;
            if (rtype != null)
            {
                #region RoomType Id

                using (Label labelRoomTypeId = e.Item.FindControl("labelRoomTypeId") as Label)
                {
                    if (labelRoomTypeId != null)
                    {
                        labelRoomTypeId.Text = rtype.Id.ToString();
                    }
                }

                #endregion

                TextBox textBoxPrice = (TextBox)e.Item.FindControl("textBoxPrice");
                TextBox textBoxPriceVND = (TextBox) e.Item.FindControl("textBoxPriceVND");
                Label labelSailsPriceConfigId = (Label)e.Item.FindControl("labelSailsPriceConfigId");

                //Kiểm tra xem có tồn tại room nào mà class và type là rtype và father ko?
                IList room = Module.RoomGetBy_ClassType(ActiveCruise, father, rtype);
                //Nếu có thì hiện giá
                if (room.Count > 0)
                {
                    Domain.SailsPriceConfig priceConfig = Module.SailsPriceConfigGet(SailsPriceTable, rtype, father, Trip, ActiveCruise, Option);
                    //Module.SailsPriceConfigGetBy_RoomType_RoomClass_Trip(_trip,rtype,father,Option);
                    //Nếu có giá thì hiện
                    if (priceConfig != null)
                    {
                        labelSailsPriceConfigId.Text = priceConfig.Id.ToString();
                        textBoxPrice.Text = priceConfig.NetPrice.ToString("#,0.#");
                        textBoxPriceVND.Text = priceConfig.NetPriceVND.ToString("#,0.#");
                        if (txtSingle != null)
                        {
                            txtSingle.Text = priceConfig.SpecialPrice.ToString("#,0.#");
                        }
                        if (txtSingleVND != null) txtSingleVND.Text = priceConfig.SpecialPriceVND.ToString("#,0.#");
                    }

                    _hasRoom = true;
                }
                //Nếu không tồn tại room thì để N/A
                else
                {
                    textBoxPrice.Visible = true;
                    textBoxPrice.Enabled = false;
                    textBoxPrice.Text = @"N/A";
                    textBoxPriceVND.Visible = true;
                    textBoxPriceVND.Enabled = false;
                    textBoxPriceVND.Text = @"N/A";
                }
            }
        }

        public void Save()
        {
            foreach (RepeaterItem rptClassItem in rptRoomClass.Items)
            {
                Repeater rptRoomTypeCell = rptClassItem.FindControl("rptRoomTypeCell") as Repeater;
                Label labelRoomClassId = rptClassItem.FindControl("labelRoomClassId") as Label;
                if (labelRoomClassId != null && labelRoomClassId.Text != string.Empty && rptRoomTypeCell != null)
                {
                    RoomClass roomClass = Module.RoomClassGetById(Convert.ToInt32(labelRoomClassId.Text));

                    #region -- Kiểm tra tính hợp lệ của bảng giá --

                    foreach (RepeaterItem priceItem in rptRoomTypeCell.Items)
                    {
                        TextBox txtCellPrice = priceItem.FindControl("textBoxPrice") as TextBox;
                        //Kiểm tra xem textboxPrice có enable ko ( không nghĩa là o tồn tại giá kiểu class và type đó)
                        if (txtCellPrice != null && txtCellPrice.Enabled)
                        {
                            double price;
                            //kiểm tra xem price có hợp lệ ko
                            _isValid = double.TryParse(txtCellPrice.Text, out price);
                            if (!_isValid) break;
                        }
                    }

                    #endregion

                    //Nếu bảng giá hợp lệ thì lưu
                    if (_isValid)
                    {
                        //Module.SaveOrUpdate(Table);

                        TextBox txtSingle = rptClassItem.FindControl("txtSingle") as TextBox;
                        TextBox txtSingleVND = rptClassItem.FindControl("txtSingleVND") as TextBox;
                        double single = 0;
                        double singleVND = 0;
                        if (txtSingle != null && !string.IsNullOrEmpty(txtSingle.Text))
                        {
                            single = Convert.ToDouble(txtSingle.Text);
                        }

                         if (txtSingleVND != null && !string.IsNullOrEmpty(txtSingleVND.Text))
                        {
                            singleVND = Convert.ToDouble(txtSingleVND.Text);
                        }

                        foreach (RepeaterItem priceItem in rptRoomTypeCell.Items)
                        {
                            Label labelRoomTypeId = priceItem.FindControl("labelRoomTypeId") as Label;
                            Label labelSailsPriceConfigId =
                                priceItem.FindControl("labelSailsPriceConfigId") as Label;
                            TextBox textBoxPrice = priceItem.FindControl("textBoxPrice") as TextBox;
                            TextBox textBoxPriceVND = priceItem.FindControl("textBoxPriceVND") as TextBox;
                            RoomTypex roomType = null;

                            #region Lấy về RoomType tương ứng để chuẩn bị lưu

                            if (labelRoomTypeId != null && labelRoomTypeId.Text != string.Empty)
                            {
                                if (Convert.ToInt32(labelRoomTypeId.Text) > 0)
                                {
                                    roomType = Module.RoomTypexGetById(Convert.ToInt32(labelRoomTypeId.Text));
                                }
                            }

                            #endregion

                            if (textBoxPrice != null && textBoxPrice.Enabled &&
                                textBoxPrice.Text != string.Empty && textBoxPriceVND != null && textBoxPriceVND.Enabled &&
                                textBoxPriceVND.Text != string.Empty  )
                            {
                                double price;
                                double priceVND;
                                //Check giá người dùng nhập vào
                                if (!double.TryParse(textBoxPrice.Text, out price))
                                {
                                    _isValid = false;
                                    break;
                                }

                                if (!double.TryParse(textBoxPriceVND.Text, out priceVND))
                                {
                                    _isValid = false;
                                    break;
                                }
                                Domain.SailsPriceConfig rPrice;
                                if (SailsPriceTableTemplate != null)
                                {
                                    rPrice = new SailsPriceConfig();
                                    rPrice.RoomType = roomType;
                                    rPrice.RoomClass = roomClass;
                                    rPrice.TripOption = Option;
                                    rPrice.Trip = Trip;
                                    rPrice.SpecialPrice = single;
                                    rPrice.SpecialPriceVND = singleVND;
                                    rPrice.NetPrice = price;
                                    rPrice.NetPriceVND = priceVND;
                                    rPrice.Table = SailsPriceTable;
                                    rPrice.Trip = Trip;
                                    rPrice.TripOption = Option;
                                    rPrice.Cruise = ActiveCruise;
                                    Module.SaveOrUpdate(rPrice);
                                }
                                else
                                {
                                    if (labelSailsPriceConfigId != null &&
                                  !string.IsNullOrEmpty(labelSailsPriceConfigId.Text) &&
                                  Convert.ToInt32(labelSailsPriceConfigId.Text) > 0)
                                    {
                                        //update
                                        rPrice =
                                            Module.SailsPriceConfigGetById(Convert.ToInt32(labelSailsPriceConfigId.Text));
                                    }
                                    else
                                    {
                                        //insert
                                        rPrice = new Domain.SailsPriceConfig();
                                        rPrice.RoomType = roomType;
                                        rPrice.RoomClass = roomClass;
                                        rPrice.TripOption = Option;
                                        rPrice.Trip = Trip;
                                    }
                                    rPrice.SpecialPrice = single;
                                    rPrice.SpecialPriceVND = singleVND;
                                    rPrice.NetPrice = price;
                                    rPrice.NetPriceVND = priceVND;
                                    rPrice.Table = SailsPriceTable;
                                    rPrice.Trip = Trip;
                                    rPrice.TripOption = Option;
                                    rPrice.Cruise = ActiveCruise;
                                    Module.SaveOrUpdate(rPrice);

                                }
                            }
                        }
                    }
                }
            }
        }       
    }
}