var prevItemIndex = -1;
var prevItemBgColor = "";
var indexMouseOver = -1;
/*var actionEditEmployee = false;*/

/* Company View */
$('#tbody').on('click', 'tr', onRowClick);

function onRowClick()
{
	if (prevItemIndex > -1) {
		$('#tbody tr').eq(prevItemIndex).css("background-color", prevItemBgColor);
	}

	prevItemIndex = $(this).index();
	prevItemBgColor = $(this).css("background-color");

	$(this).changeCurrentRowBgcolor();

	$('input#entityId').val($(this).children(':nth-child(3)').text());
};

$('#tbody').on("mouseover",'tr', function () {

	indexMouseOver = $(this).index();
});

$(document).on("keypress", function (event) {
	$('#tbody tr').eq(indexMouseOver).trigger("click");
});

$.fn.changeCurrentRowBgcolor = function () {
	$(this).css("background-color", 'rgb(13, 32, 129)');
};

$('#tbody tr:even').css("background-color", 'rgb(45, 45, 47 )');

$("#companiesTable").css("background-color", "rgb(100, 101, 111 )");
//"rgb(116, 116, 118)" "rgb(83, 83, 87)"
$("#companiesTable").css("border-radius", "15px");

$("#companiesTable tr:last td").last().css("border-end-end-radius", "15px");

$("#companiesTable tr:last td").first().css("border-bottom-left-radius", "15px");


