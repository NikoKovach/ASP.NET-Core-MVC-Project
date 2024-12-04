//#################### JQuery script ###########################################

$(".stylish-btn").on("click", function () {
       $(this).trigger("blur");
});

//################### Clear Java Script #################################

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

const checkboxEditAgreementType = document.getElementById("checkbox-edit-contractType");
const checkboxEditLaborArticle = document.getElementById("checkbox-edit-laborCode");
const checkboxEditDepartment = document.getElementById("checkbox-edit-department");

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

//Event listener chechbox
checkboxEditAgreementType.addEventListener("change", function () {
       let typeForEdit = selectTagContractType.options[selectTagContractType.selectedIndex].text;

       if (checkboxEditAgreementType.checked == true) {
              inputAgreementType.value = typeForEdit;
              btnAddEditPactType.innerHTML = "Edit";
       }
       else {
              inputAgreementType.value = "";
              btnAddEditPactType.innerHTML = "Add";
       }
});

checkboxEditLaborArticle.addEventListener("change", function () {
       let articleForEdit = selectLaborCodeArticle.options[selectLaborCodeArticle.selectedIndex].text;

       if (checkboxEditLaborArticle.checked == true) {
              inputLaborArticleType.value = articleForEdit;
              btnAddEditLaborArticle.innerHTML = "Edit";
       }
       else {
              inputLaborArticleType.value = "";
              btnAddEditLaborArticle.innerHTML = "Add";
       }
});

checkboxEditDepartment.addEventListener("change", function () {
       let departmentForEdit = selectDepartments.options[selectDepartments.selectedIndex].text;

       if (checkboxEditDepartment.checked == true) {
              inputDepartmentName.value = departmentForEdit;
              btnAddDepartment.innerHTML = "Edit";
       }
       else {
              inputDepartmentName.value = "";
              btnAddDepartment.innerHTML = "Add";
       }
});

//Event listener select tags
selectTagContractType.addEventListener("change", function (e) {
       let selectTagText = e.currentTarget.options[e.currentTarget.selectedIndex].text;

       if (checkboxEditAgreementType.checked == true) {
              inputAgreementType.value = selectTagText;
       }
});

selectLaborCodeArticle.addEventListener("change", function (e) {

       let selectTagText = e.currentTarget.options[e.currentTarget.selectedIndex].text;

       if (checkboxEditLaborArticle.checked == true) {
              inputLaborArticleType.value = selectTagText;
       }
});

selectDepartments.addEventListener("change", function (e) {
       let selectTagText = e.currentTarget.options[e.currentTarget.selectedIndex].text;

       if (checkboxEditDepartment.checked == true) {
              inputDepartmentName.value = selectTagText;
       }
});
//####################################################################

