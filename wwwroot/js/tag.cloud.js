//Inicializar biblioteca
new Vue({ el: '#tag-cloud' })

//Ativar e desativar botão de submit
$(document).on('change', '.tag-input', function (evt) {
    var contador = 0;
    var checkboxes = $("form#form-cloud :input")
    var btnSubmit = $("form#form-cloud :submit")
    checkboxes.each(function () {
        contador += $(this).prop('checked') ? 1 : 0
    });
    if (contador >= 3) {
        btnSubmit.prop('disabled', false)
    } else {
        btnSubmit.prop('disabled', true)
    }

    //Salvar ordem em que as tags foram selecionadas
    if ($(this).prop('checked')) {
        $('#lista-tags').val($('#lista-tags').val() + '"' + $(this).val() + '",')
    } else {
        $('#lista-tags').val($('#lista-tags').val().replace('"' + $(this).val() + '",', ''))
    }
})
