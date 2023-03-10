using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiếp nhận nghỉ thai sản")]
    public class QuyetDinhTiepNhanNghiThaiSan : QuyetDinh
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        //private string _LuuTru;
        private bool _QuyetDinhMoi;

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhTiepNhanNghiThaiSan-ListChiTietTiepNhanNghiThaiSan")]
        public XPCollection<ChiTietTiepNhanNghiThaiSan> ListChiTietTiepNhanNghiThaiSan
        {
            get
            {
                return GetCollection<ChiTietTiepNhanNghiThaiSan>("ListChiTietTiepNhanNghiThaiSan");
            }
        }

        public QuyetDinhTiepNhanNghiThaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTiepNhanNghiThaiSan;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietTiepNhanNghiThaiSan item in ListChiTietTiepNhanNghiThaiSan)
                    {
                        item.GiayToHoSo.QuyetDinh = this;
                        item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                        item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                        item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                        item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                        item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietTiepNhanNghiThaiSan.Count == 1)
                {
                    BoPhanText = ListChiTietTiepNhanNghiThaiSan[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietTiepNhanNghiThaiSan[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
    }

}
