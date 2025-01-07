
const personsPagingForm = document.getElementById( "personsPagingForm" );

const totalPagesLbl = document.getElementById( "lblTotalPages" );

const gotoPageInput = document.getElementById("go-to-page");

const prevPageBtn = document.getElementById("prevPageBtn");
const nextPageBtn = document.getElementById( "nextPageBtn" );

nextPageBtn.addEventListener( "click", e => {
       e.preventDefault();

       let currentPageIndex = Number( gotoPageInput.getAttribute( "value" ) );

       if ( pageIndexIsValid( currentPageIndex + 1 ) ) {
              gotoPageInput.setAttribute( "value", currentPageIndex + 1 );

              personsPagingForm.submit();
       }
       else {
              alert("Invalid page index ! -> " + e.target.innerText);
       }
} );

prevPageBtn.addEventListener( "click",function() {
      /*this.preventDefault();*/

       let currentPageIndex = Number( gotoPageInput.getAttribute( "value" ) );

       if ( currentPageIndex > 1 ) {
              gotoPageInput.setAttribute( "value", currentPageIndex - 1 );

              personsPagingForm.submit();
       }
       else {
                     console.log( "Invalid page index ! -> "  + this.innerText);
        }
} );

gotoPageInput.addEventListener( "change", e => {
       gotoPageInput.value = gotoPageInput.value;

    /*   console.log( "new page index = " + gotoPageInput.value);*/
});
//##############################################################

function pageIndexIsValid (pageIndex) {
       if ( typeof pageIndex != "number" ) return false;

       if ( Number.isInteger( pageIndex ) == false ) return false;

       let totalPages = Number( totalPagesLbl.innerText );

       if ( pageIndex < 1 || pageIndex > totalPages ) return false;

       return true;
};

