

async function fetchGetRequest ( formAction, urlParams ) { 
       let url = constructUrlFunction( formAction, urlParams );

       try {
              const response = await fetch( url );

              if (!response.ok) {
                     throw new Error(`Response status: ${response.status}`);
              }

              const result = await response.json();

              return result;
       } catch (error) {
              alert(error.message);
       }
};

function constructUrlFunction ( formAction, urlParams ) {
       let getUrl = location.href.split( '/' );
       let baseUrl = getUrl[ 0 ] + '//' + getUrl[ 2 ];

       const resultUrl = new URL( formAction, baseUrl );

       if ( urlParams != null || urlParams != undefined ) {
              resultUrl.search = new URLSearchParams( urlParams ).toString();
       }

       return resultUrl;
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

function manageResponseBodyFetchPost ( responseBody, inputFieldsForValidate, successDivMessage, dataListKey ) {
       responseBody
              .then( ( value ) => {
                     if ( !valueIsModelState( value ) ) {

                            if ( ( inputFieldsForValidate instanceof HTMLInputElement || 
                                   inputFieldsForValidate instanceof HTMLCollection ) && inputFieldsForValidate != null ) {

                                   clearErrorAlertDiv();
                                   clearRedStyleFromInputTag( inputFieldsForValidate );
                                   clearSpanBeforeOrNextToInputTag( inputFieldsForValidate );
                                   clearSuccsessDivFunction();

                                   if ( Array.isArray( value ) ) {
                                          generatDataList( dataListKey, value );
                                   }
                            }

                            if ( isString( value ) ) {

                                   showSuccessDivFunction( value );
                            }
                            else if ( isString( successDivMessage ) && !stringIsNullOrEmpty( successDivMessage ) ){
                                   showSuccessDivFunction( successDivMessage );
                            } 
                     }
                     else {
                            modelStateHandlerFunction( value, inputFieldsForValidate );
                     }
              } )
              .catch( ( err ) => {
                     console.error( err );
                     alert( err );
              } );
};

//##################################################################
function isObjectLike(value) {
       return value != null && typeof value == 'object' && !Array.isArray(value);
}

function isObjectLikeArray(value) {
       return value != null && typeof value == 'object' && Array.isArray(value);
}

function stringIsNullOrEmpty(value) {
       if (value == null) return true;

       if (value != null && typeof value == "string" && value == "") return true;

       return false;
}

function isString (value) {
       if (value != null && typeof value == "string") return true;

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

function isIterable (obj) {
       if ( obj == null ) return false;

       return typeof obj[ Symbol.iterator ] === 'function';
};

//#####################################################################

function modelStateHandlerFunction(modelState, inputFieldsForValidate) { 
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
              showErrorsAlertDiv( invalidItemsList, inputFieldsForValidate );
       }
};

function showErrorsAlertDiv(invalidItemsList, activeInput) {
       let errorsMsgString = "";
       let counter = 1;

       invalidItemsList.forEach(function (value, index, array) {
              errorsMsgString += "<li><p class='my-0'>" +  counter +". Property ' " + value.key + " ' :</p>";

              value.errors.forEach(function (error) {
                     errorsMsgString += "<p class='my-0'>&emsp;<span>->&emsp;</span>" + error + "</p>";
              });

              errorsMsgString += "</li > ";

              if (isIterable(activeInput)) {
                     for (const inputTag of activeInput) {
                            if ( String( inputTag.id ).includes( value.key ) ) {
                                   inputTag.style.backgroundColor = "rgb(255, 179, 179)";
                                   showRedSpan(inputTag);
                            }
                     }
              }
              else {
                     if ( activeInput != undefined && String( activeInput.id ).includes( value.key ) ) {
                            activeInput.style.backgroundColor = "rgb(255, 179, 179)";
                            showRedSpan(activeInput); 
                     }
              }

              counter++;
       });

       let ulErrorDiv = errorDiv.querySelector("ul");
       ulErrorDiv.setAttribute("style", "list-style-type: none");

       while (ulErrorDiv.hasChildNodes()) {
              ulErrorDiv.removeChild(ulErrorDiv.firstChild);
       }

       ulErrorDiv.innerHTML = errorsMsgString;

       errorDiv.style.display = "list-item";
};

function clearErrorAlertDiv () {
       let ulErrorDiv = errorDiv.querySelector("ul");

       while (ulErrorDiv.hasChildNodes()) {
              ulErrorDiv.removeChild(ulErrorDiv.firstChild);
       }

       errorDiv.style.display = "none";
};

function showSuccessDivFunction (message ) { 
       let divInnerText = "";

       if ( typeof message == "string" ) {

              if ( message === "Add" ) {
                     divInnerText = "' Create ' operation is successful !";
              }
              else if ( message === "Edit" ) {
                     divInnerText = "' Edit ' operation is successful !";
              }
              else {
                     divInnerText = message;
              }
       }

       let divSuccess = document.getElementById("edit-success");
       let childParagraph = divSuccess.querySelector("p");

       if (childParagraph != null) {
              childParagraph.innerText = divInnerText;
       }

       divSuccess.style.display = "block";
};

function clearSuccsessDivFunction () {
       let divSuccess = document.getElementById("edit-success");
       let childParagraph = divSuccess.querySelector("p");

       if (childParagraph != null && childParagraph.innerText != "") {
              childParagraph.innerText = "";
       }

       divSuccess.style.display = "none";
};

function showRedSpan(activeInput) {
       if ( activeInput instanceof HTMLInputElement ) {
              clearSpanBeforeOrNextToInputTag( activeInput );

              let spanNearToInput = activeInput.parentNode.querySelector( "span.red-span" );
              spanNearToInput.innerHTML = "*";
       }
};

function clearRedStyleFromInputTag ( inputTags ) {
       if ( inputTags instanceof HTMLCollection ) {
              for ( const inputTag of inputTags ) {
                     console.log( inputTag );
                     inputTag.style.backgroundColor = "rgb(255, 255, 255)";
              }
              return;
       }

       inputTags.style.backgroundColor = "rgb(255, 255, 255)";
};

function clearSpanBeforeOrNextToInputTag ( activeInput ) {
       if ( activeInput instanceof HTMLInputElement ) {
              clearContentFromRedSpan( activeInput );
       }
       else if ( activeInput instanceof HTMLCollection ) {
              for ( const inputTag of activeInput ) {
                     clearContentFromRedSpan( inputTag );
              }
       }
};

function clearContentFromRedSpan(inputTag) {
       let spanAfterInput = inputTag.parentNode.querySelector("span.red-span");

       if (spanAfterInput != null) {
              inputTag.parentNode.querySelector( "span" ).innerHTML = "";
       }
};

function showAgreementSpanText (spanText, spanElement) {
       if (String(spanText) != "" && isDomElement(spanElement)) {
              spanElement.innerHTML = String(spanText);
       }
};

function removeSpanWithClassTextDanger ( parentInputTag ) {
       if ( parentInputTag instanceof HTMLInputElement ) {
              let inputId = parentInputTag.getAttribute( "id" );

              let dangerSpan = document.querySelector( `input#${inputId} + span.text-danger` );

              if ( dangerSpan != null ) {
                     dangerSpan.remove();
              }
       }
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
