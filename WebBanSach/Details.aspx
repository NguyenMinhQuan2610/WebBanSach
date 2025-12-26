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
</asp:Content>