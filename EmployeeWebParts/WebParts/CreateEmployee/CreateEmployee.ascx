<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateEmployee.ascx.cs" Inherits="EmployeeWebParts.WebParts.CreateEmployee.CreateEmployee" %>

<link type="text/css" rel="stylesheet" href="/Style%20Library/CSS/bootstrap.min.css" />
<script src="/Style Library/Scripts/jquery-3.5.1.min.js"></script>



<div class="container">

    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="txtName" id="lblName">Name</label>
        </div>
        <div class="col-md-6 col-sm-6">
            <asp:TextBox ID="txtName" runat="server" MaxLength="100" EnableViewState="false"></asp:TextBox>

            <%--            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ID="reqName"
                  ErrorMessage="Please enter Name"></asp:RequiredFieldValidator>

            <asp:RangeValidator runat="server" 
                ID="rangVldName" ErrorMessage="Range" ControlToValidate="txtName" MaximumValue="100" MinimumValue="20">

            </asp:RangeValidator>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="txtBirthdate" id="lblBirthdate">Birthdate</label>
        </div>
        <div class="col-md-6 col-sm-6">
            <asp:TextBox ID="txtBirthdate" TextMode="Date" runat="server" EnableViewState="false"></asp:TextBox>

            <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBirthdate" ID="reqtxtBirthdate"
                  ErrorMessage="Please enter Birthdate"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="rbGender" id="lblGender">Gender</label>
        </div>
        <div class="col-md-6 col-sm-6">
            <asp:DropDownList runat="server" ID="rbGender" EnableViewState="false">
            </asp:DropDownList>

            <%-- <asp:RequiredFieldValidator runat="server" ControlToValidate="rbGender" ID="reqVldNName"
                  ErrorMessage="Please enter Gender"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="txtSalary" id="lblSalary">Salary</label>
        </div>
        <div class="col-md-6 col-sm-6">
            <asp:TextBox TextMode="Number" runat="server" ID="txtSalary" EnableViewState="false"></asp:TextBox>

            <%--            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSalary" ID="reqSalary"
                  ErrorMessage="Please enter Gender"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="chDepartment" id="lblDepartment">Department</label>
        </div>
        <div class="col-md-6 col-sm-6">
            <asp:DropDownList runat="server" ID="chDepartment" EnableViewState="false">
            </asp:DropDownList>

            <%--            <asp:RequiredFieldValidator runat="server" ControlToValidate="chDepartment" ID="reqDepartment"
                  ErrorMessage="Please enter Gender"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="txtProfileURL " id="lblProfileURL">Profile URL</label>
        </div>
        <div class="col-md-4 col-sm-4">
            <asp:TextBox runat="server" ID="txtProfileURL"></asp:TextBox>
        </div>
        <div class="col-md-3 col-sm-3">
            <asp:TextBox TextMode="MultiLine" runat="server" placeholder="Description" ID="txtProfileURLDes" EnableViewState="false"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3">
            <label for="txtBriefofExp " id="lbltxtBriefofExp">Brief of experience</label>
        </div>
        <div class="col-md-6 col-sm-6">
            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtBriefofExp" EnableViewState="false"></asp:TextBox>
            <%-- <asp:RangeValidator runat="server" 
                ID="rangTxtBriefofExp" ControlToValidate="txtBriefofExp" MaximumValue="1000" >

            </asp:RangeValidator>--%>
        </div>
    </div>

    <div class="row">

        <div class="col-md-6 col-sm-6" style="align-content: center">
            <asp:Button ID="btnSave" CssClass="btn btn-button" EnableViewState="false" Text="Create" runat="server" OnClick="btnSave_Click" OnClientClick="checkPermission(); return false;" />
        </div>
    </div>
    <%-- <asp:Label  runat="server" ID="lblErrorMessage" Visible="false" ></asp:Label>--%>
    <label id="lblErrorMessage"></label>
    <%--<asp:ValidationSummary ID="vldSummary" runat="server" 
      DisplayMode ="BulletList" ShowSummary ="true" HeaderText="Errors:" />--%>
</div>


