<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListEmployees.ascx.cs" Inherits="EmployeeWebParts.WebParts.ListEmployees.ListEmployees" %>

<link type="text/css" rel="stylesheet" href="/Style%20Library/CSS/bootstrap.min.css" />
<script src="/Style Library/Scripts/jquery-3.5.1.min.js"></script>
<div id="divContainer" class="container"></div>

<script type="text/javascript">
    'use strict'
    var listTitle = 'Employees';
    var allowAdd = false;
    function GetEmployees() {
        //Get Employees Data Using SharePointAPI 
        var apiPath = _spPageContextInfo.webAbsoluteUrl + "/_api/lists/getbytitle('Employees')/items?$select=id,EmployeeName,EmployeeDepartment,ProfileURL,EmployeeGender,BriefOfExperience,EmployeeBirthdate,EmployeeSalary";
        //? $select = id, EmployeeGender, EmployeeName, EmployeeBirthdate, Profile URL, EmployeeDepartment, BriefOfExperience, EmployeeSalary";
        $.ajax({
            url: apiPath,
            type: "GET",
            headers: {
                "accept": "application/json;odata=verbose",
                "content-Type": "application/json;odata=verbose"
            },
            success: function (data) {
                var employees = data.d.results;
                console.log(data.d.results);


                for (var i = 0; i < data.d.results.length; i++) {
                    AddEmployeeToDev(employees[i]);
                }
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });
    }

    function AddEmployeeToDev(employee) {
        // Append to dom after creation
        $("#divContainer").append(`<div class="row" id="${employee['Id']}-emp">
                                        <div class="col-md-2 col-sm-2">
                                            <label>Employee Name: </label>
                                         </div>
                                         <div class="col-md-2 col-sm-2" >
                                            <label >${employee['EmployeeName']}</label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label>Gender: </label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label >${ employee['EmployeeGender']}</label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label>Salary: </label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label >${employee['EmployeeSalary']}
                                            </label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label>Department: </label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label  >${employee['EmployeeDepartment']}</label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label>Birthdate: </label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label>${employee['EmployeeBirthdate']}</label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                           <label>ProfileURL: </label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <label>${employee['ProfileURL'].Url}</label>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <button  class= "btn btn-danger" value="Delete Me" onClick=checkPermission(${employee['Id']})>Delete Me</button>
                                         </div>
                                         <div class="xol-md-2 col-sm-2">
                                             <a href="/SitePages/UpdateEmployee.aspx?empId=${employee['Id']}" class="btn btn-primary btn-lg active" role="button" aria-pressed="true">Update</a>
                                          </div>
                
                                    </div>`);
       
    }


    function Delete(id) {
        // Delete List item using sharepoint APi passing Id 
        var result = confirm("Do you want to delete?");
        if (result) {
            var apiPath = _spPageContextInfo.webAbsoluteUrl + "/_api/lists/getbytitle('Employees')/items(" + id + ")"
            $.ajax({
                url: apiPath,
                type: "POST",
                headers: {
                    "accept": "application/json;odata=verbose",
                    "content-Type": "application/json;odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                    "X-HTTP-Method": "DELETE",
                    "IF-MATCH": "*"
                },
                success: function (data) {
                    var r = confirm("Successful Message!");
                    if (r === true) {
                        $('#' + id + '-emp').hide();
                    }
                },
                error: function (error) {
                    alert(JSON.stringify(error));
                }
            });
        }
    }



    function getListUserEffectivePermissions(id) {
        var endpointUrl = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listTitle + "')/items(" + id + ")/getusereffectivepermissions(@u)?@u='" + encodeURIComponent(_spPageContextInfo.userLoginName) + "'";
        return $.getJSON(endpointUrl);
    }
    var allowed = false;
    function parseBasePermissions(value, id) {
        debugger;
        var permissions = new SP.BasePermissions();
        permissions.initPropertiesFromJson(value);
        allowAdd = permissions.has(SP.PermissionKind.addListItems);
        console.log(allowAdd);
        if (allowAdd)
            Delete(id);
    }

    function checkPermission(id) {
        getListUserEffectivePermissions(id)
            .done(function (data) {
                parseBasePermissions(data, id);
            });
    }

</script>
