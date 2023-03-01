using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách hưởng phụ cấp ưu đãi")]
    [Appearance("Report_NangLuong_DanhSachHuongPhuCapUuDai", TargetItems = "QuyetDinhHuongPhuCapUuDai", Enabled = false, Criteria = "TatCaQuyetDinh")]
    public class Report_NangLuong_DanhSachHuongPhuCapUuDai : StoreProcedureReport
    {
        // Fields...
        private int _Nam;
        private QuyetDinh.QuyetDinhHuongPhuCapUuDai _QuyetDinhHuongPhuCapUuDai;
        private bool _TatCaQuyetDinh = true;     

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả quyết định")]
        public bool TatCaQuyetDinh
        {
            get
            {
                return _TatCaQuyetDinh;
            }
            set
            {
                SetPropertyValue("TatCaQuyetDinh", ref _TatCaQuyetDinh, value);
            }
        }        

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaQuyetDinh")]
        [ModelDefault("Caption", "Quyết định hưởng phụ cấp ưu đãi")]
        public QuyetDinh.QuyetDinhHuongPhuCapUuDai QuyetDinhHuongPhuCapUuDai
        {
            get
            {
                return _QuyetDinhHuongPhuCapUuDai;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongPhuCapUuDai", ref _QuyetDinhHuongPhuCapUuDai, value);
                Nam = QuyetDinhHuongPhuCapUuDai.NgayHieuLuc.Year;
            }
        }
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
              
            }
        }

        public Report_NangLuong_DanhSachHuongPhuCapUuDai(Session session) : base(session) { }


        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@Nam", Nam);
            parameter[1] = new SqlParameter("@QuyetDinhHuongPhuCapUuDai", QuyetDinhHuongPhuCapUuDai != null ? QuyetDinhHuongPhuCapUuDai.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_NangLuong_DanhSachHuongPhuCapUuDai", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();    
           
        }
    }

}
