function getalltheloai() {       
    $("#viewContent").html("");

    $("#viewContent").add("<div id='lstData'></div>");
    
    $.ajax({
        url: '@Url.Action("GetAll","Theloai")',
        dataType: 'json',
        success: function (json) {
            lstData = $('#lstData').columns({
                data: json,
                schema: [
                { "header": "Tên", "key": "ten" },
                { "header": "Ngày Tạo", "key": "ngaytao"},
                { "header": "", "key": "action" }
                ]
            });
        }
    });
}