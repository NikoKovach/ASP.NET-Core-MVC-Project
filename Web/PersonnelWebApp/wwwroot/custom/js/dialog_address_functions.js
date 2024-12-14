var innerTextPlaceBtn = "-1";

//const enumPlaceArr = ["place of conclusion" , "place of work"];

const dialogNewAddress = document.getElementById("newAddressDialog");
const dialogSelectAddress = document.getElementById("selectAddressDialog");

const selectAddresses = document.getElementById("addressesList-select");

const btnsPlaceAddress = document.getElementsByClassName("place-address-btn");

const inputAddressId = document.getElementsByClassName("address-input");

for (var i = 0; i < btnsPlaceAddress.length; i++) {
       btnsPlaceAddress[i].addEventListener("click", function () {
              innerTextPlaceBtn = String(this.innerHTML).trim() ;

              dialogSelectAddress.show();
       }, false);
};


for (var i = 0; i < inputAddressId.length; i++) {
       inputAddressId[i].addEventListener("mouseover", function (e) {
              let addressIdValue = this.value;
              let currentInputId = e.target.getAttribute("id") ;

              if (this instanceof HTMLInputElement) {
                     let spanElement = document.querySelector(`input#${currentInputId} + span`) ;

                     getFullAddressFunction(addressIdValue,spanElement);
              }     
       }, false);
};

function showAddAddressDialog() {
       dialogNewAddress.show();
};

function closeNewAddressDialogBox() {
       dialogNewAddress.close();
};

//####################################################

function findAddressesFunction() {
       let confirmText = "Do you want to proceed ?";

       if (confirm(confirmText) == true) {
              const request = addressesFindRequest();

              let responseBody = fetchPost(request);

              responseBody
                     .then((value) => {
                            if (!valueIsModelState(value)) {
                                   generateSelectAddressesList(value);

                                   //clearErrorAlertDiv(inputDepartmentName);
                                   //showSuccsessDivFunction(textBtnAddDepartment);
                            }
                            else {
                                   console.log("ModelState : " + value);
                                   /*modelStateHandlerFunction(value, inputDepartmentName);*/
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       };
};

function addressesFindRequest() {
       let url = "/Addresses/GetAddresses";

       let token = document.querySelector('input[name="__RequestVerificationToken"]').getAttribute("value");

       let filterAddressVM = takeFilterAddressVMObject();

       const searchRequest = new Request(url, {
              method: "POST",
              headers: {
                      RequestVerificationToken: token,
                     "Content-Type": "application/json",
              },
              body: JSON.stringify(filterAddressVM),
       });

       return searchRequest;
};

function takeFilterAddressVMObject() {
       let inputCollection = document.getElementById("filter-address-div").getElementsByClassName("filter-input");

       let searchAddressVM = { Country: "", City: "", Street: "" , Number : ""};
       let objKeys = Object.keys(searchAddressVM);

       if (inputCollection.length == objKeys.length ) {
              objKeys.forEach(function (value, index) {
                     let key = value;
                    
                     let filterInputVal = document.getElementById("filter-address-div").querySelector("input[id*=" + key + "]");
                     
                     searchAddressVM[key] = !stringIsNullOrEmpty(filterInputVal.value) ? filterInputVal.value : null ;
              });
       }

       return searchAddressVM;
};

function generateSelectAddressesList(addressesList) {
       if (isObjectLikeArray(addressesList) && addressesList != null) {
              let selectTagOptions = "";

              addressesList.forEach((element,index) => {
                     selectTagOptions += `<option value=${element.id}>${element.address}</option>`;
              });

              selectAddresses.innerHTML = selectTagOptions;
       }
};

function chooseAddressIdfunction() {
       //clearErrorAlertDiv(inputDepartmentName);
       //clearSuccsessDivFunction();

       let addressId = selectAddresses.value;

       if (innerTextPlaceBtn == "Place Id") {
              document.getElementById("PlaceId").setAttribute("value", addressId);
       }
       else {
              document.getElementById("WorkPlaceId").setAttribute("value", addressId);
       }

       dialogSelectAddress.close();
};

function getFullAddressFunction(addressId,spanElement) {
      
       if (String(addressId) != "") {
              let url = "/Addresses/GetFullAddress";

              let responseBody = fetchGetRequest(url, String(addressId))

              responseBody
                     .then((value) => {
                            if (!stringIsNullOrEmpty(value)) {
                                   showAgreementSpanText(value,spanElement);
                            }
                     })
                     .catch((err) => {
                            console.error(err);
                            alert(err);
                     });
       }
};

//#########################################################################
function catchModelStateFunction(modelState) {
       const validityValuesEnum = { isValid: 2, isNotValid: 1 };

       let invalidItemsList = [];

       for (let entityName in modelState) {
              console.log(modelState[entityName]);

              //if (modelState[entityName].validationState != validityValuesEnum.isNotValid) {
              //       continue;
              //}

              //let keysArray = Object.keys(modelState[entityName]);

              //let errorMessages = [];

              //if (keysArray.includes("errors")) {
              //       let errorsList = modelState[entityName].errors;

              //       errorMessages = errorsList.map(function (item) {
              //              return String(item.errorMessage);
              //       });
              //}

              //invalidItemsList.push({ key: entityName, errors: errorMessages });
       }

       //if (invalidItemsList.length > 0) {
       //       showErrorsAlertDiv(invalidItemsList, activeInput);
       //}
};

