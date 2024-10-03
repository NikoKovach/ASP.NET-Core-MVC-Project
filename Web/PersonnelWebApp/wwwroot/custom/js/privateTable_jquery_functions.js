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
/* Custom Table functions*/


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

$("#edit-entities").on("click", function () {
       var rowsForEdit = $(".edit-checkbox:checked").parents("tr");

       var columnsLabel = $("#privateTable > thead > tr label.column-lbl");

       var colsTitles = columnsLabel.map(function () {
              return $(this).attr("value");
       }).toArray();

       var entitiesList= [];

       if (rowsForEdit.length > 0) {
              for (var z = 0; z < rowsForEdit.length; z++) {
                     var entityElements = rowsForEdit.eq(z).children("td").children("input.table-field");

                     var textValuesArr = [];

                     for (var i = 0; i < entityElements.length-1; i++) {
                            if (!entityElements.eq(i).val() == "") {
                                   textValuesArr.push(entityElements.eq(i).val());
                            }
                            else {
                                   textValuesArr.push(null);
                            }
                     }

                     var viewTableRow = entityElements.eq(entityElements.length - 1).attr("name");
                     textValuesArr.push(viewTableRow);

                     var entityForEdit = {};
                     
                     if (colsTitles.length == textValuesArr.length-1) {
                            for (var i = 0; i < textValuesArr.length-1; i++) {
                                   entityForEdit[colsTitles[i]] = textValuesArr[i];
                            }
                     }

                     var lastIndex = textValuesArr.length - 1;
                     entityForEdit["ViewTableRow"] = textValuesArr[lastIndex];

                     entitiesList.push(entityForEdit);
              }
       }

       var actionData = GetActionParameters();

       actionData["entitiesForEdit"] = entitiesList;

       PostRequest(actionData);
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

function GetActionParameters() {
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

       var filterInputs = $('#formGoToPage').find("input.model-filter");

       var filterObj = {};

       filterInputs.each( function (index, element) {
              var keyFilter = $(this).attr("name");
              var valueFilter = $(this).val();

              filterObj[keyFilter] = (valueFilter == "") ? null : valueFilter;
       });

       data["filter"] = filterObj;

       return data;
};

function PostRequest(actionData) {
       var formAction = $('#formGoToPage').attr("action");
/*       var formMethod = $('#formGoToPage').attr("method");*/

       $.post(formAction, actionData, function (data) {
              alert("success");

              PostResponse(data);
       });
};

function PostResponse(data) {
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

                     console.log(rowText);

                     var errorsMsgString = "<li><p>Property : " + fieldNameText + " - row " + rowText + " of the table :</p>";

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

/* End Custom Table functions

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

$("#go-back-btn.employees").on("click", backToEmployeesIndex);

function backToEmployeesIndex() {
       document.location = "/Employees/Index";
}






