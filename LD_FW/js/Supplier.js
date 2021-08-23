var keyValue = $.request("keyValue");
var type = $.request("type");
layui.use(['form', 'laydate', 'table'], function () {
    var $ = layui.$, form = layui.form, laydate = layui.laydate, table = layui.table;
    if (type != null && (type == "2" || type == "3")) {
        $("#SupplierNameText").text("个人姓名");
        $("#BusinessLicenseText").text("身份证号");
    }
    laydate.render({
        elem: '#PeriodTime'
    });
    laydate.render({
        elem: '#EndTime'
    });
    var SelectBankName = [];
    var BankSet = [];
    var selectDefault = [];
    $.ajax({
        url: "/Supplier/GetFormJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.SupplierType >= 2) {
                $("#SupplierNameText").text("个人姓名");
                $("#BusinessLicenseText").text("身份证号");
            }
            $("#form1").formSerialize(data);
            $("#form1").find('div.ckbox label').attr('for', '');
        }
    });

    $.ajax({
        url: "/Supplier/GetSupplierBankJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            for (var i in data.data) {
                BankSet.push({
                    AccountName: data.data[i].AccountName,
                    BankName: data.data[i].BankName,
                    BankBranch: data.data[i].BankBranch,
                    BankAccount: data.data[i].BankAccount,
                    IsDefault: data.data[i].IsDefault,
                });
                if (data.data[i].BankName != null) {
                    SelectBankName.push(data.data[i].BankNameCode);
                }
                if (data.data[i].IsDefault != "") {
                    selectDefault.push(data.data[i].IsDefault);
                }
            }
        }
    });
    var SAPSet = [];
    $.ajax({
        url: "/Supplier/GetSupplierSAPJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            for (var i in data.data) {
                SAPSet.push({
                    SAPCode: data.data[i].SAPCode,
                    SAPName: data.data[i].SAPName
                });
            }
        }
    });

    var ContactsSet = [];
    $.ajax({
        url: "/Supplier/GetSupplierContactsJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            for (var i in data.data) {
                ContactsSet.push({
                    UserName: data.data[i].UserName,
                    Phone: data.data[i].Phone,
                    Mailbox: data.data[i].Mailbox,
                    Position: data.data[i].Position,
                });
            }
        }
    });

    var checkedSet = new Set();
    $.ajax({
        url: "/Supplier/GetSupplierParameterJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            for (k in data.data) {
                checkedSet.add(data.data[k].ParameterID);
            }
        }
    });



    form.on('select(SupplierType)', function (data) {
        console.log(data.value);
        if (data.value >= 2) {
            $("#SupplierNameText").text("个人姓名");
            $("#BusinessLicenseText").text("身份证号");
            type = data.value;
        } else {
            $("#SupplierNameText").text("供方名称");
            $("#BusinessLicenseText").text("统一社会信用代码");
            type = data.value;
        }
    });

    table.render({
        elem: '#LAY-Parameter-list'
        , skin: 'line' //行边框风格
        , even: false //开启隔行背景
        , url: '/Parameter/GetGridJson?ItemCode=0'
        , title: '产品与服务分类'
        , totalRow: true
        , limit: 15
        , id: 'LAY-Parameter-list'
        , cols: [[
            { type: 'checkbox', fixed: 'left', field: 'LAY_CHECKED' }
            , { field: 'ParameterCode', title: '产品与服务分类名称' }
            , { field: 'ParameterName', title: '产品与服务分类编码' }
        ]]
        , page: false
        , parseData: function (res) { //res 即为原始返回的数据
            for (var i in res.data) {
                if (checkedSet.has(res.data[i].pId)) {
                    //如果set集合中有的话，给rows添加check属性选中
                    res.data[i]["LAY_CHECKED"] = true;
                }
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
    });

    table.render({
        elem: '#LAY-Parameter1-list'
        , skin: 'line' //行边框风格
        , even: false //开启隔行背景
        , url: '/Parameter/GetGridJson?ItemCode=1'
        , title: '法人公司数据表'
        , totalRow: true
        , limit: 15
        , id: 'LAY-Parameter1-list'
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'ParameterCode', title: '税目名称' }
            , { field: 'ParameterName', title: '税目编码' }
            , { field: 'F1', title: "税率(%)" }
        ]]
        , page: false
        , parseData: function (res) { //res 即为原始返回的数据
            for (var i in res.data) {
                if (checkedSet.has(res.data[i].pId)) {
                    //如果set集合中有的话，给rows添加check属性选中
                    res.data[i]["LAY_CHECKED"] = true;
                }
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
    });

    table.render({
        elem: '#LAY-Parameter2-list'
        , skin: 'line' //行边框风格
        , even: false //开启隔行背景
        , url: '/Parameter/GetGridJson?ItemCode=2'
        , title: '法人公司数据表'
        , totalRow: true
        , limit: 15
        , id: 'LAY-Parameter2-list'
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'ParameterCode', title: '资质等级名称' }
            , { field: 'ParameterName', title: '资质等级编码' }
            , { field: 'F1', title: "等级" }
        ]]
        , page: false
        , parseData: function (res) { //res 即为原始返回的数据
            for (var i in res.data) {
                if (checkedSet.has(res.data[i].pId)) {
                    //如果set集合中有的话，给rows添加check属性选中
                    res.data[i]["LAY_CHECKED"] = true;
                }
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
    });

    table.render({
        elem: '#LAY-Bank-list'
        , even: false //开启隔行背景
        , title: '银行公司数据表'
        , totalRow: false
        , limit: 15
        , id: 'LAY-Bank-list'
        , cols: [[
            { title: '操作', toolbar: '#barDemo', width: 120 }
            , { field: 'AccountName', title: '户名', edit: 'text', width: 240 }
            , { field: 'BankName', title: '开户行', templet: '#selectDictName', width: 240 }
            , { field: 'BankBranch', title: '开户行支行', edit: 'text', width: 350 }
            , { field: 'BankAccount', title: '开户行账号', edit: 'number', width: 240 }
            , { field: 'IsDefault', title: '是否默认', templet: '#selectDefault', width: 120 }

        ]]
        , data: BankSet
        , done: function (res, curr, count) {
            //下拉框绑定
            $.ajax({
                type: "get",
                url: "/Parameter/GetGridJson",
                dataType: "json",
                data: {
                    page: 1,
                    limit: 20,
                    ItemCode: "3"
                },
                async: false,
                success: function (data) {
                    for (k in data.data) {
                        $("select[name='BankName']").append('<option value="' + data.data[k].ParameterCode + '">' + data.data[k].ParameterName + '</option>');
                    }
                }
            });
            $(".layui-table-body, .layui-table-box, .layui-table-cell").css("overflow", "visible");
            //根据已有的值回填下拉框
            layui.each($("select[name='BankName']"), function (index, item) {
                var elem = $(item);
                elem.val(SelectBankName[index]);
            });

            layui.each($("select[name='IsDefault']"), function (index, item) {
                var elem = $(item);
                elem.val(selectDefault[index]);
            });
            form.render("select");
        }
    });

    form.on("select(SelectBankName)", function (data) {
        SelectBankName.push(data.value);
    });

    form.on("select(SelectIsDefault)", function (data) {
        selectDefault.push(data.value);
    });

    table.on('tool(LAY-Bank-list)', function (obj) {
        var Data = table.cache["LAY-Bank-list"];
        if (obj.event === 'add') {
            var datas = {
                "AccountName": ""
                , "BankName": ""
                , "BankBranch": ""
                , "BankAccount": ""
                , "IsDefault": ""
            }
            Data.push(datas);
        } else if (obj.event === 'del') {
            if (obj.tr.data('index') != 0) {
                Data.splice(obj.tr.data('index'), 1)//根据索引删除当前行
            }
        }
        table.reload("LAY-Bank-list", {
            data: Data
        });
    });

    table.on('edit(LAY-Bank-list)', function (obj) {
        var data = obj.data;
        var field = obj.field;
        if (field == "BankAccount") {
            if (isNaN(data.BankAccount)) {
                layer.msg("开户行账号请输入数字....");
            }
        }
    });

    table.render({
        elem: '#LAY-Contacts-list'
        , even: false //开启隔行背景
        , title: '联系人数据表'
        , totalRow: false
        , id: 'LAY-Contacts-list'
        , cols: [[
            { title: '操作', toolbar: '#barContacts', width: 120 }
            , { field: 'UserName', title: '姓名', edit: 'text' }
            , { field: 'Phone', title: '手机号', edit: 'number' }
            , { field: 'Mailbox', title: '邮箱', edit: 'text' }
            , { field: 'Position', title: '职位', edit: 'text' }
        ]]
        , data: ContactsSet
    });

    table.on('tool(LAY-Contacts-list)', function (obj) {
        var Data = table.cache["LAY-Contacts-list"];
        if (obj.event === 'add') {
            var datas = {
                "UserName": ""
                , "Phone": ""
                , "Mailbox": ""
                , "Position": ""
            }
            Data.push(datas);
        } else if (obj.event === 'del') {
            if (obj.tr.data('index') != 0) {
                Data.splice(obj.tr.data('index'), 1)//根据索引删除当前行
            }
        }
        table.reload("LAY-Contacts-list", {
            data: Data,
        });
    });

    table.on('edit(LAY-Contacts-list)', function (obj) {
        var data = obj.data;
        var field = obj.field;
        if (field == "Phone") {
            var msg = $.validata.validatePhone(data.Phone);
            if (msg.length > 0) {
                layer.msg(msg);
            }
        }
        if (field == "Mailbox") {
            var msg = $.validata.validateMailbox(data.Mailbox);
            if (msg.length > 0) {
                layer.msg(msg);
            }
        }
    });

    table.render({
        elem: '#LAY-SAPCompany-list'
        , even: false //开启隔行背景
        , title: 'SAP数据表'
        , totalRow: false
        , page: false
        , id: 'LAY-SAPCompany-list'
        , cols: [[
            { title: '操作', toolbar: '#barSAPCompany', width: 120 }
            , { field: 'SAPCode', title: 'SAP编号', edit: 'text' }
            , { field: 'SAPName', title: 'SAP名称', edit: 'text' }
        ]]
        , data: SAPSet
    });

    table.on('tool(LAY-SAPCompany-list)', function (obj) {
        var Data = table.cache["LAY-SAPCompany-list"];
        if (obj.event === 'add') {
            var datas = {
                "SAPCode": ""
                , "SAPName": ""
            }
            Data.push(datas);
        } else if (obj.event === 'del') {
            if (obj.tr.data('index') != 0) {
                Data.splice(obj.tr.data('index'), 1)//根据索引删除当前行
            }
        }
        table.reload("LAY-SAPCompany-list", {
            data: Data,
        });
    });

    form.verify({
        validateMoney: function (value) {
            var result = validateMoney(value);
            if (result != "Y") {
                return result;
            }
        }
    });

    function validateMoney(money) {
        var reg = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;
        if (reg.test(money)) {
            return "Y";
        }
        return "请输入正确的金额,且最多两位小数!";
    }

    form.on("submit(component-form-element)", function (data) {
        data.field.SupplierClass = type;
        var Parameterlist = table.checkStatus('LAY-Parameter-list');
        var Parameterlist1 = table.checkStatus('LAY-Parameter1-list');
        var Parameterlist2 = table.checkStatus('LAY-Parameter2-list');
        var arr = [];
        for (var i = 0; i < Parameterlist.data.length; i++) { //循环筛选出id
            arr.push({
                ParameterID: Parameterlist.data[i].pId,
                ParameterType: 0
            });
        }
        for (var i = 0; i < Parameterlist1.data.length; i++) { //循环筛选出id
            arr.push({
                ParameterID: Parameterlist1.data[i].pId,
                ParameterType: 1
            });
        }
        for (var i = 0; i < Parameterlist2.data.length; i++) { //循环筛选出id
            arr.push({
                ParameterID: Parameterlist2.data[i].pId,
                ParameterType: 2
            });
        }
        var Bank = table.cache["LAY-Bank-list"];
        for (v in Bank) {
            Bank[v].BankName = SelectBankName[v];
            Bank[v].IsDefault = selectDefault[v];
        }
        var Contacts = table.cache["LAY-Contacts-list"];
        var SAPCompany = table.cache["LAY-SAPCompany-list"];
        $.ajax({
            url: "/Supplier/SaveSupplierForm?keyValue=" + keyValue,
            data:
            {
                supplierEntity: data.field,
                supplierBanks: Bank,
                supplierSAPs: SAPCompany,
                supplierContacts: Contacts,
                supplierParameters: arr
            },
            type: "post",
            async: true,
            success: function (data) {
                var json = $.parseJSON(data);
                layer.msg(json.message);
                window.setTimeout(function () {
                    window.parent.layui.admin.events.closeThisTabs();
                }, 500);
            }
        });
    });
});
var i = 1;
$(function () {
    $(".applypeve").click(function () {
        i--;
        $("#status li").removeClass("active").eq(i - 1).addClass("active");
        showitem(i);
    });
    $(".applynext").click(function () {
        i++;
        $("#status li").removeClass("active").eq(i - 1).addClass("active");
        showitem(i);
    });
    $(".search_btn").click(function () {
        if ($.validata.validateInput($("#BusinessLicense").val(), $("#BusinessLicenseText").text()).length > 0) {
            layer.msg($.validata.validateInput($("#BusinessLicense").val(), $("#BusinessLicenseText").text()));
            return;
        } else {
            if ($("#BusinessLicenseText").text() == "身份证号") {
                var msg = $.validata.validateIdentity($("#BusinessLicense").val());
                if (msg.length > 0) {
                    layer.msg(msg);
                    return;
                }
            }
            $.ajax({
                url: "/Supplier/GetFormSupplier",
                data: { SupplierName: $("#SupplierName").val(), keyValue: keyValue },
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.SupplierName == null || data.SupplierName.length <= 0) {
                        i++;
                        $("#status li").removeClass("active").eq(i - 1).addClass("active");
                        showitem(i);
                    } else {
                        layer.msg($("#SupplierName").val() + " 已存在...");
                    }
                }
            });

        }
    });
    $("#SupplierName").blur(function () {
        console.log($("#BusinessLicenseText").text());
        if ($("#BusinessLicenseText").text() == "统一社会信用代码") {
            $.ajax({
                url: "/Supplier/GetBasicDetailsByNameJson",
                data: { keyValue: $("#SupplierName").val() },
                dataType: "json",
                type: "get",
                async: false,
                success: function (data) {
                    console.log(data);
                    //if (data.Status == "200") {
                    //    $("#BusinessLicense").val(data.data[0].Value);
                    //} else {
                    //    layui.msg(data.Message);
                    //}
                }
            });
        }
    });
    function showitem(i) {
        debugger;
        console.log(i);
        $(".navpage").css("display", "none");
        $("#item" + i).show();
    }
});