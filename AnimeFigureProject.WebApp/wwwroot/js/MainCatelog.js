function updateUrlWithFilters(searchTerm)
{

    var selectedBrands = [];
    var selectedCategories = [];
    var selectedOrigins = [];

    $('#Brand input[type="checkbox"]:checked').each(function ()
    {

        selectedBrands.push($(this).attr('id'));

    });

    $('#Category input[type="checkbox"]:checked').each(function ()
    {

        selectedCategories.push($(this).attr('id'));

    });

    $('#Origin input[type="checkbox"]:checked').each(function ()
    {

        selectedOrigins.push($(this).attr('id'));

    });

    var url = 'Catalog?searchTerm=' + searchTerm;

    if (selectedBrands.length > 0)
        url += '&brands=' + selectedBrands.join(',');

    if (selectedCategories.length > 0)
        url += '&categories=' + selectedCategories.join(',');

    if (selectedOrigins.length > 0)
        url += '&origins=' + selectedOrigins.join(',');


    window.location.href = url;

}

var searchbar = document.getElementById("SearchbarInput");

if (searchbar != null)
{

    searchbar.addEventListener("keyup", function (event)
    {

        if (event.key === "Enter")
        {

            var searchTerm = $('input[name="MainSearchbar"]').val();
            updateUrlWithFilters(searchTerm);

            return false;

        }

    });

}

document.querySelectorAll('#Brand input[type="checkbox"]').forEach(function (checkbox)
{

    checkbox.addEventListener('change', function ()
    {

        var searchTerm = $('input[name="MainSearchbar"]').val();
        updateUrlWithFilters(searchTerm);

    });

});

document.querySelectorAll('#Category input[type="checkbox"]').forEach(function (checkbox)
{

    checkbox.addEventListener('change', function ()
    {

        var searchTerm = $('input[name="MainSearchbar"]').val();
        updateUrlWithFilters(searchTerm);

    });

});

document.querySelectorAll('#Origin input[type="checkbox"]').forEach(function (checkbox)
{

    checkbox.addEventListener('change', function ()
    {

        var searchTerm = $('input[name="MainSearchbar"]').val();
        updateUrlWithFilters(searchTerm);

    })

});

document.addEventListener("DOMContentLoaded", function ()
{

    const urlParams = new URLSearchParams(window.location.search);
    const searchTerm = urlParams.get('searchTerm');

    $('.SearchBar').show();
    $('#SearchbarInput').val(searchTerm);

    const brands = urlParams.get('brands');

    if (brands != null)
    {

        allBrands = brands.split(',');
        allBrands.forEach(brandId => { $('#Brand #' + brandId).prop('checked', true); });

    }

    const categories = urlParams.get('categories');

    if (categories != null)
    {

        allCategories = categories.split(',');
        allCategories.forEach(categoryId => { $('#Category #' + categoryId).prop('checked', true); });

    }

    const origins = urlParams.get('origins');

    if (origins != null)
    {

        allOrigins = origins.split(',');
        allOrigins.forEach(originId => { $('#Origin #' + originId).prop('checked', true); });

    }

});