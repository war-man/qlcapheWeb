﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qlCaPhe.Models;
using qlCaPhe.App_Start;
using System.Data.Entity;
using qlCaPhe.Models.Business;
using qlCaPhe.App_Start.Cart;
using qlCaPhe.Models.Entities;


namespace qlCaPhe.Controllers
{
    public class NhomTaiKhoanController : Controller
    {
        #region CREATE
        /// <summary>
        /// Hàm vào giao diện tạo mới nhóm tài khoản
        /// </summary>
        /// <returns></returns>
        public ActionResult ntk_TaoMoiNhomTK()
        {
            if (xulyChung.duocTruyCap("201"))
            {
                xulyChung.ghiNhatKyDtb(1, "Tạo mới nhóm tài khoản");
                return View();
            }
            return null;
        }
        /// <summary>
        /// Hàm thực hiện thêm mới nhóm tài khoản vào csdl
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ntk_TaoMoiNhomTK(FormCollection f, nhomTaiKhoan nhomTK)
        {
            if (xulyChung.duocCapNhat("201", "7"))
            {
                string noiDungTB = ""; int kqLuu = 0;
                try
                {
                    qlCaPheEntities db = new qlCaPheEntities();
                    layDuLieuTuView(nhomTK, f);
                    db.nhomTaiKhoans.Add(nhomTK);
                    kqLuu = db.SaveChanges();
                    if (kqLuu > 0)
                    {
                        noiDungTB = createHTML.taoNoiDungThongBao("Nhóm Tài Khoản", xulyDuLieu.traVeKyTuGoc(nhomTK.tenNhom), "ntk_TableNhomTK");
                        xulyChung.ghiNhatKyDtb(2, "Nhóm tài khoản \" " + xulyDuLieu.traVeKyTuGoc(nhomTK.tenNhom) + " \"");
                    }
                }
                catch (Exception ex)
                {
                    noiDungTB = ex.Message;
                    xulyFile.ghiLoi("Class: NhomTaiKhoanController - Function: ntk_TaoMoiNhomTK", ex.Message);
                    this.doDuLieuLenView(nhomTK);
                }

                ViewBag.ThongBao = createHTML.taoThongBaoLuu(noiDungTB);
            }
            return View();

        }
        #endregion
        #region READ
        /// <summary>
        /// Hàm thực hiện tạo giao diện danh sách nhóm tài khoản
        /// </summary>
        /// <returns></returns>
        public ActionResult ntk_TableNhomTK()
        {
            try
            {
                if (xulyChung.duocTruyCap("201"))
                {
                    var nhomTK = new qlCaPheEntities().nhomTaiKhoans.ToList();
                    string htmlTable = "";
                    foreach (nhomTaiKhoan n in nhomTK)
                    {
                        htmlTable += "<tr role=\"row\" class=\"odd\">";
                        htmlTable += "      <td>";
                        htmlTable += xulyDuLieu.traVeKyTuGoc(n.tenNhom);
                        htmlTable += "      </td>";
                        htmlTable += "      <td>" + xulyDuLieu.traVeKyTuGoc(n.dienGiai) + "</td>";
                        htmlTable += "      <td>" + xulyDuLieu.traVeKyTuGoc(n.trangMacDinh) + "</td>";
                        htmlTable += "      <td>";
                        htmlTable += "          <div class=\"btn-group\">";
                        htmlTable += "              <button type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\">";
                        htmlTable += "                  Chức năng <span class=\"caret\"></span>";
                        htmlTable += "              </button>";
                        htmlTable += "              <ul class=\"dropdown-menu\" role=\"menu\">";
                        htmlTable += createTableData.taoNutChinhSua("/NhomTaiKhoan/ntk_ChinhSuaNhomTK", n.maNhomTK.ToString());
                        htmlTable += createTableData.taoNutXoaBo(n.maNhomTK.ToString());
                        htmlTable += "              </ul>";
                        htmlTable += "          </div>";
                        htmlTable += "      </td>";
                        htmlTable += "</tr>";
                    }
                    ViewBag.TableData = htmlTable;
                    ViewBag.ModalXoa = createHTML.taoCanhBaoXoa("Nhóm tài khoản");
                    ViewBag.ScriptAjax = createScriptAjax.scriptAjaxXoaDoiTuong("NhomTaiKhoan/ntk_XoaNhomTaiKhoan?maNhom=");
                    xulyChung.ghiNhatKyDtb(1, "Danh mục nhóm tài khoản");
                    return View(nhomTK);
                }
            }
            catch (Exception ex)
            {
                xulyFile.ghiLoi("Class: NhomTaiKhoanController - Function: ntk_TableNhomTK", ex.Message);
            }
            return View();
        }
        #endregion
        #region UPDATE
        /// <summary>
        /// Hàm tạo giao diện chỉnh sửa nhóm tài khoản
        /// </summary>
        /// <returns></returns>
        public ActionResult ntk_ChinhSuaNhomTK()
        {
            if (xulyChung.duocTruyCap("201"))
            {
                try
                {
                    string urlAction = (string)Session["urlAction"];
                    if (urlAction.Length > 0)
                    {
                        string thamSo = urlAction.Split('|')[1];
                        int maNhomTK = xulyDuLieu.doiChuoiSangInteger(thamSo.Replace("request=", ""));
                        var nhomTK = new qlCaPheEntities().nhomTaiKhoans.First(n => n.maNhomTK == maNhomTK);
                        if (nhomTK != null)
                        {
                            this.doDuLieuLenView(nhomTK);
                            xulyChung.ghiNhatKyDtb(1, "Chỉnh sửa nhóm tài khoản \" " + xulyDuLieu.traVeKyTuGoc(nhomTK.tenNhom) + " \"");
                            return View(nhomTK);
                        }
                    }
                    else
                        throw new Exception("không nhận được tham số");
                }
                catch (Exception ex)
                {
                    xulyFile.ghiLoi("Class: NhomTaiKhoanController - Function: ntk_ChinhSuaNhomTK_Get", ex.Message);
                }
            }
            return RedirectToAction("goTo404PageNotFound", "Home");

        }
        /// <summary>
        /// Hàm thực hiện cập nhật 1 nhóm tài khoản vào CSDL.
        /// </summary>
        /// <param name="maNhom">Mã nhóm tài khoản cần sửa</param>
        /// <param name="f"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ntk_ChinhSuaNhomTK(FormCollection f)
        {
            if (xulyChung.duocCapNhat("201", "7"))
            {
                string ndThongBao = ""; int kqLuu = 0;
                nhomTaiKhoan nhomSua = new nhomTaiKhoan();
                try
                {
                    qlCaPheEntities db = new qlCaPheEntities();
                    int maNhom = xulyDuLieu.doiChuoiSangInteger(f["txtMaNhom"]);
                    nhomSua = db.nhomTaiKhoans.Single(n => n.maNhomTK == maNhom);
                    this.layDuLieuTuView(nhomSua, f);
                    db.Entry(nhomSua).State = EntityState.Modified;
                    kqLuu = db.SaveChanges();
                    if (kqLuu > 0)
                    {
                        xulyChung.ghiNhatKyDtb(4, "Nhóm tài khoản \" " + xulyDuLieu.traVeKyTuGoc(nhomSua.tenNhom) + " \"");
                        return RedirectToAction("ntk_TableNhomTK");
                    }
                }
                catch (Exception ex)
                {
                    ndThongBao = ex.Message;
                    xulyFile.ghiLoi("Class: NhomTaiKhoanController - Function: ntk_ChinhSuaNhomTK_Post", ex.Message);
                    this.doDuLieuLenView(nhomSua);
                }
                ViewBag.ThongBao = createHTML.taoThongBaoLuu(ndThongBao);
            }
            return View();
        }
        #endregion
        #region DELETE
        /// <summary>
        /// Hàm thực hiện xóa 1 nhóm tài khoản khỏi CSDL
        /// </summary>
        /// <param name="maNhom">Mã nhóm tài khoản cần xóa</param>
        /// <returns></returns>
        public void ntk_XoaNhomTaiKhoan(int maNhom)
        {
            try
            {
                int kqLuu = 0;
                qlCaPheEntities db = new qlCaPheEntities();
                var nhomXoa = db.nhomTaiKhoans.First(n => n.maNhomTK == maNhom);
                if (nhomXoa != null)
                {
                    db.nhomTaiKhoans.Remove(nhomXoa);
                    kqLuu = db.SaveChanges();
                    if (kqLuu > 0)
                        xulyChung.ghiNhatKyDtb(3, "Nhóm tài khoản \"" + xulyDuLieu.traVeKyTuGoc(nhomXoa.tenNhom) + " \"");
                }
            }
            catch (Exception ex)
            {
                xulyFile.ghiLoi("Class: NhomTaiKhoanController - Function: ntk_XoaNhomTaiKhoan", ex.Message);
            }
        }
        #endregion
        #region ORTHERS
        /// <summary>
        /// Hàm thực hiện lấy dữ liệu từ giao diện
        /// </summary>
        /// <param name="x"></param>
        private void layDuLieuTuView(nhomTaiKhoan x, FormCollection f)
        {
            string loi = "";
            x.tenNhom = xulyDuLieu.xulyKyTuHTML(f["txtTenNhom"]);
            if (x.tenNhom.Length <= 0)
                loi += "Vui lòng nhập tên nhóm tài khoản <br/>";
            x.dienGiai = xulyDuLieu.xulyKyTuHTML(f["txtDienGiai"]);
            if (x.dienGiai.Length <= 0)
                loi += "Vui lòng nhập thông tin diễn giải cho nhóm tài khoản<br/>";
            x.trangMacDinh = xulyDuLieu.xulyKyTuHTML(f["txtTrangMacDinh"]);
            x.ghiChu = xulyDuLieu.xulyKyTuHTML(f["txtGhiChu"]);
            if (loi.Length > 0)
                throw new Exception(loi);

        }
        /// <summary>
        /// Hàm thực hiện đổ tất cả dữ liệu từ nhomTaiKhoan lên giao diện
        /// </summary>
        /// <param name="x"></param>
        private void doDuLieuLenView(nhomTaiKhoan x)
        {
            ViewBag.txtMaNhom = x.maNhomTK.ToString();
            ViewBag.txtTenNhom = xulyDuLieu.traVeKyTuGoc(x.tenNhom);
            ViewBag.txtDienGiai = xulyDuLieu.traVeKyTuGoc(x.dienGiai);
            ViewBag.txtTrangMacDinh = xulyDuLieu.traVeKyTuGoc(x.trangMacDinh);
            ViewBag.txtGhiChu = xulyDuLieu.traVeKyTuGoc(x.ghiChu);
        }
        /// <summary>
        /// Hàm tạo danh sách chức năng phân quyền
        /// </summary>
        /// <returns></returns>
        public ActionResult ntk_PartListPagePermission()
        {

            return PartialView(new bMenuTools().readAllMenuTools());
        }

