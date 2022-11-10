function Login() {
    var param = {
        "userName": $("#username").val(),
        "passWord": $("#password").val()
    }
    $.ajax({
        url: '/Login/Login',
        dataType: 'html',
        type: 'POST',
        data: param,
        dataType: 'html',
        success: function (data, status, xhr) { 
            if (data == "True") {
                location.href = "/";
            }
            else {
                swal({
                title: "Đăng nhập", text: "Đăng nhập thất bại!", type:
     "error"
            }).then(function () {
            }
 );
            }
            
        },
        error: function (xhr, status, error) {
            alert(status);
        }
    });

}
function logout() {
    $.ajax({
        url: '/Login/Logout',
        type: 'POST',
        dataType: 'html',
        success: function (data, status, xhr) {
            window.location.href = "/Login";
        },
        error: function (xhr, status, error) {
            alert(status);
        }
    });
}



$('input[name="daterange"]').daterangepicker({
    timePicker: true,
    startDate: moment(),
    endDate: moment(),
    timePicker: false, 
    locale: {
        format: 'DD/MM/YYYY',
        minDate: new Date(),

    }
});
function reportData() {

   
    $("#contents").html('');
    //
    var date_range = $("#daterange").val();
    var dates = date_range.split(" - ");
    var currentTime = new Date();
    var start = dates[0];
    var end = dates[1];

    //Trường hợp là nhập kho, trung chuyển với giao nhận
    var permissionId = $("#slPermission").val();  
    if (permissionId == 2 || permissionId == 5 || permissionId == 6) {
        var param = {
            "fromDate": start,
            "toDate": end,
            "UserID": $("#slUser").val()
        }  
        $.ajax({
            url: '/Report/LoadReceivedDataList',
            dataType: 'html',
            type: 'POST',
            data: param,
            dataType: 'html',
            success: function (data, status, xhr) {
                $("#contents").html(data);
            },
            error: function (xhr, status, error) {
                alert(status);
            }
        });
    }
    else {
        var param = {
            "fromDate": start,
            "toDate": end,
            "UserID": $("#slUser").val(),
            "Code": $("#slDelivery option:selected").text()
        }
        $.ajax({
            url: '/Report/LoadTrackingData2',
            dataType: 'html',
            type: 'POST',
            data: param,
            dataType: 'html',
            success: function (data, status, xhr) { 
                $("#contents").html(data);
            },
            error: function (xhr, status, error) {
                alert(status);
            }
        });
    }
   
}

function reportDataTracking() {
    var date_range = $("#daterange").val();
    var dates = date_range.split(" - ");
    var currentTime = new Date();
    var start = dates[0];
    var end = dates[1];

    //

    var param = {
        "fromDate": start,
        "toDate": end,
        "UserID": $("#slUser").val(),
        "Code": $("#slDriver").val()
    }
    $.ajax({
        url: '/Report/LoadTrackingData2',
        dataType: 'html',
        type: 'POST',
        data: param,
        dataType: 'html',
        success: function (data, status, xhr) {
            $("#tbody").html(data); 
            $("#slTai").show(); 
        },
        error: function (xhr, status, error) {
            alert(status);
        }
    });
}

function changeUser() {
    var getitem = $("#slUser").val();
    if (getitem == 1) {
        $("#slTai").show();
    }
    else {
        $("#slTai").hide();
    }
}
 
function ExportExcel()
{ 
    //var user = $("#slUser").val();
    //alert(user);
    //return;
    var table = $("#tbHistory");
    if (table && table.length) {
        var preserveColors = (table.hasClass('table2excel_with_colors') ? true : false);
        $(table).table2excel({
            exclude: ".table",
            name: "Excel Document Name",
            filename: "History" + new Date().toISOString().replace(/[\-\:\.]/g, "") + ".xls",
            fileext: ".xls",
            exclude_img: true,
            exclude_links: true,
            exclude_inputs: true,
            preserveColors: preserveColors
        });
    } 
} 