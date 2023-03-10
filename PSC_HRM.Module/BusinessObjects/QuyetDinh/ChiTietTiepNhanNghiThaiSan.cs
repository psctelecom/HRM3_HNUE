using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định tiếp nhận nghỉ thai sản")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhTiepNhanNghiThaiSan;ThongTinNhanVien")]
    public class ChiTietTiepNhanNghiThaiSan : TruongBaseObject
    {
        private QuyetDinhTiepNhanNghiThaiSan _QuyetDinhTiepNhanNghiThaiSan;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private TinhTrang _TinhTrang;
        private TinhTrang _TinhTrangMoi;
        private GiayToHoSo _GiayToHoSo;

        [Browsable(false)]
        [Association("QuyetDinhTiepNhanNghiThaiSan-ListChiTietTiepNhanNghiThaiSan")]
        public QuyetDinhTiepNhanNghiThaiSan QuyetDinhTiepNhanNghiThaiSan
        {
            get
            {
                return _QuyetDinhTiepNhanNghiThaiSan;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhanNghiThaiSan", ref _QuyetDinhTiepNhanNghiThaiSan, value);
            }
        }

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
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
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
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    //
                    TinhTrang = value.TinhTrang;
                }
            }
        }

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Tinh trạng cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TinhTrang TinhTrangMoi
        {
            get
            {
                return _TinhTrangMoi;
            }
            set
            {
                SetPropertyValue("TinhTrangMoi", ref _TinhTrangMoi, value);
            }
        }

        public ChiTietTiepNhanNghiThaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định tiếp nhận nghỉ BHXH"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            TinhTrangMoi = HoSoHelper.DangLamViec(Session);
            UpdateNhanVienList();
        }
        
        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (QuyetDinhTiepNhanNghiThaiSan.QuyetDinhMoi && QuyetDinhTiepNhanNghiThaiSan.TuNgay <= HamDungChung.GetServerTime())
            {
                ThongTinNhanVien.TinhTrang = TinhTrangMoi;
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhTiepNhanNghiThaiSan.QuyetDinhMoi)
            {
                //Chưa có quyết định nghỉ thai sản
                //=> tự sửa bằng tay Tình trạng = 'Nghỉ BHXH' trước khi lập quyết định tiếp nhận
                ThongTinNhanVien.TinhTrang = TinhTrang;
            }

            //xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }

}
