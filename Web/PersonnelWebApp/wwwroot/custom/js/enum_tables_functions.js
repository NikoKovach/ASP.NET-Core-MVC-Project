
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

              openEnumAgreementdialog(subAction, enumDialogId);     
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

const dialogGetAgreementTypeId = document.getElementById("selectAgreementTypeDialog");
const dialogGetLaborCodeArticle = document.getElementById("selectLaborCodeArticleDialog");
const dialogDepartments = document.getElementById("DepartmentsDialog");

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

const errorDiv = document.getElementById("error-alert-div");

// Event listener to open the dialog

btnOpenCompanyDialog.addEventListener('click', function () {
       dialogGetCompanyId.show();
});

// Event listener to close the dialog

btnGetCompanyId.addEventListener('click', function () {
       let companyIdVal = inputSelCompanyId.getAttribute("value");

       document.getElementById("CompanyId").setAttribute("value", companyIdVal);

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

              let responseBody = fetchGetRequest(formAction, companyIdValue)

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

       let response = fetchGetRequest(formAction, valueCompanyId);

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

       document.getElementById("EmployeeId").setAttribute("value", empIdValue);

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

              let responseBody = fetchGetRequest(formAction, employeeIdValue)

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

function openEnumAgreementdialog(controllerAction,enumDialogId) {
       let areaName = "LaborAgreements";
       let dialogId = String(enumDialogId);

       let formAction = `/${areaName}${String(controllerAction)}`;

       let enumDialog = document.getElementById(dialogId);
       let selectTagInDialog = document.querySelector(`dialog#${dialogId} select`); // ?????????????

       let responseBody = fetchGetRequest(formAction, ""); 

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
       clearErrorAlertDiv(inputAgreementType);
       clearSuccsessDivFunction();

       let selValue = selectTagContractType.value;

       document.getElementById("ContractTypeId").setAttribute("value",selValue);

       dialogGetAgreementTypeId.close();
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

              let responseBody = fetchPost(request);

              responseBody
                     .then((value) => {
                            if (Array.isArray(value)) {
                                   generateAgreementTypesDatalist(value);
                                   clearErrorAlertDiv(inputAgreementType);
                                   showSuccsessDivFunction(textBtnAddType);
                            }
                            else {
                                   modelStateHandlerFunction(value, inputAgreementType);
                            }
                     })
                     .catch((err) => {
                            console.error(err);

                            alert(err);
                     });
       } 
};

function generateAgreementTypesDatalist(typesList) {
       let selectTagOptions = "";

       typesList.forEach((element) => {
              selectTagOptions += `<option value=${element.id}>${element.type}</option>`;
       });

       selectTagContractType.innerHTML = selectTagOptions;
};

function getAgreementTypeFunction() {
       let contractIdValue = document.getElementById("ContractTypeId").getAttribute("value");
       let spanAgreementType = document.getElementById("agreementType-span");

       if (contractIdValue != "") {
              let formAction = "/LaborAgreements/AgreementTypes/GetAgreementType";

              let responseBody = fetchGetRequest(formAction, contractIdValue);

              responseBody
                     .then((value) => {
                            if (!stringIsNullOrEmpty(value)) {
                                   showAgreementSpanText(value, spanAgreementType);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            //alert(err);
                     });
       }
};

//######################################################################

function generateArticlesDatalist(articlesList) {
       let selectTagOptions = "";

       articlesList.forEach((element) => {
              selectTagOptions += `<option value=${element.id}>${element.article}</option>`;
       });

       selectLaborCodeArticle.innerHTML = selectTagOptions;
};

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

              responseBody
                     .then((value) => {
                            if (Array.isArray(value)) {
                                   generateArticlesDatalist(value);
                                   clearErrorAlertDiv(inputLaborArticleType);
                                   showSuccsessDivFunction(textBtnAddArticle);
                            }
                            else {
                                   modelStateHandlerFunction(value, inputLaborArticleType);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       } 
};

function getLaborCodeArticle() {
       let articleIdValue = document.getElementById("LaborCodeArticleId").getAttribute("value");
       let spanLaborArticle = document.getElementById("articleText-span");

       if (articleIdValue != "") {
              let formAction = "/LaborAgreements/LaborCodeArticles/GetLaborArticle";

              let responseBody = fetchGetRequest(formAction, articleIdValue)

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
       clearErrorAlertDiv(inputLaborArticleType);
       clearSuccsessDivFunction();

       let articleValue = selectLaborCodeArticle.value;

       document.getElementById("LaborCodeArticleId").setAttribute("value", articleValue);

       dialogGetLaborCodeArticle.close();
};

//#######################################################################

function generateDepartmentsDatalist(departmetList) {
       let selectTagOptions = "";

       departmetList.forEach((element) => {
              selectTagOptions += `<option value=${element.departmentId}>${element.name}</option>`;
       });

       selectDepartments.innerHTML = selectTagOptions;
};

function chooseDepartment() {
       clearErrorAlertDiv(inputDepartmentName);
       clearSuccsessDivFunction();

       let departmentId = selectDepartments.value;

       document.getElementById("DepartmentDepartmentID").setAttribute("value", departmentId);

       dialogDepartments.close();
};

function getDepartmentName() {
       let departmentId = document.getElementById("DepartmentDepartmentID").getAttribute("value");
       let spanDepartmentName = document.getElementById("departmentText-span");

       if (departmentId != "") {
              let formAction = "/LaborAgreements/Departments/GetDepartment";

              let responseBody = fetchGetRequest(formAction, departmentId)

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

              let responseBody = fetchPost(request);
              
              responseBody
                     .then((value) => {
                            if (Array.isArray(value)) {
                                   generateDepartmentsDatalist(value);
                                   clearErrorAlertDiv(inputDepartmentName);
                                   showSuccsessDivFunction(textBtnAddDepartment);
                            }
                            else {
                                   modelStateHandlerFunction(value, inputDepartmentName);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       } 
};

//######################################################################

//function chooseEnumType(inputTypeName) {
//       let inputElement = HTMLInputElement(inputTypeName);

//       clearErrorAlertDiv(inputDepartmentName);
//       clearSuccsessDivFunction();

//       let departmentId = selectDepartments.value;

//       document.getElementById("DepartmentDepartmentID").setAttribute("value", departmentId);

//       dialogDepartments.close();
//};

function showAgreementSpanText(spanText,spanElement) {
       if (String(spanText) != "" && isDomElement(spanElement)) {
              spanElement.innerHTML = String(spanText);
       }
};

async function fetchGetRequest(formAction,id ) {
       let url = formAction + "/" + id;

       if ((typeof id === 'string' || id instanceof String) && id == "" ) {
              url = formAction;
       }

       try {
              const response = await fetch(url);

              if (!response.ok) {
                     throw new Error(`Response status: ${response.status}`);
              }

              const result = await response.json();

              return result;
       } catch (error) {
              alert(error.message);
       }
};

async function fetchPost(request) {
       try {
              const response = await fetch(request);

              const result = await response.json();

              return result;

       } catch (error) {
              console.error("Error:", error);
              alert(error.message);
       }
};

function generatePostRequest(url, antiForgeryToken,bodyObject) {
       return new Request(url, {
              method: "POST",
              headers: {
                     RequestVerificationToken: antiForgeryToken,
                     "Content-Type": "application/json",
              },
              body: JSON.stringify(bodyObject),
       });
};

//###################################################################
function isObjectLike(value) {
       return value != null && typeof value == 'object' && !Array.isArray(value);
}

function isObjectLikeArray(value) {
       return value != null && typeof value == 'object' && Array.isArray(value);
}

function stringIsNullOrEmpty(value) {
       if (value == null) return true;

       if (typeof value == "string" && value != null &&  value == "") return true;

       return false;
}

function valueIsModelState(value) {
       let valueIsObject= (value != null && typeof value == 'object' && !Array.isArray(value)) ? true : false ;

       if (valueIsObject) {
              for (let x in value) {
                     let keysArr = Object.keys(value[x]);
                     let errorExists = keysArr.some(x => x == "errors");

                     if (errorExists) return true;
              }
       }

       return false;
};

function isDomElement(element) {
       return element instanceof Element;
};
//#####################################################################

function modelStateHandlerFunction(modelState, activeInput) {
       const validityValuesEnum = { isValid: 2, isNotValid: 1 };

       let invalidItemsList = [];

       for (let entityName in modelState) {
              if (modelState[entityName].validationState != validityValuesEnum.isNotValid) {
                     continue;
              }

              let keysArray = Object.keys(modelState[entityName]);

              let errorMessages = [];

              if (keysArray.includes("errors")) {
                     let errorsList = modelState[entityName].errors;

                     errorMessages = errorsList.map(function (item) {
                                                               return String( item.errorMessage);
                                                        });
              }

              invalidItemsList.push({key:entityName , errors : errorMessages});
       }

       if (invalidItemsList.length > 0) {
              showErrorsAlertDiv(invalidItemsList, activeInput);
       }
};

function showErrorsAlertDiv(invalidItemsList, activeInput) {
       let errorsMsgString = "";
       invalidItemsList.forEach(function (value, index, array) {
              errorsMsgString = "<li><p>Property ' " + value.key + " ' :</p>";

              value.errors.forEach(function (error) {
                     errorsMsgString += "<p><span>-></span>" + error + "</p>";
              });

              errorsMsgString += "</li > ";

              if (activeInput != undefined &&  String(activeInput.id).includes(value.key)) {
                     changeInputStyleToRedAlert(activeInput);
              }
       });

       let ulErrorDiv = errorDiv.querySelector("ul");

       while (ulErrorDiv.hasChildNodes()) {
              ulErrorDiv.removeChild(ulErrorDiv.firstChild);
       }

       ulErrorDiv.innerHTML = errorsMsgString;

       errorDiv.style.display = "list-item";
};

function changeInputStyleToRedAlert(activeInput) {
       let spanString = "<span  class='text-danger' style='color:#f44336;font-size: 12px;'> *</span>";

       if (activeInput instanceof HTMLInputElement) {
              removeSpanNextToInputTag(activeInput);
              activeInput.insertAdjacentHTML("afterend", spanString);
              activeInput.style.backgroundColor = "rgb(255, 179, 179)";
       }
};

function removeSpanNextToInputTag(activeInput) {
       let spanAfterInput = activeInput.parentNode.querySelector("span");

       if (spanAfterInput != null) {
              activeInput.parentNode.querySelector("span").remove();
       }
};

function clearErrorAlertDiv(activeInput) {
       let ulErrorDiv = errorDiv.querySelector("ul");

       while (ulErrorDiv.hasChildNodes()) {
              ulErrorDiv.removeChild(ulErrorDiv.firstChild);
       }

       errorDiv.style.display = "none";

       if (activeInput instanceof HTMLInputElement) {
              removeSpanNextToInputTag(activeInput);
       }

       activeInput.style.backgroundColor = "rgb(255, 255, 255)";
};

function showSuccsessDivFunction(buttonInnerText) {
       let divInnerText = "";

       if (typeof buttonInnerText == "string") {
              if (buttonInnerText === "Add") {
                     divInnerText = "' Create ' operation is successful !";
              }
              else {
                     divInnerText = "' Edit ' operation is successful !";
              }
       }

       let divSuccess = document.getElementById("edit-success");
       let childParagraph = divSuccess.querySelector("p");

       if (childParagraph != null) {
              childParagraph.innerText = divInnerText;
       }

       divSuccess.style.display = "block";
};

function clearSuccsessDivFunction() {
       let divSuccess = document.getElementById("edit-success");
       let childParagraph = divSuccess.querySelector("p");

       if (childParagraph != null && childParagraph.innerText !="") {
              childParagraph.innerText = "";
       }

       divSuccess.style.display = "none";
};

//#######################################################################


//Archive = fetch() post with Antiforgery token
//async function post(request) {
//       try {
              //let url = "/AgreementTypes/CreateType";
              //let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              //const response = await fetch(url, {
              //       method: "POST",
              //       headers: {
              //              RequestVerificationToken: token,
              //              "Content-Type": "application/json",
              //       },
              //       body: JSON.stringify({ type: "example" }),
              //});

//              //const response = await fetch(request);

//              //const result = await response.json();

//              //return result;

//       } catch (error) {
//              /*console.error("Error:", error);*/
//              alert(error.message);
//       }
//};

