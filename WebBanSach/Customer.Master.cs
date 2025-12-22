using System;
using System.Collections.Generic;
using System.Data; // Thêm thư viện này
using System.Data.SqlClient;
using System.Configuration; // Thêm thư viện này để đọc Web.config
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBanSach
{
    public partial class Customer : System.Web.UI.MasterPage
    {
        string strCon = ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenuChuDe();
            }
        }

        private void LoadMenuChuDe()
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = @"SELECT C.MaCD, C.Tenchude, COUNT(S.MaSach) AS SoLuong 
                               FROM ChuDe C LEFT JOIN Sach S ON C.MaCD = S.MaCD 
                               GROUP BY C.MaCD, C.Tenchude";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptChuDe.DataSource = dt;
                rptChuDe.DataBind();
            }
        }
    }
}