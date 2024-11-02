var prevItemIndex = -1;
var currentRowIndex = -1;
var prevItemBgColor = "";
var indexMouseOver = -1;
var currentFieldIndex = 1;
/*var propHiddenValue =true;*/

const ascending = "_asc";
const descending = "_desc";
const ascArray = "&#11165;";
const descArray = "&#11167;";
const underscore = "_";
const rightArrayError = "&#129170;";


$('#customTableBody').on('click','tr' ,onRowClick);

$('#customTableBody').on("mouseover", 'tr', function () {
       indexMouseOver = $(this).index();
});

$(".edit-checkbox").on("click", function () {
       var isChecked = $(this).is(":checked");

       var inputs = $(this).parents("tr").find("input.table-field");

       if (isChecked) { 
              inputs.removeAttr("readonly");
       }
       else {
              inputs.attr("readonly",true);
       }
});

$("#edit-entities-btn").on("click", function () {
       var formAction = $(this).val();

       var actionData = getActionParameters();

       actionData["entitiesForEdit"] = getEntitiesForEdit();

       postRequest(actionData,formAction);
});

$(document).on("keypress", function (event) {
       $("#customTableBody tr").eq(indexMouseOver).trigger("click");
});

$(document).on("keydown", function (key) {
       arrowKeysPress(key);
});

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

       let addressViewIsLoaded = headerText.localeCompare("Addresses List");

       if (addressViewIsLoaded === 0) {
              let addressId = $(this).children("td").children("input.table-field").eq(0).val();
              $("input#address-id").val(addressId);
       }


       deleteEntityCaseGetEntityId();
       //####################################################

   //Employees->AllPresent View
       var inputEditCount = $("#edit-entity-id").length;
       var inputDeleteCount = $("#delete-entity-id").length;

       if (inputEditCount > 0) {
              $('#edit-entity-id').val($(this).children(':nth-child(2)').text());
       }

       if (inputDeleteCount > 0) {
              $('#delete-entity-id').val($(this).children(':nth-child(2)').text());
       }
       //####################################################
};

function deleteEntityCaseGetEntityId() {
       let editCheckBoxes = $("input.edit-checkbox:checked");

       if (editCheckBoxes.length == 1) {
              let inputEntityId = $(".edit-checkbox:checked").parents("tr").find("input[entity-id]");

              $("#entity-id-number").val(inputEntityId.val());
       }
};

function getActionParameters() {
       var data = { };

       var inputToken = $('#formGoToPage > input[name="__RequestVerificationToken"]');
       var keyToken = inputToken.attr("name");
       var valueToken = inputToken.val();
       data[keyToken] = valueToken;

       var inputPageIndex = $('#formGoToPage').find("input[name='pageIndex']"); 
       var keyPageIndex = inputPageIndex.attr("name");
       var valuePageIndex = inputPageIndex.val();
       data[keyPageIndex] = valuePageIndex;

       var inputSort = $('#formGoToPage').find("input[name='sortParam']");
       var keySort= inputSort.attr("name");
       var valueSort = inputSort.val();
       data[keySort] = valueSort;

       var inputPageSize = $('#formGoToPage').find("input[name='pageSize']");

       if (inputPageSize.length > 0) {
              var keyPageSize = inputPageSize.attr("name");
              var valuePageSize = inputPageSize.val();
              data[keyPageSize] = valuePageSize;
       }

       var inputPersonId = $('#formGoToPage').find("input[name='personId']");

       if (inputPersonId.length > 0) {
              var keyPersonId = inputPersonId.attr("name");
              var valuePersonId = inputPersonId.val();
              data[keyPersonId] = valuePersonId;
       }

       var filterInputs = $('#formGoToPage').find("input.model-filter");

       if (filterInputs.length > 0) {
              data["filter"] = getFilterObject(filterInputs);
       }

       return data;
};