        public ActionResult ntk_Script()
        {
            return PartialView();
        }
        /// <summary>
        /// Hàm ajax thêm quyền hạn khi click chọn vào quyền trên vùng quyền hạn
        /// </summary>
        /// <param name="data"></param>
        public void selectPermission(string data)
        {

            //data = "2|201&2/2|201&4/2|201&1/2|202&4/3|301&1/2|201&6";
            //List<QuyenHan> listQuyen = new List<QuyenHan>();
            //foreach (string itemQuyen in data.Split('/')) //------itemQuyen = 2|201&2
            //{
            //    QuyenHan quyen = new QuyenHan();
            //    quyen.IdMenuCha = itemQuyen.Split('|')[0]; //======cha = 2
            //    string valueQuyen = itemQuyen.Split('|')[1]; //-------valueQuyen=201&2
            //    quyen.IdMenuCon = valueQuyen.Split('&')[0];//------con=201
            //    quyen.SoQuyen = xulyDuLieu.doiChuoiSangInteger(valueQuyen.Split('&')[1]);
            //    listQuyen.Add(quyen);
            //}

            //List<QuyenHan> listKq = new List<QuyenHan>();
            //string idConTem = ""; int soQuyen=0;
            //foreach (QuyenHan q in listQuyen)
            //{
            //    if (q.IdMenuCon.Equals(idConTem))//------Nếu trùng id con
            //    {
            //        soQuyen++;
            //    }
            //    var listQuyenGroup = listQuyen.Where(c => c.IdMenuCon.Equals(q.IdMenuCon));

            //}

        }
        #endregion
    }
}