<script type="text/javascript">  
    'use strict';
    $(document).ready(function () {
      
        $.ajax({
            url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('Employees')/fields?$filter=EntityPropertyName eq 'EmployeeDepartment'",
            type: "GET",
            headers: {
                "accept": "application/json;odata=verbose",
            },
            success: function (data) {
                console.log(data.d.results[0].Choices.results);
                var depChoices = data.d.results[0].Choices.results;
                for (var dep of depChoices) {
                    console.log(dep);
                    $("select[id*='chDepartment']").append(`<option Value="${dep}">${dep}</option>`);
                }
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });

        $.ajax({
            url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('Employees')/fields?$filter=EntityPropertyName eq 'EmployeeGender'",
            type: "GET",
            headers: {
                "accept": "application/json;odata=verbose",
            },
            success: function (data) {
                console.log(data.d.results[0].Choices.results);
                var genderChoices = data.d.results[0].Choices.results;
                for (var gender of genderChoices) {
                    console.log(gender);
                    $("select[id*='rbGender']").append(`<option Value="${gender}">${gender}</option>`);
                }
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });
        

    });


    var allowAdd = false;
    var listTitle = "Employees";

    function CreateListItem() {
        // validate Input
        var valid = true;
        var txtError = $("#lblErrorMessage");
        txtError.val("");
        var textName = $("input[id*='txtName']").val();
        var birthDate = $("input[id *= 'txtBirthdate']").val();
        var txtSalary = $("input[id *= 'txtSalary']").val();
        var txtBriefofExp = $("textarea[id *= 'txtBriefofExp']").val();
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

        if (textName.length < 20 || textName.length > 100) {
            txtError.append("Name Length between 20 & 100 ").show();
            valid = false;
        }
        if (new Date(birthDate) > new Date() || birthDate === "") {
            txtError.append(" Date is inValid");
            valid = false;
        }
        if (txtSalary === null || txtSalary === undefined) {
            txtError.append(" Salary required");
            valid = false;
        }
        if (txtBriefofExp === null || txtBriefofExp === undefined || txtBriefofExp.length > 200) {
            txtError.append(" Brief  required");
            valid = false;
        }
        if (!valid) {
            txtError.show();
        }

        var siteURL = _spPageContextInfo.webAbsoluteUrl;
        console.log("from top nav - " + siteURL);
        var apiPath = siteURL + "/_api/lists/getbytitle('Employee')/items";


        if (valid) {
            var item = {
                "__metadata": {
                    "type": "SP.Data.EmployeeListItem"
                },
                "Title": $("input[id*='txtName']").val(),
                "EmployeeName": $("input[id *= 'txtName']").val(),
                "EmployeeBirthdate": $("input[id *= 'txtBirthdate']").val(),
                "BriefOfExperience": $("textarea[id *= 'txtBriefofExp']").val(),
                "EmployeeGender": $("select[id*='rbGender'] :selected").val(),
                "EmployeeSalary": parseInt($("input[id *= 'txtSalary']").val()),
                "EmployeeDepartment": $("select[id*='chDepartment'] :selected").val(),
                "ProfileURL": {
                    '__metadata': { 'type': 'SP.FieldUrlValue' },
                    'Description': $("textarea[id *= 'txtProfileURLDes']").val(),
                    'Url': $("input[id *= 'txtProfileURL']").val()
                }
            };
            siteURL = _spPageContextInfo.webAbsoluteUrl;
            console.log("from top nav - " + siteURL);
            var apiPath = siteURL + "/_api/lists/getbytitle('Employees')/items";
            $.ajax({
                url: apiPath,
                type: "POST",
                headers: {
                    "accept": "application/json;odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                    "content-Type": "application/json;odata=verbose"
                },
                data: JSON.stringify(item),
                //"{__metadata:{'type':'SP.Data.EmployeesListItem'},EmployeeName: " + $("input[id*='txtName']").val() + ",Title: " + $("input[id*='txtName']").val() + ",EmployeeBirthdate: " + $("input[id*='txtBirthdate']").val() + ",BriefOfExperience:" + $("input[id*='txtBriefofExp']").val() + ",Gender:Male,Salary: " + $("input[id*='txtSalary']").val() + ",EmployeeDepartment:HR,ProfileURL:http://nsasa  } ",
                async: false,
                success: function (data) {
                    console.log(data.d.results);
                    var r = confirm("Successful Message!");
                    if (r == true) {
                        window.location.reload();
                    }
                }, eror: function (data) {
                    console.log("An error occurred. Please try again.");
                }
            });
        }

    }



    // check permoissions forUser
    function getListUserEffectivePermissions() {
        var endpointUrl = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listTitle + "')/getusereffectivepermissions(@u)?@u='" + encodeURIComponent(_spPageContextInfo.userLoginName) + "'";
        return $.getJSON(endpointUrl);
    }

    function parseBasePermissions(value) {
        var permissions = new SP.BasePermissions();
        permissions.initPropertiesFromJson(value);
        allowAdd = permissions.has(SP.PermissionKind.addListItems);
        if (allowAdd)
            CreateListItem();
        console.log(allowAdd);
    }


    function checkPermission() {
        debugger;
        getListUserEffectivePermissions()
            .done(function (data) {
                parseBasePermissions(data);
            });
    }
</script>
