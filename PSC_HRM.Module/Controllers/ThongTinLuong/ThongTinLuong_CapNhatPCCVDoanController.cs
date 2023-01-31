﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CapNhatPCCVDoanController : ViewController
    {
        public ThongTinLuong_CapNhatPCCVDoanController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_CapNhatPCCVDoanController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                using (XPCollection<ChucVuDoan> cmList = new XPCollection<ChucVuDoan>(((XPObjectSpace)obs).Session))
                {
                    XPCollection<DoanVien> nvList = new XPCollection<DoanVien>(((XPObjectSpace)obs).Session);
                    foreach (ChucVuDoan item in cmList)
                    {
                        nvList.Criteria = CriteriaOperator.Parse("ChucVuDoan=?", item.Oid);
                        foreach (DoanVien nvItem in nvList)
                        {
                        	nvItem.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDoan = item.HSPCChucVuDoan;
                        }
                    }
                    obs.CommitChanges();
                }
            }
        }
    }
}
