<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebBanSach.Cart" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-4 text-success"><i class="bi bi-basket-fill"></i> SHOPPING CART</h3>
        
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" 
            CssClass="table table-hover align-middle border" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Sản phẩm">
                    <ItemTemplate>
                        <div class="d-flex align-items-center">
                            <img src='<%# "Image/" + Eval("AnhBia") %>' style="width:50px; margin-right:10px;" />
                            <strong><%# Eval("TenSach") %></strong>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Dongia" HeaderText="Đơn giá" DataFormatString="{0:N0} đ" />
                <asp:TemplateField HeaderText="Số lượng">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Soluong") %>' 
                            type="number" CssClass="form-control" Width="70px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Thanhtien" HeaderText="Thành tiền" DataFormatString="{0:N0} đ" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-outline-danger btn-sm">
                            <i class="bi bi-trash"></i> Xóa
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="d-flex justify-content-between align-items-center mt-4 p-3 bg-light border rounded">
            <a href="Default.aspx" class="btn btn-secondary">Tiếp tục mua hàng</a>
            <div>
                <span class="fs-5 me-3">Tổng cộng: <strong class="text-danger"><asp:Label ID="lblTotal" runat="server"></asp:Label> đ</strong></span>
                <asp:Button ID="btnCheckout" runat="server" Text="ĐẶT HÀNG NGAY" CssClass="btn btn-danger btn-lg" OnClick="btnCheckout_Click" />
            </div>
        </div>
    </div>
</asp:Content>