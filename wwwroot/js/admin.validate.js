//Cria classe "number-only" para entradas de texto
$(document).on('input', '.number-only', function (evt) {
    if (event.inputType != "insertFromPaste") {
        evt.target.value = evt.target.value.replace(/([^0-9]+)/g, "")
    }
})
$(document).on('paste', '.number-only', function (evt) {
    evt.preventDefault()
    var paste = (event.clipboardData || window.clipboardData).getData('text')
    paste = paste.replace(/([^0-9]+)/g, "")
    if (evt.target.maxLength > 0) paste = paste.slice(0, evt.target.maxLength)
    evt.target.value = paste
})

//Restringir entrada numérica para "maxlength" dígitos
$(document).on('input', '.input-len-number', function (evt) {
    evt.target.value = evt.target.value.slice(0, evt.target.maxLength)
})

//Contador de caracteres
$(document).on('input', '.maxcounter', function (evt) {
    var out = $(this).next('.maxcounter-out')
    var maxlength = $(this).attr('maxlength')
    var counter = $(this).val().length
    var threshold = Math.floor(maxlength*0.8)
    if (counter < threshold) {
        out.html('')
        return
    }
    if (counter == maxlength) {
        out.addClass('text-danger')
    } else {
        out.removeClass('text-danger')
    }
    var result = counter + " / " + maxlength
    out.html(result)
})


//Exibir nome de arquivo na caixa de seleção (upload)
//$(document).on('input', '.custom-file-input', function (evt) {
//    var name = (evt.target.files[0]?.name) ?? ''
//    $(this).next('.custom-file-label').html(name)
//})