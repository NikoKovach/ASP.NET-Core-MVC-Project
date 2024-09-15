var prevItemIndex = -1;
var currentRowIndex = -1;
var prevItemBgColor = "";
var indexMouseOver = -1;

const ascending = "_asc";
const descending = "_desc";
const ascArray = "&#11165;";
const descArray = "&#11167;";
/* Custom Table functions*/

$('#privateTable-body').on('click', 'tr', onRowClick);

$('#privateTable-body').on("mouseover",'tr', function () {

	indexMouseOver = $(this).index();
});

$(document).on("keypress", function (event) {
       $('#privateTable-body tr').eq(indexMouseOver).trigger("click");
});

$(document).on("keydown", function (key) {
       arrowKeysPress(key);
});

function onRowClick() {
       if (prevItemIndex > -1) {
              $('#privateTable-body tr').eq(prevItemIndex).css("background-color", prevItemBgColor);
       }

       currentRowIndex = $(this).index();
       prevItemIndex = $(this).index();
       prevItemBgColor = $(this).css("background-color");

       changeCurrentRowBgColor($(this));

       //############################################
       var inputEditCount = $("#edit-entity-id").length;
       var inputDeleteCount = $("#delete-entity-id").length;

       if (inputEditCount > 0) {
              $('#edit-entity-id').val($(this).children(':nth-child(2)').text());
       }

       if (inputDeleteCount > 0) {
              $('#delete-entity-id').val($(this).children(':nth-child(2)').text());
       }
};

function arrowKeysPress(key) {

       switch (key.which) {
              case 37:    //left arrow key alert("left arrow ");
                     
                     break;
              case 39:    //right arrow key alert("Right arrow ");
                     
                     break;
              case 38:    //up arrow key
                     upArrowPress();
                     break;
              case 40:    //bottom arrow key
                     downArrowPress();
                     break;
       }
};

function upArrowPress() {
       if (currentRowIndex > 0) {
              currentRowIndex -= 1;
              $('#privateTable-body tr').eq(currentRowIndex).trigger("click");
       }
};

function downArrowPress() {

       var rowCount = $('#privateTable-body').children().length;

       if (currentRowIndex < rowCount-1) {
              currentRowIndex += 1;
              $('#privateTable-body tr').eq(currentRowIndex).trigger("click");
       }
};

function changeCurrentRowBgColor(row) {
       row.css("background-color", 'rgb(231, 255, 245 )'); //231, 255, 245 <> 206, 254, 235
};
/* End Custom Table functions*/

$("#btn-link-test").on("click", function () {
       $(this).trigger("blur");
});

/*##########################################*/


$("#btn-sort-FName").on('click', function () {
/*       specialSymbols();*/
       var sortLabelValue = $("label.lbl-sortFName").text();
       var txtSortFName = $("input#txt-sort-FName").val();

       sortLabelValue = sortLabelValue.replace(" ", "");

       var sortFirstNameAsc = sortLabelValue + ascending;
       var sortFirstNameDesc = sortLabelValue + descending;

       if (txtSortFName == "") {
              $("input#txt-sort-FName").val(sortFirstNameDesc);
              $("#sort-btn-span").html(descArray); 
       }
       else if (txtSortFName == sortFirstNameDesc) {
              $("input#txt-sort-FName").val(sortFirstNameAsc);
              $("#sort-btn-span").html(ascArray); 
       }
       else if (txtSortFName == sortFirstNameAsc) {
              $("input#txt-sort-FName").val(sortFirstNameDesc);
              $("#sort-btn-span").html(descArray); 
       }

       $(this).trigger("blur");
});

function setToAsc() { 

}

function setToDesc() {

}

$("#btn-sort-LastName").on('click', function () {
       var sortLabelValue = $("label.lbl-sortLastName").text();
       var txtSortLastName = $("input#txt-sort-LastName").val();

       sortLabelValue = sortLabelValue.replace(" ", "");

       var sortLastNameAsc = sortLabelValue + ascending;
       var sortLastNameDesc = sortLabelValue + descending;

       if (txtSortLastName == "" || txtSortLastName != sortLastNameDesc) {
              $("input#txt-sort-LastName").val(sortLastNameDesc);
       }
       else if (txtSortLastName == sortLastNameDesc) {
              $("input#txt-sort-LastName").val(sortLastNameAsc);
       }
       else if (txtSortLastName == sortLastNameAsc) {
              $("input#txt-sort-LastName").val(sortLastNameDesc);
       }

       $(this).trigger("blur");
});

$("#btn-sort-EGN").on('click', function () {
       var sortLabelValue = $("label.lbl-sortEGN").text();
       var txtSortEGN = $("input#txt-sort-EGN").val();

       sortLabelValue = sortLabelValue.replace(" ", "");

       var sortCivilIdAsc = sortLabelValue + ascending;
       var sortCivilIdDesc = sortLabelValue + descending;

       if (txtSortEGN == "" || txtSortEGN != sortCivilIdDesc) {
              $("input#txt-sort-EGN").val(sortCivilIdDesc);
       }
       else if (txtSortEGN == sortCivilIdDesc) {
              $("input#txt-sort-EGN").val(sortCivilIdAsc);
       }
       else if (txtSortEGN == sortCivilIdAsc) {
              $("input#txt-sort-EGN").val(sortCivilIdDesc);
       }

       $(this).trigger("blur");
});

/*############################################## */

//function goToPageFunction(event) {
//       if (event.which == 13) {
//              var pageNumber = $("input#go-to-page").val();

//              var index = $("#lblTotalPages").text().trim().indexOf("/");
//              var cleanStringPageNumber = $("#lblTotalPages").text().trim().substring(index + 1).trim();

//              var maxPageNumber = parseInt(cleanStringPageNumber);

//              alert(pageNumber + " ; " + index + " ; " + maxPageNumber);

//              if (pageNumber <= maxPageNumber) {
//                     $("#formGoToPage").on("submit", function () {
//                            alert("GOGO");
//                     });
//              }
//       }
//};

//$.fn.changeCurrentRowBgcolor = function () {
//       $(this).css("background-color", 'rgb(231, 255, 245 )'); //231, 255, 245 <> 206, 254, 235
//};
/*
       alert($(this).children(':nth-child(2)').text());
       alert(currentRowIndex);
*/
