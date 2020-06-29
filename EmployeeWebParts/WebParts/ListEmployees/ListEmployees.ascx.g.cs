﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeWebParts.WebParts.ListEmployees {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    using System.CodeDom.Compiler;
    
    
    public partial class ListEmployees {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "16.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(ListEmployees target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "16.0.0.0")]
        private void @__BuildControlTree(global::EmployeeWebParts.WebParts.ListEmployees.ListEmployees @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"/Style%20Library/CSS/bootstrap.m" +
                        "in.css\" />\r\n<script src=\"/Style Library/Scripts/jquery-3.5.1.min.js\"></script>\r\n" +
                        "<div id=\"divContainer\" class=\"container\"></div>\r\n\r\n<script type=\"text/javascript" +
                        "\">\r\n    \'use strict\'\r\n    var listTitle = \'Employees\';\r\n    var allowAdd = false" +
                        ";\r\n    function GetEmployees() {\r\n        //Get Employees Data Using SharePointA" +
                        "PI \r\n        var apiPath = _spPageContextInfo.webAbsoluteUrl + \"/_api/lists/getb" +
                        "ytitle(\'Employees\')/items?$select=id,EmployeeName,EmployeeDepartment,ProfileURL," +
                        "EmployeeGender,BriefOfExperience,EmployeeBirthdate,EmployeeSalary\";\r\n        //?" +
                        " $select = id, EmployeeGender, EmployeeName, EmployeeBirthdate, Profile URL, Emp" +
                        "loyeeDepartment, BriefOfExperience, EmployeeSalary\";\r\n        $.ajax({\r\n        " +
                        "    url: apiPath,\r\n            type: \"GET\",\r\n            headers: {\r\n           " +
                        "     \"accept\": \"application/json;odata=verbose\",\r\n                \"content-Type\"" +
                        ": \"application/json;odata=verbose\"\r\n            },\r\n            success: functio" +
                        "n (data) {\r\n                var employees = data.d.results;\r\n                con" +
                        "sole.log(data.d.results);\r\n\r\n\r\n                for (var i = 0; i < data.d.result" +
                        "s.length; i++) {\r\n                    AddEmployeeToDev(employees[i]);\r\n         " +
                        "       }\r\n            },\r\n            error: function (error) {\r\n               " +
                        " alert(JSON.stringify(error));\r\n            }\r\n        });\r\n    }\r\n\r\n    functio" +
                        "n AddEmployeeToDev(employee) {\r\n        // Append to dom after creation\r\n       " +
                        " $(\"#divContainer\").append(`<div class=\"row\" id=\"${employee[\'Id\']}-emp\">\r\n      " +
                        "                                  <div class=\"col-md-2 col-sm-2\">\r\n             " +
                        "                               <label>Employee Name: </label>\r\n                 " +
                        "                        </div>\r\n                                         <div cl" +
                        "ass=\"col-md-2 col-sm-2\" >\r\n                                            <label >$" +
                        "{employee[\'EmployeeName\']}</label>\r\n                                        </di" +
                        "v>\r\n                                        <div class=\"col-md-2 col-sm-2\">\r\n   " +
                        "                                         <label>Gender: </label>\r\n              " +
                        "                          </div>\r\n                                        <div c" +
                        "lass=\"col-md-2 col-sm-2\">\r\n                                            <label >$" +
                        "{ employee[\'EmployeeGender\']}</label>\r\n                                        <" +
                        "/div>\r\n                                        <div class=\"col-md-2 col-sm-2\">\r\n" +
                        "                                            <label>Salary: </label>\r\n           " +
                        "                             </div>\r\n                                        <di" +
                        "v class=\"col-md-2 col-sm-2\">\r\n                                            <label" +
                        " >${employee[\'EmployeeSalary\']}\r\n                                            </l" +
                        "abel>\r\n                                        </div>\r\n                         " +
                        "               <div class=\"col-md-2 col-sm-2\">\r\n                                " +
                        "            <label>Department: </label>\r\n                                       " +
                        " </div>\r\n                                        <div class=\"col-md-2 col-sm-2\">" +
                        "\r\n                                            <label  >${employee[\'EmployeeDepar" +
                        "tment\']}</label>\r\n                                        </div>\r\n              " +
                        "                          <div class=\"col-md-2 col-sm-2\">\r\n                     " +
                        "                       <label>Birthdate: </label>\r\n                             " +
                        "           </div>\r\n                                        <div class=\"col-md-2 " +
                        "col-sm-2\">\r\n                                            <label>${employee[\'Emplo" +
                        "yeeBirthdate\']}</label>\r\n                                        </div>\r\n       " +
                        "                                 <div class=\"col-md-2 col-sm-2\">\r\n              " +
                        "                             <label>ProfileURL: </label>\r\n                      " +
                        "                  </div>\r\n                                        <div class=\"co" +
                        "l-md-2 col-sm-2\">\r\n                                            <label>${employee" +
                        "[\'ProfileURL\'].Url}</label>\r\n                                        </div>\r\n   " +
                        "                                     <div class=\"col-md-2 col-sm-2\">\r\n          " +
                        "                                  <button  class= \"btn btn-danger\" value=\"Delete" +
                        " Me\" onClick=checkPermission(${employee[\'Id\']})>Delete Me</button>\r\n            " +
                        "                             </div>\r\n                                         <d" +
                        "iv class=\"xol-md-2 col-sm-2\">\r\n                                             <a h" +
                        "ref=\"/SitePages/UpdateEmployee.aspx?empId=${employee[\'Id\']}\" class=\"btn btn-prim" +
                        "ary btn-lg active\" role=\"button\" aria-pressed=\"true\">Update</a>\r\n               " +
                        "                           </div>\r\n                \r\n                           " +
                        "         </div>`);\r\n       \r\n    }\r\n\r\n\r\n    function Delete(id) {\r\n        // De" +
                        "lete List item using sharepoint APi passing Id \r\n        var result = confirm(\"D" +
                        "o you want to delete?\");\r\n        if (result) {\r\n            var apiPath = _spPa" +
                        "geContextInfo.webAbsoluteUrl + \"/_api/lists/getbytitle(\'Employees\')/items(\" + id" +
                        " + \")\"\r\n            $.ajax({\r\n                url: apiPath,\r\n                typ" +
                        "e: \"POST\",\r\n                headers: {\r\n                    \"accept\": \"applicati" +
                        "on/json;odata=verbose\",\r\n                    \"content-Type\": \"application/json;o" +
                        "data=verbose\",\r\n                    \"X-RequestDigest\": $(\"#__REQUESTDIGEST\").val" +
                        "(),\r\n                    \"X-HTTP-Method\": \"DELETE\",\r\n                    \"IF-MAT" +
                        "CH\": \"*\"\r\n                },\r\n                success: function (data) {\r\n      " +
                        "              var r = confirm(\"Successful Message!\");\r\n                    if (r" +
                        " === true) {\r\n                        $(\'#\' + id + \'-emp\').hide();\r\n            " +
                        "        }\r\n                },\r\n                error: function (error) {\r\n      " +
                        "              alert(JSON.stringify(error));\r\n                }\r\n            });\r" +
                        "\n        }\r\n    }\r\n\r\n\r\n\r\n    function getListUserEffectivePermissions(id) {\r\n   " +
                        "     var endpointUrl = _spPageContextInfo.webAbsoluteUrl + \"/_api/web/lists/getb" +
                        "ytitle(\'\" + listTitle + \"\')/items(\" + id + \")/getusereffectivepermissions(@u)?@u" +
                        "=\'\" + encodeURIComponent(_spPageContextInfo.userLoginName) + \"\'\";\r\n        retur" +
                        "n $.getJSON(endpointUrl);\r\n    }\r\n    var allowed = false;\r\n    function parseBa" +
                        "sePermissions(value, id) {\r\n        debugger;\r\n        var permissions = new SP." +
                        "BasePermissions();\r\n        permissions.initPropertiesFromJson(value);\r\n        " +
                        "allowAdd = permissions.has(SP.PermissionKind.addListItems);\r\n        console.log" +
                        "(allowAdd);\r\n        if (allowAdd)\r\n            Delete(id);\r\n    }\r\n\r\n    functi" +
                        "on checkPermission(id) {\r\n        getListUserEffectivePermissions(id)\r\n         " +
                        "   .done(function (data) {\r\n                parseBasePermissions(data, id);\r\n   " +
                        "         });\r\n    }\r\n\r\n</script>\r\n"));
        }
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "16.0.0.0")]
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "16.0.0.0")]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "16.0.0.0")]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
