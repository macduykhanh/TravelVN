const tabItems = document.querySelectorAll(".tab-item");
const tabContentItems = document.querySelectorAll(".tab-content-item");
function NotifyLogin() {
    alert("Login Account");
}


$(document).ready(function () {
    alert($("#tonggiatien").val())
});

function removeShow() {
    tabContentItems.forEach(item => item.classList.remove("show"));
}
function removeBorder() {
    tabItems.forEach(item => item.classList.remove("tab-border"));
}

//tabItems.forEach(item => item.addEventListener("click",selectItem));
function insertCustomer(a1, a2, a3, a4, a5, a6, a7, a8, a9) {
    var x = document.getElementById("tablecustomers").rows.length;
    document.getElementById("tablecustomers").insertRow(-1).innerHTML = '<tr><td>' + x + '</td><td><input type="text"class="fullname"></td><td><input type="date" class="dateofb" id="dateofb' + x + '" onchange="CapNhatTuoi(event,' + x + ',' + a1 + ',' + a2 + ',' + a3 + ',' + a4 + ',' + a5 + ',' + a6 + ',' + a7 + ',' + a8 + ',' + a9 + ')"></td><td><input type="text" class="adr"></td><td><select name="sex" class="sex"><option value="Male">Male</option><option value=">Female">Female</option></select></td><td><select name="typecustomer" class="typecustomer" id="loai' + x + '" onchange="CapNhatLoai(event,' + x + ',' + a1 + ',' + a2 + ',' + a3 + ',' + a4 + ',' + a5 + ',' + a6 + ',' + a7 + ',' + a8 + ',' + a9 + ')"><option value="VietNamese">VietNamese</option><option value="Overseas Vietnamese">Overseas Vietnamese</option><option value="Foreigner">Foreigner</option></select></td><td><div class="age" id="age' + x + '"></div></td><td><div class="pri" id="pri' + x + '"></div></td></tr><td><button class="trash fas fa-trash"onclick="deletecustomer(this)"></button></td>';
    DayformatBirth()
    TongGiaTien()
}
function deletecustomer(x) {
    var i = x.parentNode.parentNode.rowIndex;
    document.getElementById("tablecustomers").deleteRow(i);
    var l = document.getElementById("tablecustomers").rows.length;
    for (var t = 1; t < l; t++) {
        document.getElementById("tablecustomers").rows[t].cells[0].innerHTML = t;
    }
}
//function getDaycheckout() {
//    	$.ajax({
//		type: 'GET',
//		data: { mavung: index },
//		url: 'ListDesByArea',
//		success: function (result) {
//			$('#lisdesbyarea').html(result);
//		},
//		error: function (e) {
//			alert(e.responseText);
//		}
//	});
//}

function CapNhatTuoi(e, index, GiaVN_NguoiLon, GiaVN_TreEm, GiaVN_EmBe, GiaVK_NguoiLon, GiaVK_TreEm, GiaVK_EmBe, GiaNC_NguoiLon, GiaNC_TreEm, GiaNC_EmBe) {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    var Cdate = new Date(today)
    var Bdate = new Date(e.target.value)
    var diff = Cdate - Bdate;
    var diffdays = diff / 1000 / (60 * 60 * 24);
    var age = Math.floor(diffdays / 365.25);
    document.getElementById("age" + index).innerHTML = age;
    var loai = $('#loai' + index + ' option:selected').val();
    var n1 = loai.localeCompare("VietNamese");
    var n2 = loai.localeCompare("Overseas Vietnamese");
    var n3 = loai.localeCompare("Foreigner");
    var price = 0;
    if (n1 == 0 && age < 5) {
        price = GiaVN_EmBe
    }
    else if (n1 == 0 && age >= 5 && age <= 8) {
        price = GiaVN_TreEm
    }
    else if (n1 == 0 && age >= 9) {
        price = GiaVN_NguoiLon
    }

    if (n2 == 0 && age < 5) {
        price = GiaVK_EmBe
    }
    else if (n2 == 0 && age >= 5 && age <= 8) {
        price = GiaVK_TreEm
    }
    else if (n2 == 0 && age >= 9) {
        price = GiaVK_NguoiLon
    }

    if (n3 == 0 && age < 5) {
        price = GiaNC_EmBe
    }
    else if (n3 == 0 && age >= 5 && age <= 8) {
        price = GiaNC_TreEm
    }
    else if (n3 == 0 && age >= 9) {
        price = GiaNC_NguoiLon
    }
    document.getElementById("pri" + index).innerHTML = price;
    document.getElementById("pri" + index).value = price;
    debugger
    TongGiaTien()
}
function CapNhatLoai(e, index, GiaVN_NguoiLon, GiaVN_TreEm, GiaVN_EmBe, GiaVK_NguoiLon, GiaVK_TreEm, GiaVK_EmBe, GiaNC_NguoiLon, GiaNC_TreEm, GiaNC_EmBe) {
    var dateselect = $('#dateofb' + index).val()
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    var Cdate = new Date(today)
    var Bdate = new Date(dateselect)
    var diff = Cdate - Bdate;
    var diffdays = diff / 1000 / (60 * 60 * 24);
    var age = Math.floor(diffdays / 365.25);
    var loai = e.target.value
    var n1 = loai.localeCompare("VietNamese");
    var n2 = loai.localeCompare("Overseas Vietnamese");
    var n3 = loai.localeCompare("Foreigner");
    var price = 0;
    if (n1 == 0 && age < 5) {
        price = GiaVN_EmBe
    }
    else if (n1 == 0 && age >= 5 && age <= 8) {
        price = GiaVN_TreEm
    }
    else if (n1 == 0 && age >= 9) {
        price = GiaVN_NguoiLon
    }

    if (n2 == 0 && age < 5) {
        price = GiaVK_EmBe
    }
    else if (n2 == 0 && age >= 5 && age <= 8) {
        price = GiaVK_TreEm
    }
    else if (n2 == 0 && age >= 9) {
        price = GiaVK_NguoiLon
    }

    if (n3 == 0 && age < 5) {
        price = GiaNC_EmBe
    }
    else if (n3 == 0 && age >= 5 && age <= 8) {
        price = GiaNC_TreEm
    }
    else if (n3 == 0 && age >= 9) {
        price = GiaNC_NguoiLon
    }
    document.getElementById("pri" + index).innerHTML = price;
    document.getElementById("pri" + index).value = price;
    TongGiaTien()
}
function DayformatCheckIn() {

    var today = new Date().toISOString().split('T')[0];
    document.getElementsByName("ngaycheckin")[0].setAttribute('min', today);
}
function DayformatBirth() {
    var today = new Date().toISOString().split('T')[0];
    var list = document.getElementsByClassName("dateofb");
    Array.prototype.forEach.call(list, function (el) {
        el.setAttribute('max', today);
    });
}
DayformatCheckIn()
DayformatBirth()
function TongGiaTien() {
    var tongtien = 0;
    var list = document.getElementsByClassName("pri");
    for (var i = 0; i < list.length; i++) {
        if (typeof (list[i].value) == 'undefined') { list[i].value = 0 }
    }
    for (var i = 0; i < list.length; i++) {
        tongtien += list[i].value;
    }
    debugger
    document.getElementById("tonggiatien").value = tongtien;
    document.getElementById("tonggiatien").innerHTML = tongtien + ' $';
}

