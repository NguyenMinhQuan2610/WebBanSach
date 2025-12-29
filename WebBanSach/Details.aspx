<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebBanSach.Details" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="rptChiTiet" runat="server">
        <ItemTemplate>
            <div class="row mt-4">
                <div class="col-md-4 text-center">
                    <img src='<%# "Image/" + Eval("AnhBia") %>' class="img-fluid shadow" style="max-height: 400px;" />
                </div>
                <div class="col-md-8">
                    <h2 class="text-primary"><%# Eval("TenSach") %></h2>
                    <p><strong>Tác giả:</strong> <%# Eval("TenTG") %></p>
                    <p><strong>Nhà xuất bản:</strong> <%# Eval("TenNXB") %></p>
                    <p><strong>Giá bán:</strong> <span class="text-danger fw-bold fs-4"><%# Eval("Dongia", "{0:N0}") %> đ</span></p>
                    <div class="alert alert-light border">
                        <strong>Mô tả:</strong><br />
                        <%# Eval("Mota") %>
                    </div>
                    <asp:Button ID="btnMua" runat="server" Text="CHỌN MUA" CssClass="btn btn-danger btn-lg" 
                        OnClick="btnMua_Click" CommandArgument='<%# Eval("MaSach") %>' />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <hr />
<div class="row mt-5">
    <div class="col-12">
        <h4 class="text-success border-bottom pb-2">SÁCH CÙNG CHỦ ĐỀ</h4>
    </div>
</div>

<div class="row row-cols-1 row-cols-md-4 g-4 mt-2">
    <asp:Repeater ID="rptSachCungChuDe" runat="server">
        <ItemTemplate>
            <div class="col">
                <div class="card h-100 text-center shadow-sm">
                    <div class="p-2">
                        <img src='<%# "Image/" + Eval("AnhBia") %>' class="card-img-top mx-auto" 
                             style="width: 100px; height: 140px; object-fit: cover;">
                    </div>
                    <div class="card-body p-2">
                        <h6 class="card-title small fw-bold text-truncate"><%# Eval("TenSach") %></h6>
                        <p class="card-text small text-danger fw-bold"><%# Eval("Dongia", "{0:N0}") %> đ</p>
                        <a href='Details.aspx?id=<%# Eval("MaSach") %>' class="btn btn-outline-primary btn-sm w-100">Xem</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
</asp:Content>