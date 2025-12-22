<%@ Page Title="Trang Chủ" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebBanSach.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .book-card { transition: transform 0.2s; border: 1px solid #eee; }
        .book-card:hover { transform: translateY(-5px); box-shadow: 0 4px 15px rgba(0,0,0,0.1); }
        .book-title { font-size: 0.95rem; font-weight: bold; color: #007bff; height: 3rem; overflow: hidden; }
        .price-tag { color: #d9534f; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mb-4">
        <div class="col-12">
            <div class="section-title d-flex align-items-center">
                <i class="bi bi-grid-fill me-2"></i> SÁCH MỚI
            </div>
        </div>
    </div>

    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4">
        <asp:Repeater ID="rptSachMoi" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 text-center book-card">
                        <div class="p-3">
                            <img src='<%# "Image/" + Eval("AnhBia") %>' 
                                 class="card-img-top mx-auto" 
                                 style="width: 150px; height: 210px; object-fit: cover;" 
                                 alt='<%# Eval("TenSach") %>'>
                        </div>
                        <div class="card-body d-flex flex-column">
                            <h6 class="book-title"><%# Eval("TenSach") %></h6>
                            <div class="mt-auto">
                                <p class="card-text small mb-2">
                                    Giá bán: <span class="price-tag"><%# Eval("Dongia", "{0:N0}") %> đồng</span>
                                </p>
                                <a href='ChiTietSach.aspx?id=<%# Eval("MaSach") %>' class="btn btn-success btn-sm w-100">
                                    Chi tiết
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>