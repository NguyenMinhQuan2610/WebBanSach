using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebBanSach
{
    public partial class Product : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSachTheoLoai();
            }
        }

        private void LoadSachTheoLoai()
        {
            string maCD = Request.QueryString["MaCD"];
            if (string.IsNullOrEmpty(maCD)) return;

            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = @"SELECT S.*, C.Tenchude 
                               FROM Sach S INNER JOIN ChuDe C ON S.MaCD = C.MaCD 
                               WHERE S.MaCD = @MaCD";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaCD", maCD);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblTenChuDe.Text = "SÁCH THEO CHỦ ĐỀ: " + dt.Rows[0]["Tenchude"].ToString().ToUpper();
                }

                rptSachTheoLoai.DataSource = dt;
                rptSachTheoLoai.DataBind();
            }
        }
    }
}