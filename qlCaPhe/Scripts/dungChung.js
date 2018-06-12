﻿$(document).ready(function () {
    //////======Sự kiện lấy và hiển thị danh sách thông báo
    setInterval(function() {
        $.ajax({
            url: getDuongDan() + 'Home/Notifications',
        }).success(function(data) {
            $('#vungThongBao').html(data);
        });
    }, 1000);


    xoaTatCaThongBao();
});

function getDuongDan() {
    var kq = window.location.protocol + '//' + window.location.host;
    return kq + '/';
}

//Thực hiện mở file hình và gán hình vào img có id la hinhDD
function OpenFileBrowser(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#hinhDD')
                .attr('src', e.target.result)
                .width(150)
                .height(200);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
//Thực hiện mở file hình cho view bài viết với độ dài hình lớn hơn
function OpenFileBrowserBaiViet(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#hinhDD')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
//----Thực hiện trở về trang trước cho button Trở về trên form
function troVeTrangTruoc() {
    window.history.back();
}
//-----------Hàm ajax xóa thông báo
function xoaTatCaThongBao() {
    $("#vungThongBao").on("click", "#jsBtnXoaThongBao", function (e) {
        $.ajax({
            url: getDuongDan() + 'Home/AjaxXoaTatCaThongBao',
            beforeSend: function () {
                $('#jsBtnXoaThongBao').unbind("click");
                //---Sự kiện unbind không click vào item nữa
                e.preventDefault();
                e.stopImmediatePropagation();
            },
            success: function (data, textStatus, xhr) {
                xoaTatCaThongBao();
            },
            error: function (xhr, textStatus, errorThrown) {
                xuLyNghiepVuBan();
            }
        });
    });
}

//-------Hàm in mở chức năng in 1 element trên giao diện
function printContent(el) {
    var restorepage = $('body').html();
    var printcontent = $('#' + el).clone();
    $('body').empty().html(printcontent);
    window.print();
    $('body').html(restorepage);
}


//-------HÀM ĐỌC CÁC REQUEST TRUYỀN VÀO CÓ TRÊN URL
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
//-----Hàm lấy tham số trang hiện tại có trên url
function getCurrentPageInRequest() {
    var res = getUrlVars()["page"];
    if (typeof (res) === 'undefined')
        return 1;
    return res;
}

//-----Hàm hiện thông báo Toast
function showToastNotify(element) {
    $(element).fadeIn("slow").delay(5000);//--------Hiện thông báo trong 5s
    $(element).slideUp(300).fadeOut("slow");
}