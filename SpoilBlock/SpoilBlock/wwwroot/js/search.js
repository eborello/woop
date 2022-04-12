
$(document).ready(function (){
    $('#searchButton').click(function () {

        if ($('#searchQuery').val().length == 0) {
            $('#resultsTableDiv').empty().append(`<p class="text-danger">Please enter a title to search for.</p>`)
            return;
        } else if ($('#searchQuery').val().length > 50) {
            $('#resultsTableDiv').empty().append(`<p class="text-danger">Sorry, that title is too long.</p>`)
            return;
        }

        toggleButtonLoading();
        
        $.ajax({
            url: '/Search/SearchAjax',
            data: { query: $("#searchQuery").val() },
            method: 'GET',
            success: buildSearchResults,
            error: errorState
        });

    });
});


function buildSearchResults(data) {
    toggleButtonLoading();

    if (data.list == "[]") {
        $('#resultsTableDiv').empty().append(`<p class="text-danger">Sorry, there were no results for ${data.query}</p>`)
        return;
    }

    var results = JSON.parse(data.list);

    let searchTable = $('<table class="table"><tbody>');
    $.each(results, function (i) {
        let tr = $(`<tr><td><div class="img-max"><img src=${results[i].image} class="img-fluid"></div></td><td>${results[i].title}</td><td>${results[i].description}</td><td>`).appendTo(searchTable);
    });
    console.log(searchTable);
    $('#resultsTableDiv')
        .empty()
        .append(searchTable);
}


function errorState(message) {
    console.log(message)
    toggleButtonLoading();

    $('#resultsTableDiv').empty().append(`<p class="text-danger">Sorry, we're having issues communicating with IMDb.  Please try again later.</p>`)
}




function toggleButtonLoading() {
    if ($('#searchButton').text() == "Search") {
        $('#searchButton').prop("disabled", true);
        $('#searchButton').empty().append(`<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Loading...`)
    } else {
        $('#searchButton').prop("disabled", false);
        $('#searchButton').empty().append("Search")
    }
}