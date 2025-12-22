using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebBanSach
{
    public partial class Default : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSachMoi();
            }
        }

        private void LoadSachMoi()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = "SELECT TOP 6 MaSach, TenSach, Dongia, AnhBia FROM Sach ORDER BY Ngaycapnhat DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    rptSachMoi.DataSource = dt;
                    rptSachMoi.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("Lỗi kết nối dữ liệu: " + ex.Message);
                }
            }
        }
    }
}