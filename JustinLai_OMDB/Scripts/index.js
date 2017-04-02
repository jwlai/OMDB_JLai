
var movieTitleObj;
var movieYearObj;

var doingSearch;

var pageNumber = 1;
var getMovieSearchData = function ()
{
    var dataObj = {
        title: movieTitleObj.val(),
        year: movieYearObj.val(),
        page: pageNumber
    }

    return dataObj;
};

var getMovieTitle = function (itemTitle, itemImdbId, itemYear, itemPoster, modalInstance)
{
    if (itemPoster !== "N/A") {
        modalInstance.find("#movieModal-poster").attr("src", itemPoster);
    }
    else {
        modalInstance.find("#movieModal-poster").attr("src", "");
    }

    modalInstance.find("#movieModal-title").html(itemTitle);
    modalInstance.find("#movieModal-year").html(itemYear);

    var data = {
        title: itemTitle,
        imdbId: itemImdbId,
        year: itemYear
    };

    $.ajax({
        url: "/OmdbAPI/GetMovieByTitle",
        method: "GET",
        data: data
    })
    .done(function (data)
    {
        modalInstance.find("#movieModal-plot").html(data.Plot);
        modalInstance.find("#movieModal-rated").html(data.Rated);
        modalInstance.find("#movieModal-genre").html(data.Genre);
        modalInstance.find("#movieModal-runtime").html(data.Runtime);
        modalInstance.find("#movieModal-director").html(data.Director);
        modalInstance.find("#movieModal-writer").html(data.Writer);
        modalInstance.find("#movieModal-language").html(data.Language);
        modalInstance.find("#movieModal-country").html(data.Country);
        modalInstance.find("#movieModal-awards").html(data.Awards);
        modalInstance.find("#movieModal-boxoffice").html(data.BoxOffice);
        modalInstance.find("#movieModal-dvd").html(data.DVD);
        modalInstance.find("#movieModal-production").html(data.Production);
        modalInstance.find("#movieModal-website").html(data.Website);
        
        $('#movieModal-rating tr:not(:nth-child(1))').remove();
        var ratings = data.Ratings;
        var ratingElement = "<tr><td>{0}</td><td>{1}</td></tr>";
        $.each(ratings, function (i, obj)
        {
            var element = String.format(ratingElement, obj.Source, obj.Value);
            $('#movieModal-rating').append(element);
        });

        $("#infoTableDiv").show();
        $("#plotSummaryDiv").show();
    })
    .fail(function (data)
    {
        alert("Error Occured");
    })
};

var searchMovieTitle = function ()
{
    doingSearch = true;

    var data = getMovieSearchData();
    $.ajax({
        url: "/OmdbAPI/SearchMovieByTitle",
        method: "GET",
        data: data
    })
    .done(function (data)
    {
        doingSearch = false;
        var response = data.Response;
        if (!response) {
            var errorDiv = $("#errorDiv");
            errorDiv.html(data.Error);
            errorDiv.show();
            $("#searchResultsDiv").hide();
        }
        else {
            var movieList = data.Search;
            var movieResults = data.TotalResults;

            movieTable = $('#movieSearchTable tbody');
            $('#movieSearchTable tbody tr:not(:nth-child(1))').remove();

            var btn = "<button type=\"button\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#movieModal\" data-imdbid=\"{0}\" data-title=\"{1}\" data-year=\"{2}\" data-poster=\"{3}\">Open</button>";
            $.each(movieList, function (i, obj)
            {
                movieTable.append("<tr><td>" + obj.Title + "</td><td>" + obj.Year + "</td>"+"<td>" + String.format(btn, obj.ImdbId, obj.Title, obj.Year,obj.Poster) + "</td>" + "</tr>");
            });
            
            totalPages = Math.round((movieResults / 10));
            if((movieResults % 10) < 5){
                totalPages += 1;
            }
            if (movieResults <= 10) {
                totalPages = 1;
            }

            $("#resultCount").html("Showing page " + pageNumber + " of " + totalPages);

            if (pageNumber > 1) {
                $('#prevBtn').prop('disabled', false);
            }
            else {
                $('#prevBtn').prop('disabled', true);
            }

            if (pageNumber < totalPages) {
                $('#nextBtn').prop('disabled', false);
            }
            else {
                $('#nextBtn').prop('disabled', true);
            }

            $("#searchResultsDiv").show();
        }
    })
    .fail(function (data)
    {
        var errorDiv = $("#errorDiv");
        errorDiv.html("Error - Search failed");
        errorDiv.show();
    })
};

var prevButtonClick = function (e)
{
    pageNumber -= 1;
    searchMovieTitle();
};

var nextButtonClick = function (e)
{
    pageNumber += 1;
    searchMovieTitle();
};

var submitEvent = function (event)
{
    $("#errorDiv").hide();
    event.preventDefault();
    pageNumber = 1;
    var title = movieTitleObj.val();
    if (title.length === 0) {
        var errorDiv = $("#errorDiv");
        errorDiv.html("Please enter a title before submitting.");
        errorDiv.show();
    }
    else {
        if (doingSearch) {
            var errorDiv = $("#errorDiv");
            errorDiv.html("Please wait for the previous search to finish.");
            errorDiv.show();
        }
        else {
            searchMovieTitle();
        }
    }

};

$(document).ready(function ()
{
    movieTitleObj = $("#movieTitle");
    movieYearObj = $("#movieYear");
    doingSearch = false;

    $("#searchForm").submit(submitEvent);

    $("#prevBtn").click(prevButtonClick);
    $("#nextBtn").click(nextButtonClick);

    $('#movieModal').on('show.bs.modal', function (event)
    {
        $("#infoTableDiv").hide();
        $("#plotSummaryDiv").hide();

        var button = $(event.relatedTarget); // Button that triggered the modal

        var title = button.data('title'); // Extract info from data-* attributes
        var imdbId = button.data("imdbid");
        var year = button.data("year");
        var poster = button.data("poster");

        getMovieTitle(title, imdbId, year, poster, $(this));
        
    })
});