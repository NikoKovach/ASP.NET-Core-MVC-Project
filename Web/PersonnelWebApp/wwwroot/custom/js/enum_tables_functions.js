
//##############################################################
const fieldsetButtons = document.getElementById("fieldsetCreateAgreement").getElementsByTagName("button");

for (let button of fieldsetButtons) {
       button.addEventListener("focus", (event) => {
              event.target.blur();
       });
};

const btnsListEnumDialog = document.getElementsByClassName("openEnumDialog-btn");

for (let btnOpenDialog of btnsListEnumDialog) {
       btnOpenDialog.addEventListener("click", (event) => {
              let enumDialogId = event.target.getAttribute("enumdialog");
              let subAction = event.target.getAttribute("subaction");

              openEnumAgreementDialog(subAction, enumDialogId);     
       });
};

const selectList = document.getElementsByClassName("enum-dialog-select");

for (let selectTag of selectList) {
       selectTag.addEventListener("change", (event) => {
               let selectTagText = selectTag.options[ selectTag.selectedIndex ].text;
               let selectTagId = selectTag.getAttribute("id");

              let checkbox = document.querySelector(`dialog:has(select#${selectTagId})`)
                                                                 .querySelector("input[type='checkbox']");

              let addEditInput = document.querySelector(`dialog:has(select#${selectTagId})`)
                     .querySelector("input.add-edit-input");

              if (checkbox.checked == true) {
                     addEditInput.value = selectTagText;
              }
       });
};

const checkboxList = document.getElementsByClassName("edit-enum-type");

for (let checkBox of checkboxList) {
       checkBox.addEventListener("change", (event) => {
              let checkBoxId = checkBox.getAttribute("id");
              let selectElement = document.querySelector(`dialog:has(input#${checkBoxId})`)
                                                                    .querySelector("select");

              let addEditInput = document.querySelector(`dialog:has(input#${checkBoxId})`)
                                                                 .querySelector("input.add-edit-input");

              let addEditBtn = document.querySelector(`dialog:has(input#${checkBoxId})`)
                                                              .querySelector("button.add-edit-btn");

              let valueForEdit = selectElement.options[selectElement.selectedIndex].text;

              if (checkBox.checked == true) {
                     addEditInput.value = valueForEdit;
                     addEditBtn.innerHTML = "Edit";
              }
              else {
                     addEditInput.value = "";
                     addEditBtn.innerHTML = "Add";
              }
       });
};

//##########################################################################

const dialogGetCompanyId = document.getElementById("selectCompanyDialog");
const dialogGetEmployeeId = document.getElementById("selectEmployeeDialog");

const btnOpenCompanyDialog = document.getElementById("btnOpenCompanyIdDialog");
const btnGetCompanyId = document.getElementById("btnGetCompanyId");
const btnAddEditPactType = document.getElementById("btnAddEditAgreementType");
const btnAddEditLaborArticle = document.getElementById("btnAddEditArticle");
const btnAddDepartment = document.getElementById("btnAddEditDepartment");

const inputSelCompanyId = document.getElementById("selCompanyId-input");
const inputSelEmployeeId = document.getElementById("selEmployeeId-input");
const inputAgreementType = document.getElementById("createAgreementType-input");
const inputLaborArticleType = document.getElementById("createLaborCodeArticle-input");
const inputDepartmentName = document.getElementById("createDepartmentName-input");

const selectTagContractType = document.getElementById("agreementType-select");
const selectLaborCodeArticle = document.getElementById("articleLaborCode-select");
const selectDepartments = document.getElementById("department-select");

const errorDiv = document.getElementById("dialog-alert-div");

// Event listener to open the dialog

btnOpenCompanyDialog.addEventListener('click', function () {
       dialogGetCompanyId.show();
});

// Event listener to close the dialog

btnGetCompanyId.addEventListener('click', function () {
       let companyIdVal = inputSelCompanyId.getAttribute("value");

       let parentInputTag = document.getElementById( "CompanyId" );
       parentInputTag.setAttribute("value", companyIdVal);

       if ( parentInputTag.getAttribute( "value" ) != "" ) {
              removeSpanWithClassTextDanger( parentInputTag );
              clearRedStyleFromInputTag( parentInputTag );
       }

       dialogGetCompanyId.close();
});

