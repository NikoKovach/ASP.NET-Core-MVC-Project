var innerTextPlaceBtn = "-1";

const dialogNewAddress = document.getElementById("newAddressDialog");
const dialogSelectAddress = document.getElementById("selectAddressDialog");

const selectAddresses = document.getElementById("addressesList-select");

const btnsPlaceAddress = document.getElementsByClassName("place-address-btn");
const btnSaveAddress = document.getElementById("save-address-btn");

const inputAddressId = document.getElementsByClassName("address-input");
const inputFilterCollection = document.getElementById("filter-address-div")
                                                                      .getElementsByClassName("filter-input");

const inputListNewAddress = dialogNewAddress.getElementsByClassName( "add-address-tag" );

const  token = document.querySelector( 'input[name="__RequestVerificationToken"]' ).getAttribute( "value" );

for (var i = 0; i < btnsPlaceAddress.length; i++) {
       btnsPlaceAddress[i].addEventListener("click", function () {
              innerTextPlaceBtn = String(this.innerHTML).trim() ;

              dialogSelectAddress.show();
       }, false);
};

for (var i = 0; i < inputAddressId.length; i++) {
       inputAddressId[i].addEventListener("mouseover", function (e) {
             /* let addressIdValue = this.value;*/
              let currentInputId = e.target.getAttribute("id") ;

              if (this instanceof HTMLInputElement) {
                     let spanElement = document.querySelector(`input#${currentInputId} + span`) ;
                     let urlParams = { id: String( this.value ),  returnType : "string"};
                     getFullAddressFunction(urlParams,spanElement);
              }     
       }, false);
};

btnSaveAddress.addEventListener( "click", createEditAddressFunction );

//#########################################################################
function showAddAddressDialog () {
       let checkBoxEditAddress = document.getElementById( "editAddressCheckbox" );

       if ( checkBoxEditAddress.checked == true ) {
              btnSaveAddress.innerHTML = "Update";
              let urlParams = { id: String( selectAddresses.value ) };

              getFullAddressFunction( urlParams );
       }
       else {
              btnSaveAddress.innerHTML = "Create";
       }

       dialogNewAddress.show();
};

function showAddressForEditInDialogBox (addressVM) {
       if ( addressVM instanceof Object && isObjectLike( addressVM ) ) {
              for ( let key in addressVM ) {
                     let formattedKey = String( key ).charAt( 0 ).toUpperCase() + String( key ).slice( 1 );

                     let addressInput = dialogNewAddress.querySelector( `input[id*=${formattedKey}]` );

                     if ( addressInput == null ) continue;

                     addressInput.value = addressVM[key];
                     //console.log( key + " : " + formattedKey );
                     //console.log(addressInput);
              };
       }
      
};

function closeNewAddressDialogBox() {
       dialogNewAddress.close();

       clearRedStyleFromInputTag( inputListNewAddress );
       clearSpanBeforeOrNextToInputTag( inputListNewAddress );
       //clearErrorAlertDiv( inputLaborArticleType );
       //clearSuccsessDivFunction();
       findAddressesFunction();
};

//####################################################

function findAddressesFunction () {
       const request = addressesFindRequest();

       let responseBody = fetchPost( request );

       let dataListKey = "Addresses";

       manageResponseBodyFetchPost( responseBody, inputFilterCollection, "", dataListKey );

       /*let confirmText = "Do you want to proceed ?";*/
      /* if (confirm(confirmText) == true) {*/ 
       /*};*/
};

function addressesFindRequest() {
       let url = "/Addresses/GetAddresses";

       let filterAddressVM = takeFilterAddressVMObject();

       return  generatePostRequest(url,token,filterAddressVM);
};

function takeFilterAddressVMObject() {
       let searchAddressVM = { Country: "", City: "", Street: "" , Number : ""};
       let objKeys = Object.keys(searchAddressVM);

       if (inputFilterCollection.length == objKeys.length ) {
              objKeys.forEach(function (value, index) {
                     let key = value;
                    
                     let filterInputVal = document.getElementById("filter-address-div").querySelector("input[id*=" + key + "]");
                     
                     searchAddressVM[key] = !stringIsNullOrEmpty(filterInputVal.value) ? filterInputVal.value : null ;
              });
       }

       return searchAddressVM;
};

function collectAddEditAddressDataFunction () {
       let addressVM = {
              Id: "",
              Country: "",
              Region: "",
              Municipality: "",
              City: "",
              Street: "",
              Number: "",
              Floor: "",
              ApartmentNumber: ""
       };

       let keysList = Object.keys(addressVM);

       if ( inputListNewAddress.length == keysList.length ) {
              keysList.forEach(function (key) {
                     let addressInputVal = dialogNewAddress.querySelector(`input[id*=${key}]`).value;

                     addressVM[ key ] = !stringIsNullOrEmpty(addressInputVal) ? addressInputVal : null;
              });
       }

       return addressVM;
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

       if (!String(addressId) == "") {
              dialogSelectAddress.close();
       };
};

function getFullAddressFunction ( urlParams, spanElement ) { 
       let url = "/Addresses/GetFullAddress";

       let responseBody = fetchGetRequest( url, urlParams );

       responseBody
              .then( ( value ) => {
                     if ( isString( value ) && !stringIsNullOrEmpty( value ) ) {
                                   showAgreementSpanText( value, spanElement );
                     }
                     else if ( isObjectLike( value ) ) {
                            showAddressForEditInDialogBox( value );
                     }               
              } )
              .catch( ( err ) => {
                     console.error( err );
                     alert( err );
              } );
};

function createEditAddressFunction () {
       let confirmText = "", url = "", entityId = ""; let addressVM = {};

       addressVM = collectAddEditAddressDataFunction();
       
       let saveBtnText = btnSaveAddress.innerHTML.trim();

       if (saveBtnText === "Create") {
              confirmText = "Do you want to create a new address ?";
              url = "/Addresses/Add";
              addressVM[ "Id" ] = "0";
       }
       else {
              confirmText = "Do you want to edit the existing address ?";
              url = "/Addresses/Update";
              addressVM[ "Id" ] = selectAddresses.value ;
       }

       if (confirm(confirmText) == true) {
              const request = generatePostRequest(url, token, addressVM);
             
              let responseBody = fetchPost( request );

              manageResponseBodyFetchPost( responseBody, inputListNewAddress );
       }
};

//#########################################################################
//console.log( addressVM );
// "/Addresses/AddPost/CreateAddress" ;; url = "/Addresses/Add";
//"/Addresses/Update/EditAddress";
/*
                    {
                            "id": 22,
                            "addressType": null,
                            "hasBeenDeleted": false,
                            "personId": null,
                            "country": "Bulgaria",
                            "region": "Plovdiv",
                            "municipality": null,
                            "city": "Plovdiv",
                            "street": "September the 6th",
                            "number": 111,
                            "entrance": null,
                            "floor": null,
                            "apartmentNumber": null,
                            "viewTableRow": null
                    }
                    */

