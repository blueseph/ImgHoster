(function () {
    'use strict'

    var processFiles = function (files) {
        var acceptedFiles = [];
        var rejectedFiles = [];
        var VALID_FILETYPES = ['png', 'jpg', 'jpeg', 'gif', 'bmp'];

        _.each(files, function (file) {
            if (file.type.split('/')[0] === 'image' &&
                _.intersection(VALID_FILETYPES, file.type.split('/')[1]))
            {
                acceptedFiles.push(file);
            } else {
                rejectedFiles.push(file);
            }
        });

        return acceptedFiles;
    }

    var uploadFiles = function (files) {
        var fd = new FormData();

        fd.append('images', files);

        $.ajax('images/upload', {
            data: fd,
            processData: false,
            contentType: false,
            type: 'POST',
        })
            .success(function (data) { console.log(data) })
            .error(function (err) { console.log(err) });
    }

    var doDrag = function(files) {
        var accepted = processFiles(files);
        
        uploadFiles(accepted)
    }

    var wrap = $('.wrapper');

    wrap.on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });

    wrap.on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
    })

    wrap.on('drop', function (e) {
        e.preventDefault();

        var files = e.originalEvent.dataTransfer.files;

        doDrag(files);
    });

})();