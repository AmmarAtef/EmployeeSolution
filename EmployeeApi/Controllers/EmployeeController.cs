using EmployeeApi.Models;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace EmployeeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {

        string webUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];
        string listTitle = System.Configuration.ConfigurationManager.AppSettings["listName"];

        [HttpGet]
        [Route("api/Employee/GetEmployees")]
        [ResponseType(typeof(List<EmployeeDto>))]
        [Authorize(Roles = "Read, FullControl")]
        public HttpResponseMessage GetEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            try
            {
                using (var ctx = new ClientContext(webUrl))
                {

                    //// Get Permission For the User
                    //ClientResult<BasePermissions> permissions = ctx.Web.Lists.GetByTitle(listTitle).GetUserEffectivePermissions(userName);
                    //ctx.ExecuteQuery();
                    //// check if the permission has view items
                    //Boolean hasPermission = permissions.Value.Has(PermissionKind.ViewListItems);

                    //if (!hasPermission)
                    //    return Request.CreateResponse(HttpStatusCode.Unauthorized, "you're not allowed to view list items");
                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml =
                        @"<View>  
                                <ViewFields>
                                    <FieldRef Name='ID' />
                                    <FieldRef Name='EmployeeName' />
                                    <FieldRef Name='BriefOfExperience' />
                                    <FieldRef Name='EmployeeBirthdate' />
                                    <FieldRef Name='EmployeeGender' />
                                    <FieldRef Name='EmployeeSalary' />
                                    <FieldRef Name='EmployeeDepartment' />
                                    <FieldRef Name='ProfileURL' />
                                </ViewFields> 
                         </View>";


                    var employeesCollection = ctx.Web.Lists.GetByTitle(listTitle).GetItems(camlQuery);

                    ctx.Load(employeesCollection, items => items.Include(
                        item => item["ID"],
                        item => item["EmployeeName"],
                        item => item["EmployeeBirthdate"],
                        item => item["BriefOfExperience"],
                        item => item["EmployeeGender"],
                        item => item["EmployeeSalary"],
                        item => item["EmployeeDepartment"],
                        item => item["ProfileURL"]));

                    ctx.ExecuteQuery();

                    foreach (ListItem emp in employeesCollection)
                    {
                        employees.Add(new EmployeeDto()
                        {
                            ID = Convert.ToInt32(emp["ID"]),
                            EmployeeName = Convert.ToString(emp["EmployeeName"]),
                            EmployeeBirthdate = Convert.ToDateTime(emp["EmployeeBirthdate"]),
                            EmployeeDepartment = Convert.ToString(emp["EmployeeDepartment"]),
                            EmployeeGender = Convert.ToString(emp["EmployeeGender"]),
                            EmployeeSalary = Convert.ToInt64(emp["EmployeeSalary"]),
                            BriefOfExperience = Convert.ToString(emp["BriefOfExperience"]),
                            ProfileURL = emp["ProfileURL"] == null ? "" : ((FieldUrlValue)emp["ProfileURL"]).Url,
                            Description = emp["ProfileURL"] == null ? "" : ((FieldUrlValue)emp["ProfileURL"]).Description
                        });

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, employees);
                }
            }
            catch (Exception ex)
            {
                // return the error back 
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Employee/GetEmployee/{Id}")]
        [ResponseType(typeof(List<EmployeeDto>))]
        [Authorize(Roles = "Read, FullControl")]
        public HttpResponseMessage GetEmployee(int Id)
        {
            EmployeeDto employee = new EmployeeDto();
            try
            {
                using (var ctx = new ClientContext(webUrl))
                {
                    //get user from token
                    var claims = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
                    // Get Permission For the User
                    ClientResult<BasePermissions> permissions = ctx.Web.Lists.GetByTitle(listTitle).GetUserEffectivePermissions(claims.Name);
                    ctx.ExecuteQuery();
                    // check if the permission has view items
                    Boolean hasPermission = permissions.Value.Has(PermissionKind.ViewListItems);

                    if (!hasPermission)
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "you're not allowed to view list items");

                    ListItem emp = ctx.Web.Lists.GetByTitle(listTitle).GetItemById(Id);

                    ctx.Load(emp);

                    ctx.ExecuteQuery();

                    employee = new EmployeeDto()
                    {

                        ID = Convert.ToInt32(emp["ID"]),
                        EmployeeName = Convert.ToString(emp["EmployeeName"]),
                        EmployeeBirthdate = Convert.ToDateTime(emp["EmployeeBirthdate"]),
                        EmployeeDepartment = Convert.ToString(emp["EmployeeDepartment"]),
                        EmployeeGender = Convert.ToString(emp["EmployeeGender"]),
                        EmployeeSalary = Convert.ToInt64(emp["EmployeeSalary"]),
                        BriefOfExperience = Convert.ToString(emp["BriefOfExperience"]),
                        ProfileURL = ((FieldUrlValue)emp["ProfileURL"]).Url,
                        Description = ((FieldUrlValue)emp["ProfileURL"]).Description
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
            }
            catch (Exception ex)
            {
                // return the error back 
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Employee/AddEmployee")]
        [Authorize(Roles = "FullControl")]
        public HttpResponseMessage AddEmployee([FromBody] EmployeeCreationDto employeeCreationDto)
        {
            try
            {
                
                // check the valdation of the date 
                if (employeeCreationDto.EmployeeBirthdate >= DateTime.Today)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Birthday less than today");
                // check the model state 
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Values);

                using (var ctx = new ClientContext(webUrl))
                {
                    //check Permissions
                    //ClientResult<BasePermissions> permissions = ctx.Web.Lists.GetByTitle(listTitle).GetUserEffectivePermissions(userName);
                    //ctx.ExecuteQuery();
                    //Boolean hasPermission = permissions.Value.Has(PermissionKind.AddListItems);
                    //if (!hasPermission)
                    //    return Request.CreateResponse(HttpStatusCode.Unauthorized, "you're not allow to add employee");

                    //create item list 
                    List oList = ctx.Web.Lists.GetByTitle(listTitle);

                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.AddItem(itemCreateInfo);
                    foreach (var property in typeof(EmployeeCreationDto).GetProperties())
                    {
                        if (property.Name != "ProfileURL" & property.Name != "Description")
                            oListItem[property.Name] = property.GetValue(employeeCreationDto);
                    }
                    FieldUrlValue fieldUrl = new FieldUrlValue();
                    fieldUrl.Url = employeeCreationDto.ProfileURL;
                    fieldUrl.Description = employeeCreationDto.Description;
                    oListItem["ProfileURL"] = fieldUrl;
                    oListItem.Update();
                    ctx.ExecuteQuery();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                // return the error back 
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPut]
        [Route("api/Employee/UpdateEmployee")]
        [Authorize(Roles = "FullControl")]
        public HttpResponseMessage UpdateEmployee([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            try
            {
               
                // check the valdation of the date 
                if (employeeUpdateDto.EmployeeBirthdate >= DateTime.Today)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Birthday less than today");
                // check the model state 
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                using (var ctx = new ClientContext(webUrl))
                {
                    //get user from token
                    var claims = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
                    //check user permission on the Item 
                    ClientResult<BasePermissions> permissions = ctx.Web.Lists.GetByTitle(listTitle).GetItemById(employeeUpdateDto.ID).GetUserEffectivePermissions(claims.Name);
                    ctx.ExecuteQuery();
                    Boolean hasPermission = permissions.Value.Has(PermissionKind.AddListItems);
                    if (!hasPermission)
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "you're not allow to update employee");

                    //Get Item
                    ListItem oListItem = ctx.Web.Lists.GetByTitle(listTitle).GetItemById(employeeUpdateDto.ID);

                    foreach (var property in typeof(EmployeeUpdateDto).GetProperties())
                    {
                        if (property.Name != "ID" & property.Name != "ProfileURL" & property.Name != "Description" & property.GetValue(employeeUpdateDto) != null)
                            oListItem[property.Name] = property.GetValue(employeeUpdateDto);
                    }
                    if (!String.IsNullOrWhiteSpace(employeeUpdateDto.ProfileURL))
                    {
                        FieldUrlValue fieldUrl = new FieldUrlValue();
                        fieldUrl.Url = employeeUpdateDto.ProfileURL;
                        if (!String.IsNullOrWhiteSpace(employeeUpdateDto.Description))
                            fieldUrl.Description = employeeUpdateDto.Description;
                        oListItem["ProfileURL"] = fieldUrl;
                    }
                    oListItem.Update();
                    ctx.ExecuteQuery();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpDelete]
        [Route("api/Employee/DeleteEmployee")]
        [Authorize(Roles = "FullControl")]
        public HttpResponseMessage DeleteEmployee(int Id)
        {
            try
            {
                using (var ctx = new ClientContext(webUrl))
                {
                    //get user from token
                    var claims = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
                    //check user permission on the Item 
                    ClientResult<BasePermissions> permissions = ctx.Web.Lists.GetByTitle(listTitle).GetItemById(Id).GetUserEffectivePermissions(claims.Name);
                    ctx.ExecuteQuery();
                    Boolean hasPermission = permissions.Value.Has(PermissionKind.AddListItems);
                    if (!hasPermission)
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "you're not allow to Delete  employee");

                    ListItem listItem = ctx.Web.Lists.GetByTitle(listTitle).GetItemById(Id);
                    listItem.DeleteObject();
                    ctx.ExecuteQuery();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }



        //Method to retrive options for field choice 
        [HttpGet]
        [Route("api/Employee/GetFieldsValues")]
        [Authorize(Roles = "Read, FullControl")]
        public HttpResponseMessage GetFieldsValues(string fieldName)
        {
            try
            {
                using (var ctx = new ClientContext(webUrl))
                {
                    Field field = ctx.Web.Lists.GetByTitle(listTitle).Fields.GetByInternalNameOrTitle(fieldName);
                    FieldChoice fldChoice = ctx.CastTo<FieldChoice>(field);
                    ctx.Load(fldChoice, f => f.Choices);
                    ctx.ExecuteQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, fldChoice.Choices);
                }
            }
            catch (Exception ex)
            {
                // return the error back 
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
