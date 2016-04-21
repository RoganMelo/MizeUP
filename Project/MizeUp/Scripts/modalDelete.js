/* Abre modal de deleção. */
var modalsdeletes = $("[data-modal-delete]");
var title = language.title == undefined ? "Title" : language.title;
var textBtnNot = language.textBtnNot == undefined ? "textBtnNot" : language.textBtnNot;
var url = language.url == undefined ? "textBtnNot" : language.url;

function showModalDelete() {
    var modal = new Modal({ title: title, textBtnNot: textBtnNot });
    var id = + $(this).attr("data-modal-delete");
    var content = $("#content").clone().removeAttr("hidden");
    var modelId = content.find("#modelId");
    modelId.val(id);

    modal.setContentFooter(content);
    //modal.setClickBtnNot(function () {
    //    $.post(url, { modelId : id }, function (data) {
    //        modal.hide();
    //        var feedback = new Modal({
    //            title: data,
    //            textBtnNot: "",
    //            textBtnYes: ""
    //        });

    //        feedback.removeFooter();
    //        feedback.setFunctionHide(function () {
    //            location.reload();
    //        });
    //        feedback.show();
    //    });
    //});
    
    modal.show();
}

modalsdeletes.click(showModalDelete);
/* Fim abre modal de deleção. */