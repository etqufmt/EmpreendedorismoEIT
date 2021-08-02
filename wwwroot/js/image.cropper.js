var bs_modal = $('#modal-upload');
var img_canvas = document.getElementById('img_canvas');
var selected = false;
var filename;
var croppr;


$(document).on("change", "#input-logo", (evt) => {
    var file = evt.target.files[0]
    if (file) {
        var url = URL.createObjectURL(file);
        img_canvas.src = url;
        filename = file.name
        bs_modal.modal('show');
    }
});

bs_modal.on('shown.bs.modal', (evt) => {
    selected = false
    croppr = new Croppr(img_canvas, {
        aspectRatio: 0.8
    });
}).on('hide.bs.modal', (evt) => {
    if (!selected) {
        filename = ''
        document.getElementById('input-logo').value = null
    }
    $('#input-logo-label').html(filename)
    croppr.destroy()
    croppr = null
});

$("#btn-select").click((evt) => {
    const cropRect = croppr.getValue();
    const canvas = document.createElement("canvas");
    const context = canvas.getContext("2d");
    canvas.width = cropRect.width > 1000 ? 1000 : cropRect.width;
    canvas.height = cropRect.height > 800 ? 800 : cropRect.height;
    context.drawImage(
        croppr.imageEl,
        cropRect.x,
        cropRect.y,
        cropRect.width,
        cropRect.height,
        0,
        0,
        canvas.width,
        canvas.height,
    );

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
    }, 'image/jpeg', 0.9);
});