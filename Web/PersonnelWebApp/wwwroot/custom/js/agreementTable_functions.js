
var prevItemIndex = -1;
var currentRowIndex = -1;
var prevItemBgColor = "";
var indexMouseOver = -1;
var currentFieldIndex = 1;

const ascending = "_asc";
const descending = "_desc";
const ascArray = "&#11165;";
const descArray = "&#11167;";
const underscore = "_";
const rightArrayError = "&#129170;";

$( ".final-btn" ).on( "click", function () {
       $( this ).trigger( "blur" );
} )

$('#customTableBody').on('click', 'tr', onRowClick)

$('#customTableBody').on("mouseover", 'tr', function () {
       indexMouseOver = $(this).index();
})

$(document).on("keypress", function (event) {
       $("#customTableBody tr").eq(indexMouseOver).trigger("click");
})

$(document).on("keydown", function (key) {
       arrowKeysPress(key);
} )

function arrowKeysPress ( key ) {

       switch ( key.which ) {
              case 38:    //up arrow key
                     upArrowPress();
                     break;
              case 40:    //bottom arrow key
                     downArrowPress();
                     break;
              case 37:    //left arrow key alert("left arrow ");
                     leftArrowPress();
                     break;
              case 39:    //right arrow key alert("Right arrow ");
                     rightArrowPress();
                     break;
       }
}

function upArrowPress () {
       if ( currentRowIndex > 0 ) {
              currentRowIndex -= 1;
              $( '#customTableBody tr' ).eq( currentRowIndex ).trigger( "click" );

              var rowTextFields = $( '#customTableBody tr' ).eq( currentRowIndex ).find( "input.table-field" );
              rowTextFields.eq( currentFieldIndex ).trigger( "focus" );
       }
}

function downArrowPress () {
       var tableRowCount = $( '#customTableBody tr' ).length;
       if ( currentRowIndex < tableRowCount - 1 ) {
              currentRowIndex += 1;
              $( '#customTableBody tr' ).eq( currentRowIndex ).trigger( "click" );

              var rowTextFields = $( '#customTableBody tr' ).eq( currentRowIndex ).find( "input.table-field" );
              rowTextFields.eq( currentFieldIndex ).trigger( "focus" );
       }
}

function leftArrowPress () {
       var rowTextFields = $( '#customTableBody tr' ).eq( currentRowIndex ).find( "input.table-field" );

       if ( currentFieldIndex > 1 ) {
              currentFieldIndex -= 1;
              rowTextFields.eq( currentFieldIndex ).trigger( "focus" );
       }
}

function rightArrowPress () {
       var rowTextFields = $( '#customTableBody tr' ).eq( currentRowIndex ).find( "input.table-field" );
       var textFieldsCount = rowTextFields.length;

       if ( currentFieldIndex < textFieldsCount - 1 ) {
              currentFieldIndex += 1;
              rowTextFields.eq( currentFieldIndex ).trigger( "focus" );
       }
}

function changeCurrentRowBgColor ( currentRow ) {
       var rowInputs = currentRow.find( "input" );

       currentRow.css( "background-color", 'rgb(231, 255, 245 )' ); //231, 255, 245 <> 206, 254, 235
       rowInputs.css( "background-color", 'rgb(231, 255, 245 )' );
}

function onRowClick() {
       if (prevItemIndex > -1) {
              var prevRow = $('#customTableBody tr').eq(prevItemIndex);
              var prevRowInputs = prevRow.find("input");

              prevRow.css("background-color", prevItemBgColor);
              prevRowInputs.css("background-color", prevItemBgColor);
       }

       currentRowIndex = $(this).index();
       prevItemIndex = $(this).index();
       prevItemBgColor = $(this).css("background-color");

       changeCurrentRowBgColor($(this));

       let headerText = $("#private-header").text();
       let personViewIsLoaded = headerText.localeCompare("Persons List");

       if (personViewIsLoaded === 0) {
              let id = $(this).children("td").children("input.table-field").eq(0).val();
              $("input.person-id").val(id);
       }

       //let addressViewIsLoaded = headerText.localeCompare("Addresses List");

       //if (addressViewIsLoaded === 0) {
       //       let addressId = $(this).children("td").children("input.table-field").eq(0).val();
       //       $("input#address-id").val(addressId);
       //}

       /*deleteEntityCaseGetEntityId();*/
       //####################################################

       ////Employees->AllPresent View
       //var inputEditCount = $("#edit-entity-id").length;
       //var inputDeleteCount = $("#delete-entity-id").length;

       //if (inputEditCount > 0) {
       //       $('#edit-entity-id').val($(this).children(':nth-child(2)').text());
       //}

       //if (inputDeleteCount > 0) {
       //       $('#delete-entity-id').val($(this).children(':nth-child(2)').text());
       //}
       //####################################################
}

