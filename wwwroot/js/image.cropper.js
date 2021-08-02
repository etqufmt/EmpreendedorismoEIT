var bs_modal = $('#modal-upload');
var img_cropper = document.getElementById('img-cropper');
var selected = false;
var filename;
var cropper;


$(document).on("change", "#input-logo", (evt) => {
    var file = evt.target.files[0]
    if (file) {
        var url = URL.createObjectURL(file);
        img_cropper.src = url;
        filename = file.name
        bs_modal.modal('show');
    }
});

bs_modal.on('shown.bs.modal', (evt) => {
    selected = false
    cropper = new Cropper(img_cropper, {
        aspectRatio: 1.5,
        viewMode: 2,
        wheelZoomRatio: 0.3
    });
}).on('hide.bs.modal', (evt) => {
    if (!selected) {
        filename = ''
        document.getElementById('input-logo').value = null
    }
    $('#input-logo-label').html(filename)
    cropper.destroy()
    cropper = null
});

$("#btn-select").click((evt) => {
    canvas = cropper.getCroppedCanvas({
        maxWidth: 1500,
        maxHeight: 1000,
    });

    canvas.toBlob((blob) => {
        var input = document.getElementById('input-logo')
        var container = new DataTransfer()
        var file = new File([blob], "logo.jpg", {
            type: "image/jpeg",

            lastModified: new Date().getTime()
        })
        container.items.add(file)
        input.files = container.files
        console.log(input.files)
        selected = true;
        bs_modal.modal('hide')
    }, 'image/jpeg');
});