using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

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

        protected int totalPagesCount = 0;

        private void LoadSachMoi()
        {
            int pageSize = 6;
            int curPage = 1;
            if (Request.QueryString["page"] != null)
                curPage = int.Parse(Request.QueryString["page"]);

            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = @"SELECT MaSach, TenSach, Dongia, AnhBia 
                       FROM Sach 
                       ORDER BY Ngaycapnhat DESC 
                       OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                       
                       SELECT COUNT(*) FROM Sach;";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Offset", (curPage - 1) * pageSize);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                rptSachMoi.DataSource = ds.Tables[0];
                rptSachMoi.DataBind();

                int totalRows = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                totalPagesCount = (int)Math.Ceiling((double)totalRows / pageSize);

                TaoPager(totalPagesCount, curPage);
            }
        }
        private void TaoPager(int totalPages, int curPage)
        {
            var pages = new List<object>();
            for (int i = 1; i <= totalPages; i++)
            {
                pages.Add(new { PageIndex = i, PageText = i, Active = (i == curPage) });
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected int GetPreviousPage()
        {
            int curPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : int.Parse(Request.QueryString["page"]);
            return curPage > 1 ? curPage - 1 : 1;
        }

        protected int GetNextPage()
        {
            int curPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : int.Parse(Request.QueryString["page"]);
            return curPage < totalPagesCount ? curPage + 1 : totalPagesCount;
        }

        protected bool IsLastPage()
        {
            int curPage = string.IsNullOrEmpty(Request.QueryString["page"]) ? 1 : int.Parse(Request.QueryString["page"]);
            return curPage >= totalPagesCount;
        }
    }
}