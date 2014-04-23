$(document).ready(function () {
    // Multiselect backend
    $('#cphBackend_lbPackageChannels').multiSelect();
    $('#cphBackend_lbPackageStreamingServices').multiSelect();

    // Multiselect frontend
    $('#cphFrontend_lbFavouriteChannels').multiSelect();

    // Highlight Checkbox
    if ($('#cphFrontend_internetCheck').is(':checked')) {
        $('.checkboxFour label').addClass("highlightCheckbox");
    }

    $('.checkboxFour label').click(function () {
        $(this).toggleClass("highlightCheckbox");
    });

});