//#############################################################

$( "button.new-sort-btn" ).on( 'click', function () {
       let sortLabelValue = $( this ).children( "label" ).text();
       sortLabelValue = sortLabelValue.replace( / /g, "" );

       let sortNameAsc = sortLabelValue + ascending;
       let sortNameDesc = sortLabelValue + descending;

       let inputSortValue = $( "#new-input-sort" ).val();

       if ( inputSortValue == "" || inputSortValue != sortNameDesc ) {
              $( "#new-input-sort" ).val( sortNameDesc );
       }
       else if ( inputSortValue == sortNameDesc ) {
              $( "#new-input-sort" ).val( sortNameAsc );
       }
       else if ( inputSortValue == sortNameAsc ) {
              $( "#new-input-sort" ).val( sortNameDesc );
       }

       console.log( "input  sort has value : " + $( "#new-input-sort" ).val() );

       $( this ).trigger( "blur" );
} )

$( function () {
       changeNewSortButtons();

       if ( $( "#addresses-table-div" ).prop( "hidden" ) == false ) {

              attachDetachBtnsAreDisabled( $( "input.checkAddressType" ) );
       }
} )

function changeNewSortButtons () {
       $( "button.new-sort-btn" ).each( function ( index ) {
              let sortLabelValue = $( this ).children( "label" ).text();
              sortLabelValue = sortLabelValue.replace( / /g, "" );

              let inputSortValue = $( "#new-input-sort" ).val();
              let underscoreIndex = inputSortValue.indexOf( underscore );

              let modifiedSortVal = inputSortValue.substring( 0, underscoreIndex );
              let sortType = underscore + inputSortValue.substring( underscoreIndex + 1 );

              if ( sortLabelValue == modifiedSortVal ) {
                     $( this ).css( "color", 'rgb(204, 0, 82 )' );
                     if ( sortType == ascending ) {
                            addArraySymbolSpan( $( this ), ascArray );
                     }
                     else {
                            addArraySymbolSpan( $( this ), descArray );
                     }
              }
       } );
}

function addArraySymbolSpan ( button, arraySymbol ) {
       var spanText = "<span>" + arraySymbol + "</span>";
       $( button ).prepend( spanText );
}

//#############################################################

function moveToNextPage () {

       let inputPageIndex = document.getElementById( "go-to-page" );

       let pageIndexValue = Number.parseInt( inputPageIndex.getAttribute( "value" ) );

       inputPageIndex.setAttribute( "value", pageIndexValue + 1 );

       let value = inputPageIndex.getAttribute( "value" );

       document.getElementById( "main-paging-form" ).submit();
}

function moveToPreviousPage () {

       let inputPageIndex = document.getElementById( "go-to-page" );

       let pageIndexValue = Number.parseInt( inputPageIndex.getAttribute( "value" ) );

       inputPageIndex.setAttribute( "value", pageIndexValue - 1 );

       let value = inputPageIndex.getAttribute( "value" );

       document.getElementById( "main-paging-form" ).submit();
}

function clearSearchFilterSettings () {

       let searchInputsList = document.getElementsByClassName( "input-baseSearchForm" );

       for ( let i = 0; i < searchInputsList.length; i++ ) {

              searchInputsList[ i ].setAttribute( "value", "" );
       }

       document.getElementById( "agreement-base-search-form" ).submit();
}