function closeDialogBox() {
       let dialogs = document.getElementsByClassName("agreement-dialod");

       for (let i = 0; i < dialogs.length; i++) {
              dialogs[i].close();
       }
};

// Event listener to change the dialog

inputSelCompanyId.addEventListener("change", function (e) {
       inputSelCompanyId.setAttribute("value", e.target.value);
});

inputSelEmployeeId.addEventListener("change", function (e) {
       inputSelEmployeeId.setAttribute("value", e.target.value);
});

//####################################################################
function getCompanyNameFunction() {
       let companyIdValue = document.getElementById("CompanyId").getAttribute("value");
       let spanCompanyName = document.getElementById("companyName-span");

       if (companyIdValue != "") {
              let formAction = "/Companies/GetCompany";

              let urlParams = { "id": companyIdValue };

              let responseBody = fetchGetRequest( formAction, urlParams )

              responseBody
                     .then((value) => {
                            if (!stringIsNullOrEmpty(value)) {
                                   showAgreementSpanText(value, spanCompanyName);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       }
};

//###################################################################
function openEmployeeIdDialog() {
       let valueCompanyId = document.getElementById("CompanyId").getAttribute("value");

       if (valueCompanyId == "") {
              alert("Pleace, select company from dialog box by clicking the ' Company Id ' button !");

              return;
       }

       let formAction = "/Employees/GetEmloyeesByCompany";
       let urlParams = { id : valueCompanyId};
       let response = fetchGetRequest(formAction, urlParams);

       response
              .then((value) => {
                     if (Array.isArray(value)) {
                            generateEmployeesDatalist(value);
                     }
              })
              .catch((err) => {
                     console.error(err);
              });

       dialogGetEmployeeId.show();
};

function getEmployeeIdFunction() {
       let empIdValue = inputSelEmployeeId.getAttribute("value");

       let parentInputTag = document.getElementById( "EmployeeId" ) ;
       parentInputTag.setAttribute( "value", empIdValue );

       if ( parentInputTag.getAttribute( "value" ) != "" ) {
              removeSpanWithClassTextDanger( parentInputTag );
              clearRedStyleFromInputTag( parentInputTag );
       }

       dialogGetEmployeeId.close();
};

function generateEmployeesDatalist(emloyeesList) {
       let datalist = document.getElementById("employees-list");

       let datalistOptions = "";

       emloyeesList.forEach((element) => {
              datalistOptions += `<option value=${element.id}>${element.employeeName}
                                                 ->Civil Id No : ${element.civilIdNumber}</option>`;
       });

       datalist.innerHTML = datalistOptions;
};

function getEmployeeNameFunction() {
       let employeeIdValue = document.getElementById("EmployeeId").getAttribute("value");
       let spanEmpName = document.getElementById("employeeName-span");

       if (employeeIdValue != "") {
              let formAction = "/Employees/GetEmployee";
              let urlParams = { id: employeeIdValue };
              let responseBody = fetchGetRequest( formAction, urlParams );

              responseBody
                     .then((value) => {
                            if (!stringIsNullOrEmpty(value)) {
                                   showAgreementSpanText(value, spanEmpName);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            //alert(err);
                     });
       }
};

//#####################################################################

function openEnumAgreementDialog(controllerAction,enumDialogId) {
       let areaName = "LaborAgreements";
       let dialogId = String(enumDialogId);

       let formAction = `/${areaName}${String(controllerAction)}`;

       let enumDialog = document.getElementById(dialogId);
       let selectTagInDialog = document.querySelector(`dialog#${dialogId} select`);

       let responseBody = fetchGetRequest(formAction); 

       manageOpenEnumDialogResponseBody(responseBody,selectTagInDialog); 

       enumDialog.show();
};

function manageOpenEnumDialogResponseBody(responseBody, selectTagInDialog) {
       responseBody
              .then((value) => {
                     if (Array.isArray(value)) {
                            generateSelectDatalist(value, selectTagInDialog);
                     }
              })
              .catch((err) => {
                     console.error(err);

                     alert(err);
              });
};

function generateSelectDatalist(typesList,selectTag) {
       let selectTagOptions = "";

       typesList.forEach((element) => {
              let arrValues = Object.values(element);
              selectTagOptions += `<option value=${arrValues[0]}>${arrValues[1]}</option>`;
       });

       selectTag.innerHTML = selectTagOptions; 
};

//################################################################3
function chooseAgreementType() {
       clearErrorAlertDiv();
       clearRedStyleFromInputTag( inputAgreementType );
       clearSpanBeforeOrNextToInputTag( inputAgreementType );
       clearSuccsessDivFunction();

       let selValue = selectTagContractType.value;

       let parentInputTag = document.getElementById( "ContractTypeId" );
       parentInputTag.setAttribute( "value", selValue );

       if ( parentInputTag.getAttribute("value")  != "") {
              removeSpanWithClassTextDanger( parentInputTag );
              clearRedStyleFromInputTag( parentInputTag );
       }
      
       document.getElementById( "selectAgreementTypeDialog" ).close();
};

function addAgreementType() {
       let contractTypeString = String(inputAgreementType.value);

       if (!(contractTypeString.includes("contract") || contractTypeString.includes("Contract")) ) {
                     alert("Agreement type must contains the word 'CONTRACT' ! ");
                     return;
       }

       let confirmText = "" , url = "" , entityId = "";

       let textBtnAddType =  btnAddEditPactType.innerHTML.trim();

       if (textBtnAddType === "Add") {
              confirmText = "Do you want to create a new record ?";
              url = "/LaborAgreements/AgreementTypes/CreateType";
              entityId = "0";
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/LaborAgreements/AgreementTypes/EditType";
              entityId  = selectTagContractType.value;
       }

       if (confirm(confirmText) == true) {
             
              let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              let agreementType = { Id: entityId, Type: contractTypeString };

              const request = generatePostRequest(url, token, agreementType);

              let responseBody = fetchPost( request );

              let dataListKey = "AgreementTypes";

              manageResponseBodyFetchPost( responseBody, inputAgreementType, textBtnAddType,dataListKey );
       } 
};

function getAgreementTypeFunction() {
       let contractIdValue = document.getElementById("ContractTypeId").getAttribute("value");
       let spanAgreementType = document.getElementById( "agreementType-span" );

       if (contractIdValue != "") {
              let formAction = "/LaborAgreements/AgreementTypes/GetAgreementType";
              let urlParams = {id : contractIdValue};
              let responseBody = fetchGetRequest(formAction, urlParams);

              responseBody
                     .then((value) => {
                            if (!stringIsNullOrEmpty(value)) {
                                   showAgreementSpanText(value, spanAgreementType);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                     });
       }
};

//######################################################################
function addArticleType() {
       let laborArticleString = String(inputLaborArticleType.value);

       let confirmText = "", url = "", entityId = "";

       let textBtnAddArticle = btnAddEditLaborArticle.innerHTML.trim();

       if (textBtnAddArticle === "Add") {
              confirmText = "Do you want to create a new record ?";
              url = "/LaborAgreements/LaborCodeArticles/CreateType";
              entityId = "0";
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/LaborAgreements/LaborCodeArticles/EditType";

              entityId = selectLaborCodeArticle.value;
       }

       if (confirm(confirmText) == true) {

              let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              let articleType = { Id: entityId, Article: laborArticleString };

              const request = generatePostRequest(url, token, articleType);

              let responseBody = fetchPost(request);

              let dataListKey = "LaborArticles";

              manageResponseBodyFetchPost( responseBody, inputLaborArticleType, textBtnAddArticle,dataListKey );
       } 
};

function getLaborCodeArticle() {
       let articleIdValue = document.getElementById("LaborCodeArticleId").getAttribute("value");
       let spanLaborArticle = document.getElementById("articleText-span");

       if (articleIdValue != "") {
              let formAction = "/LaborAgreements/LaborCodeArticles/GetLaborArticle";
              let urlParams = {id : articleIdValue};
              let responseBody = fetchGetRequest( formAction, urlParams );

              responseBody
                     .then((value) => {
                            if (!stringIsNullOrEmpty(value) ) {
                                   showAgreementSpanText(value, spanLaborArticle);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       }
};

function chooseLaborCodeArticle() {
       clearErrorAlertDiv();
       clearRedStyleFromInputTag( inputLaborArticleType );
       clearSpanBeforeOrNextToInputTag( inputLaborArticleType );
       clearSuccsessDivFunction();

       let articleValue = selectLaborCodeArticle.value;

       let parentInputTag = document.getElementById( "LaborCodeArticleId" );
       parentInputTag.setAttribute( "value", articleValue );

       if ( parentInputTag.getAttribute("value")  != "") {
              removeSpanWithClassTextDanger( parentInputTag );
              clearRedStyleFromInputTag( parentInputTag );
       }

       document.getElementById( "selectLaborCodeArticleDialog" ).close();
};

//#######################################################################
function chooseDepartment() {
       clearErrorAlertDiv();
       clearRedStyleFromInputTag( inputDepartmentName );
       clearSpanBeforeOrNextToInputTag( inputDepartmentName );
       clearSuccsessDivFunction();

       let departmentId = selectDepartments.value;

       let parentInputTag = document.getElementById( "DepartmentID" );
       parentInputTag.setAttribute("value", departmentId);

       if ( parentInputTag.getAttribute( "value" ) != "" ) {
              removeSpanWithClassTextDanger( parentInputTag );
              clearRedStyleFromInputTag( parentInputTag );
       }

       document.getElementById( "DepartmentsDialog" ).close();
};

function getDepartmentName() {
       let departmentId = document.getElementById("DepartmentID").getAttribute("value");
       let spanDepartmentName = document.getElementById("departmentText-span");

       if (departmentId != "") {
              let formAction = "/LaborAgreements/Departments/GetDepartment";
              let urlParams = { id: departmentId };
              let responseBody = fetchGetRequest( formAction, urlParams )

              responseBody
                     .then((value) => {
                            if ( !stringIsNullOrEmpty(value)) {
                                   showAgreementSpanText(value, spanDepartmentName);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       }
};

function addDepartmentFunction() {

       let departmentName = String(inputDepartmentName.value);

       let confirmText = "", url = "", entityId = "";

       let textBtnAddDepartment = btnAddDepartment.innerHTML.trim();

       if (textBtnAddDepartment === "Add") {
              confirmText = "Do you want to create a new record ?";
              url = "/LaborAgreements/Departments/Create";
              entityId = "0";
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/LaborAgreements/Departments/Edit";

              entityId = selectDepartments.value;
       }

       if (confirm(confirmText) == true) {

              let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              let departmentVM = { DepartmentId: entityId, Name: departmentName };

              const request = generatePostRequest(url, token, departmentVM);

              let responseBody = fetchPost( request );

              let dataListKey = "Departments";

              manageResponseBodyFetchPost( responseBody, inputDepartmentName, textBtnAddDepartment, dataListKey );
       } 
};

//######################################################################

function generatDataList ( dataListKey, value ) {
       let dataListMap = new Map();

       dataListMap.set( "AgreementTypes", generateAgreementTypesDatalist( value ) );
       dataListMap.set( "LaborArticles", generateArticlesDatalist( value ) );
       dataListMap.set( "Departments", generateDepartmentsDatalist( value ) );
       dataListMap.set( "Addresses", generateSelectAddressesList( value ) );

       dataListMap.set( "Default", generateEmptyDataList() );

       if ( isString(dataListKey)  && !stringIsNullOrEmpty(dataListKey) && dataListMap.has(dataListKey) )
              return dataListMap.get(dataListKey);

       return dataListMap.get( "Default" );
};

function generateAgreementTypesDatalist ( typesList ) {
       let selectTagOptions = "";

       typesList.forEach( ( element ) => {
              selectTagOptions += `<option value=${element.id}>${element.type}</option>`;
       } );

       selectTagContractType.innerHTML = selectTagOptions;
};

function generateArticlesDatalist ( articlesList ) {
       let selectTagOptions = "";

       articlesList.forEach( ( element ) => {
              selectTagOptions += `<option value=${element.id}>${element.article}</option>`;
       } );

       selectLaborCodeArticle.innerHTML = selectTagOptions;
};

function generateDepartmentsDatalist ( departmetList ) {
       let selectTagOptions = "";

       departmetList.forEach( ( element ) => {
              selectTagOptions += `<option value=${element.departmentId}>${element.name}</option>`;
       } );

       selectDepartments.innerHTML = selectTagOptions;
};

function generateEmptyDataList () {
      /* alert("Nothing happened !");*/
};

//#####################################################################