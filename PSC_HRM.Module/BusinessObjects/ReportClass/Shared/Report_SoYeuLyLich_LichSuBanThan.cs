﻿using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Lịch sử bản thân")]
    public class Report_SoYeuLyLich_LichSuBanThan : StoreProcedureReport, IBoPhan
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        public Report_SoYeuLyLich_LichSuBanThan(Session session) : base(session) { }
        //public override SqlCommand CreateCommand()
        //{
        //    SqlCommand cmd = DataProvider.GetCommand("spd_Report_SoYeuLyLich_LichSuBanThan", 
        //        System.Data.CommandType.StoredProcedure, new SqlParameter("@ThongTinNhanVien", ThongTinNhanVien.Oid));
        //    return cmd;
        //}
        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_SoYeuLyLich_LichSuBanThan", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);
                da.Fill(DataSource);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetAllNhanVien(BoPhan);
        }
    }

}
