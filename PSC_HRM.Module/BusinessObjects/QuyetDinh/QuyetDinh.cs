using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định")]
    [DefaultProperty("SoQuyetDinh")]

    [Appearance("QuyetDinh.LamViecTaiTruong", TargetItems = "NguoiKy1", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiNguoiKy=0")]
    [Appearance("QuyetDinh.Khac", TargetItems = "NguoiKy", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiNguoiKy=2  or PhanLoaiNguoiKy=1")]
    [Appearance("QuyetDinh.TruongRaQuyetDinh", TargetItems = "TenCoQuan", Visibility = ViewItemVisibility.Hide, Criteria = "CoQuanRaQuyetDinh=0")]
    [Appearance("QuyetDinh.CoQuanKhacRaQuyetDinh", TargetItems = "ThongTinTruong", Visibility = ViewItemVisibility.Hide, Criteria = "CoQuanRaQuyetDinh=1 or CoQuanRaQuyetDinh=2")]
    [Appearance("HideKhac.NEU", TargetItems = "SoPhieuTrinh;NgayPhieuTrinh", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'NEU'")]

    public class QuyetDinh : TruongBaseObject, IThongTinTruong
    {
        private DateTime _CreateDate;
        private string _CanCu;
        private bool _IsDirty;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private DateTime _NgayHieuLuc;
        private string _NoiDung;
        private CapQuyetDinh _CapQuyetDinh;
        private ChucVu _ChucVuNguoiKy;
        private NguoiKyEnum _PhanLoaiNguoiKy;
        private ThongTinNhanVien _NguoiKy;
        private string _NguoiKy1;
        private CoQuanRaQuyetDinhEnum _CoQuanRaQuyetDinh;
        private ThongTinTruong _ThongTinTruong;
        private string _TenCoQuan;
        private string _NhanVienText;
        private string _BoPhanText;
        private NguoiSuDung _NguoiSuDung;    
        private int _DaCapNhat;
        private string _SoPhieuTrinh;
        private DateTime _NgayPhieuTrinh;
       
        //private string _LuuTru;
        private GiayToHoSo _GiayToHoSo;

        [ImmediatePostData]
        [ModelDefault("Caption", "Cơ quan ra quyết định")]
        public CoQuanRaQuyetDinhEnum CoQuanRaQuyetDinh
        {
            get
            {
                return _CoQuanRaQuyetDinh;
            }
            set
            {
                SetPropertyValue("CoQuanRaQuyetDinh", ref _CoQuanRaQuyetDinh, value);
                UpdateChucVuList();
                if (!IsLoading)
                {
                    ChucVuNguoiKy = null;
                    if (value == CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh)
                    { PhanLoaiNguoiKy = NguoiKyEnum.NgoaiTruong; }
                    else
                    { PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc; }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
                if (!IsLoading && value != null) 
                    TenCoQuan = value.TenBoPhan;
            }
        }

        [ModelDefault("Caption", "Tên cơ quan")]
        public string TenCoQuan
        {
            get
            {
                return _TenCoQuan;
            }
            set
            {
                SetPropertyValue("TenCoQuan", ref _TenCoQuan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
                if (!IsLoading && !string.IsNullOrEmpty(value))
                    QuyetDinhChanged();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số phiếu trình")]
        public string SoPhieuTrinh
        {
            get
            {
                return _SoPhieuTrinh;
            }
            set
            {
                SetPropertyValue("SoPhieuTrinh", ref _SoPhieuTrinh, value);
                if (!IsLoading && !string.IsNullOrEmpty(value))
                    QuyetDinhChanged();
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UTE'")]        
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hiệu lực")]
        public DateTime NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
                if (!IsLoading && value != DateTime.MinValue)
                    QuyetDinhChanged();
            }
        }

        [ModelDefault("Caption", "Ngày phiếu trình")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UTE'")]        
        public DateTime NgayPhieuTrinh
        {
            get
            {
                return _NgayPhieuTrinh;
            }
            set
            {
                SetPropertyValue("NgayPhieuTrinh", ref _NgayPhieuTrinh, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Căn cứ")]
        public string CanCu
        {
            get
            {
                return _CanCu;
            }
            set
            {
                SetPropertyValue("CanCu", ref _CanCu, value);
            }
        }

        [Size(1000)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Về việc")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
                if (!IsLoading && !string.IsNullOrEmpty(value))
                    QuyetDinhChanged();
            }
        }

        [ModelDefault("Caption", "Cấp quyết định")]
        public CapQuyetDinh CapQuyetDinh
        {
            get
            {
                return _CapQuyetDinh;
            }
            set
            {
                SetPropertyValue("CapQuyetDinh", ref _CapQuyetDinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại người ký")]
        public NguoiKyEnum PhanLoaiNguoiKy
        {
            get
            {
                return _PhanLoaiNguoiKy;
            }
            set
            {
                SetPropertyValue("PhanLoaiNguoiKy", ref _PhanLoaiNguoiKy, value);
                if (!IsLoading && ChucVuNguoiKy != null)
                {
                    UpdateNguoiKyList();
                    NguoiKy = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ người ký")]
        [DataSourceProperty("ChucVuList")]
        public ChucVu ChucVuNguoiKy
        {
            get
            {
                return _ChucVuNguoiKy;
            }
            set
            {
                SetPropertyValue("ChucVuNguoiKy", ref _ChucVuNguoiKy, value);
                if (!IsLoading)
                {
                    UpdateNguoiKyList();
                    NguoiKy = null;
 
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NguoiKyList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Người ký")]
        //[DataSourceProperty("NguoiKyList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoaiNguoiKy=0")]
        public ThongTinNhanVien NguoiKy
        {
            get
            {
                return _NguoiKy;
            }
            set
            {
                SetPropertyValue("NguoiKy", ref _NguoiKy, value);
                if (!IsLoading && NguoiKy != null)
                    NguoiKy1 = NguoiKy.HoTen;
            }
        }

        [ModelDefault("Caption", "Người ký 1")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoaiNguoiKy=2")]
        public string NguoiKy1
        {
            get
            {
                return _NguoiKy1;
            }
            set
            {
                SetPropertyValue("NguoiKy1", ref _NguoiKy1, value);
            }
        }

        //lưu vết ngày lập quyết định
        [Browsable(false)]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        //chi su dung de lam cho parent object bi changed
        [NonPersistent]
        [Browsable(false)]
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                SetPropertyValue("IsDirty", ref _IsDirty, value);
            }
        }

        //Dùng để biết job có cập nhật chưa
        [Browsable(false)]
        public int DaCapNhat
        {
            get
            {
                return _DaCapNhat;
            }
            set
            {
                SetPropertyValue("DaCapNhat", ref _DaCapNhat, value);
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Nhân viên (text)")]
        public string NhanVienText
        {
            get
            {
                return _NhanVienText;
            }
            set
            {
                SetPropertyValue("NhanVienText", ref _NhanVienText, value);
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Đơn vị (text)")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }

        [Browsable(false)]
        public NguoiSuDung NguoiSuDung
        {
            get
            {
                return _NguoiSuDung;
            }
            set
            {
                SetPropertyValue("NguoiSuDung", ref _NguoiSuDung, value);
            }
        }

        //[ModelDefault("Caption", "Lưu trữ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        //public string LuuTru
        //{
        //    get
        //    {
        //        return _LuuTru;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LuuTru", ref _LuuTru, value);
        //    }
        //}

        //[Aggregated]
        //[VisibleInListView(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Lưu trữ")]
        //[ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
                if(!IsLoading && value != null && !string.IsNullOrEmpty(value.DuongDanFile))
                {
                    XemGiayToHoSo();
                }
            }
        }
        public QuyetDinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
            PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc;
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            if (TruongConfig.MaTruong.Equals("HVNH"))
            {
                CapQuyetDinh = Session.FindObject<CapQuyetDinh>(CriteriaOperator.Parse("TenCapQuyetDinh like ?", "BAN GIÁM ĐỐC"));
                ChucVuNguoiKy = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Phó Giám Đốc"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ? and ThongTinTruong=?", "Phó Giám Đốc", "Đang làm việc", ThongTinTruong.Oid));
            }
            else
            {
                CapQuyetDinh = Session.FindObject<CapQuyetDinh>(CriteriaOperator.Parse("TenCapQuyetDinh like ?", "BAN GIÁM HIỆU"));
                ChucVuNguoiKy = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Hiệu trưởng%"));
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ? and ThongTinTruong=?", "Hiệu trưởng", "Đang làm việc", ThongTinTruong.Oid));
            }
            NgayQuyetDinh = HamDungChung.GetServerTime();
            NgayHieuLuc = HamDungChung.GetServerTime();
            CreateDate = HamDungChung.GetServerTime();
            
            UpdateChucVuList();
            UpdateNguoiKyList();
            MaTruong = TruongConfig.MaTruong;
            
            DaCapNhat = 0;
            if (TruongConfig.MaTruong.Equals("NEU"))
                SoQuyetDinh = "/QĐ-ĐHKTQD ";

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.QuyetDinh = this;
            GiayToHoSo.SoBan = 1;
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "Bản gốc"));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateChucVuList();
            //UpdateNguoiKyList();
            MaTruong = TruongConfig.MaTruong;
        }      

        protected virtual void QuyetDinhChanged() 
        { }

        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyList { get; set; }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }

        private void UpdateChucVuList()
        {
            if (ChucVuList == null)
                ChucVuList = new XPCollection<ChucVu>(Session);

            if (CoQuanRaQuyetDinh != CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh)
                ChucVuList.Criteria = CriteriaOperator.Parse("PhanLoai=2 or PhanLoai=1");
            else
                ChucVuList.Criteria = CriteriaOperator.Parse("PhanLoai=2 or PhanLoai=0 or PhanLoai is null");
        }

        public void UpdateNguoiKyList()
        {
            if (NguoiKyList == null)
                NguoiKyList = new XPCollection<HoSo.ThongTinNhanVien>(Session, false);
            else
                NguoiKyList.Reload();
            if (ChucVuNguoiKy != null)
            {
                CriteriaOperator filter = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKy);
                XPCollection<HoSo.ThongTinNhanVien> ds = new XPCollection<ThongTinNhanVien>(Session, filter);
                foreach(HoSo.ThongTinNhanVien item in ds)
                {
                    NguoiKyList.Add(item);
                }
            }
            //NguoiKyList.Criteria = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKy);
            //if (ChucVuNguoiKy != null)
            //    NguoiKyList.Criteria = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKy);
        }

        protected override void OnSaving()
        {
 	         base.OnSaving();

            if (GiayToHoSo != null)
            {
                GiayToHoSo.QuyetDinh = this;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.NgayLap = NgayQuyetDinh;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        }
        
        private void XemGiayToHoSo()
        {
            using (DialogUtil.AutoWait())
            {
                try
                {
                    byte[] data = FptProvider.DownloadFile(GiayToHoSo.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                    if (data != null)
                    {
                        string strTenFile = "TempFile.pdf";
                        //Lưu file vào thư mục bin\Debug
                        HamDungChung.SaveFilePDF(data, strTenFile);
                        //Đọc file pdf
                        frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                        viewer.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

