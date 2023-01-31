﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.ChamCong;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChamCong_TheoDoiNgayNghiController : ViewController
    {
        public ChamCong_TheoDoiNgayNghiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChamCong_TheoDoiNgayNghiController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            //ObjectSpace obs = Application.CreateObjectSpace();
            if (view != null)
            {
                DanhSachTheoDoiNgayNghi danhSachTheoDoiNgayNghi = view.CurrentObject as DanhSachTheoDoiNgayNghi;
                if (danhSachTheoDoiNgayNghi != null)
                {
                    foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                    {
                        if (item.Id == "CustomControl")
                        {
                            SimpleButton button = item.Control as SimpleButton;
                            if (button != null)
                            {
                                button.Text = "Tìm kiếm";
                                button.Click += (se, ea) =>
                                    {
                                        using (DialogUtil.AutoWait())
                                        {
                                            danhSachTheoDoiNgayNghi.LoadData();
                                        }
                                    };
                            }
                        }
                    }
                }
            }
        }
    }
}