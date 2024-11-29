//#################### JQuery script ###########################################

$(".stylish-btn").on("click", function () {
       $(this).trigger("blur");
});

//################### Clear Java Script #################################

const dialogGetCompanyId = document.getElementById("selectCompanyDialog");
const dialogGetEmployeeId = document.getElementById("selectEmployeeDialog");
const dialogGetAgreementTypeId = document.getElementById("selectAgreementTypeDialog");
const dialogGetLaborCodeArticle = document.getElementById("selectLaborCodeArticleDialog");

const btnOpenCompanyDialog = document.getElementById("btnOpenCompanyIdDialog");
const btnGetCompanyId = document.getElementById("btnGetCompanyId");
const btnAddEditPactType = document.getElementById("btnAddEditAgreementType");
const btnAddEditLaborArticle = document.getElementById("btnAddEditArticle");

const inputSelCompanyId = document.getElementById("selCompanyId-input");
const inputSelEmployeeId = document.getElementById("selEmployeeId-input");
const inputAgreementType = document.getElementById("createAgreementType-input");
const inputLaborArticleType = document.getElementById("createLaborCode-input");

const selectTagContractType = document.getElementById("agreementType-select");
const selectLaborCodeArticle = document.getElementById("articleLaborCode-select");

const checkboxEditAgreementType = document.getElementById("checkbox-edit-contractType");
const checkboxEditLaborArticle = document.getElementById("checkbox-edit-laborCode");

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
/*       console.log(responseBody);*/

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
              console.log(agreementType);

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

                                   console.log(value);
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

              console.log(entityId);
       }
       else {
              confirmText = "Do you want to edit the existing record ?";
              url = "/LaborCodeArticles/EditType";

              entityId = selectLaborCodeArticle.value;
              console.log(entityId);
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
              console.log(request);
              let responseBody = post(request);

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

function isObjectLike(value) {
       return value != null && typeof value == 'object' && !Array.isArray(value);
}
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

//#######################################################################





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

//              /*console.log("Success:", result);*/

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
