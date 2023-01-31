﻿using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.XuLyMailMerge.XuLy
{
    public class MailMerge_QuyetDinhTamDungGiaiQuyetCheDo : IMailMerge<IList<QuyetDinhTamDungGiaiQuyetCheDo>>
    {
        public void Merge(DevExpress.ExpressApp.IObjectSpace obs, IList<QuyetDinhTamDungGiaiQuyetCheDo> qdList)
        {
            var list = new List<Non_QuyetDinhTamDungGiaiQuyetCheDo>();
            Non_QuyetDinhTamDungGiaiQuyetCheDo qd;
            foreach (QuyetDinhTamDungGiaiQuyetCheDo quyetDinh in qdList)
            {
                qd = new Non_QuyetDinhTamDungGiaiQuyetCheDo();
                qd.Oid = quyetDinh.Oid.ToString();
                qd.DonViChuQuan = quyetDinh.ThongTinTruong != null ? quyetDinh.ThongTinTruong.DonViChuQuan : "BỘ GIÁO DỤC VÀ ĐÀO TẠO";
                qd.TenTruongVietHoa = quyetDinh.TenCoQuan.ToUpper();
                qd.TenTruongVietThuong = quyetDinh.TenCoQuan;
                qd.TenTruongVietTat = quyetDinh.ThongTinTruong.TenVietTat;
                qd.SoQuyetDinh = quyetDinh.SoQuyetDinh;
                qd.SoPhieuTrinh = quyetDinh.SoPhieuTrinh;
                qd.NgayQuyetDinh = quyetDinh.NgayQuyetDinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayQuyetDinhDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                if (TruongConfig.MaTruong.Equals("NEU") && quyetDinh.NgayHieuLuc == DateTime.MinValue)
                {
                    qd.NgayHieuLuc = quyetDinh.NgayQuyetDinh.ToString("'ngày  tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayQuyetDinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    qd.NgayHieuLuc = quyetDinh.NgayHieuLuc.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                    qd.NgayHieuLucDate = quyetDinh.NgayHieuLuc.ToString("dd/MM/yyyy");
                }
                qd.NgayHop = quyetDinh.NgayHop.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgayKy=DateTime.Now.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.CanCu = quyetDinh.CanCu;
                qd.NoiDung = quyetDinh.NoiDung;
                qd.ChucVuNguoiKy = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu.ToUpper() : "";
                qd.ChucVuNguoiKyVietThuong = quyetDinh.ChucVuNguoiKy != null ? quyetDinh.ChucVuNguoiKy.TenChucVu : "";
                qd.ChucDanhNguoiKy = HamDungChung.GetChucDanhNguoiKy(quyetDinh.NguoiKy);
                qd.NguoiKy = quyetDinh.NguoiKy1;
                if (TruongConfig.MaTruong == "QNU")
                {
                    qd.ChucDanh = quyetDinh.ThongTinNhanVien.ChucDanh != null ? quyetDinh.ThongTinNhanVien.ChucDanh.TenChucDanh : "";
                }
                else
                {
                    qd.ChucDanh = HamDungChung.GetChucDanh(quyetDinh.ThongTinNhanVien);
                    qd.ChucDanhVietThuong = HamDungChung.GetChucDanhVietThuong(quyetDinh.ThongTinNhanVien);
                }
                qd.DanhXungVietHoa = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông" : "Bà";
                qd.DanhXungVietThuong = quyetDinh.ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "ông" : "bà";
                qd.NhanVien = quyetDinh.ThongTinNhanVien.HoTen;
                qd.DonVi = HamDungChung.GetTenBoPhan(quyetDinh.BoPhan);
                qd.MaDonVi = quyetDinh.BoPhan.MaQuanLy;
                qd.TenVietTatDonVi = quyetDinh.BoPhan.TenBoPhanVietTat;
                qd.LoaiNhanVien = quyetDinh.ThongTinNhanVien.LoaiNhanSu != null ? quyetDinh.ThongTinNhanVien.LoaiNhanSu.TenLoaiNhanSu : "";
                qd.NgaySinh =quyetDinh.ThongTinNhanVien.NgaySinh.ToString("'ngày' dd 'tháng' MM 'năm' yyyy");
                qd.NgaySinhDate = quyetDinh.ThongTinNhanVien.NgaySinh.ToString("d");
                qd.NoiSinh = quyetDinh.ThongTinNhanVien.NoiSinh.ToString();
                qd.DiaChiThuongTru = quyetDinh.ThongTinNhanVien.DiaChiThuongTru.ToString();
                qd.ChucVu = quyetDinh.ThongTinNhanVien.ChucVu != null ? quyetDinh.ThongTinNhanVien.ChucVu.TenChucVu : "";
                qd.ChucVuNhanVienVietThuong = HamDungChung.GetChucVuNhanVien(quyetDinh.ThongTinNhanVien).ToLower();
                qd.ChucVuNhanVienVietHoa = HamDungChung.GetChucVuNhanVien(quyetDinh.ThongTinNhanVien);
                qd.TuNgay = quyetDinh.TuNgay != DateTime.MinValue ? quyetDinh.TuNgay.ToString("d") : "";
                qd.LyDo = quyetDinh.LyDo.ToLower();

                list.Add(qd);
            }

            MailMergeTemplate merge = HamDungChung.GetTemplate(obs, "QuyetDinhTamDungGiaiQuyetCheDo.rtf");
            if (merge != null)
                MailMergeHelper.ShowEditor<Non_QuyetDinhTamDungGiaiQuyetCheDo>(list, obs, merge);
            else
                HamDungChung.ShowWarningMessage("Không tìm thấy mấu in QĐ tạm dừng giải quyết các chế độ trong hệ thống.");
        }
    }
}
