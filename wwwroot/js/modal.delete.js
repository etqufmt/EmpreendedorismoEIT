//Preencher modal de exclusão
$('#modal-delete').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    //Extraído do atributo "data-id"
    var itemid = button.data('id')
    //Extraído do atributo "data-item"
    //ID do elemento que possui o texto a ser exibido
    var itemtag = button.data('tag')
    var modal = $(this)
    var itemtext = $('#' + itemtag).text().trim()
    modal.find('.modal-item-text').text(itemtext)
    modal.find('.modal-item-id').val(itemid)
})