function goStep(ngaycheck, diadiemdon, ten, diachi, sdt, email, thoigianbook, tien) {
    setTimeout(function () {
        removeBorder();
        removeShow();
        const tabitem = document.querySelector("#tab-" + 3);
        tabitem.classList.add("tab-border")
        const tabContentItem = document.querySelector("#tab-" + 3 + "-content");
        tabContentItem.classList.add('show');
        $("#ngaycheckin2").html(ngaycheck);
        $("#diadiemdon").html(diadiemdon);
        $("#fullname").html(ten);
        $("#diachi2").html(diachi);
        $("#sdt2").html(sdt);
        $("#email2").html(email);
        $("#tgbook").html(thoigianbook);
        $("#tien2").html(tien);
    }, 4000);
}
//function Kichhoat(list) {

//}

function Update_PhieuTour() {
    var _maphieutour = $("#maphieutour").val();
    var _manlh = $("#manlh").val();
    var _tennlh = $('#tennlh').val();
    var _emailnlh = $('#emailnlh').val();
    var _diachinlh = $('#diachinlh').val();
    var _dtnlh = $('#dtnlh').val();
    var _notenlh = $('#notenlh').val();

    var lisNameKH = document.getElementsByClassName("fullname")
    var lisNSKH = document.getElementsByClassName("dateofb")
    var lisAdrKH = document.getElementsByClassName("adr")
    var lisTypeKH = document.getElementsByClassName("typecustomer")
    var lisSexKH = document.getElementsByClassName('sex')
    var listnamekh = []
    var listnskh = []
    var listadrkh = []
    var listsexkh = []
    var listtypekh = []
    for (i = 0; i < lisNameKH.length; i++) {
        if (lisNameKH[i].value != '' && lisNSKH[i].value != 'undefined' && lisAdrKH[i].value != '') {
            listnamekh.push(lisNameKH[i].value)
            listnskh.push(lisNSKH[i].value)
            listadrkh.push(lisAdrKH[i].value)
            listtypekh.push(lisTypeKH[i].value)
            listsexkh.push(lisSexKH[i].value)
        }
    }
    var Matour = document.getElementById('_matour').getAttribute('value');
    var Ngaycheckin = $("#_ngaycheckin").val()
    var Pickupplace = $("#_pickupplace").val()
    debugger


    var Tonggia = $(".tonggiatien").val()




    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    debugger
    today = mm + '/' + dd + '/' + yyyy;
    if (Ngaycheckin != '' && Pickupplace != '' && _tennlh != '' && _diachinlh != '' && _dtnlh != '' && _emailnlh != '' && today != '' && Tonggia != '' && listnamekh.length != 0 && listnskh.length != 0 && listadrkh.length != 0 && listsexkh.length != 0 && listtypekh.length != 0) {
        var form = new FormData();
        form.append('ma', _manlh);
        form.append('ten', _tennlh);
        form.append('email', _emailnlh);
        form.append('diachi', _diachinlh);
        form.append('dienthoai', _dtnlh);
        form.append('note', _notenlh);
        form.append('mangten', JSON.stringify(listnamekh));
        form.append('mangdiachi', JSON.stringify(listadrkh));
        form.append('mangloai', JSON.stringify(listtypekh));
        form.append('manggt', JSON.stringify(listsexkh));
        form.append('mangngay', JSON.stringify(listnskh));
        form.append('soluong', lisNameKH.length);
        form.append('maphieutour', _maphieutour);
        form.append('pickupplace', Pickupplace);
        form.append('matour', Matour);
        form.append('gia', Tonggia);
        var xhr = new XMLHttpRequest();
        xhr.open("POST", domain + '/BillTour/CapnhatTTNLH', true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var content = xhr.responseText;
                var json = JSON.parse(content);

                if (json.Data.status == "OK") {

                    window.location = '/Admin/BillTour/Index';

                }
                else {
                    alert("Failure!")
                    //window.location = '/DangNhap/Index';
                }

            }
        }
        xhr.send(form);
    }
    else {
        alert('You have not entered enough information')
        location.reload(true);
    }
}
