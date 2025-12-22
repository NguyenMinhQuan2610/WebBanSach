<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebBanSach.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .section-title { background-color: #90ee90; padding: 10px; border-radius: 4px; font-weight: bold; margin-bottom: 20px; }
        .book-card { border: 1px solid #ddd; padding: 10px; text-align: center; height: 100%; }
        .price { color: red; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section-title">
        <asp:Label ID="lblTenChuDe" runat="server" Text="DANH SÁCH SÁCH"></asp:Label>
    </div>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="rptSachTheoLoai" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="book-card shadow-sm">
                        <img src='<%# "Image/" + Eval("AnhBia") %>' style="width:150px; height:200px; object-fit:cover;" />
                        <h6 class="mt-2 text-primary text-truncate"><%# Eval("TenSach") %></h6>
                        <p>Giá bán: <span class="price"><%# Eval("Dongia", "{0:N0}") %> đồng</span></p>
                        <a href='ChiTiet.aspx?MaSach=<%# Eval("MaSach") %>' class="btn btn-success btn-sm">Chi tiết</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>