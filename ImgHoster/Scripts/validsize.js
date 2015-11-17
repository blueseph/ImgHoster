$("#i_submit").click(function (e) {

    var fsize = $("#i_file")[0].files[0].size;
    if (fsize > 3 * 1024 * 1024) {
        e.preventDefault();
        alert("Maximum file size allowed is 3MB.");
    }
});