﻿using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách hoàn thành đào tạo")]
    public class Report_DaoTao_DanhSachHoanThanhDaoTao : StoreProcedureReport
    {
        // Fields...
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private LoaiDaoTaoEnum _LoaiDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;

        [ModelDefault("Caption", "Loại đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiDaoTaoEnum LoaiDaoTao
        {
            get
            {
                return _LoaiDaoTao;
            }
            set
            {
                SetPropertyValue("LoaiDaoTao", ref _LoaiDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public Report_DaoTao_DanhSachHoanThanhDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            LoaiDaoTao = LoaiDaoTaoEnum.TatCa;
        }

        public override SqlCommand CreateCommand()
        {

            SqlCommand cmd = new SqlCommand("spd_Report_DaoTao_DanhSachHoanThanhDaoTao");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TuNgay", TuNgay);
            cmd.Parameters.AddWithValue("@DenNgay", DenNgay);          
            return cmd;
        }
    }

}
