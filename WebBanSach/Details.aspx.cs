using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebBanSach
{
    public partial class Details : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDetails();
            }
        }

        private void LoadDetails()
        {
            string maSach = Request.QueryString["id"];
            if (string.IsNullOrEmpty(maSach)) return;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = @"SELECT S.*, N.TenNXB, T.TenTG 
                               FROM Sach S 
                               JOIN NhaXuatBan N ON S.MaNXB = N.MaNXB
                               LEFT JOIN VietSach VS ON S.MaSach = VS.MaSach
                               LEFT JOIN TacGia T ON VS.MaTG = T.MaTG
                               WHERE S.MaSach = @MaSach";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptChiTiet.DataSource = dt;
                rptChiTiet.DataBind();
            }
        }


        protected void btnMua_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            List<CartItem> gioHang = Session["GioHang"] as List<CartItem> ?? new List<CartItem>();

            CartItem tonTai = gioHang.FirstOrDefault(x => x.MaSach == id);

            if (tonTai != null)
            {
                tonTai.Soluong++;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    string sql = "SELECT MaSach, TenSach, AnhBia, Dongia FROM Sach WHERE MaSach = @MaSach";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@MaSach", id);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        gioHang.Add(new CartItem
                        {
                            MaSach = int.Parse(dr["MaSach"].ToString()),
                            TenSach = dr["TenSach"].ToString(),
                            AnhBia = dr["AnhBia"].ToString(),
                            Dongia = double.Parse(dr["Dongia"].ToString()),
                            Soluong = 1
                        });
                    }
                    con.Close();
                }
            }

            Session["GioHang"] = gioHang;
            Response.Redirect("Cart.aspx");
        }
    }
}