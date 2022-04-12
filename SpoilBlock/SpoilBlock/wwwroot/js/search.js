
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

    $(document).on("click", ".btn", function () {
        $.ajax({
            url: '/Add/AddMediaToWatchlist',
            data: {
                media: `{Imdbid: "${$(event.target).attr("id")}", image: "${$(event.target).parent().parent().find("img").attr('src')}", title: "${$(event.target).parent().parent().find(".mediatitle").text()}", description: "${$(event.target).parent().parent().find(".mediadescription").text()}"}`
            },
            method: 'POST',
            success: DisableButtonsForShowsAlreadyOnWatchlist
        });
    });

});




function buildSearchResults(data) {
    toggleButtonLoading();

    if (data.list == "[]") {
        $('#resultsTableDiv').empty().append(`<p class="text-danger">Sorry, there were no results for ${data.query}</p>`)
        return;
    }

    try {
        var results = JSON.parse(data.list);
    } catch (error) {
        errorState("There was a problem with IMDb");
        return;
    }


    let searchTable = $('<table class="table" id="resultsTable"><tbody>');
    $.each(results, function (i) {
        let tr = $(`<tr><td><div class="img-max"><img src=${results[i].image} class="img-fluid"></div></td><td class="mediatitle">${results[i].title}</td><td class="mediadescription">${results[i].description}</td><td><input type="submit" value="Add" id="${results[i].id}" class="btn btn-primary"</td></tr>`).appendTo(searchTable);
    });
    $('#resultsTableDiv')
        .empty()
        .append(searchTable);

    $.ajax({
        url: '/Add/GetCurrentWatchListIDs',
        success: DisableButtonsForShowsAlreadyOnWatchlist
    });
}


function DisableButtonsForShowsAlreadyOnWatchlist(data) {
    var results = JSON.parse(data.list);
    $.each(results, function (i) {
        $('.btn').each(function () {
            if ($(this).attr('id') == results[i]) {
                $(this).prop("disabled", true);
                $(this).prop("class", "btn btn-secondary");
                $(this).prop("value", "Added");
            }
        });
    });
}


function errorState(message) {
    console.log(message)

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