$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var soluong = 1;
        var tSoLuong = ($('#quantity_value').text());
        if (tSoLuong != '') {
            soluong = parseInt(tSoLuong);
        }

        $.ajax({
            url: '/GioHang/ThemGioHang',
            type: 'POST',
            data: { id: id, soluong: soluong },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);

                }
            }
        });
    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc muốn xóa sản phẩm này khỏi cửa hàng?');
        if (conf == true) {

        $.ajax({
            url: '/GioHang/Xoa',
            type: 'POST',
            data: { id: id },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.Count);
                    $('#trow_' + id).remove();
                    LoadGioHang();
                }
            }
        });
        }
    });
    $('body').on('click', '.btnSua', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var soluong = $('#soluong_' + id).val();

        Sua(id, soluong);

    });

    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng không?');
        if (conf == true) {
            DeleteAll();
        }

    });
});

function ShowCount() {
    $.ajax({
        url: '/GioHang/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}

function LoadGioHang() {
    $.ajax({
        url: '/GioHang/PartialItemGioHang',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}

function DeleteAll() {
    $.ajax({
        url: '/GioHang/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                LoadGioHang();
            }
        }
    });
}

function Sua(id, soluong) {
    $.ajax({
        url: '/GioHang/Sua',
        type: 'POST',
        data: { id: id, soluong:soluong },
        success: function (rs) {
            if (rs.success) {
                LoadGioHang();
            }
        }
    });
}