function getEntitiesForEdit() {
       var selectedElements = [];

       var rowsForEdit = $(".edit-checkbox:checked").parents("tr");

       var columnsLabel = $("#privateTable > thead > tr label.column-lbl");

       //************************************************** */
       var colsTitleArray = [];

       for (var d = 0; d < columnsLabel.length; d++) {
              colsTitleArray.push(columnsLabel[d].getAttribute("value") );
       }

       if (rowsForEdit.length > 0) {
              for (var z = 0; z < rowsForEdit.length; z++) {
                     var entityElements = rowsForEdit.eq(z).children("td").children("input.table-field");
                     let notTableElements = rowsForEdit.eq(z).children("td").children("input.not-table-field");

                     var textValuesArr = [];

                     for (var i = 0; i < entityElements.length ; i++) {
                            if (!entityElements.eq(i).val() == "") {
                                   textValuesArr.push(entityElements.eq(i).val());
                            }
                            else {
                                   textValuesArr.push(null);
                            }
                     }

                     var entityForEdit = {};

                     if (colsTitleArray.length == textValuesArr.length) {
                            for (var i = 0; i < textValuesArr.length ; i++) {
                                   entityForEdit[colsTitleArray[i]] = textValuesArr[i];
                            }
                     }

                     //***************************************************** */
                     if (notTableElements.length > 0) {

                            for (var i = 0; i < notTableElements.length; i++) {

                                   let attrName = notTableElements[i].getAttribute("name");
                                   let dotIndex = attrName.indexOf(".");

                                   let key = attrName.slice(dotIndex +1);
                                   let value = notTableElements[i].getAttribute("value");

                                   if (key.startsWith("ViewTableRow")) {
                                          entityForEdit[key] = attrName;
                                   }
                                   else {
                                          entityForEdit[key] = (value =="")? null : value;
                                   }
                            }
                     }

                     //***************************************************** */
                     selectedElements.push(entityForEdit);
                     console.log(selectedElements);
              }
       }

       return selectedElements;
};

function getFilterObject(filterInputs) {
       var filterObj = {};

       filterInputs.each(function (index, element) {
              var keyFilter = $(this).attr("name");
              var valueFilter = $(this).val();

              filterObj[keyFilter] = (valueFilter == "") ? null : valueFilter;
       });

       return filterObj;
};

function postRequest(actionData, formAction) {
       $.post(formAction, actionData, function (data) {
              alert("success");

              postResponse(data);
       });
};

function postResponse(data) {
       var enumValid = {isValid:2, isNotValid:1};

       var arrModelState = [];

       $.each(data, function (key, obj) {
              arrModelState.push({ key: key, value: obj });
       });

       var viewTableRowArr = arrModelState.filter(x => x.key.includes("ViewTableRow"));

       var arrTableRowsName = [];

       $.each(viewTableRowArr, function (index, item) {
              let dotIndexKey = item.key.indexOf(".");
              let dotIndexValue = item.value.rawValue.indexOf(".");

              let rowKey = item.key.slice(0,dotIndexKey);
              let rowValue = item.value.rawValue.slice(0,dotIndexValue);

              arrTableRowsName.push({ key: rowKey, value: rowValue });
       });

       var invalidItems = arrModelState.filter(x => x.value.validationState == enumValid.isNotValid );

       if (invalidItems.length > 0) {
              $("div.alert ul li").remove();

              $.each(invalidItems, function (index, item) {
                    var itemErrors = $.map(item.value.errors,function (x) {
                            return x;
                     });

                     let dotIndex = item.key.indexOf(".");

                     let fieldNameText = item.key.slice(dotIndex + 1);

                     let oldRowText = item.key.slice(0, dotIndex);
                     let replacement = arrTableRowsName.find(x => x.key == oldRowText);
                     let newNameAttr = replacement.value + item.key.slice(dotIndex);

                     let leftBracket = replacement.value.indexOf("[");
                     let rightBracket = replacement.value.indexOf("]");
                     let rowText = replacement.value.substring(leftBracket + 1, rightBracket);

                     let actualTableRow = parseInt(rowText) + 1;
/*                     console.log(actualTableRow);*/

                     var errorsMsgString = "<li><p>Property : " + fieldNameText + " - row " + actualTableRow  + " of the table :</p>";

                     for (var z = 0; z < itemErrors.length; z++) {
                            var anyError = "<p>" + rightArrayError + "  " + itemErrors[z].errorMessage + "</p>";
                            errorsMsgString = errorsMsgString + anyError
                     }

                     errorsMsgString = errorsMsgString + "</li > ";

                     var html = $.parseHTML(errorsMsgString);

                     $("div.alert ul").remove("li");
                     $("div.alert ul").append(html);

                     var spanString = "<span  class=\"text-danger\"  style=\"color:#f44336;font-size: 14px;\" > *</span>";
                     var html = $.parseHTML(spanString);

                     $("#customTableBody input[name='" + newNameAttr + "']").parent("td").children("span").remove();
                     $("#customTableBody input[name='" + newNameAttr + "']").parent("td").append(html);

                     $("#customTableBody input[name='" + newNameAttr + "']").css("background-color", "rgb(255, 179, 179)");
              });

              $("div.alert").css("display", 'list-item');
              $("div.alert").show();  

              $("#edit-success").css("display", 'none');
       }

       if (invalidItems.length == 0) {
              $("div.alert").css("display", 'none');
              $("div.alert").hide();  

              $("#edit-success").css("display", 'list-item');
              $("#edit-success").show();

              $("#customTableBody input").parent("td").children("span").remove();

              $(".edit-checkbox:checked").prop("checked", false);

             var inputTableFields = $("#customTableBody").find("input.table-field");

              console.log(inputTableFields.length);

              inputTableFields.attr("readonly", true);
       }
};

