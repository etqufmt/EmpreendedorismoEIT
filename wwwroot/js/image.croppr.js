var _bsModal = $('#modal-upload')
var _btnSelect = $('#btn-select')
var _inputLogoId = 'input-logo'
//Não funciona com o seletor JQuery
var _imgCanvas = document.getElementById('img-canvas')
var _inputLogo = document.getElementById(_inputLogoId)
var _selected, _filename, _croppr


$(document).on("change", '#' + _inputLogoId, (evt) => {
    var file = evt.target.files[0]
    if (file) {
        var url = URL.createObjectURL(file)
        _imgCanvas.src = url
        _filename = file.name
        _bsModal.modal('show')
    }
})

_bsModal.on('shown.bs.modal', (evt) => {
    _selected = false
    if (_imgCanvas.complete && _imgCanvas.naturalHeight !== 0) {
        _croppr = new Croppr(_imgCanvas, {
            //Cálculo invertido: altura/largura
            aspectRatio: 0.8
        })
        _btnSelect.removeAttr('disabled')
    } else {
        _btnSelect.attr('disabled', true)
    }
})

_btnSelect.click((evt) => {
    const width = 500
    const height = 400
    const cropRect = _croppr.getValue()
    //Upload da imagem original se já estiver na resolução desejada
    if (_imgCanvas.naturalWidth == width && _imgCanvas.naturalHeight == height) {
        if (cropRect.width == width && cropRect.height == height) {
            _selected = true
            _bsModal.modal('hide')
            console.log('imagem original')
            return
        }
    }
    const canvas = document.createElement('canvas')
    const context = canvas.getContext('2d')
    canvas.width = width
    canvas.height = height
    context.drawImage(
        _croppr.imageEl,
        cropRect.x,
        cropRect.y,
        cropRect.width,
        cropRect.height,
        0,
        0,
        canvas.width,
        canvas.height,
    )

    //Testado com Edge 92 e Firefox 89
    canvas.toBlob((blob) => {
        var container = new DataTransfer()
        var file = new File([blob], _filename, {
            type: "image/jpeg",
            lastModified: new Date().getTime()
        })
        container.items.add(file)
        _inputLogo.files = container.files
        console.log(_inputLogo.files)
        _selected = true
        _bsModal.modal('hide')
    }, 'image/jpeg', 0.9)
})

_bsModal.on('hide.bs.modal', (evt) => {
    if (!_selected) {
        _filename = ''
        _inputLogo.value = null
    }
    $('#' + _inputLogoId).next('.custom-file-label').html(_filename)
    _croppr?.destroy()
    _croppr = null
})