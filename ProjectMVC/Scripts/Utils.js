function displayValidationErrors(errors) {
    var $ul = $('<ul>');
    $ul.empty();

    $.each(errors, function (idx, errorMessage) {
        $ul.append('<li>' + errorMessage + '</li>');
    })

    $('#errors').html($ul);
    $('#divErrors').show();
}