function arrowKeysPress(key) {

       switch (key.which) {
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
};

function upArrowPress() {
       if (currentRowIndex > 0) {
              currentRowIndex -= 1;
              $('#customTableBody tr').eq(currentRowIndex).trigger("click");

              var rowTextFields = $('#customTableBody tr').eq(currentRowIndex).find("input.table-field");
              rowTextFields.eq(currentFieldIndex).trigger("focus");
       }
};

function downArrowPress() {
       var tableRowCount = $('#customTableBody tr').length;
       if (currentRowIndex < tableRowCount -1) {
              currentRowIndex += 1;
              $('#customTableBody tr').eq(currentRowIndex).trigger("click");

              var rowTextFields = $('#customTableBody tr').eq(currentRowIndex).find("input.table-field");
              rowTextFields.eq(currentFieldIndex).trigger("focus");
       }
};

function leftArrowPress() {
       var rowTextFields = $('#customTableBody tr').eq(currentRowIndex).find("input.table-field");

       if (currentFieldIndex > 1) {
              currentFieldIndex -= 1;
              rowTextFields.eq(currentFieldIndex).trigger("focus"); 
       }
};

function rightArrowPress() {
       var rowTextFields = $('#customTableBody tr').eq(currentRowIndex).find("input.table-field");
       var textFieldsCount = rowTextFields.length;

       if (currentFieldIndex < textFieldsCount-1) {
              currentFieldIndex += 1;
              rowTextFields.eq(currentFieldIndex).trigger("focus");
       }
};

function changeCurrentRowBgColor(currentRow) {
       var rowInputs = currentRow.find("input");

       currentRow.css("background-color", 'rgb(231, 255, 245 )'); //231, 255, 245 <> 206, 254, 235
       rowInputs.css("background-color", 'rgb(231, 255, 245 )');
};


/*##########################################*/

$(".table-sortBtn").on('click', function () {
       var sortLabelValue = $(this.getElementsByTagName("label")).text();
       sortLabelValue = sortLabelValue.replace(" ", "");

       var sortNameAsc = sortLabelValue + ascending;
       var sortNameDesc = sortLabelValue + descending;

       var inputSort = $(this.parentElement.getElementsByClassName("input-sort"));
       var inputSortValue = $(this.parentElement.getElementsByClassName("input-sort")).val();

       if (inputSortValue == "" || inputSortValue != sortNameDesc) {
              inputSort.val(sortNameDesc);
       }
       else if (inputSortValue == sortNameDesc) {
              inputSort.val(sortNameAsc);
       }
       else if (inputSortValue == sortNameAsc) {
              inputSort.val(sortNameDesc);
       }

       $(this).trigger("blur");
});

$(function () {
       changeSortButtons();

       if ($("#addresses-table-div").prop("hidden") == false) {

              attachDetachBtnsAreDisabled($("input.checkAddressType"));
       }
});

function changeSortButtons() {
       $(".table-sortBtn").each(function (index) {
              var sortLabelValue = $(this.getElementsByTagName("label")).text();
              sortLabelValue = sortLabelValue.replace(" ", "");

              var inputSortValue = $(this.parentElement.getElementsByClassName("input-sort")).val();
              var underscoreIndex = inputSortValue.indexOf(underscore);

              var modifiedSortVal = inputSortValue.substring(0, underscoreIndex);
              var sortType = underscore + inputSortValue.substring(underscoreIndex + 1);

              if (sortLabelValue == modifiedSortVal) {
                     $(this).css("color", 'rgb(204, 0, 82 )');
                     if (sortType == ascending) {
                            addArraySymbolSpan($(this), ascArray);
                     }
                     else {
                            addArraySymbolSpan($(this), descArray);
                     }
              }
       });
};

function addArraySymbolSpan(button, arraySymbol) {
       var spanText = "<span>" + arraySymbol + "</span>";
       button.prepend(spanText);
};

/*############################################## */
$(".final-btn").on("click", function () {
       $(this).trigger("blur");
});

$(".person-item-btn").on("click", function () {
       $(this).trigger("blur");
});

$("#add-entity").on("click", function () {
       var addRow = $("tr#last-row-add");

       var attr = addRow.attr("hidden");

       if (typeof attr !== 'undefined' && attr !== false) {
              addRow.removeAttr("hidden");
       }
       else {
              addRow.attr("hidden",true);
       }
});

$("#add-record").on("click", function () {
       return confirm("\" Do you want to create a new record ? \"");
});

$("#delete-entity-btn").on("click", function () {
       return confirm("\" Do you want to delete the selected record ? \"");
});

$("#go-back-btn.employees").on("click", function(){
       document.location = "/Employees/Index";
});

$("#go-back-btn.diplomas").on("click", function () {
       document.location = "/Persons/AllPersons";
});


//**************************************************************/
// -- > Address View <--

$("button.manage-btn").on("click", function () {
       $(this).trigger("blur");
});

$("#create-address-btn").on("click", function () {
       showHideElement($("#change-address-form"));

       clearAddressFotm();
});

$("#edit-address-btn").on("click", function () {
       showHideElement($("#change-address-form"));

       let addressForEdit = getAddress();

       fillAddressForm(addressForEdit);
});

$("#showAddressesBtn").on("click", function () {
       showHideElement($("#addresses-table-div"));
});

$("input.checkAddressType").on("click", function () {
       let arrayChecks = $("input.checkAddressType").toArray();

       if ($(this).is(":checked") == true) {
             
              let checkBoxIndex = $("input.checkAddressType").index($(this));

              $.each(arrayChecks, function (index, value) {
                     if (index != checkBoxIndex) {
                            $(value).prop("checked", false);
                     }
              });

              if ($("#addresses-table-div").prop("hidden") == false) {
                     $("#attachAddressButton").prop("disabled", false);
                     $("#detachAddressButton").prop("disabled", false);
              }

              $("input#attachAddressType").val($(this).val());
              $("input#detachAddressType").val($(this).val());
       }

       attachDetachBtnsAreDisabled($("input.checkAddressType"));
});

function attachDetachBtnsAreDisabled(checkBoxElements) {
       let arrayCheckBoxes = $(checkBoxElements).toArray();

       let isChecked = (element) => $(element).is(":checked") == false;

       let allCheckBoxIsUnchecked = arrayCheckBoxes.every(isChecked);

       if (allCheckBoxIsUnchecked == true) {
              $("#attachAddressButton").prop("disabled", true);
              $("#detachAddressButton").prop("disabled", true);

              $("input#attachAddressType").val("");
              $("input#detachAddressType").val("");
       }
};

function showHideElement(element) {
       let propHiddenValue = $(element).prop("hidden");

       if (propHiddenValue == true) {
              $(element).prop("hidden",false);
       }
       else {
              $(element).prop("hidden",true);
       }
};

function getAddress() {
       let address = {};

       if (currentRowIndex > -1) {
              let selectedRow = $("#customTableBody > tr").eq(currentRowIndex);

              let addressFields = $(selectedRow).children("td").children(".table-field");

              $(addressFields).each(function (i) {
                     let dotIndex = $(this).attr("name").indexOf(".");

                     let modelName = $(this).attr("name").slice(dotIndex +1);
                     let modelValue = $(this).val();

                     address[modelName] = modelValue;
              });

              return address;
       }

       return address;
};

function fillAddressForm(addressForEdit) {
       for (let x in addressForEdit) {
              let resultInput = $("#newAddressForm input[id$='" + x + "']");

              $(resultInput).val(addressForEdit[x]);
       };
};

function clearAddressFotm() {
       let inputsInForm = $("#newAddressForm input.address-field");

       $(inputsInForm).each(function (i) {

              let attrId = $(this).attr("id");

              if (attrId.endsWith("Id")) {
                     $(this).val(0);
              }
              else {
                     $(this).val("");
              }
       });
};


/*resultText += $(resultInput).val() + " ; ";*/
/*text += addressForEdit[x] + " ";*/
/* text += x + " ; ";*/

//let resultInput = $("#newAddressForm input[id$='Id']");
/* "input[name~='man']"*/
/*  let resultInput = $(inputsInForm).filter("input[id~='" + x + "']");*/
//console.log($(resultInput).attr("id"));