function openEmployeeIdDialog() {
       let valueCompanyId = document.getElementById("CompanyId").getAttribute("value");

       if (valueCompanyId == "") {
              alert("Pleace, select company from dialog box by clicking the ' Company Id ' button !");

              return;
       }

       let formAction = "/Employees/GetEmloyeesByCompany";

       let response = getRequestAgreement(formAction, valueCompanyId);

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

//#####################################################################

function openContractTypeDialog() {
       let formAction = "/AgreementTypes/GetTypes";

       let responseBody = getRequestAgreement(formAction, "");

       responseBody
              .then((value) => {
                     if (Array.isArray(value)) {
                            generateAgreementTypesDatalist(value);
                     }
              })
              .catch((err) => {
                     console.error(err);

                     alert(err);
              });

       dialogGetAgreementTypeId.show();
};

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
              url = "/AgreementTypes/CreateType";
              entityId = "0";
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/AgreementTypes/EditType";
              entityId  = selectTagContractType.value;
       }

       if (confirm(confirmText) == true) {
             
              let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              let agreementType = { Id: entityId, Type: contractTypeString };

              const request = new Request(url, {
                     method: "POST",
                     headers: {
                            RequestVerificationToken: token,
                            "Content-Type": "application/json",
                     },
                     body: JSON.stringify(agreementType),
              });

              let responseBody = post(request);

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

       if (contractIdValue != "") {
              let formAction = "/AgreementTypes/GetAgreementType";

              let responseBody = getRequestAgreement(formAction, contractIdValue)

              responseBody
                     .then((value) => {
                            if (isObjectLike(value) == true) {
                                    showAgreementType(value);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            //alert(err);
                     });
       }
};

function showAgreementType(agreementTypeVM) {
       let contractTypeValue = String(agreementTypeVM.type);

       let span = document.getElementById("agreementType-span");

       if (contractTypeValue != "") {
              span.innerHTML = contractTypeValue;
       }
};

//######################################################################

function openLaborCodeDialog() {
       let formAction = "/LaborCodeArticles/GetTypes";

       let responseBody = getRequestAgreement(formAction, "");

       responseBody
              .then((value) => {
                     if (Array.isArray(value)) {
                             generateArticlesDatalist(value);
                     }
              })
              .catch((err) => {
                     console.error(err);
                     alert(err);
              });

       dialogGetLaborCodeArticle.show();
};

function chooseLaborCodeArticle() {
       clearErrorAlertDiv(inputLaborArticleType);
       clearSuccsessDivFunction();

       let articleValue = selectLaborCodeArticle.value;

       document.getElementById("LaborCodeArticleId").setAttribute("value", articleValue);

       dialogGetLaborCodeArticle.close();
};

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
              url = "/LaborCodeArticles/CreateType";
              entityId = "0";
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/LaborCodeArticles/EditType";

              entityId = selectLaborCodeArticle.value;
       }

       if (confirm(confirmText) == true) {

              let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              let articleType = { Id: entityId, Article: laborArticleString };

              const request = new Request(url, {
                     method: "POST",
                     headers: {
                            RequestVerificationToken: token,
                            "Content-Type": "application/json",
                     },
                     body: JSON.stringify(articleType),
              });

              let responseBody = post(request);

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

       if (articleIdValue != "") {
              let formAction = "/LaborCodeArticles/GetLaborArticle";

              let responseBody = getRequestAgreement(formAction, articleIdValue)

              responseBody
                     .then((value) => {
                            if (isObjectLike(value) == true ) {
                                   showLaborCodeArticle(value);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       }
};

function showLaborCodeArticle(laborCodeActicleVM) {
       let articleValue = String(laborCodeActicleVM.article);

       let spanLaborArticle = document.getElementById("articleText-span");

       if (articleValue != "" ) {
              spanLaborArticle.innerHTML = articleValue;
       }
};

//#######################################################################

function openDepartmentDialog() {
       let formAction = "/Departments/Get";

       let responseBody = getRequestAgreement(formAction, "");

       responseBody
              .then((value) => {
                     if (Array.isArray(value)) {
                            generateDepartmentsDatalist(value);
                     }
              })
              .catch((err) => {
                     console.error(err);
                     alert(err);
              });

       dialogDepartments.show();
};

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

       if (departmentId != "") {
              let formAction = "/Departments/GetDepartment";

              let responseBody = getRequestAgreement(formAction, departmentId)

              responseBody
                     .then((value) => {
                            if (isObjectLike(value) == true) {
                                    showDepartmentName(value);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       }
};

function showDepartmentName(departmentVM) {
       let departmentName = String(departmentVM.name);

       let spanDepartmentName = document.getElementById("departmentText-span");

       if (departmentName != "") {
              spanDepartmentName.innerHTML = departmentName;
       }
};

function addDepartmentFunction() {
      /* let activeInput = document.activeElement.parentNode.parentNode.querySelector('[id*="Name"]');*/

       let departmentName = String(inputDepartmentName.value);

       let confirmText = "", url = "", entityId = "";

       let textBtnAddDepartment = btnAddDepartment.innerHTML.trim();

       if (textBtnAddDepartment === "Add") {
              confirmText = "Do you want to create a new record ?";
              url = "/Departments/Create";
              entityId = "0";
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/Departments/Edit";

              entityId = selectDepartments.value;
       }

       if (confirm(confirmText) == true) {

              let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

              let departmentVM = { DepartmentId: entityId, Name: departmentName };

              const request = new Request(url, {
                     method: "POST",
                     headers: {
                            RequestVerificationToken: token,
                            "Content-Type": "application/json",
                     },
                     body: JSON.stringify(departmentVM),
              });

              let responseBody = post(request);
              
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


//#######################################################################

async function getRequestAgreement(formAction,id ) {
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

async function post(request) {
       try {
              const response = await fetch(request);

              const result = await response.json();

              return result;

       } catch (error) {
              console.error("Error:", error);
              alert(error.message);
       }
};

function isObjectLike(value) {
       return value != null && typeof value == 'object' && !Array.isArray(value);
}

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


//var spanString = "<span  class=\"text-danger\"  style=\"color:#f44336;font-size: 14px;\" > *</span>";
//var html = $.parseHTML(spanString);

//$("#customTableBody input[name='" + newNameAttr + "']").parent("td").children("span").remove();
//$("#customTableBody input[name='" + newNameAttr + "']").parent("td").append(html);

//$("#customTableBody input[name='" + newNameAttr + "']").css("background-color", "rgb(255, 179, 179)");

//$("div.alert ul").remove("li");
//$("div.alert ul").append(html);

//$("div.alert").css("display", 'list-item');
//$("div.alert").show();

//$("#edit-success").css("display", 'none');

//######################################################################

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

//######################################################################

//$("#add-record").on("click", function () {
//       return confirm("\" Do you want to create a new record ? \"");
//});

//$("#delete-entity-btn").on("click", function () {
//       return confirm("\" Do you want to delete the selected record ? \"");
//});

//############################################## 
