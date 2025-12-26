using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; // Thêm cái này
using System.Configuration; // Thêm cái này

namespace WebBanSach
{
    public partial class Cart : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            List<CartItem> gioHang = Session["GioHang"] as List<CartItem>;
            if (gioHang != null && gioHang.Count > 0)
            {
                gvCart.DataSource = gioHang;
                gvCart.DataBind();
                double tongTien = gioHang.Sum(x => x.Thanhtien);
                lblTotal.Text = tongTien.ToString("N0");
            }
            else
            {
                gvCart.DataSource = null;
                gvCart.DataBind();
                lblTotal.Text = "0";
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            List<CartItem> gioHang = Session["GioHang"] as List<CartItem>;
            if (gioHang == null || gioHang.Count == 0) return;

            if (Session["TaiKhoan"] == null)
            {
                Response.Write("<script>alert('Vui lòng đăng nhập để đặt hàng!'); window.location='Login.aspx';</script>");
                return;
            }


            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    string sqlDH = "INSERT INTO DonDatHang (MaKH, NgayDH, Dagiao) VALUES (@MaKH, GETDATE(), 0); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdDH = new SqlCommand(sqlDH, con, trans);
                    cmdDH.Parameters.AddWithValue("@MaKH", 1); 
                    int soDH = Convert.ToInt32(cmdDH.ExecuteScalar());

                    foreach (var item in gioHang)
                    {
                        string sqlCT = "INSERT INTO CTDatHang (SoDH, MaSach, Soluong, Dongia) VALUES (@SoDH, @MaSach, @Soluong, @Dongia)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, con, trans);
                        cmdCT.Parameters.AddWithValue("@SoDH", soDH);
                        cmdCT.Parameters.AddWithValue("@MaSach", item.MaSach);
                        cmdCT.Parameters.AddWithValue("@Soluong", item.Soluong);
                        cmdCT.Parameters.AddWithValue("@Dongia", item.Dongia);
                        cmdCT.ExecuteNonQuery();
                    }

                    trans.Commit();
                    Session["GioHang"] = null;
                    Response.Write("<script>alert('Đặt hàng thành công!'); window.location='Default.aspx';</script>");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Response.Write("Lỗi đặt hàng: " + ex.Message);
                }
            }
        }
    }

    [Serializable]
    public class CartItem
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string AnhBia { get; set; }
        public double Dongia { get; set; }
        public int Soluong { get; set; }
        public double Thanhtien => Soluong * Dongia;
    }
}