//###############################################################

function onCompanyChange () {
       document.getElementById( "changeCompanyForm" ).submit();
}

function submitAgreementBaseSearchForm () {
       document.getElementById( "agreement-base-search-form" ).submit();
}

//###################################################################
function createAgreementFunction () {
       getCompanyId();

       let actionAttrValue = "/LaborAgreements/Agreements/Create";
       submitCreateEditForm( actionAttrValue );
}

let editContractAlert = "Please select a contract by checking the appropriate check box. !";

function editAgreementFunction () {
       if ( !companyAndContractWereChosen() ) return;

       let actionAttrValue = "/LaborAgreements/Agreements/Edit";

       submitCreateEditForm( actionAttrValue );
}

function getCompanyId () {
       let selCompanyValue = Number( document.getElementById( "selectCompany" ).value );

       if ( selCompanyValue < 0 ) return false;

       let companyIdInput = document.getElementById( "inputCompanyId" );

       companyIdInput.setAttribute( "value", selCompanyValue );

       return true;
}

function companyAndContractWereChosen () {
       if ( !getCompanyId() ) {
              alert( "Please select a company from the 'Select Company' menu !" );

              return false;
       }

       if ( currentRowIndex < 0 ) {
              alert( editContractAlert );

              return false;
       }

       let selectedRow = document.getElementById( "customTableBody" )
                                                          .getElementsByTagName( "tr" )
                                                          .item( currentRowIndex );

       let selectedCheckBox = selectedRow.querySelector( "input[type='checkbox']" );

       if ( selectedCheckBox.checked ) {
              let contractIdValue = selectedRow.querySelector( "input[id*='__Id']" ).getAttribute( "value" );
              document.getElementById( "inputAgreementId" ).setAttribute( "value", contractIdValue );

              let editTableRow = selectedRow.querySelector( "input[id*='__IsRegistered']" ).getAttribute( "name" );
              document.getElementById( "inputViewTableRow" ).setAttribute( "value", editTableRow );
       }
       else {
              alert( editContractAlert );

              return false;
       }

       return true;
}

function submitCreateEditForm ( formAction ,formTarget) {
       let formElement = document.getElementById( "openCreateEditView-form" );

       formElement.target = ( !stringIsNullOrEmpty( formTarget ) ) ? formTarget : "_self" ;

       formElement.setAttribute( "action", formAction );

       formElement.submit();
}

//##############################################################

const checkBoxesEdit = document.querySelectorAll( '.edit-checkbox' );

checkBoxesEdit.forEach( function ( btn, index ) {
       btn.addEventListener( 'click', () => {
              if ( btn.checked ) {
                     for ( var y = 0; y < checkBoxesEdit.length; y++ ) {
                            if ( y != index ) {
                                   checkBoxesEdit[ y ].checked = false;
                            }
                     }
              }
       } );
} );

document.getElementById( "create-contract-btn" ).addEventListener( "click", createAgreementFunction );

document.getElementById( "edit-contract-btn" ).addEventListener( "click", editAgreementFunction );

document.getElementById( "goBackToEmpFilesBtn" ).addEventListener( "click", function () {
       document.location = "/Employees/Index";
} );

const tempFileBtns = document.getElementsByClassName( "get-temp-file" );

for ( let button of tempFileBtns ) {
       button.addEventListener( "click", ( event ) => {
              let btnValue = event.target.getAttribute( "value" );

              if ( !companyAndContractWereChosen() ) return;

              document.getElementById( "inputFileType" ).setAttribute( "value", btnValue );

              let actionAttrValue = "/LaborAgreements/Agreements/Details";

              let formTarget = "_blank";

              submitCreateEditForm( actionAttrValue, formTarget );
       } );
};
//document.getElementById( "contract-details-btn" ).addEventListener( "click", e => {
//       if ( !companyAndContractWereChosen() ) return;

//       let actionAttrValue = "/LaborAgreements/Agreements/Details";

//       let formTarget = "_blank";

//       submitCreateEditForm( actionAttrValue, formTarget );
//} );




